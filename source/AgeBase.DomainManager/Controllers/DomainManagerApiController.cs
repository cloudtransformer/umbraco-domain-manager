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
        public IEnumerable<DomainManagerDomain> Get()
        {
            var contentService = ApplicationContext.Services.ContentService;
            return Domain.GetDomains(true).OrderBy(d => d.Name).Select(d => new DomainManagerDomain(d, contentService));
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var domain = Domain.GetDomains(true).FirstOrDefault(d => d.Id.Equals(id));
            if (domain == null) 
                return false;

            domain.Delete();
            return true;
        }
    }
}