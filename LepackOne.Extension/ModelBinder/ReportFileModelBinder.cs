using LepackOne.Extension.Models;
using LepackOne.Extension.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Umbraco.Core;
using Umbraco.Core.IO;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Security;

namespace LepackOne.Extension.ModelBinder
{
    public class ReportFileModelBinder : IModelBinder
    {
        protected ApplicationContext ApplicationContext { get; private set; }

        public ReportFileModelBinder(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }

        public ReportFileModelBinder()
            : this(ApplicationContext.Current)
        {
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            LogHelper.Info<ReportFileModelBinder>("Starting BindModel...");


            if (actionContext.Request.Content.IsMimeMultipartContent() == false)
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = IOHelper.MapPath("~/App_Data/TEMP/FileUploads/LeReport");
            //ensure it exists
            Directory.CreateDirectory(root);
            var provider = new CustomMultipartFormDataStreamProvider(root);

            var task = Task.Run(() => GetModelAsync(actionContext, bindingContext, provider))
                            .ContinueWith(x => 
                            {
                                if (x.IsFaulted && x.Exception != null)
                                {
                                    throw x.Exception;
                                }
                                
                                bindingContext.Model = x.Result;
                            });

            task.Wait();

            return bindingContext.Model != null;
        }

        private async Task<Report> GetModelAsync(HttpActionContext actionContext, ModelBindingContext bindingContext, MultipartFormDataStreamProvider provider)
        {
            LogHelper.Info<ReportFileModelBinder>("Starting GetModelAsync...");

            var request = actionContext.Request;
            
            //IMPORTANT!!! We need to ensure the umbraco context here because this is running in an async thread
            var httpContext = (HttpContextBase)request.Properties["MS_HttpContext"];
            UmbracoContext.EnsureContext(
                httpContext,
                ApplicationContext.Current,
                new WebSecurity(httpContext, ApplicationContext.Current));

            var content = request.Content;

            LogHelper.Info<ReportFileModelBinder>("Starting ReadAsMultipartAsync...");

            var result = await content.ReadAsMultipartAsync(provider);

            //get the string json from the request
            var reportItem = result.FormData["report"];

            //deserialize into our model
            var model = JsonConvert.DeserializeObject<Report>(reportItem);
            model.Files = new List<ReportDataFile>();

            //get the default body validator and validate the object
            var bodyValidator = actionContext.ControllerContext.Configuration.Services.GetBodyModelValidator();
            var metadataProvider = actionContext.ControllerContext.Configuration.Services.GetModelMetadataProvider();
            //all validation errors will not contain a prefix
            bodyValidator.Validate(model, typeof(ReportRecordViewModel), metadataProvider, actionContext, "");

            foreach (var file in result.FileData)
            {
                var fileName = file.Headers.ContentDisposition.FileName.Trim(new char[] { '\"' });

                model.Files.Add(new ReportDataFile
                {
                    TempFilePath = file.LocalFileName,
                    FileName = fileName
                });
            }

            return model;
        }
    }
}