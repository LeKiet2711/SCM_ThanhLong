using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class ChiTietPhieuNhap_Service
    {
        private readonly ICTPNRepository _ctpnRepository;

        public ChiTietPhieuNhap_Service(ICTPNRepository ctpnRepository)
        {
            _ctpnRepository = ctpnRepository;
        }

        public async Task<List<ChiTietPhieuNhap>> getAllData(int sophieunhap)
        {
            return await _ctpnRepository.getAllData(sophieunhap);
        }

        public async Task<int> addData(ChiTietPhieuNhap chitietPhieuNhap)
        {
            return await _ctpnRepository.addData(chitietPhieuNhap);
        }


        public async Task deleteData(int machitiet)
        {
            await _ctpnRepository.deleteData(machitiet);
        }

    }
}
