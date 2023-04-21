using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Application.PlaceType.Commands;
using CoreCMS.Application.PlaceType.Models;
using CoreCMS.Application.PlaceType.Queries;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    // [Authorize(AuthenticationSchemes = "Bearer")]
    public class PlaceTypeController : BaseController
    {
       [HttpGet("Select2PlaceType")]
       [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> PlaceType(Select2PlaceTypeQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
