using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Application.Room.Commands;
using CoreCMS.Application.Room.Models;
using CoreCMS.Application.Room.Queries;
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
    public class RoomController : BaseController
    {
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Find([FromBody] RoomDataTable model)
        {
            var query = new SearchRoomQuery() { DataTable = model };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Add([FromBody] AddRoomCommand model)
        {
            model.CreateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Get(GetRoomQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Edit([FromBody] EditRoomCommand model)
        {
            model.UpdateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Delete(Guid id)
        {
            CommandResult<bool> result = null;
            SoftDeleteRoomCommand deleteCmd = new SoftDeleteRoomCommand()
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
        
        [HttpPost("Select2Room")]
        public async Task<IActionResult> Select2Room([FromBody] Select2RoomQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("GP/{id}")]
        public async Task<IActionResult> GP(Guid id)
        {
            var model = new GetPlaceGPQuery() { Id = id} ;
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
