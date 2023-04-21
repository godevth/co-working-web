using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Application.Facility.Commands;
using CoreCMS.Application.Facility.Models;
using CoreCMS.Application.Facility.Queries;
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
    public class FacilityController : BaseController
    {
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Find([FromBody] FacilityDataTable model)
        {
            var query = new SearchFacilityQuery() { DataTable = model };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Add([FromBody] AddFacilityCommand model)
        {
            model.CreateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Get(GetFacilityQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Edit([FromBody] EditFacilityCommand model)
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
            SoftDeleteFacilityCommand deleteCmd = new SoftDeleteFacilityCommand()
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

        [HttpPost("Select2Facility")]
        public async Task<IActionResult> FacilityType([FromBody]Select2FacilityQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
