using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreCMS.Application.Booking.Commands;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Booking.Queries;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    public class BookingController : BaseController
    {

        [Route("History")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> History([FromBody]HistoryQuery command)
        {
            command.UserId = GetCurrentUserId();
            var result = await Mediator.Send(command);
           return Ok(result);
        }

        [Route("Detail")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Detail(GetBookingDetailQuery command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Route("Alert")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Alert(AlertBookingCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Route("Create")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Create([FromBody] BookingCommand command)
        {
            CommandResult<string> result;
            if (!ModelState.IsValid)
            {
                var mes = command.Language == "EN" ? "Data incorrect" : "ข้อมูลไม่ถูกต้อง";
                result = new CommandResult<string>() { Message = mes };
                var errors = ModelState
                            .SelectMany(v => v.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                result.Errors = new Dictionary<string,string[]>(){
                        { "MODEL_VALIDATION", errors.ToArray() }
                };
                return BadRequest(result);
            }

            command.CurrentUserId = GetCurrentUserId();
            result = await Mediator.Send(command);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Route("Update")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Update([FromBody] EditBookingCommand command)
        {
            var mes = command.Language == "EN" ? "Data incorrect" : "ข้อมูลไม่ถูกต้อง";
            CommandResult<string> result;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<string>() { Message = mes };
                var errors = ModelState
                            .SelectMany(v => v.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                result.Errors = new Dictionary<string, string[]>(){
                        { "MODEL_VALIDATION", errors.ToArray() }
                };
                return BadRequest(result);
            }

            command.CurrentUserId = GetCurrentUserId();
            result = await Mediator.Send(command);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Route("Cancel")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Cancel([FromBody] CancelBookingCommand command)
        {
            CommandResult<bool> result;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<bool>() { Message = "ข้อมูลไม่ถูกต้อง" };
                var errors = ModelState
                            .SelectMany(v => v.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                result.Errors = new Dictionary<string, string[]>(){
                        { "MODEL_VALIDATION", errors.ToArray() }
                };
                return BadRequest(result);
            }

            command.UpdateUserId = GetCurrentUserId();
            command.IsMobile = true;
            result = await Mediator.Send(command);
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
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> CheckIn(int id)
        {
            CheckInCommand command = new CheckInCommand() 
            { 
                BookingId = id,
                CreateUserId = GetCurrentUserId(),
                IsSuperAdmin = User.IsInRole("super_admin")
            };
            var result = await Mediator.Send(command);
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
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> CheckOut(int id)
        {
            CheckOutCommand command = new CheckOutCommand() 
            { 
                BookingId = id,
                CreateUserId = GetCurrentUserId(),
                IsSuperAdmin = User.IsInRole("super_admin")
            };
            var result = await Mediator.Send(command);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }      
        
        [Route("CalculatePricing")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> CalculatePricing([FromBody] CalculatePricingCommand command)
        {
            CommandResult<CalculatePricing> result;
            if (!ModelState.IsValid)
            {
                var mes = command.Language == "EN" ? "Data incorrect" : "ข้อมูลไม่ถูกต้อง";
                result = new CommandResult<CalculatePricing>() { Message = mes };
                var errors = ModelState
                            .SelectMany(v => v.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();
                result.Errors = new Dictionary<string,string[]>(){
                        { "MODEL_VALIDATION", errors.ToArray() }
                };
                return BadRequest(result);
            }
            command.CurrentUserId = GetCurrentUserId();
            result = await Mediator.Send(command);
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