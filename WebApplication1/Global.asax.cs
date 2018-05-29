using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Emma.WeiXin.Config;
using Emma.WeiXin.Message;
using Emma.WeiXin.Token;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Emma.WeiXin.Global.Configure(new WeiXinConfig())
            .WithTokenCacheProvider(new DefaultTokenCacheProvider())
            .WithMessageHandler(new DefaultMessageHandler());
        }
    }
}
