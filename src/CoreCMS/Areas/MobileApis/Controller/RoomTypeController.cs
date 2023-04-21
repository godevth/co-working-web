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

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    // [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile-alt")]
    public class RoomTypeController : BaseController
    {
       [HttpGet("Select2RoomType")]
       [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> RoomType(Select2RoomTypeQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
