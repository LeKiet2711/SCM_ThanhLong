using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IKhoUserRepository
    {
        Task<List<KhoUser>> getAllData(int khoId);
        Task<List<string>> GetUsersByKhoId(int khoId);
        Task AddUserToKho(int khoId, string userId);
        Task DeleteUserFromKho(int khoId, string userId);
    }
}
