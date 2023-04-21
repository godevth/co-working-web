using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Persistence;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Models.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var iden = context.Subject.Identities.FirstOrDefault();
            if (iden != null)
            {
                var sub = iden.FindFirst("sub");
                foreach (var claim in iden.Claims)
                {
                    if (claim.Type == "name")
                    {
                        string displayName = await _context.Users
                            .Where(o => o.Id == sub.Value)
                            .Select(o => !string.IsNullOrEmpty(o.FirstName) ? o.FirstName + " " + o.LastName : o.UserName)
                            .SingleAsync();
                        context.IssuedClaims.Add(new Claim(claim.Type, displayName));
                    }
                    else
                    {
                        context.IssuedClaims.Add(claim);
                    }
                }
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}