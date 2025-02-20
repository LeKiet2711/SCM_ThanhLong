using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IChucNangRepository
    {
        Task<List<ChucNang>> getAllData();
        Task updateData(ChucNang data);

    }
}
