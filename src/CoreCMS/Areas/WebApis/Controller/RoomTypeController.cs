using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Application.RoomType.Commands;
using CoreCMS.Application.RoomType.Models;
using CoreCMS.Application.RoomType.Queries;
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
    public class RoomTypeController : BaseController
    {
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Find([FromBody] RoomTypeDataTable model)
        {
            var query = new SearchRoomTypeQuery() { DataTable = model };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Add([FromBody] AddRoomTypeCommand model)
        {
            model.CreateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Get(GetRoomTypeQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Edit([FromBody] EditRoomTypeCommand model)
        {
            model.UpdateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Delete(int id)
        {
            CommandResult<bool> result = null;
            SoftDeleteRoomTypeCommand deleteCmd = new SoftDeleteRoomTypeCommand()
            {
                Id  = id,
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

        [HttpPost("Select2RoomType")]
        public async Task<IActionResult> RoomType([FromBody]Select2RoomTypeQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
