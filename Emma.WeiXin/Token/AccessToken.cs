using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emma.WeiXin.Token
{
    public class AccessToken
    {
        public string Value { get; set; }

        public int ExpireIn { get; set; }
    }
}
