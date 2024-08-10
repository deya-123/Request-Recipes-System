using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace OurRecipes.Services
{
    public class OrderHub : Hub
    {
        // Concurrent dictionary to map customer IDs to SignalR connection IDs
        private static ConcurrentDictionary<int, string> userConnections = new ConcurrentDictionary<int, string>();

        public async Task RegisterUser(int userId)
        {
            // Register or update the user connection
            userConnections[userId] = Context.ConnectionId;
        }

        public async Task ReceiveNotification(int userId, string orderId)
        {
            //// Check if the user is connected
            //if (userConnections.TryGetValue(userId, out string connectionId))
            //{
            //    // Send a notification to the specific user
            //    await Clients.All.sendMessage();
            //}
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            userConnections.TryRemove(userId, out _);
            await base.OnDisconnectedAsync(exception);
        }

      


    }
}
