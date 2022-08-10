using Microsoft.AspNetCore.SignalR;

namespace ChatPlatformProject.Server.Hubs
{
    public class ChatHub : Hub
    {
        //making a hashmap for storing username and connection id 

        private static Dictionary<string, string> Users = new Dictionary<string, string>();
        
        
        //When user connects (made second becauses it uses the first function)

        public override async Task OnConnectedAsync()
        {
            // Uses the function below to acknowledge new person joining in.
            
            //Requesting username 
            string username = Context.GetHttpContext().Request.Query["username"];
            // Adding username to hashmap along with connection id
            Users.Add(Context.ConnectionId,username);  


            await AddMessageToChat(String.Empty, $"{username} has joined the chat!"); 
            await base.OnConnectedAsync(); //call base ????
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string username = Users.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
            await AddMessageToChat(String.Empty, $"{username} left :(");
            
        }

        //For sending messages  (Made first)
        public async Task AddMessageToChat(string user, string message)
        {
            // Sends the Hub update to all clients (users). "RecieveMessage" is an identifier.
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }

        // Satarting to make the chat hub program : after this we add service and response compression in program.cs

        //We can use connection id to identify a user hence make usernames possible
    }
}
