namespace Emma.WeiXin.Config
{
    public class WeiXinConfig:IConfig
    {
        public string AppId
        {
            get { return "wx62e48631b2f8b7f2"; } 
        }

        public string AppSecret
        {
            get { return "c9b4754d3728d7ade2ae4dadc94e53f5"; } 
        }


        public string Token
        {
            get { return "sss"; }
        }

        public string Url
        {
            get { return "http://www.test.com/weixin/message"; }
        }


        public string EncodingAESKey
        {
            get { return "sss"; }
        }
     
    }
}
