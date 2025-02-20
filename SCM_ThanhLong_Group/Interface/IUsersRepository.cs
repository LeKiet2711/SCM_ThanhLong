using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IUsersRepository
    {
        Task<bool> AuthenticateUser(string username, string password);
        Task<bool> isTheUserLocked(string userName);
        Task<bool> ChangePassword(string username, string oldPassword, string newPassword);
        Task<List<Users>> getAllData();
        Task<string> GetCurrentUserName();
        Task<string> GetCurrentPassWord();
        Task Logout();
        Task ExecuteSqlCommand(string sqlCommand);
        Task<List<TablePermission>> GetUserPermissions(string username);
        Task killSession(string userName);
        Task LoadSession();
        Task LoadSessionUser();
        Task GrantPermissionNhap(string username);
        Task GrantPermissionXuat(string username);

    }
}
