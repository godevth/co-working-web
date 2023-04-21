using CoreCMS.Application.Privilege.Commands;
using CoreCMS.Application.Privilege.Queries;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Controllers;
using CoreCMS.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice")]
    public class PrivilegeController : BaseController
    {
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Find([FromBody] SearchPrivilegeQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Get(int id)
        {
            GetPrivilegeQuery query = new GetPrivilegeQuery() { PrivilegeId = id};
            return Ok(await Mediator.Send(query));
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Toggle(int id)
        {
            CommandResult<bool> result = null;
            ToggleActiveStatusPrivilegeCommand toggleCmd = new ToggleActiveStatusPrivilegeCommand()
            {
                PrivilegeId = id,
                UpdatedUserId = GetCurrentUserId()
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

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Delete(int id)
        {
            CommandResult<bool> result = null;
            SoftDeletePrivilegeCommand deleteCmd = new SoftDeletePrivilegeCommand()
            {
                PrivilegeId = id,
                DeleteUserId = GetCurrentUserId()
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

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> PostAdd([FromBody] InviteUserCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                return BadRequest(result);
            }
          
            model.CreateUserId = GetCurrentUserId();
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

        [HttpPost("ImportUser")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> ImportUser([FromBody] ImportUserCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                return BadRequest(result);
            }
            model.CreateUserId = GetCurrentUserId();
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

        [HttpPost("AddCustomer")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> AddCustomer([FromBody] AddUserCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                return BadRequest(result);
            }
          
            model.CreateUserId = GetCurrentUserId();
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

        [HttpPost("ImportInvite")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> ImportInvite([FromBody] ImportInviteCommand model)
        {
            CommandResult<string> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = "ข้อมูลไม่ถูกต้อง" };
                return BadRequest(result);
            }
          
            model.CreateUserId = GetCurrentUserId();
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

    }
}
