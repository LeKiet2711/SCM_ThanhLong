using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IKhoRepository
    {
        Task<List<Kho>> getAllData();
        Task<List<Users>> getAllData2();
        Kho getDataById(string id);
        Task addData(Kho kho);
        Task updateData(Kho kho);
        Task deleteData(string autoId);
        Task<bool> isMaKhoExist(Kho kho, List<Kho> lstData, string ma);
        Task<bool> isTenKhoExist(Kho kho, List<Kho> lstData, string ma);
    }
}
