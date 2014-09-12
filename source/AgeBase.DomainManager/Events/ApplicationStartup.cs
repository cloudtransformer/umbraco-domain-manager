using System.Web.Mvc;
using System.Web.Routing;
using AgeBase.DomainManager.Helpers;
using umbraco.cms.businesslogic.packager;
using Umbraco.Core;

namespace AgeBase.DomainManager.Events
{
    public class ApplicationStartup : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RouteTable.Routes.MapRoute(
                "DomainManager",
                "App_Plugins/AgeBase.DomainManager/{action}/{id}",
                new {controller = "DomainManager", action = "Resource", id = UrlParameter.Optional});
        }

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // Install
            DashboardHelper.AddTab("StartupDashboardSection", "Domain Manager", "/App_Plugins/AgeBase.DomainManager/Index.html");

            // Uninstall
            InstalledPackage.BeforeDelete += delegate
            {
                DashboardHelper.RemoveTab("StartupDashboardSection", "Domain Manager");
            };
        }
    }
}