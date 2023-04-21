using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace CoreCMS.Controllers
{
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        
        private string _currentCulture;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public string Localization(string defaultVal, params (string culture, string value)[] locales)
        {
            if (locales != null && locales.Length > 0)
            {
                var locale = locales.Where(o => o.culture == GetCurrentCulture()).FirstOrDefault();
                if (!string.IsNullOrEmpty(locale.culture) && !string.IsNullOrEmpty(locale.value))
                    return locale.value;
            }

            return defaultVal;
        }

        public string GetCurrentCulture()
        {
            if (string.IsNullOrEmpty(_currentCulture))
            {
                _currentCulture = ControllerContext.RouteData.Values.ContainsKey("culture") ? ControllerContext.RouteData.Values["culture"].ToString() : "th";
            }
            return _currentCulture;
        }

        public string GetCurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return null;
        }

        public Dictionary<string, string[]> GetErrorsDictionary(ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, string[]>();
            foreach (var item in ModelState)
            {
                errors.Add(item.Key, item.Value.Errors.Select(o => o.ErrorMessage).ToArray());
            }
            return errors;
        }

        public string GetCurrentEmail()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirstValue(ClaimTypes.Email);
            }
            return null;
        }
    }
}