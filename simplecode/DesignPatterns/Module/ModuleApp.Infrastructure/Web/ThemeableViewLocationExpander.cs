using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace ModuleApp.Infrastructure.Web
{
    public class ThemeableViewLocationExpander : IViewLocationExpander
    {
        //private const string THEME_KEY = "theme";
        private const string _moduleKey = "module";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //context.Values.TryGetValue(THEME_KEY, out string theme);
            context.Values.TryGetValue(_moduleKey, out string theme);

            if (!string.IsNullOrWhiteSpace(theme) && !string.Equals(theme, "Generic", System.StringComparison.InvariantCultureIgnoreCase))
            {
                var moduleViewLocations = new string[]
                {
                    $"/Themes/{theme}/Areas/{{2}}/Views/{{1}}/{{0}}.cshtml",
                    $"/Themes/{theme}/Areas/{{2}}/Views/Shared/{{0}}.cshtml",
                    $"/Themes/{theme}/Views/{{1}}/{{0}}.cshtml",
                    $"/Themes/{theme}/Views/Shared/{{0}}.cshtml"
                };
                viewLocations = moduleViewLocations.Concat(viewLocations);
            }

            return viewLocations;
            //var path = Directory.GetCurrentDirectory();
            //if (context.Values.ContainsKey(_moduleKey))
            //{
            //    var module = context.Values[_moduleKey];
            //    if (!string.IsNullOrWhiteSpace(module))
            //    {
            //        var moduleViewLocations = new string[]
            //        {
            //           $"/ModuleApp.Module."+module+"/Areas/"+context.AreaName+"/Views/"+context.ControllerName+"/"+context.ViewName+".cshtml",
            //           $"/ModuleApp.Module." + module + "/Views/{1}/{0}.cshtml",
            //           $"/ModuleApp.Modules." + module + "/Views/Shared/{0}.cshtml"
            //        };

            //        viewLocations = moduleViewLocations.Concat(viewLocations);
            //    }
            //}
            //return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            //var controllerName = context.ActionContext.ActionDescriptor.DisplayName;
            //if (controllerName == null) // in case of render view to string
            //{
            //    return;
            //}

            //context.ActionContext.HttpContext.Request.Cookies.TryGetValue("theme", out string previewingTheme);
            //if (!string.IsNullOrWhiteSpace(previewingTheme))
            //{
            //    context.Values[THEME_KEY] = previewingTheme;
            //}
            //else
            //{
            //    var config = context.ActionContext.HttpContext.RequestServices.GetService<IConfiguration>();
            //    context.Values[THEME_KEY] = config["Theme"];
            //}

            var controllerName = context.ActionContext.ActionDescriptor.DisplayName;
            if (controllerName == null) // in case of render view to string
            {
                return;
            }

            context.ActionContext.HttpContext.Request.Cookies.TryGetValue(_moduleKey, out string previewingTheme);
            if (!string.IsNullOrWhiteSpace(previewingTheme))
            {
                context.Values[_moduleKey] = previewingTheme;
            }
            else
            {
                var config = context.ActionContext.HttpContext.RequestServices.GetService<IConfiguration>();
                context.Values[_moduleKey] = config["module"];
            }

            //var controller = context.ActionContext.ActionDescriptor.DisplayName;
            //var moduleName = controller.Split('.')[2];
            //if (moduleName != "ModuleApp")
            //{
            //    context.Values[_moduleKey] = moduleName;
            //}
        }
    }
}