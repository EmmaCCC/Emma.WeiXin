using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emma.WeiXin.Token
{
    public interface ITokenCacheProvider
    {
        void SaveToken(WeiXinToken token);

        WeiXinToken GetToken();
    }
}
