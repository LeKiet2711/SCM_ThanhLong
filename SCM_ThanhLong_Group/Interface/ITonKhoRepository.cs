using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface ITonKhoRepository
    {
        Task<List<TonKho>> BaoCaoTonKho(DateTime ngayNhap, DateTime ngayXuat);
    }
}
