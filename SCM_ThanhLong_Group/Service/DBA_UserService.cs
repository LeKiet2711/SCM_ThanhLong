using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;
using System.Runtime.InteropServices;

namespace SCM_ThanhLong_Group.Service
{
    public class DBA_UserService
    {
        private readonly IDBA_UserRepository _dBA_UserRepository;
        public DBA_UserService(IDBA_UserRepository dBA_UserRepository)
        {
            _dBA_UserRepository = dBA_UserRepository;
        }

        public async Task<List<DBA_User>> GetAllDataUser()
        {
            return await _dBA_UserRepository.GetAllDataUser();
        }

        public async Task<bool> UnlockAccount(string AccountName)
        {
            return await _dBA_UserRepository.UnlockAccount(AccountName);
        }

        public async Task<bool> LockAccount(string AccountName)
        {
            return await _dBA_UserRepository.LockAccount(AccountName);
        }

    }
}
