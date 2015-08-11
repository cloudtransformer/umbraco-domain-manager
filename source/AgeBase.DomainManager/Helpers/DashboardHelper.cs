using System.Linq;
using System.Web.Hosting;
using System.Xml.Linq;
using System.Xml.XPath;

namespace AgeBase.DomainManager.Helpers
{
    public static class DashboardHelper
    {
        internal static void EnsureTab(string section, string caption, string path)
        {
            var configPath = HostingEnvironment.MapPath("~/config/dashboard.config");
            if (configPath == null)
                return;

            var configXml = XDocument.Load(configPath);

            // Loop through each tab to see if the Domain
            // Manager tab exists in any section

            var tabs = configXml.XPathSelectElements("//section/tab");
            if (tabs.Any())
            {
                foreach (var tab in tabs)
                {
                    var control = tab.XPathSelectElement("control");
                    if (control == null) 
                        continue;

                    // If it exists, there is no need to carry
                    // on searching through other tabs

                    if (control.Value.Equals(path))
                        return;
                }
            }

            // If we have got this far, we know that the Domain
            // Mananger tab does not exist in any section.
            // So let's add it

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

        internal static void RemoveTab(string section, string caption)
        {
            var configPath = HostingEnvironment.MapPath("~/config/dashboard.config");
            if (configPath == null)
                return;

            var configXml = XDocument.Load(configPath);

            var sectionXml = configXml.XPathSelectElement(string.Format("//section [@alias='{0}']", section));
            if (sectionXml == null)
                return;

            var tabXml = sectionXml.XPathSelectElement(string.Format("//tab [@caption='{0}']", caption));
            if (tabXml == null)
                return;

            tabXml.Remove();
            configXml.Save(configPath);
        }
    }
}