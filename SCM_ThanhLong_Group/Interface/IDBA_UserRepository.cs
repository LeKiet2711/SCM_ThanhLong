using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IDBA_UserRepository
    {
        Task<List<DBA_User>> GetAllDataUser();
        Task<bool> UnlockAccount(string AccountName);
        Task<bool> LockAccount(string AccountName);

    }
}
