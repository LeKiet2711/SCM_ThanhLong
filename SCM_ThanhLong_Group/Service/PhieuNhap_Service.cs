using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class PhieuNhap_Service
    {
        private readonly IPhieuNhapRepository _phieuNhapRepository;
        public PhieuNhap_Service(IPhieuNhapRepository phieuNhapRepository)
        {
            _phieuNhapRepository = phieuNhapRepository;
        }

        public async Task<List<PhieuNhap>> getAllData(int khoId, string userId)
        {
            return await _phieuNhapRepository.getAllData(khoId, userId);
        }
        public async Task<List<HoTrong>> getHoTrongData()
        {
            return await _phieuNhapRepository.getHoTrongData();
        }

        public async Task<List<Kho>> getKhoData()
        {
            return await _phieuNhapRepository.getKhoData();
        }

        public PhieuNhap getDataByID(int id)
        {
            return _phieuNhapRepository.getDataByID(id);
        }

        public async Task<List<LoThanhLong>> getLoThanhLongData()
        {
            return await _phieuNhapRepository.getLoThanhLongData();
        }

        public async Task<int> addData(PhieuNhap phieuNhap)
        {
            return await _phieuNhapRepository.addData(phieuNhap);   
        }



        public async Task updateData(PhieuNhap phieuNhap)
        {
            await _phieuNhapRepository.updateData(phieuNhap);
        }
        public async Task<decimal> tinhTongTienPhieuNhap(int phieuNhapId)
        {
            return await _phieuNhapRepository.tinhTongTienPhieuNhap(phieuNhapId);
        }

        public async Task deleteData(string id)
        {
            await _phieuNhapRepository.deleteData(id);
        }

        public async Task<bool> isSoPhieuNhapExist(PhieuNhap phieuNhap, List<PhieuNhap> lstData, string ma)
        {
            return await _phieuNhapRepository.isSoPhieuNhapExist(phieuNhap, lstData, ma);
        }

        public async Task<string> getSoPhieuNhapById(int nhapKhoId)
        {
            return await _phieuNhapRepository.getSoPhieuNhapById(nhapKhoId);
        }

        public async Task<List<PhieuNhap>> getAllPhieuNhapForException()
        {
            return await _phieuNhapRepository.getAllPhieuNhapForException();
        }

        public async Task updateTrangThai(string autoId, int trangThai)
        {
            await _phieuNhapRepository.updateTrangThai(autoId,trangThai);
        }
    }
}
