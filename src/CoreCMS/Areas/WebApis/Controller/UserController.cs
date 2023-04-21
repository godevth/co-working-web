using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Commands;
using CoreCMS.Application.User.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer")] 
    public class UserController : BaseController
    {
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> PostSearch([FromBody]SearchUserQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        //No Authen
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> PostResetPassword([FromBody] ResetPasswordCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                return BadRequest(result);
            }
          
            result = await Mediator.Send(model);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        //No Authen
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> PostConfirmEmail([FromBody] ConfirmEmailCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                return BadRequest(result);
            }
          
            result = await Mediator.Send(model);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Delete(string id)
        {
            CommandResult<bool> result = null;
            SoftDeleteUserCommand deleteCmd = new SoftDeleteUserCommand()
            {
                UserId  = id,
                DeleteUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            result = await Mediator.Send(deleteCmd);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Toggle(string id)
        {
            CommandResult<bool> result = null;
            ToggleActiveStatusUserCommand toggleCmd = new ToggleActiveStatusUserCommand()
            {
                UserId  = id,
                UpdatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            result = await Mediator.Send(toggleCmd);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> PostAdd([FromBody] AddUserCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                return BadRequest(result);
            }
          
            model.CreateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            result = await Mediator.Send(model);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Get(string id)
        {
            GetUserQuery query = new GetUserQuery() { UserId = id};
            return Ok(await Mediator.Send(query));
        }

        [HttpPut("Edit")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> PutEdit([FromBody] EditUserCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                bool chk = true;
                var errors = ModelState.Where(ms => ms.Value.Errors.Any())
                                        .Select(x => x.Key);
                foreach (var e in errors) {
                    if(e != nameof(model.Password) && e != nameof(model.ConfirmPassword))
                    {
                        chk = false;
                        break;
                    }
                }
                if(!chk)
                {
                    result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                    return BadRequest(result);
                }
            }
          
            model.UpdatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            result = await Mediator.Send(model);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("Select2UserByType")]
        public async Task<IActionResult> Users([FromBody]Select2UserByTypeQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("Select2User")]
        public async Task<IActionResult> Users(Select2UserQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("Select2UserOwner")]
        public async Task<IActionResult> UserOwner(Select2UserOwnerQuery model)
        {
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("Select2UserPrivilege")]
        public async Task<IActionResult> UserPrivilege([FromBody] Select2UserPrivilegeQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}