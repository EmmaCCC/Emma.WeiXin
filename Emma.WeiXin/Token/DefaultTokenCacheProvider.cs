using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emma.WeiXin.Token
{
    public class DefaultTokenCacheProvider:ITokenCacheProvider
    {
        public void SaveToken(WeiXinToken token)
        {
            //实现token的刷新和存储
            throw new NotImplementedException();
        }

        public WeiXinToken GetToken()
        {
            return new WeiXinToken()
            {
                AccessToken = new AccessToken
                {
                    Value = "123",
                    ExpireIn = 7200
                }
            };
        }
    }
}
