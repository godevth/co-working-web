using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
// using CoreCMS.Application.Picture.Commands;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using IdentityModel;
using IdentityModel.Client;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace CoreCMS.Models.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        const string PROVIDER_KEY = "provider";
        const string EMAIL_KEY = "email";
        const string FIRSTNAME_KEY = "firstname";
        const string LASTNAME_KEY = "lastname";
        const string PICTURE_KEY = "picture";
        const string CONTENT_TYPE_KEY = "content_type";

        private readonly ISystemClock _clock;
        private readonly OpenIDConfig _config;
        protected readonly IMediator _mediator;

        // For DB
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEventService _events;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ResourceOwnerPasswordValidator(ISystemClock clock, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IEventService events, IOptions<OpenIDConfig> configOption, IMediator mediator, ApplicationDbContext context)
        {
            _clock = clock;
            _config = configOption.Value;
            _mediator = mediator;

            // For DB
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
            _context = context;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            string[] remoteProviders = new string[] { "Boonrawd LDAP", "Agent LDAP", "facebook", "google", "apple" };

            if (context.Request.Raw.AllKeys.Contains(PROVIDER_KEY) && remoteProviders.Contains(context.Request.Raw[PROVIDER_KEY]))
            {
                string provider = context.Request.Raw[PROVIDER_KEY];
                string email = context.Request.Raw[EMAIL_KEY];
                string firstname = context.Request.Raw[FIRSTNAME_KEY];
                string lastname = context.Request.Raw[LASTNAME_KEY];
                string picture = context.Request.Raw[PICTURE_KEY];
                string contentType = context.Request.Raw[CONTENT_TYPE_KEY];
                await LoginRemote(context, provider, email, firstname, lastname, picture, contentType);
            }
            else
            {
                await LoginLocal(context);
                if (context.Result.Error == "invalid_grant")
                {
                    string provider = context.Request.Raw[PROVIDER_KEY];
                    string email = context.Request.Raw[EMAIL_KEY];
                    string firstname = context.Request.Raw[FIRSTNAME_KEY];
                    string lastname = context.Request.Raw[LASTNAME_KEY];
                    string picture = context.Request.Raw[PICTURE_KEY];
                    string contentType = context.Request.Raw[CONTENT_TYPE_KEY];
                    await LoginRemote(context, provider, email, firstname, lastname, picture, contentType);
                }
            }
        }

        private async Task<ApplicationUser> LoginRemote(ResourceOwnerPasswordValidationContext context, string provider = null, string email = null,
             string firstname = null, string lastname = null, string picture = null, string contentType = null)
        {
            ApplicationUser user = null;

            // begin check provicer = facebook, google
            string[] socials = new string[] { "facebook", "google", "apple" };
            if (socials.Contains(provider))
            {
                #region ValidatePassword
                string hashPass = GetHash(_config.HashSecret, provider, context.UserName);
                if(context.Password != hashPass)
                {
                    context.Result.Error  = "Invalid social login.";
                    return null;
                }
                #endregion
                
                var signInResult = await _signInManager.ExternalLoginSignInAsync(provider, context.UserName, isPersistent: false, bypassTwoFactor: true);
                if (signInResult.Succeeded)
                {
                    user = await _userManager.FindByLoginAsync(provider, context.UserName);

                    if(user.InActiveStatus)
                    {
                        await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "user inActive", interactive: false));
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "user ถูกปิดการใช้งาน");
                        return user;
                    }
                    else if(user.IsDeleted)
                    {
                        await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "user isDeleted", interactive: false));
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "user ถูกปิดการใช้งาน");
                        return user;
                    }

                    var principle = await _signInManager.CreateUserPrincipalAsync(user);
                    context.Result = new GrantValidationResult(
                            user.Id ?? throw new ArgumentException("Subject ID not set", nameof(user.Id)),
                            OidcConstants.AuthenticationMethods.Password,
                            _clock.UtcNow.UtcDateTime,
                            principle.Claims);

                    return user;
                }
                else {
                    // context.Result.Error  = "Invalid social login.";
                    // return null;

                    #region Validate DupEmail
                    if(!string.IsNullOrEmpty(email))
                    {
                        if (_context.Users.Where(o => o.NormalizedEmail == email.Trim().ToUpper()).Any())
                        {
                            var userTmp= await _userManager.FindByEmailAsync(email);
                            if(!userTmp.IsDeleted)
                            {
                                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, $"Email {email} นี้มีในระบบแล้ว ไม่สามารถสร้างข้อมูลสมาชิกด้วย Email ซ้ำได้");
                                return null;
                                //throw new Exception($"Email {email} นี้มีในระบบแล้ว ไม่สามารถสร้างข้อมูลสมาชิกด้วย Email ซ้ำได้");
                            }
                            else
                            {
                                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "user ถูกปิดการใช้งาน");
                                return null;
                            }
                            
                        }
                    }
                    #endregion

                    // Auto create user for social account
                    // Create User Information
                    user = new ApplicationUser();
                    user.UserName = context.UserName;
                    user.DisplayName = context.UserName;
                    user.Email = !string.IsNullOrEmpty(email) ? email : "temporary@cowapp.com";
                    user.EmailConfirmed = true;
                    user.FirstName = firstname;
                    user.LastName = lastname;

                    user.NormalizedUserName = user.UserName.ToUpper();
                    user.NormalizedEmail = user.Email.ToUpper();

                    var cResult = await _userManager.CreateAsync(user);
                    if (!cResult.Succeeded)
                    {
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "System can not create an identity.");
                        return null;
                        //throw new Exception("System can not create an identity.");
                    }
                    
                    #region AddUserRole
                    //default user role (bronze)
                    string userRole = "bronze";
                    var roleResult = await _userManager.AddToRoleAsync(user, userRole);
                    if (!roleResult.Succeeded)
                    {
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, $"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                        return null;
                        //throw new Exception($"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                    }
                    #endregion

                    var alResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, context.UserName, context.UserName));

                    #region AddPicture
                    // if(!string.IsNullOrEmpty(picture) && !string.IsNullOrEmpty(contentType))
                    // {
                        // AddPictureCommand addPictureCmd = new AddPictureCommand()
                        // {
                        //     FileName = user.Id,
                        //     CodeRef = user.Id,
                        //     TypeRef = PictureTypeRef.User,
                        //     ContentType = contentType,
                        //     FileBase64 = picture,
                        //     CreateUserId = user.Id
                        // };
                        // var picResult = await _mediator.Send(addPictureCmd);
                        // int errorSize = picResult.Errors?.Count ?? 0; //picResult.Errors == null ? 0 : picResult.Errors.Count
                        // if (!picResult.Status)
                        // {
                        //     if(errorSize > 0)
                        //         throw new Exception($"{string.Join(", ", picResult.Errors.Select(o => o.Value.Join(", ")))}");
                        //     else 
                        //         throw new Exception(picResult.Message);
                        // }
                    // }
                    // #region Validate PictureFile & ContentType
                    // else if(!string.IsNullOrEmpty(picture) && string.IsNullOrEmpty(contentType))
                    // {
                    //     throw new Exception($"กรุณาระบุ {CONTENT_TYPE_KEY}");
                    // }
                    // else if(!string.IsNullOrEmpty(contentType) && string.IsNullOrEmpty(picture))
                    // {
                    //     throw new Exception($"กรุณาระบุ {PICTURE_KEY}");
                    // }
                    // #endregion
                    #endregion

                    var principle = await _signInManager.CreateUserPrincipalAsync(user);
                    context.Result = new GrantValidationResult(
                            user.Id ?? throw new ArgumentException("Subject ID not set", nameof(user.Id)),
                            OidcConstants.AuthenticationMethods.Password,
                            _clock.UtcNow.UtcDateTime,
                            principle.Claims);

                    return user;
                }
            }
            // end check provicer = facebook, google

            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_config.Authority);

            if (disco.IsError)
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "Endpoint not found", interactive: false));
            }
            else
            {
                // request token
                var tokenRequest = new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = _config.ClientId,
                    ClientSecret = _config.ClientSecret,

                    UserName = context.UserName,
                    Password = context.Password,
                    Scope = _config.ClientScopes
                };
                if (!string.IsNullOrEmpty(provider))
                {
                    tokenRequest.Parameters.Add(PROVIDER_KEY, provider);
                }
                var tokenResponse = await client.RequestPasswordTokenAsync(tokenRequest);

                if (tokenResponse.IsError)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, tokenResponse.Error, interactive: false));
                    return user;
                }

                var userInfoResponse = await client.GetUserInfoAsync(new UserInfoRequest()
                {
                    Address = disco.UserInfoEndpoint,
                    Token = tokenResponse.AccessToken
                });

                // retrieve claims of the external user
                var claims = userInfoResponse.Claims.ToList();

                // try to determine the unique id of the external user - the most common claim type for that are the sub claim and the NameIdentifier
                // depending on the external provider, some other claim type might be used
                var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
                if (userIdClaim == null)
                {
                    userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                }
                if (userIdClaim == null)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Unknown userid");
                    return null;
                    //throw new Exception("Unknown userid");
                }

                var externalUserId = userIdClaim.Value;
                var externalUsername = claims.FirstOrDefault(o => o.Type == "empCode")?.Value;

                var signInResult = await _signInManager.ExternalLoginSignInAsync(disco.Issuer, externalUserId, isPersistent: false, bypassTwoFactor: true);
                if (signInResult.Succeeded)
                {
                    user = await _userManager.FindByLoginAsync(disco.Issuer, externalUserId);

                    var principle = await _signInManager.CreateUserPrincipalAsync(user);
                    context.Result = new GrantValidationResult(
                            user.Id ?? throw new ArgumentException("Subject ID not set", nameof(user.Id)),
                            OidcConstants.AuthenticationMethods.Password,
                            _clock.UtcNow.UtcDateTime,
                            principle.Claims);

                    return user;
                }
                else if (signInResult.IsLockedOut)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "locked out", interactive: false));
                }
                else
                {
                    // If the user does not have an account, then ask the user to create an account.
                    user = await _userManager.FindByNameAsync(externalUsername);
                    if (user != null)
                    {
                        // Update User Information
                        user.EmployeeCode = claims.FirstOrDefault(o => o.Type == "empCode")?.Value;
                        user.DisplayName = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.Name)?.Value;
                        user.FirstName = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.GivenName)?.Value;
                        user.LastName = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.FamilyName)?.Value;
                        user.PhoneNumber = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.PhoneNumber)?.Value;
                        user.Email = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.Email)?.Value;
                        user.EmailConfirmed = true;

                        user.NormalizedEmail = user.Email.ToUpper();

                        await _userManager.UpdateAsync(user);
                    }
                    else
                    {
                        #region Validate DupEmail
                        string emailTmp = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.Email)?.Value;
                        if(!string.IsNullOrEmpty(emailTmp))
                        {
                            if (_context.Users.Where(o => o.NormalizedEmail == emailTmp.Trim().ToUpper()).Any())
                            {
                                var userTmp= await _userManager.FindByEmailAsync(emailTmp);
                                if(!userTmp.IsDeleted)
                                {
                                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, $"Email {emailTmp} นี้มีในระบบแล้ว ไม่สามารถสร้างข้อมูลสมาชิกด้วย Email ซ้ำได้");
                                    return null;
                                    //throw new Exception($"Email {email} นี้มีในระบบแล้ว ไม่สามารถสร้างข้อมูลสมาชิกด้วย Email ซ้ำได้");
                                }
                                else
                                {
                                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "user ถูกปิดการใช้งาน");
                                    return null;
                                }
                                
                            }
                        }
                        #endregion

                        // Create User Information
                        user = new ApplicationUser();
                        user.UserName = user.EmployeeCode = claims.FirstOrDefault(o => o.Type == "empCode")?.Value;
                        user.DisplayName = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.Name)?.Value;
                        user.FirstName = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.GivenName)?.Value;
                        user.LastName = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.FamilyName)?.Value;
                        user.PhoneNumber = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.PhoneNumber)?.Value;
                        user.Email = claims.FirstOrDefault(o => o.Type == JwtClaimTypes.Email)?.Value;
                        user.EmailConfirmed = true;
                        user.OpenID = true;

                        user.NormalizedUserName = user.UserName.ToUpper();
                        user.NormalizedEmail = user.Email.ToUpper();

                        var cResult = await _userManager.CreateAsync(user);
                        if (!cResult.Succeeded)
                        {
                            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "System can not create an identity.");
                            return null;
                            //throw new Exception("System can not create an identity.");
                        }
                        
                        #region AddUserRole
                        //default user role (bronze)
                        string userRole = "bronze";
                        var roleResult = await _userManager.AddToRoleAsync(user, userRole);
                        if (!roleResult.Succeeded)
                        {
                            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, $"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                            return null;
                            //throw new Exception($"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                        }
                        #endregion
                    }

                    var alResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(disco.Issuer, externalUserId, externalUsername));

                    var principle = await _signInManager.CreateUserPrincipalAsync(user);
                    context.Result = new GrantValidationResult(
                            user.Id ?? throw new ArgumentException("Subject ID not set", nameof(user.Id)),
                            OidcConstants.AuthenticationMethods.Password,
                            _clock.UtcNow.UtcDateTime,
                            principle.Claims);

                    return user;
                }
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            return user;
        }

        private async Task<ApplicationUser> LoginLocal(ResourceOwnerPasswordValidationContext context)
        {
            // DB for others
            var user = await _userManager.FindByNameAsync(context.UserName);

            if (user != null)
            {
                if(user.InActiveStatus)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "user inActive", interactive: false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "user ถูกปิดการใช้งาน");
                    return user;
                }
                else if(user.IsDeleted)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "user isDeleted", interactive: false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "user ถูกปิดการใช้งาน");
                    return user;
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, context.Password, true);
                if (result.Succeeded)
                {
                    var sub = await _userManager.GetUserIdAsync(user);
                    var principle = await _signInManager.CreateUserPrincipalAsync(user);

                    await _events.RaiseAsync(new UserLoginSuccessEvent(context.UserName, sub, context.UserName, interactive: false));

                    context.Result = new GrantValidationResult(
                            user.Id ?? throw new ArgumentException("Subject ID not set", nameof(user.Id)),
                            OidcConstants.AuthenticationMethods.Password,
                            _clock.UtcNow.UtcDateTime,
                            principle.Claims);

                    return user;
                }
                else if (result.IsLockedOut)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "locked out", interactive: false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "locked out");
                }
                else if (result.IsNotAllowed)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "not allowed", interactive: false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "not allowed");
                }
                else
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid credentials", interactive: false));
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "กรุณากรอกข้อมูลให้ถูกต้อง");
                }
            }
            else
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(context.UserName, "invalid username", interactive: false));
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "กรุณากรอกข้อมูลให้ถูกต้อง");
            }

            //context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            
            return user;
        }

        private string GetHash(string secret, string provider, string providerKey)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            StringBuilder sb = new StringBuilder();
            sb.Append(provider);
            sb.Append(".");
            sb.Append(providerKey);

            Byte[] textBytes = encoding.GetBytes(sb.ToString());
            Byte[] keyBytes = encoding.GetBytes(secret);
            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
