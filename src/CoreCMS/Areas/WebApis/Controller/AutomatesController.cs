using System.Threading.Tasks;
using CoreCMS.Application.Booking.Commands;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.WebApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "automate")]
    public class AutomatesController : BaseController
    {
        [HttpGet("CheckOut")] 
        public async Task<IActionResult> CheckOut()
        {
            return Ok(await Mediator.Send(new CheckOutAllCommand()));
        }

        [HttpGet("WaitingForCheckIn")] 
        public async Task<IActionResult> WaitingForCheckIn()
        {
            return Ok(await Mediator.Send(new WaitingForCheckInCommand()));
        }
    }
}