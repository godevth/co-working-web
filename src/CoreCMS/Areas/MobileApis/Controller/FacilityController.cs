using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCMS.Application.Facility.Queries;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    public class FacilityController : BaseController
    {
        [HttpGet("Select2Facility")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Facility(Select2FacilityByGroupQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
