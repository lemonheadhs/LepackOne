using ClosedXML.Excel;
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
        public IEnumerable<ReportRecord> ImportReport(
            [ModelBinder(typeof(ReportFileModelBinder))]
            Report report)
        {
            LogHelper.Info<LeReportController>("lemon");

            var workbook = new XLWorkbook(report.Files[0].TempFilePath);
            var ws = workbook.Worksheet(1);

            var dataRows = ws.RowsUsed().Where(r => r.RowNumber() != 1);

            //int rowCount = ws.RowCount() - 1;// emit the header row
            //var dataRows = ws.Rows(2, 2 + rowCount);
            var datas = dataRows.Select(r => new ReportRecord
                        {
                            Price = Decimal.Parse(r.Cell(1).Value as string),
                            UniversityName = r.Cell(2).Value as string,
                            Region = r.Cell(3).Value as string,
                            Year = Int32.Parse(r.Cell(4).Value as string),
                            Gender = r.Cell(5).Value as string,
                            EthnicityGroup = r.Cell(6).Value as string,
                            Enrollment = Int32.Parse(r.Cell(7).Value as string)
                        }).ToList();
                       

            return datas;
        }
    }
}