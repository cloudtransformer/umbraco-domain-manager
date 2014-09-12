using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AgeBase.DomainManager.Models;
using umbraco.cms.businesslogic.web;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace AgeBase.DomainManager.Controllers
{
    [PluginController("AgeBaseDomainManager")]
    public class DomainManagerApiController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public IEnumerable<DomainManagerDomain> List()
        {
            var contentService = ApplicationContext.Services.ContentService;
            return Domain.GetDomains(true).OrderBy(d => d.Name).Select(d => new DomainManagerDomain(d, contentService));
        }
    }
}