using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.XmlTransform;

using Umbraco.Core;
using Umbraco.Core.Logging;
using System.Configuration;

namespace LepackOne.Extension.Events
{
    public class LepackOneApplicationEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            string filePath = "~/config/Dashboard.config";
            string xdtPth = "~/App_plugins/LeReport/Dashboard.config.install.xdt";

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["LeReport"]))
            {
                transform(filePath, xdtPth);

                var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                config.AppSettings.Settings.Add("LeReport", "on");
                config.Save();
            }
        }

        private void transform(string filePath, string xdtPth)
        {
            using (var xmlDoc = new XmlTransformableDocument())
            {
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load(HttpContext.Current.Server.MapPath(filePath));

                using (var xmlTrans = new XmlTransformation(HttpContext.Current.Server.MapPath(xdtPth)))
                {
                    if (xmlTrans.Apply(xmlDoc))
                    {
                        try
                        {
                            xmlDoc.Save(HttpContext.Current.Server.MapPath(filePath));
                        }
                        catch (Exception ex)
                        {
                            var errorMessage = "Error excuting TransformConfig: " + ex.Message;
                            LogHelper.Error<LepackOneApplicationEventHandler>(errorMessage, ex);
                        }
                    }
                }
            }
        }
    }
}