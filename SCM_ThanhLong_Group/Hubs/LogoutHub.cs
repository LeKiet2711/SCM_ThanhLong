using Microsoft.AspNetCore.SignalR;

namespace SCM_ThanhLong_Group
{
    public class LogoutHub : Hub
    {
        public async Task BroadcastLogout(string username)
        {
            await Clients.Group(username).SendAsync("Logout");
        }

        public override async Task OnConnectedAsync()
        {
            var username = Context.User.Identity.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var username = Context.User.Identity.Name;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, username);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
