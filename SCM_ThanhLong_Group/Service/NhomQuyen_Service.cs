using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;
namespace SCM_ThanhLong_Group.Service
{
    public class NhomQuyen_Service
    {
        private readonly INhomQuyenRepository _nhomQuyenrepository;
        public NhomQuyen_Service(INhomQuyenRepository nhomQuyenrepository)
        {
            _nhomQuyenrepository = nhomQuyenrepository;
        }

        public async Task CreateRole(string roleName)
        {
            await _nhomQuyenrepository.CreateRole(roleName);
        }
    }
}
