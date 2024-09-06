using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;
using System.Data.Common;

namespace SCM_ThanhLong_Group.Service
{
    public class Users_Service
    {
        private readonly OracleDbConnection _dbConnection;  
        public string currentUserName { get; set; }

        public Users_Service(OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<bool> AuthenticateUser(string username, string password)
        {
            string connectionString = $"User Id=C##{username};Password={password};Data Source=localhost:1521/orcl1;";

            using (var conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open(); 
                    string query = "SELECT 1 FROM DUAL";
                    using (var cmd = new OracleCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        await cmd.ExecuteScalarAsync();
                    }
                    return true;
                }
                catch (OracleException ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> ChangePassword(string username, string oldPassword, string newPassword)
        {
            string connectionString = $"User Id=C##{username};Password={oldPassword};Data Source=localhost:1521/orcl1;";

            using (var conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = $"ALTER USER C##{username} IDENTIFIED BY {newPassword}";
                    using (var cmd = new OracleCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        await cmd.ExecuteNonQueryAsync();
                    }
                    return true;
                }
                catch (OracleException ex)
                {
                    return false;
                }
            }
        }

        public async Task<List<Users>> getAllData()
        {
            List<Users> dataList = new List<Users>();
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("SELECT * FROM SYS.ALL_USERS", conn))
                {
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new Users
                            {
                                name= reader["USERNAME"].ToString(),

                            };
                            dataList.Add(data);
                        }

                    }
                }
            }
            return dataList;
        }

    }
}
