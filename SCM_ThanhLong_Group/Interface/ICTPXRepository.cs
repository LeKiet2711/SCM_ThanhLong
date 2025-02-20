

using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface ICTPXRepository
    {
        Task<List<ChiTietPhieuXuat>> getAllData(int sophieuXuat);
        Task<int> addData(ChiTietPhieuXuat chitietPhieuXuat);
        Task deleteData(int machitiet);

    }
}
