using ArtGallery.ResponseModels;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ArtGallery.Domains
{
    public class ChatHub : Hub
    {
        private long _userId {  get; set; }
        private static List<long> activeUserIds { get; set; } = [];
        public async Task SendMessage(string message)
        {
            // Send message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToUser(long userId, MessageResponseModel message)
        {
            var userIdString = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await Clients.User(userId.ToString()).SendAsync("ReceiveMessage", message, userIdString);

        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (long.TryParse(userId, out long userIdLong))
            {
                _userId = userIdLong;
            }
            if (!activeUserIds.Contains(_userId))
            {
                activeUserIds.Add(_userId);
            }

            Console.WriteLine($"User connected: {_userId}");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (long.TryParse(userId, out long userIdLong))
            {
                _userId = userIdLong;
            }
            Console.WriteLine("User Disconnected!");
            activeUserIds.Remove(_userId);
            await Clients.Others.SendAsync("ReceiveActiveUserIds", activeUserIds);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task OnCutCall(string targetUserId, bool isVideoChat)
        {
            await Clients.User(targetUserId).SendAsync("OnCutCall",isVideoChat);
        } 

        public async Task SendSignal(string targetUserId, string signalData, bool isVideoChat)
        {
            try
            {
                // Ensure target user ID is valid
                if (!string.IsNullOrEmpty(targetUserId))
                {
                    await Clients.User(targetUserId).SendAsync("ReceiveSignal", Context.UserIdentifier, signalData, isVideoChat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendSignal: {ex.Message}");
            }
        }

        public async Task SendActiveUserIds()
        {
            await Clients.All.SendAsync("ReceiveActiveUserIds", activeUserIds);
        }
    }
}
