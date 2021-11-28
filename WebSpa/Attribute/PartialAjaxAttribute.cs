using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using WebSpa.Extensions;
using WebSpa.Models;
using WebSpa.Services;

namespace WebSpa.Attribute
{
    public class PartialAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                IViewRenderService _renderService = context.HttpContext.RequestServices.GetService<IViewRenderService>();
                var controller = ((Controller)context.Controller);
                var actionName =controller
                    .ControllerContext.ActionDescriptor.ActionName;
                var viewName = $"_{actionName}";
                var model = controller.ViewData.Model??"";
                var jsonModel=new HtmlResult
                {
                    Html = _renderService?.RenderPartialViewToString(controller,viewName,model).Result,
                    Title = controller.ViewData["Title"]==null ? "":controller.ViewData["Title"].ToString()
                };
                ContentResult content = new ContentResult();
                content.ContentType = "application/json";
                content.Content = JsonSerializer.Serialize(jsonModel);
                context.Result = content;
            }
        }
    }
}