using System;
using System.IO;
using System.Net;
using System.Text;

namespace Emma.WeiXin.Common
{
    class HttpHelper
    {
        public static MessageObject PostObject<T>(string url, T obj)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "Post";
                //12321333333333333333
                string postdata = "{\"value\":\"哈哈\",\"expireIn\":\"ss\"}";
                byte[] bytes = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = bytes.Length;

                Stream sendStream = request.GetRequestStream();
                sendStream.Write(bytes, 0, bytes.Length);
                sendStream.Close();

                WebResponse response = request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    throw new NullReferenceException();
                }
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                var data = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                response.Close();

                //HttpClient client = new HttpClient();

                //HttpResponseMessage msg = client.PostAsJsonAsync<T>(url, obj).Result;
                //msg.EnsureSuccessStatusCode();
                //dynamic result = msg.Content.ReadAsAsync<MessageObject>().Result;
                //if (result.errcode != null && result.errcode != 0)
                //    throw new WeiXinException((int)result.errcode, result.errmsg);
                //return result;
                return new MessageObject();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static T GetObject<T>(string url)
        {
            try
            {
                //HttpClient client = new HttpClient();
                //HttpResponseMessage msg = client.GetAsync(url).Result;
                //msg.EnsureSuccessStatusCode();
                //msg.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //return msg.Content.ReadAsAsync<T>().Result;
                return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
