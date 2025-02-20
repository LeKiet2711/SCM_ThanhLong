using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IHoTrongRepository
    {
        Task<List<HoTrong>> getAllData();
        Task<List<KhuVucTrong>> getKhuVucTrongData();
        HoTrong getDataByID(string id);
        Task addData(HoTrong hoTrong);
        Task updateData(HoTrong hoTrong);
        Task deleteData(string id);
        Task<bool> isMaHoTrongExist(HoTrong hoTrong, List<HoTrong> lstData, string ma);
        Task<bool> isTenHoTrongExist(HoTrong hoTrong, List<HoTrong> lstData, string ma);
    }
}
