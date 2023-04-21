using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using CoreCMS.Application.Place.Commands;
using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Place.Queries;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.WebApis.Controller
{   
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice")]
    public class PlaceController : BaseController
    {
        [HttpGet("Select2Place")]
        public async Task<IActionResult> Place(Select2PlaceQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("Select2PlaceByCompany")]
        public async Task<IActionResult> Place([FromBody]Select2PlaceByCompanyQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Search([FromBody]PlaceDataTable model)
        {
            model.IsAdmin = User.IsInRole("super_admin");
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var query = new SearchPlaceDataTableQuery() { DataTable = model };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("GetDetail")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> GetDetail([FromBody] GetPlaceQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Add([FromBody] AddPlaceCommand model)
        {
            model.CreateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Edit([FromBody] EditPlaceCommand model)
        {
            model.UpdateUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Get(GetPlaceDetailQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Delete(Guid id)
        {
            CommandResult<bool> result = null;
            SoftDeletePlaceCommand deleteCmd = new SoftDeletePlaceCommand()
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

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> QRCode(Guid id)
        {
            PlaceQRCodeGeneratorCommand command = new PlaceQRCodeGeneratorCommand() 
            { 
                PlaceId = id
            };

            try
            {
                var result = await Mediator.Send(command);
                return File(result.FileByte, result.MimeType, HttpUtility.UrlEncode(result.FileName));
            }
            catch(Exception e)
            {
                return Ok(new CommandResult<string>()
                    {
                        Message = e.Message
                    });
            }
        }

        [HttpGet("Name/{id}")]
        public async Task<IActionResult> Name(Guid id)
        {
            var result = await Mediator.Send(new GetPlaceNameQuery() { Id = id});
            return Ok(result);
        }

        [HttpPost("SearchApprove")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> SearchApprove([FromBody]PlaceApproveDataTable model)
        {
           var query = new SearchPlaceApproveQuery() { DataTable = model };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Approve")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> Approve([FromBody]ApprovePlaceCommand model)
        {
            model.UpdateeUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("NotApprove")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin")]
        public async Task<IActionResult> NotApprove([FromBody]NotApprovePlaceCommand model)
        {
            model.UpdateeUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
