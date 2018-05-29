using System;

namespace Emma.WeiXin.Common
{
    public class WeiXinException : Exception
    {
        public int ErrorCode { get; set; }

        public WeiXinException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }


    }
}
