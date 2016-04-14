﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DMS.WebUI.Models
{
    //[Authorize]
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public void SendChatMessage(string who, string message)
        {
            //string name = Context.User.Identity.Name;
            var queryString = Context.Request.QueryString;
            string name = queryString["un"];
            foreach (var connectionId in _connections.GetConnections(who))
            {
                Clients.Client(connectionId).addChatMessage(name + ": " + message);
            }
        }

        public void SendChatGroupMessage(string who, string message)
        {
            //string name = Context.User.Identity.Name;
            var queryString = Context.Request.QueryString;
            string name = queryString["un"];
            Clients.Group(who).addChatMessage(name + ": " + message);
        }

        public override Task OnConnected()
        {
            //string name = Context.User.Identity.Name;
            var queryString = Context.Request.QueryString;
            string name = queryString["un"];
            _connections.Add(name, Context.ConnectionId);
            //Groups.Add(Context.ConnectionId, name);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //string name = Context.User.Identity.Name;
            var queryString = Context.Request.QueryString;
            string name = queryString["un"];
            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            //string name = Context.User.Identity.Name;
            var queryString = Context.Request.QueryString;
            string name = queryString["un"];
            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}