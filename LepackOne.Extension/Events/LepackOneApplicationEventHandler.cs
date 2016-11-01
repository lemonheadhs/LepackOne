using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.XmlTransform;

using Umbraco.Core;
using Umbraco.Core.Logging;
using System.Configuration;
using Umbraco.Core.Persistence;
using LepackOne.Extension.Models;
using Umbraco.Core.Models.Mapping;
using AutoMapper;
using LepackOne.Extension.Mapping;

namespace LepackOne.Extension.Events
{
    public class LepackOneApplicationEventHandler : ApplicationEventHandler, IMapperConfiguration
    {
        public const string filePath = "~/config/Dashboard.config";
        public const string xdtPth = "~/App_plugins/LeReport/Dashboard.config.install.xdt";


        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // ensure dashboard.config to contain our configuration
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["LeReport"]))
            {
                transform(filePath, xdtPth);

                var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                config.AppSettings.Settings.Add("LeReport", "on");
                config.Save();
            }

            EnsureCustomTablesRegistered(applicationContext.DatabaseContext);

        }

        private void EnsureCustomTablesRegistered(DatabaseContext dbContext)
        {
            var db = dbContext.Database;
            var sqlSyntax = dbContext.SqlSyntax;
            var helper = new DatabaseSchemaHelper(db, LoggerResolver.Current.Logger, sqlSyntax);

            if (!helper.TableExist("LeRep_Attainment"))
            {
                helper.CreateTable<Attainment>();
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

        public void ConfigureMappings(IConfiguration config, ApplicationContext applicationContext)
        {
            MappingConfiguration.Initialize(config, applicationContext);
        }
    }
}