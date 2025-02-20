using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class ChiTietPhieuXuat_Service
    {
        private readonly ICTPXRepository _ctpxRepository;
        public ChiTietPhieuXuat_Service(ICTPXRepository ctpxRepository)
        {
            _ctpxRepository = ctpxRepository;
        }

        public async Task<List<ChiTietPhieuXuat>> getAllData(int sophieuXuat)
        {
            return await _ctpxRepository.getAllData(sophieuXuat);
        }

        public async Task<int> addData(ChiTietPhieuXuat chitietPhieuXuat)
        {
            return await _ctpxRepository.addData(chitietPhieuXuat);
        }


        public async Task deleteData(int machitiet)
        {
            await _ctpxRepository.deleteData(machitiet);
        }
    }
}
