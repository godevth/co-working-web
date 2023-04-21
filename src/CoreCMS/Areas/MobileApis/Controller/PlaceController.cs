using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreCMS.Application.Place.Commands;
using CoreCMS.Application.Place.Queries;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
    public class PlaceController : BaseController
    {
        [HttpGet("Select2Place")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Place(Select2PlaceQuery model)
        {
            var result = await Mediator.Send(model);
            return Ok(result);
        }
        //[Route]
        [HttpPost("Search")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Search([FromBody]SearchPlaceQuery model)
        {
            model.UserId = GetCurrentUserId();
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("GetDetail")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> GetDetail([FromBody] GetPlaceQuery model)
        {
            model.UserId = GetCurrentUserId();
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpPost("Wishlist")]
        [Authorize(AuthenticationSchemes = "Bearer",Policy = "mobile")]
        public async Task<IActionResult> ToggleWishlist(string placeId)
        {   
            ToggleWishlistCommand model = new ToggleWishlistCommand()
            {
                PlaceId = placeId,
                UserId = GetCurrentUserId()
            };
            var result = await Mediator.Send(model);
            return Ok(result);
        }

        [HttpGet("GetWishlist")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy ="mobile")]
        public async Task<IActionResult> GetWishlist(GetWishlistQuery model)
        {
            model.UserId =GetCurrentUserId();
            var result = await Mediator.Send(model);
            return Ok(result);
        }
    }
}
