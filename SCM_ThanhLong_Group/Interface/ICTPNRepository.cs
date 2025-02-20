using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface ICTPNRepository
    {
        Task<List<ChiTietPhieuNhap>> getAllData(int sophieunhap);
        Task<int> addData(ChiTietPhieuNhap chitietPhieuNhap);
        Task deleteData(int machitiet);
        
    }
}
