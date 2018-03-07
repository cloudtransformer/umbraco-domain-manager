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
            {
                return;
            }

            var configXml = XDocument.Load(configPath);

            var tabs = configXml.XPathSelectElements("//section/tab");
            var enumerable = tabs.ToList();

            if (enumerable.Any())
            {
                foreach (var tab in enumerable)
                {
                    var control = tab.XPathSelectElement("control");
                    if (control == null)
                    {
                        continue;
                    }

                    if (control.Value.Equals(path))
                    {
                        return;
                    }
                }
            }

            var sectionXml = configXml.XPathSelectElement($"//section [@alias='{section}']");
            if (sectionXml == null)
            {
                return;
            }

            var tabXml = sectionXml.XPathSelectElement($"//tab [@caption='{caption}']");
            if (tabXml != null)
            {
                return;
            }

            var tabToAdd = XElement.Parse($"<tab caption=\"{caption}\"><control>{path}</control></tab>");

            sectionXml.Add(tabToAdd);
            configXml.Save(configPath);
        }

        internal static void RemoveTab(string section, string caption)
        {
            var configPath = HostingEnvironment.MapPath("~/config/dashboard.config");
            if (configPath == null)
            {
                return;
            }

            var configXml = XDocument.Load(configPath);

            var sectionXml = configXml.XPathSelectElement($"//section [@alias='{section}']");

            var tabXml = sectionXml?.XPathSelectElement($"//tab [@caption='{caption}']");
            if (tabXml == null)
            {
                return;
            }

            tabXml.Remove();
            configXml.Save(configPath);
        }
    }
}