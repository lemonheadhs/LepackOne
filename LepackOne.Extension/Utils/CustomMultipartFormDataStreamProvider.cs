using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LepackOne.Extension.Utils
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var randomName = base.GetLocalFileName(headers);
            var originalName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            return randomName + "_" + originalName;
        }
    }
}
