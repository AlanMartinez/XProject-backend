using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using XProject.Infrastructure.Filters.Models;

namespace XProject.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var json = new Response(filterContext.Exception.Message);

            filterContext.Result = new BadRequestObjectResult(json);
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            filterContext.ExceptionHandled = true;
        }
    }
}
