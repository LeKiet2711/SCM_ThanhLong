using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IPhieuNhapRepository
    {
        Task<List<PhieuNhap>> getAllData(int khoId, string userId);
        Task<List<HoTrong>> getHoTrongData();
        Task<List<Kho>> getKhoData();
        PhieuNhap getDataByID(int id);
        Task<List<LoThanhLong>> getLoThanhLongData();
        Task<int> addData(PhieuNhap phieuNhap);
        Task updateData(PhieuNhap phieuNhap);
        Task<decimal> tinhTongTienPhieuNhap(int phieuNhapId);
        Task deleteData(string id);
        Task<bool> isSoPhieuNhapExist(PhieuNhap phieuNhap, List<PhieuNhap> lstData, string ma);
        Task<string> getSoPhieuNhapById(int nhapKhoId);
        Task<List<PhieuNhap>> getAllPhieuNhapForException();
        Task updateTrangThai(string autoId, int trangThai);
    }
}
