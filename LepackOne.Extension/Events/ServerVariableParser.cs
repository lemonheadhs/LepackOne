using LepackOne.Extension.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.UI.JavaScript;

namespace LepackOne.Extension.Events
{
    public class ServerVariableParser : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ServerVariablesParser.Parsing += ServerVariable_Parsing;
        }

        private void ServerVariable_Parsing(object sender, Dictionary<string, object> e)
        {
            if (HttpContext.Current == null) return;
            var urlHelper = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()));

            var mainDictionary = new Dictionary<string, object>
            {
                {
                    "leReportApiBaseUrl",
                    urlHelper.GetUmbracoApiServiceBaseUrl<LeReportController>(controller => controller.ImportReport(null))
                }
            };
            
            if (!e.ContainsKey("lepackOneUrls"))
            {
                e.Add("lepackOneUrls", mainDictionary);
            }
        }
    }
}