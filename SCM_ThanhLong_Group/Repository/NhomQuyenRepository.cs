using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using System.Data;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;

namespace SCM_ThanhLong_Group.Repository
{
    public class NhomQuyenRepository:INhomQuyenRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public NhomQuyenRepository(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task CreateRole(string roleName)
        {
            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("CREATE_ROLE_PROC", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_role_name", OracleDbType.Varchar2).Value = roleName;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
