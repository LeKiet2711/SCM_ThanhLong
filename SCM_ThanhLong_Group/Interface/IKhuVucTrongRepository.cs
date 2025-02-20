using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IKhuVucTrongRepository
    {
        Task<List<KhuVucTrong>> getAllData();
        KhuVucTrong getDataByID(string id);
        Task addData(KhuVucTrong khuVucTrong);
        Task updateData(KhuVucTrong khuVucTrong);
        Task deleteData(string Auto_ID);
        Task<bool> isMaKhuVucExist(KhuVucTrong khuVucTrong, List<KhuVucTrong> lstData, string ma);
        Task<bool> isTenKhuVucExist(KhuVucTrong khuVucTrong, List<KhuVucTrong> lstData, string ma);
    }
}
