using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DMS.Entities.SQLEntites;
using DMS.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DMS.WebApp.Models
{
    [HubName("messagehub")]
    public class MessageHub : Hub
    {
        public void SendMessage(int toUid, string message, string type)
        {
            var username = Context.User.Identity.Name;

            var user = UserService.GetUserById(toUid);
            if (user == null)
            {
                Clients.Caller.showErrorMessage("Could not find that user.");
            }
            else
            {
                var connections = ChatService.GetChatConnectionByUid(user.Id);
                if (connections == null || connections.Count <= 0)
                {
                    Clients.Caller.showErrorMessage("The user is no longer connected.");
                }
                else
                {
                    var usermessage = new UserMessage()
                    {
                        ToUid = toUid,
                        Type = (MessageTypeEnum)Enum.Parse(typeof(MessageTypeEnum), type),
                        Content = message,
                        Uid = user.Id,
                        Status = 0,
                    };
                    UserMessageService.AddUserMessage(usermessage);
                    var list = UserMessageService.GetUserMessages(toUid, 0, MessageTypeEnum.提醒消息);
                    foreach (var connection in connections)
                    {
                        Clients.Client(connection.ConnectionId)
                            .pushMessage(username, type, message, DateTime.Now, list.Count);
                    }
                }
            }
        }

        public override Task OnConnected()
        {
            var username = Context.User.Identity.Name;
            var user = UserService.GetUser(username);
            if (user == null)
            {
                var connection = new ChatConnection()
                {
                    Uid = user.Id,
                    ConnectionId = Context.ConnectionId,
                    UserAgent = Context.Request.Headers["User-Agent"],
                    Connected = true
                };
                ChatService.AddChatConnection(connection);
            }
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            //待处理
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var username = Context.User.Identity.Name;
            var user = UserService.GetUser(username);
            if (user != null)
            {
                var connectionid = Context.ConnectionId;
                ChatService.UpdateConnectioned(user.Id, connectionid, false);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}