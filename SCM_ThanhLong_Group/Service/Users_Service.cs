
using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;
using System.Data.Common;
using Blazored.SessionStorage;
using Telerik.SvgIcons;
using SCM_ThanhLong_Group.Service;

namespace SCM_ThanhLong_Group.Service
{
    public class Users_Service
    {
        private readonly IUsersRepository _usersRepository;
        public string currentUserName { get; set; }
        public string currentPassWord { get; set; }
        public Users_Service(IUsersRepository usersRepository)
        {
            _usersRepository=usersRepository;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            return await _usersRepository.AuthenticateUser(username, password);
        }

        public async Task<bool> isTheUserLocked(string userName)
        {
            return await _usersRepository.isTheUserLocked(userName);
        }

        public async Task<bool> ChangePassword(string username, string oldPassword, string newPassword)
        {
            return await _usersRepository.ChangePassword(username, oldPassword, newPassword);
        }

        public async Task<List<Users>> getAllData()
        {
            return await _usersRepository.getAllData();
        }

        public async Task<string> GetCurrentUserName()
        {
            return await _usersRepository.GetCurrentUserName();
        }

        public async Task<string> GetCurrentPassWord()
        {
            return await _usersRepository.GetCurrentPassWord();
        }

        public async Task Logout()
        {
            await _usersRepository.Logout();
        }


        public async Task ExecuteSqlCommand(string sqlCommand)
        {
            await _usersRepository.ExecuteSqlCommand(sqlCommand);
        }

        public async Task<List<TablePermission>> GetUserPermissions(string username)
        {
            return await _usersRepository.GetUserPermissions(username);
        }

        public async Task killSession(string userName)
        {
            await _usersRepository.killSession(userName);
        }
        public async Task LoadSession()
        {
            await _usersRepository.LoadSession();
        }

        public async Task LoadSessionUser()
        {
            await _usersRepository.LoadSessionUser();
        }

        public async Task GrantPermissionNhap(string username)
        {
            await _usersRepository.GrantPermissionNhap(username);
        }

        public async Task GrantPermissionXuat(string username)
        {
            await _usersRepository.GrantPermissionXuat(username);
        }

    }
}
