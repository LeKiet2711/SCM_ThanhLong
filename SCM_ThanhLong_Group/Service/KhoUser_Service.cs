using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class KhoUser_Service
    {
        private readonly IKhoUserRepository _khoUserRepository;
        public KhoUser_Service(IKhoUserRepository khoUserRepository)
        {
            _khoUserRepository = khoUserRepository;
        }

        public async Task<List<KhoUser>> getAllData(int khoId)
        {
            return await _khoUserRepository.getAllData(khoId);
        }

        public async Task<List<string>> GetUsersByKhoId(int khoId)
        {
            return await _khoUserRepository.GetUsersByKhoId(khoId);
        }

        public async Task AddUserToKho(int khoId, string userId)
        {
            await _khoUserRepository.AddUserToKho(khoId, userId);
        }

        public async Task DeleteUserFromKho(int khoId, string userId)
        {
            await _khoUserRepository.DeleteUserFromKho(khoId, userId);
        }
    }
}
