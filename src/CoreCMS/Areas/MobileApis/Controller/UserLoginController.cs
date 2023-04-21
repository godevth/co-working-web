using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreCMS.Application.Booking.Commands;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Booking.Queries;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Commands;
using CoreCMS.Controllers;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreCMS.Areas.MobileApis.Controller
{
    [Area("MApi")]
    [Produces("application/json")]
    [Route("Mobile/Api/[controller]")]
    public class UserLoginController : BaseController
    {
        private UserManager<ApplicationUser> _userManager;
        public UserLoginController(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("Status")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Status()
        {
            Console.WriteLine("Test");
            var UserId = GetCurrentUserId();
            Console.WriteLine(UserId);
            var user = await _userManager.FindByIdAsync(UserId);
            // var user = await _userManager.FindByIdAsync("afa80847-39bb-4fa8-843f-4f7fd370fcc3");
            var result = await _userManager.GetLoginsAsync(user);
            return Ok(result);
        }


        [Route("Add")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Create([FromBody] RegisterCommand command)
        {
            var UserId = GetCurrentUserId();
            Console.WriteLine(UserId);
            var user = await _userManager.FindByIdAsync(UserId);
            // var user = await _userManager.FindByIdAsync("afa80847-39bb-4fa8-843f-4f7fd370fcc3");
            var get_login = await _userManager.GetLoginsAsync(user);
            var result = await _userManager.AddLoginAsync(user, new UserLoginInfo(command.LoginProvider, command.ProviderKey, command.ProviderDisplayName));
            return Ok(result);
        }

        [Route("Remove")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "mobile")]
        public async Task<IActionResult> Remove([FromBody] RegisterCommand command)
        {
            Console.WriteLine("Test");
            var UserId = GetCurrentUserId();
            Console.WriteLine(UserId);
            var user = await _userManager.FindByIdAsync(UserId);
            // var user = await _userManager.FindByIdAsync("afa80847-39bb-4fa8-843f-4f7fd370fcc3");
            var result = await _userManager.RemoveLoginAsync(user, command.LoginProvider, command.ProviderKey);
            return Ok(result);
        }

    }
}