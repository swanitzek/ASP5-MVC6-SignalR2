using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ChatSample.Hubs
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendText(string nickName, string text)
        {
            Clients.All.ReceiveText(nickName, text);
        }
    }
}