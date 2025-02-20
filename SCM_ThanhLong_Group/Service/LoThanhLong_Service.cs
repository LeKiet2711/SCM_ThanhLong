using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Interface;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class LoThanhLong_Service
    {

        private readonly ILoThanhLongRepository _loThanhLongRepository;

        public LoThanhLong_Service(ILoThanhLongRepository loThanhLongRepository)
        {
            _loThanhLongRepository = loThanhLongRepository;
        }

        public Task<List<LoThanhLong>> getAllData()
        {
            return _loThanhLongRepository.getAllData();
        }

        public Task<List<HoTrong>> getHoTrongData()
        {
            return _loThanhLongRepository.getHoTrongData();
        }

        public Task addData(LoThanhLong loThanhLong)
        {
            return _loThanhLongRepository.addData(loThanhLong);
        }

        public Task updateData(LoThanhLong loThanhLong)
        {
            return _loThanhLongRepository.updateData(loThanhLong);
        }

        public Task deleteData(string id)
        {
            return _loThanhLongRepository.deleteData(id);
        }

        public Task<string> getLinkQRByLoThanhLongID(int loThanhLongID)
        {
            return _loThanhLongRepository.getLinkQRByLoThanhLongID(loThanhLongID);
        }

        public Task<bool> isMaLoExist(LoThanhLong loThanhLong, List<LoThanhLong> lstData, string ma)
        {
            return _loThanhLongRepository.isMaLoExist(loThanhLong, lstData, ma);
        }
    }
}
