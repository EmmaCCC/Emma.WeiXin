using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Emma.WeiXin.Message
{
    public interface IMessageHandler
    {
        void HandleMessage(HttpContext context);
    }
}
