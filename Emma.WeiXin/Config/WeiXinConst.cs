using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emma.WeiXin.Config
{
    public class WeiXinConst
    {
        public const string ACCESS_TOKNE_URL =
             "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        public const string JS_TICKET_URL =
            "  https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";

    }
}
