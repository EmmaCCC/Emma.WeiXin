using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emma.WeiXin.Token
{
    public class JsSdkParam
    {
        public string AppId { get; set; }

        public long Timestamp { get; set; }

        public string NonceStr { get; set; }

        public string Signature { get; set; }
    }
}
