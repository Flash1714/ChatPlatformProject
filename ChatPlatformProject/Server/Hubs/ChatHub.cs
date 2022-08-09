using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformProject.Server.Hubs
{
    public class ChatHub : Hub
    {
        //When user connects (made second becauses it uses the first function)

        public override async Task OnConnectedAsync()
        {
            // Uses the function below to acknowledge new person joining in.
            await SendMessage("", "New User Connected!"); 
            await base.OnConnectedAsync(); //call base ????
        }
        

        //For sending messages  (Made first)
        private async Task SendMessage(string user, string message)
        {
            // Sends the Hub update to all clients (users). "RecieveMessage" is an identifier.
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }

        // Satarting to make the chat hub program : after this we add service and response compression in program.cs
    }
}
