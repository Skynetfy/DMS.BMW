using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DMS.Entities.SQLEntites;
using DMS.Services;
using Microsoft.AspNet.SignalR;

namespace DMS.WebUI.Models
{
    [Authorize]
    public class ChatSqlHub : Hub
    {
        public void SendChatMessage(string who, string message)
        {
            var username = Context.User.Identity.Name;
            var user = ChatService.GetChatUser(who);
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
                    foreach (var connection in connections)
                    {
                        Clients.Client(connection.ConnectionId)
                            .addChatMessage(username + ": " + message);
                    }
                }
            }
        }

        public override Task OnConnected()
        {
            var username = Context.User.Identity.Name;
            var user = ChatService.GetChatUser(username);
            if (user == null)
            {
                var _user = new ChatUser()
                {
                    UserName = username,
                    ConnectionIds = ""
                };
                var user_ = ChatService.AddChatUser(_user);
                var connection=new ChatConnection()
                {
                    Uid = user_.Id,
                    ConnectionId = Context.ConnectionId,
                    UserAgent = Context.Request.Headers["User-Agent"],
                    Connected = true
                };
                ChatService.AddChatConnection(connection);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var username = Context.User.Identity.Name;
            var user = ChatService.GetChatUser(username);
            if (user != null)
            {
                var connectionid = Context.ConnectionId;
                ChatService.UpdateConnectioned(user.Id, connectionid, false);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}