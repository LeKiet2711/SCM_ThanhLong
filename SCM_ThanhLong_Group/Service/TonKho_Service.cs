using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SCM_ThanhLong_Group.Repository;

namespace SCM_ThanhLong_Group.Service
{
    public class TonKho_Service
    {
        private readonly ITonKhoRepository _tonKhoRepository;
        public TonKho_Service(ITonKhoRepository tonKhoRepository)
        {
            _tonKhoRepository = tonKhoRepository;
        }

        public async Task<List<TonKho>> BaoCaoTonKho(DateTime ngayNhap, DateTime ngayXuat)
        {
            return await _tonKhoRepository.BaoCaoTonKho(ngayNhap,ngayXuat);
        }

    }
}
