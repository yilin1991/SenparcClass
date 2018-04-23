using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Senparc.Weixin.Helpers.Extensions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AppStore;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
namespace SenparcClass.Service
{
    public class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        public CustomMessageHandler(Stream inputStream, PostModel postModel = null, int maxRecordCount = 0, DeveloperInfo developerInfo = null) : base(inputStream, postModel, maxRecordCount, developerInfo)
        {
        }

        public CustomMessageHandler(XDocument requestDocument, PostModel postModel = null, int maxRecordCount = 0, DeveloperInfo developerInfo = null) : base(requestDocument, postModel, maxRecordCount, developerInfo)
        {
        }

        public CustomMessageHandler(RequestMessageBase requestMessageBase, PostModel postModel = null, int maxRecordCount = 0, DeveloperInfo developerInfo = null) : base(requestMessageBase, postModel, maxRecordCount, developerInfo)
        {
        }

        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {


            if (requestMessage.EventKey == "123")
            {
                var responseMessage = this.CreateResponseMessage<ResponseMessageNews>();
                var news = new Article()
                {
                    Title = "您点击了按钮:" + requestMessage.EventKey,
                    Description = "这是第一行\r\n这里是第二行",
                    PicUrl = "http://sdk.weixin.senparc.com/images/book-cover-front-small-3d-transparent.png",
                    Url = "http://www.badu.com"

                };
                responseMessage.Articles.Add(news);
                return responseMessage;
            }
            if (requestMessage.EventKey == "A")
            {
                var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
                responseMessage.Content = "";
                return responseMessage;
            }
            if (requestMessage.EventKey == "B")
            {
                return new ResponseMessageNoResponse();
            }

            else
            {
                var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
                responseMessage.Content = "你点击了:" + requestMessage.EventKey;
                return responseMessage;
            }


        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "你输入了文字:" + requestMessage.Content;
            return responseMessage;
        }

        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送了位置信息：lat-{0},lon-{1}".FormatWith(requestMessage.Location_X, requestMessage.Location_Y);
            return responseMessage;
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "当前服务器时间：" + DateTime.Now;
            return responseMessage;

        }
    }
}
