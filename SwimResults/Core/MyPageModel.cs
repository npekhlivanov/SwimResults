using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;

namespace SwimResults.Core
{
    public class MyPageModel : PageModel
    {
        public override Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            string refererPath = string.Empty;
            string currentPath = this.Request.Path;
            var referers = this.Request.Headers[HeaderNames.Referer];
            if (referers.Count > 0)
            {
                var referer = referers.ToString();
                var host = this.Request.Host.Value;
                if (!string.IsNullOrEmpty(referer))
                {
                    var paramsPos = referer.IndexOf('?', StringComparison.Ordinal);
                    if (paramsPos > 0)
                    {
                        referer = referer.Substring(0, paramsPos);
                    }

                    var hostPos = referer.IndexOf(host, StringComparison.OrdinalIgnoreCase);
                    if (hostPos >= 0)
                    {
                        refererPath = referer.Substring(hostPos + host.Length); 
                        if (refererPath.StartsWith(currentPath, StringComparison.OrdinalIgnoreCase))
                        {
                            refererPath = currentPath;
                        }
                    }
                }
            }

            var pageName = this.GetType().Name.Replace("Model", string.Empty, StringComparison.OrdinalIgnoreCase);
            var customNameAttr = this.GetType().GetCustomAttributes(typeof(BreadcrumbAttribute), false);
            var customName = customNameAttr.Length > 0 ? ((BreadcrumbAttribute)customNameAttr[0]).Title : string.Empty;
            var prevPage = this.PrevPageName;
            prevPage = this.CurrentPageName;
            var curPage = string.IsNullOrEmpty(customName) ? pageName : customName;
            this.PrevPageName = prevPage;
            this.CurrentPageName = curPage;

            //var attr1 = new BreadcrumbAttribute(pageName);
            //if (!string.IsNullOrEmpty(prevPage))
            //{
            //    var attr2 = new BreadcrumbAttribute(prevPage);
            //    attr1.FromPage = attr2;
            //}
            string path = string.IsNullOrEmpty(refererPath) ? $"/{curPage}" : $"/{refererPath}/{curPage}/";
            //string path = string.IsNullOrEmpty(prevPage) ? $"/{curPage}" : $"/{prevPage}/{curPage}/";

            //var node1 = new RazorPageBreadcrumbNode(refererPath, curPage);
            //ViewData["BreadcrumbNode"] = node1;

            return base.OnPageHandlerExecutionAsync(context, next);
        }

        private string PrevPageName { get => (string)TempData.Peek("PrevPage"); set => TempData["PrevPage"] = value; }

        private string CurrentPageName { get => (string)TempData.Peek("CurrentPage"); set => TempData["CurrentPage"] = value; }
    }
}
