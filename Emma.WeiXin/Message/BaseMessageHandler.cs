using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Emma.WeiXin.Common;

namespace Emma.WeiXin.Message
{
    public abstract class BaseMessageHandler : IMessageHandler
    {
        protected XmlDocument doc = new XmlDocument();
        private XmlDocument BuildXml(dynamic msg)
        {
            var xml = doc.CreateElement("xml");
            doc.AppendChild(xml);
            var toUserName = doc.CreateElement("FromUserName");
            var cdata = doc.CreateCDataSection(msg.ToUserName);
            toUserName.AppendChild(cdata);
            xml.AppendChild(toUserName);

            var fromUserName = doc.CreateElement("ToUserName");
            cdata = doc.CreateCDataSection(msg.FromUserName);
            fromUserName.AppendChild(cdata);
            xml.AppendChild(fromUserName);

            var createTime = doc.CreateElement("CreateTime");
            createTime.InnerText = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            xml.AppendChild(createTime);

            var msgType = doc.CreateElement("MsgType");
            cdata = doc.CreateCDataSection(msg.MsgType);
            msgType.AppendChild(cdata);
            xml.AppendChild(msgType);

            return doc;
        }
        public void HandleMessage(HttpContext context)
        {
            dynamic msg = new MessageObject();

            XmlDocument doc = new XmlDocument();
            doc.Load(context.Request.InputStream);
            XmlElement rootElement = doc.DocumentElement;
            msg.FromUserName = rootElement.SelectSingleNode("MsgType").InnerText;
            msg.ToUserName = rootElement.SelectSingleNode("ToUserName ").InnerText;

            var msgType = rootElement.SelectSingleNode("MsgType").InnerText;
            msg.MsgType = msgType;

            this.BuildXml(msg);

            switch (msgType)
            {
                case "text":
                    {
                        msg.Content = rootElement.SelectSingleNode("Content").InnerText;
                        msg.MsgId = Convert.ToInt64(rootElement.SelectSingleNode("MsgId").InnerText);
                        this.OnTextMessage(msg);
                        break;
                    }
                case "image":
                    {
                        msg.Content = rootElement.SelectSingleNode("Content").InnerText;
                        msg.MsgId = Convert.ToInt64(rootElement.SelectSingleNode("MsgId").InnerText);
                        this.OnImageMessage(msg);
                        break;
                    }
            }
            context.Response.Write(doc.InnerXml);
        }

        private void OnImageMessage(dynamic msg)
        {
            var xml = doc.FirstChild;

            var image = doc.CreateElement("Image");
            xml.AppendChild(image);

            var mediaId = doc.CreateElement("MediaId");
            var cdata = doc.CreateCDataSection(msg.MediaId);
            mediaId.AppendChild(cdata);
            image.AppendChild(mediaId);
         
        }

        protected virtual void OnTextMessage(dynamic msg)
        {
            if (msg.Content == "你好")
            {
                msg.Content = "hello";
            }
            var xml = doc.FirstChild;
            var content = doc.CreateElement("Content");
            var cdata = doc.CreateCDataSection(msg.Content);
            content.AppendChild(cdata);
            xml.AppendChild(content);

        }
    }
}
