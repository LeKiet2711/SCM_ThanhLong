using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace SCM_ThanhLong_Group.Service
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_anonymous));
        }

        public Task MarkUserAsAuthenticated(string username)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, username)
        }, "apiauth"));

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));

            return Task.CompletedTask;
        }

        public Task MarkUserAsLoggedOut()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
            return Task.CompletedTask;
        }
    }

}
