using System;
using System.Threading.Tasks;
using CoreCMS.Application.Booking.Commands;
using CoreCMS.Application.Booking.Queries;
using CoreCMS.Application.Payment.Commands;
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
    public class BookingController : BaseController
    {
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Find([FromBody] SearchBookingQuery query)
        {
            query.UserId = GetCurrentUserId();
            query.IsSuperAdmin = User.IsInRole("super_admin");

            try
            {
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch(Exception e)
            {
                return Ok(new CommandResult<string>()
                    {
                        Message = e.Message
                    });
            }
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Get(int id)
        {
            GetBookingQuery query = new GetBookingQuery() 
            { 
                BookingId = id,
                UserId = GetCurrentUserId(),
                IsSuperAdmin = User.IsInRole("super_admin")
            };

            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch(Exception e)
            {
                return Ok(new CommandResult<string>()
                    {
                        Message = e.Message
                    });
            }
        }

        [HttpPost("Payment")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Payment([FromBody] PaymentBookingCommand model)
        {
            CommandResult<int> result = null;
            if (!ModelState.IsValid)
            {
                result = new CommandResult<int>() { Message = "ข้อมูลไม่ถูกต้อง" };
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

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
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

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
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

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Cancel(int id)
        {
            CancelBookingCommand command = new CancelBookingCommand() 
            { 
                BookingId = id,
                UpdateUserId = GetCurrentUserId(),
                Language = "TH"
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

        [HttpPut("{id}/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "backoffice", Roles = "super_admin, admin, business_owner, system")]
        public async Task<IActionResult> Refund(int id)
        {
            RefundCommand command = new RefundCommand() 
            { 
                BookingId = id,
                CurrentUser = GetCurrentUserId(),
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
    }
}