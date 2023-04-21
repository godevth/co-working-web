using System.Threading.Tasks;
using CoreCMS.Application.Role.Commands;
using CoreCMS.Application.Role.Models;
using CoreCMS.Application.Role.Queries;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice")]
    public class RoleController : BaseController
    {
        [HttpPost("RoleOptions")]
        public async Task<IActionResult> PostRoleOptions([FromBody] GetRoleOptionsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Find([FromBody] RoleDataTable model)
        {
            var query = new SearchRoleQuery() { DataTable = model };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Add([FromBody] AddRoleCommand model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Get(GetRoleQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Edit([FromBody] EditRoleCommand model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Delete(string id)
        {
            CommandResult<bool> result = null;
            DeleteRoleCommand deleteCmd = new DeleteRoleCommand()
            {
                Id = id
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
    }
}