using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Emma.WeiXin;
using Emma.WeiXin.Common;
using Emma.WeiXin.Config;
using Emma.WeiXin.Token;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(AccessToken token)
        {
            // //Global.FromConfig(new WeiXinConfig());
            //string json = "{\"access_token\":\"13123\",\"expire_in\":7200}";
            //JavaScriptSerializer serlizer = new JavaScriptSerializer();
            //var obj = serlizer.Deserialize<dynamic>(json);
            return Json(token, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}