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
    public class WeiXinController : Controller
    {
        [HttpGet]
        public ActionResult Message(string signature, string echostr, string timestamp, string nonce)
        {
            if (Global.Current.CheckSignature(signature, timestamp, nonce))
            {
                return Content(nonce);

            }
            return Content("");
        }


        [HttpPost]
        public ActionResult Message()
        {
            Global.Current.HandleMessage(System.Web.HttpContext.Current);
            return null;
        }

    }
}