using Umbraco.Core.Services;

namespace AgeBase.DomainManager.Models
{
    public class DomainManagerDomain
    {
        public int Id { get; set; }
        public string Culture { get; set; }
        public string Name { get; set; }
        public int RootNodeId { get; set; }
        public string RootNodeName { get; set; }

        public DomainManagerDomain()
        {
        }

        public DomainManagerDomain(umbraco.cms.businesslogic.web.Domain domain, IContentService contentService)
        {
            if (domain == null || contentService == null)
                return;

            Id = domain.Id;
            Culture = domain.Language.CultureAlias;
            Name = domain.Name;
            RootNodeId = domain.RootNodeId;

            var rootNode = contentService.GetById(RootNodeId);
            if (rootNode != null)
            {
                RootNodeName = rootNode.Name;
            }
        }
    }
}