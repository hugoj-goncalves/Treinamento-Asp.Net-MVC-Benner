using System;

namespace TreinamentoBenner.Hub
{
    public class ChatHub : Microsoft.AspNet.SignalR.Hub
    {
        public void Send(string name, string message)
        {
            var date = DateTime.Now.ToString("T");
            Clients.All.broadcastMessage(name, message, date);
        }
    }
}