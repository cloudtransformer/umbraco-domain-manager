using System.Web.Hosting;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AgeBase.DomainManager.Helpers
{
    public static class DashboardHelper
    {
        public static void AddTab(string section, string caption, string path)
        {
            var configPath = HostingEnvironment.MapPath("~/config/dashboard.config");
            if (configPath == null)
                return;

            var configXml = XDocument.Load(configPath);

            var sectionXml = configXml.XPathSelectElement(string.Format("//section [@alias='{0}']", section));
            if (sectionXml == null)
                return;

            var tabXml = sectionXml.XPathSelectElement(string.Format("//tab [@caption='{0}']", caption));
            if (tabXml != null)
                return;

            var tabToAdd = XElement.Parse(string.Format("<tab caption=\"{0}\"><control>{1}</control></tab>", caption, path));

            sectionXml.Add(tabToAdd);
            configXml.Save(configPath);
        }
    }
}