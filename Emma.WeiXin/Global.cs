using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Emma.WeiXin.Config;
using Emma.WeiXin.Message;
using Emma.WeiXin.Token;

namespace Emma.WeiXin
{
    public class Global
    {
        private IConfig _config;

        private ITokenCacheProvider _tokenProvider = new DefaultTokenCacheProvider();

        private IMessageHandler _messageHandler = new DefaultMessageHandler();

        private static Global _g;

        private Global()
        {

        }

        public static Global Configure(IConfig config)
        {
            if (_g == null)
            {
                _g = new Global();
                _g._config = config;
            }
            return _g;
        }

        public static Global Current
        {
            get
            {
                if (_g == null)
                {
                    throw new Exception("weixin is not initialize");
                }
                return _g;
            }

        }

        public bool CheckSignature(string signature, string timestamp, string nonce)
        {
            List<string> list = new List<string>();
            list.Add(_config.Token);
            list.Add(timestamp);
            list.Add(nonce);
            //排序
            list.Sort();
            //拼串
            string input = string.Empty;
            foreach (var item in list)
            {
                input += item;
            }
            //加密
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            string sign = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(input)));
            sign = sign.Replace("-", "").ToLower();

            return sign == signature;
        }

        public Global WithTokenCacheProvider(ITokenCacheProvider tokenProvider)
        {
            this._tokenProvider = tokenProvider;
            return this;
        }

        public Global WithMessageHandler(IMessageHandler messageHandler)
        {
            this._messageHandler = messageHandler;
            return this;
        }

        public AccessToken GetAccessToken()
        {
            try
            {
                AccessToken accessToken = null;
                WeiXinToken token = _tokenProvider.GetToken();
                if (token.AccessToken != null)
                {
                    accessToken = token.AccessToken;
                    return accessToken;
                }

                string url = string.Format(WeiXinConst.ACCESS_TOKNE_URL, _config.AppId, _config.AppSecret);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                if (response.GetResponseStream() != null)
                {
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    string json = sr.ReadToEnd();
                    JavaScriptSerializer serlizer = new JavaScriptSerializer();
                    var obj = serlizer.Deserialize<dynamic>(json);
                    accessToken.Value = obj.access_token;
                    accessToken.ExpireIn = obj.expires_in;
                    token.AccessToken = accessToken;
                    _tokenProvider.SaveToken(token);
                }
                return accessToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsTicket GetJsTicket()
        {
            try
            {
                JsTicket ticket = null;
                WeiXinToken token = _tokenProvider.GetToken();
                if (token.JsTicket != null)
                {
                    ticket = token.JsTicket;
                    return ticket;
                }

                AccessToken accessToken = this.GetAccessToken();


                string url = string.Format(WeiXinConst.JS_TICKET_URL, accessToken.Value);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                if (response.GetResponseStream() != null)
                {
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    string json = sr.ReadToEnd();
               
                    JavaScriptSerializer serlizer = new JavaScriptSerializer();
                    var obj = serlizer.Deserialize<dynamic>(json);
                    ticket.Value = obj.ticket;
                    ticket.ExpireIn = obj.expires_in;
                    token.JsTicket = ticket;
                    _tokenProvider.SaveToken(token);
                }
                return ticket;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsSdkParam GetJsSdkParams(string url)
        {
            JsSdkParam jssdk = new JsSdkParam();
            string param = "jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}";
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            jssdk.Timestamp = Convert.ToInt64(ts.TotalSeconds);
            jssdk.AppId = _config.AppId;
            jssdk.NonceStr = Guid.NewGuid().ToString().Substring(0, 8);

            param = string.Format(param, this.GetJsTicket().Value, jssdk.NonceStr, jssdk.Timestamp, url);

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            string sign = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(param)));
            sign = sign.Replace("-", "").ToLower();

            jssdk.Signature = sign;


            return jssdk;
        }

        public void HandleMessage(HttpContext context)
        {
            _messageHandler.HandleMessage(context);
        }
    }
}
