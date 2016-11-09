using AutoMapper;
using LepackOne.Extension.Models;
using LepackOne.Extension.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Web.Mvc;

namespace LepackOne.Extension.Controllers
{
    [PluginController("LeReport")]
    public class LeReportSurfaceController : SurfaceController
    {
        private UmbracoDatabase DB { get { return DatabaseContext.Database; } } 

        public JsonNetResult GetChartData(int currentContentId, int chartIndex)
        {
            //Todo: dispatch according to chartType

            var data = DB.Fetch<Attainment>(Sql.Builder
                .Select("*")
                .From(Attainment.TableName));

            var pair = data.GroupBy(x => x.IsTarget).ToList();
            var targetGroup = pair.Find(g => g.Key == true).OrderBy(i => i.Year).ToList();
            var nonTargetGroup = pair.Find(g => g.Key == false);

            var raw =
                nonTargetGroup.GroupBy(x => x.DegreeLevel)
                    .Select(g => new
                    {
                        Key = g.Key,
                        OrderedItems = g.OrderBy(i => i.Year).ToList()
                    });

            var viewModel = new AttainmentChartViewModel
                {
                    NonTargetCategories = raw.First().OrderedItems.Select(i => i.Year),
                    TargetCategories = targetGroup.Select(i => i.Year),
                    Groups = 
                        raw.Select(h => new ChartDataGroup<decimal>
                        {
                            Name = h.Key,
                            Data = h.OrderedItems.Select(i => i.Percentage).ToList()
                        }).ToList()
                };
            viewModel.Groups.Add(
                new ChartDataGroup<decimal> {
                    Name = "Target",
                    Data = targetGroup.Select(i => i.Percentage).ToList()
                });

            return new JsonNetResult { Data = viewModel };
        }

        public ActionResult GetChartDescription(int currentContentId, int chartIndex)
        {
            IPublishedContent currentContent = Umbraco.TypedContent(currentContentId);

            var archetypeModel = currentContent.GetProperty("archtypeLemonChart").Value as Archetype.Models.ArchetypeModel;
            var fieldSet = archetypeModel.Fieldsets.ElementAt(chartIndex);

            var data = fieldSet != null
                ? fieldSet.GetValue("descriptions") : "";

            ViewBag.Descriptions = data;

            return PartialView("chartDesc");
        }

    }
}
