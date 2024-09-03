using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class Users_Service
    {
        public string currentUserName { get; set; }
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

    }
}
