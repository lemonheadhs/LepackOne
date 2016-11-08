using ClientDependency.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;

namespace LepackOne.Extension.Events
{
    public class ClientDependencyRegister : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            string leReportJsRoot = "~/App_Plugins/LeReport/lib/";

            BundleManager.CreateJsBundle("LePackExtention",
                new JavascriptFile(leReportJsRoot + "ramda/js/ramda.js"),
                new JavascriptFile(leReportJsRoot + "highcharts/js/highcharts.js"),
                new JavascriptFile(leReportJsRoot + "highcharts/js/highcharts-more.js"),
                new JavascriptFile(leReportJsRoot + "highcharts/js/exporting.js"),
                new JavascriptFile(leReportJsRoot + "highcharts/js/drilldown.js"));
        }
    }
}
