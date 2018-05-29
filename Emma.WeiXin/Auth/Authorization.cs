using System.Web;
using Emma.WeiXin.Common;
using Emma.WeiXin.Config;
using YQH.WeiXin;
using YQH.WeiXin.JSSDK;

namespace Emma.WeiXin.Auth
{
    public class Authorization
    {
        readonly string AUTH_URL = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state=STATE#wechat_redirect";
        readonly string ACCESSTOKEN_URL = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        readonly string USERINFO_URL = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
        IConfig _config;
        public Authorization(IConfig config)
        {
            _config = config;
        }

        public MessageObject Auth(string url, AuthType type)
        {
            if (HttpContext.Current.Request.QueryString["code"] == null)
            {
                HttpContext.Current.Response.Redirect(string.Format(AUTH_URL, _config.AppId, System.Web.HttpUtility.UrlEncode(url), type.ToString()));
                HttpContext.Current.Response.End();
                return null;
            }
            return HttpHelper.GetObject<MessageObject>(string.Format(ACCESSTOKEN_URL, _config.AppId, _config.AppSecret, HttpContext.Current.Request.QueryString["code"]));
        }

        public MessageObject GetUserInfo(string accesstoken, string openid)
        {
            return HttpHelper.GetObject<MessageObject>(string.Format(USERINFO_URL, accesstoken, openid));
        }
    }
}
