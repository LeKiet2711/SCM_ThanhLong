using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Interface;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class HoTrong_Service
    {
        private readonly IHoTrongRepository _hoTrongRepository;

        public HoTrong_Service(IHoTrongRepository hoTrongRepository)
        {
            _hoTrongRepository = hoTrongRepository;
        }

        public Task<List<HoTrong>> getAllData()
        {
            return _hoTrongRepository.getAllData();
        }

        public Task<List<KhuVucTrong>> getKhuVucTrongData()
        {
            return _hoTrongRepository.getKhuVucTrongData();
        }

        public HoTrong getDataByID(string id)
        {
            return _hoTrongRepository.getDataByID(id);
        }

        public Task addData(HoTrong hoTrong)
        {
            return _hoTrongRepository.addData(hoTrong);
        }

        public Task updateData(HoTrong hoTrong)
        {
            return _hoTrongRepository.updateData(hoTrong);
        }

        public Task deleteData(string id)
        {
            return _hoTrongRepository.deleteData(id);
        }

        public Task<bool> isMaHoTrongExist(HoTrong hoTrong, List<HoTrong> lstData, string ma)
        {
            return _hoTrongRepository.isMaHoTrongExist(hoTrong, lstData, ma);
        }

        public Task<bool> isTenHoTrongExist(HoTrong hoTrong, List<HoTrong> lstData, string ma)
        {
            return _hoTrongRepository.isTenHoTrongExist(hoTrong, lstData, ma);
        }
    }
}
