using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Components.Connection;
using System.Data;
using System.Threading.Tasks;
using SCM_ThanhLong_Group.Interface;

namespace SCM_ThanhLong_Group.Service
{
    public class KhuVucTrong_Service
    {
        private readonly IKhuVucTrongRepository _kvtRepository;

        public KhuVucTrong_Service(IKhuVucTrongRepository kvtRepository)
        {
            _kvtRepository = kvtRepository;
        }

        public async Task<List<KhuVucTrong>> getAllData()
        {
            return await _kvtRepository.getAllData();
        }

        public KhuVucTrong getDataByID(string id)
        {
            return _kvtRepository.getDataByID(id);
        }

        public async Task addData(KhuVucTrong khuVucTrong)
        {
            await _kvtRepository.addData(khuVucTrong);
        }

        public async Task updateData(KhuVucTrong khuVucTrong)
        {
            await _kvtRepository.updateData(khuVucTrong);
        }

        public async Task deleteData(string Auto_ID)
        {
            await _kvtRepository.deleteData(Auto_ID);
        }

        public async Task<bool> isMaKhuVucExist(KhuVucTrong khuVucTrong, List<KhuVucTrong> lstData, string ma)
        {
            return await _kvtRepository.isMaKhuVucExist(khuVucTrong, lstData, ma);
        }

        public async Task<bool> isTenKhuVucExist(KhuVucTrong khuVucTrong, List<KhuVucTrong> lstData, string ma)
        {
            return await _kvtRepository.isTenKhuVucExist(khuVucTrong, lstData, ma);
        }    
    }
}
