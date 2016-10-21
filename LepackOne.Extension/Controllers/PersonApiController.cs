using LepackOne.Extension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Persistence;
using Umbraco.Web.Editors;

namespace LepackOne.Extension.Controllers
{
    [Umbraco.Web.Mvc.PluginController("My")]
    public class PersonApiController : UmbracoAuthorizedJsonController
    {
        protected Database Database
        {
            get { return UmbracoContext.Application.DatabaseContext.Database; }
        }

        public IEnumerable<Person> GetAll()
        {
            var query = new Sql().Select("*").From("people");

            return Database.Fetch<Person>(query);
        }
    }
}