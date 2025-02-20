using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IPhieuXuatRepository
    {
        Task<List<PhieuXuat>> getAllData(int khoId, string userId);
        Task<List<HoTrong>> getHoTrongData();
        Task<List<Kho>> getKhoData();
        Task<List<LoThanhLong>> getLoThanhLongData();
        Task<int> addData(PhieuXuat phieuXuat);
        Task updateData(PhieuXuat phieuXuat);
        Task deleteData(int autoID);
        Task<bool> isSoPhieuXuatExist(PhieuXuat phieuXuat, List<PhieuXuat> lstData, string ma);
        Task<string> getSoPhieuXuatByID(int xuatkhoid);
        Task<double> getSoKgByID(int xuatKhoId);
        Task<List<PhieuXuat>> getAllPhieuXuatForException();
        Task updateTrangThai(string autoId, int trangThai);

    }
}
