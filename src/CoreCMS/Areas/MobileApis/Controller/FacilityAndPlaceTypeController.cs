using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Application.FacilityAndPlaceType.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("Api")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    // [Authorize(AuthenticationSchemes = "Bearer")]
    public class FacilityAndPlaceTypeController : BaseController
    {
        [HttpPost("FacilityAndPlaceType")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Get([FromBody] GetFacilityAndPlaceTypeQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
