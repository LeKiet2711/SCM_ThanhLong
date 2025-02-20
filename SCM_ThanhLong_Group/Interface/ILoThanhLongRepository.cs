using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface ILoThanhLongRepository
    {
        Task<List<LoThanhLong>> getAllData();
        Task<List<HoTrong>> getHoTrongData();
        Task addData(LoThanhLong loThanhLong);
        Task updateData(LoThanhLong loThanhLong);
        Task deleteData(string id);
        Task<string> getLinkQRByLoThanhLongID(int loThanhLongID);
        Task<bool> isMaLoExist(LoThanhLong loThanhLong, List<LoThanhLong> lstData, string ma);
    }
}
