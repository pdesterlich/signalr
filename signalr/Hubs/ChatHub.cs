using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace signalr.Hubs
{
    public class ChatHub: Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }

        public Task SendGroup(string groupName, string message)
        {
            return Clients.Group(groupName).InvokeAsync("Send", message);
        }

        public Task Register(string groupName, string key)
        {
            // sostituire con controllo effettivo groupname / key
            if (key != "1234")
                Clients.User(Context.ConnectionId).InvokeAsync("Send", "non autorizzato");
            
            Groups.AddAsync(Context.ConnectionId, groupName);
            return Clients.Group(groupName).InvokeAsync("Send", $"{groupName}: registered");
        }
    }
}