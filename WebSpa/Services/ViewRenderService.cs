using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebSpa.Services
{
    
    //https://stackoverflow.com/a/40932984/9218468
    public interface IViewRenderService
    {
        Task<string> RenderPartialViewToString(Controller Controller, string viewName, object model);
    }
    
    public class ViewRenderService : IViewRenderService
    {
        private readonly ICompositeViewEngine _viewEngine;

        public ViewRenderService(ICompositeViewEngine viewEngine)
        {
            _viewEngine = viewEngine;
        }
        public async Task<string> RenderPartialViewToString(Controller Controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = Controller.ControllerContext.ActionDescriptor.ActionName;

            Controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult =
                    _viewEngine.FindView(Controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    Controller.ControllerContext,
                    viewResult.View,
                    Controller.ViewData,
                    Controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}