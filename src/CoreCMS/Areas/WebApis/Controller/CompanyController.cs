using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Application.Company.Commands;
using CoreCMS.Application.Company.Models;
using CoreCMS.Application.Company.Queries;
using CoreCMS.Application.FacilityType.Commands;
using CoreCMS.Application.FacilityType.Queries;
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
    public class CompanyController : BaseController
    {
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin")]
        public async Task<IActionResult> Find([FromBody] CompanyDataTable model)
        {
            model.IsAdmin = User.FindFirstValue(ClaimTypes.Role) == "super_admin";
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = new SearchCompanyQuery() { DataTable = model };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin")]
        public async Task<IActionResult> Add([FromBody] AddCompanyCommand model)
        {
            model.CreateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin")]
        public async Task<IActionResult> Get(GetCompanyQuery model)
        {
            var result = await Mediator.Send(model);
            result.Data.IsEdit = (User.FindFirstValue(ClaimTypes.Role) == "super_admin");
            return Ok(result);
        }

        [HttpPut("Edit")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin")]
        public async Task<IActionResult> Edit([FromBody] EditCompanyCommand model)
        {
            model.UpdateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.IsEdit = (User.FindFirstValue(ClaimTypes.Role) == "super_admin");
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin")]
        public async Task<IActionResult> Delete(int id)
        {
            CommandResult<bool> result = null;
            SoftDeleteCompanyCommand deleteCmd = new SoftDeleteCompanyCommand()
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

        [HttpPost("Select2Company")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice")]
        public async Task<IActionResult> Company(Select2CompanyQuery model)
        {
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.RoleName = User.FindFirstValue(ClaimTypes.Role);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

    }
}
