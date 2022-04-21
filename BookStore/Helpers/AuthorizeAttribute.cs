using BookStore.Entities;
using BookStore.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var _user = context.HttpContext.User;

        var user = (User)context.HttpContext.Items["User"];
        if (user == null)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }

        if (user.UserType == (int)UserType.customer)
        {
            var descriptor = context?.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
            if (descriptor.ControllerName == "Users" )
            {
                context.Result = new JsonResult(new { message = "Have no right" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }

    }
}