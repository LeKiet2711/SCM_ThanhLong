using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;
using SCM_ThanhLong_Group.Interface;


namespace SCM_ThanhLong_Group.Service
{
    public class Kho_Service
    {
        private readonly IKhoRepository _khoRepository;

        public Kho_Service(IKhoRepository khoRepository)
        {
            _khoRepository = khoRepository;
        }

        public async Task<List<Kho>> getAllData()
        {
            return await _khoRepository.getAllData();
        }

        public Kho getDataById(string id)
        {
            return _khoRepository.getDataById(id);
        }

        public async Task addData(Kho kho)
        {
            await _khoRepository.addData(kho);
        }

        public async Task updateData(Kho kho)
        {
            await _khoRepository.updateData(kho);
        }

        public async Task deleteData(string autoId)
        {
            await _khoRepository.deleteData(autoId);
        }

        public async Task<bool> isMaKhoExist(Kho kho, List<Kho> lstData, string ma)
        {
            return await _khoRepository.isMaKhoExist(kho, lstData, ma);
        }

        public async Task<bool> isTenKhoExist(Kho kho, List<Kho> lstData, string ma)
        {
            return await _khoRepository.isTenKhoExist(kho, lstData, ma);
        }

        public async Task<List<Users>> getAllData2()
        {
            return await _khoRepository.getAllData2();
        }

    }
}
