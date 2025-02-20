namespace SCM_ThanhLong_Group.Interface
{
    public interface ICheckEmail
    {
        Task<bool> checkEmailUser(string gmail);
        Task<bool> GrantUserAccess(string username);
        Task<string> layGmail(string username);

    }
}
