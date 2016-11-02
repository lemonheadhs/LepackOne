using AutoMapper;
using ClosedXML.Excel;
using LepackOne.Extension.ModelBinder;
using LepackOne.Extension.Models;
using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace LepackOne.Extension.Controllers
{
    [PluginController("LeReport")]
    public class LeReportController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<ReportRecord> ImportReport(
            [ModelBinder(typeof(ReportFileModelBinder<Report>))]
            Report report)
        {
            LogHelper.Info<LeReportController>("lemon");
            List<ReportRecord> datas = RetrieveDataFromXlsx(report);

            return datas;
        }

        private List<ReportRecord> RetrieveDataFromXlsx(Report report)
        {
            var workbook = new XLWorkbook(report.Files[0].TempFilePath);
            var ws = workbook.Worksheet(1);

            var dataRows = ws.RowsUsed().Where(r => r.RowNumber() != 1);

            //int rowCount = ws.RowCount() - 1;// emit the header row
            //var dataRows = ws.Rows(2, 2 + rowCount);
            var datas = dataRows.Select(r => new ReportRecord
            {
                Fice = Convert.ToInt32(r.Cell(1).Value),
                UniversityName = r.Cell(2).Value.ToString(),
                Region = r.Cell(3).Value.ToString(),
                Year = Convert.ToInt32(r.Cell(4).Value),
                Gender = r.Cell(5).Value.ToString(),
                EthnicityGroup = r.Cell(6).Value.ToString(),
                Enrollment = Convert.ToInt32(r.Cell(7).Value)
            }).ToList();
            return datas;
        }

        public void ImportAttainment(
            [ModelBinder(typeof(ReportFileModelBinder<AttainmentUploadReport>))]
            AttainmentUploadReport uploadData)
        {
            var csvFile = uploadData.Files.FirstOrDefault();
            if (csvFile == null)
            {
                throw new Exception("Bad Request");
            }
            
            var csvContext = new CsvContext();
            var attainmentsData = 
                csvContext.Read<AttainmentDTO>(csvFile.TempFilePath, 
                    new CsvFileDescription
                    {
                        SeparatorChar = ',',
                        FirstLineHasColumnNames = true
                    })
                    .ToList();

            var attainments = 
                Mapper.Map<List<AttainmentDTO>, List<Attainment>>(attainmentsData);

            var db = DatabaseContext.Database;

            db.TruncateTable(Attainment.TableName);
            db.BulkInsertRecords(attainments);

            //Todo: should clean up uploaded file after successfully saved data

            //Todo: should log uploaded file when action failed

            //Todo: should inform the front end about the action result

        }
    }
}