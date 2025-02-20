using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;
using Telerik.SvgIcons;

namespace SCM_ThanhLong_Group.Service
{
    public class ChucNang_Service
    {
        private readonly IChucNangRepository _chucnangRepository;
        public ChucNang_Service(IChucNangRepository chucnangRepository)
        {
            _chucnangRepository = chucnangRepository;
        }

        public async Task<List<ChucNang>> getAllData()
        {
            return await _chucnangRepository.getAllData();
        }

        public async Task updateData(ChucNang data)
        {
            await _chucnangRepository.updateData(data);
        }
    }
}
