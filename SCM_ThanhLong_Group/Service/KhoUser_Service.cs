using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class KhoUser_Service
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public KhoUser_Service(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<KhoUser>> getAllData(int khoId)
        {
            List<KhoUser> dataList = new List<KhoUser>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GETALLKHOUSER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_khoId", OracleDbType.Int32).Value = khoId;

                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var data = new KhoUser
                            {
                                UserId = reader["UserId"].ToString().ToUpper(),
                                KhoID = int.Parse(reader["khoId"].ToString()),
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public async Task<List<string>> GetUsersByKhoId(int khoId)
        {
            List<string> userList = new List<string>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GETUSERSBYKHOID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_khoId", OracleDbType.Int32).Value = khoId;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userList.Add(reader["UserId"].ToString().ToUpper());
                        }
                    }
                }
            }
            return userList;
        }

        public async Task AddUserToKho(int khoId, string userId)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.ADDKHOUSER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_khoId", OracleDbType.Int32).Value = khoId;
                    cmd.Parameters.Add("p_userId", OracleDbType.Varchar2).Value = userId;

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteUserFromKho(int khoId, string userId)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.DELETEKHOUSER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_khoId", OracleDbType.Int32).Value = khoId;
                    cmd.Parameters.Add("p_userId", OracleDbType.Varchar2).Value = userId;

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
