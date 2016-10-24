using LepackOne.Extension.ModelBinder;
using LepackOne.Extension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using Umbraco.Core.Logging;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace LepackOne.Extension.Controllers
{
    [PluginController("LeReport")]
    public class LeReportController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<ReportRecordViewModel> ImportReport(
            [ModelBinder(typeof(ReportFileModelBinder))]
            Report report)
        {
            LogHelper.Info<LeReportController>("lemon");


            return null;
        }
    }
}