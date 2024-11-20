using Microsoft.AspNetCore.SignalR;

namespace SCM_ThanhLong_Group.Service
{
    public class LogoutHub : Hub
    {
        public async Task SendLogoutNotification(string userId)
        {
            await Clients.User(userId).SendAsync("Logout");
        }
    }
}
