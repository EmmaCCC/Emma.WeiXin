using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emma.WeiXin.Config
{
    public interface IConfig
    {
        string AppId { get; }

        string AppSecret { get;  }

        string Token { get; }

        string Url { get; }

        string EncodingAESKey { get;  }
    }
}
