using System.Net;
using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.UserType.Commands;
using CoreCMS.Application.UserType.Models;
using CoreCMS.Application.UserType.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice")]
    public class UserTypeController : BaseController
    {
        [HttpPost("UserTypeOptions")]
        public async Task<IActionResult> PostUserTypeOptions([FromBody] GetUserTypeOptionsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Find([FromBody] UserTypeDataTable model)
        {
            var query = new SearchUserTypeQuery() { DataTable = model };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Add([FromBody] AddUserTypeCommand model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Get(GetUserTypeQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Edit([FromBody] EditUserTypeCommand model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Delete(int id)
        {
            CommandResult<bool> result = null;
            DeleteUserTypeCommand deleteCmd = new DeleteUserTypeCommand()
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

        [HttpPut("Toggle")]
        //[Authorize(AuthenticationSchemes = "Bearer", UserTypes = "super_admin")]
        public async Task<IActionResult> Toggle([FromBody] ToggleUserTypeCommand model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}