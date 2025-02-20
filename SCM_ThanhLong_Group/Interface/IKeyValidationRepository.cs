namespace SCM_ThanhLong_Group.Interface
{
    public interface IKeyValidationRepository
    {
        Task<bool> ValidateKey(string userKey);
    }
}
