
using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;
using System.Data.Common;
using Blazored.SessionStorage;
using Telerik.SvgIcons;
using SCM_ThanhLong_Group.Service;

namespace SCM_ThanhLong_Group.Service
{
    public class Users_Service
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly ISessionStorageService _sessionStorage;
        public string currentUserName { get; set; }
        public string currentPassWord { get; set; }
        public Users_Service(OracleDbConnection dbConnection, ISessionStorageService sessionStorage)
        {
            _dbConnection = dbConnection;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            string connectionString;
            //string connectionString = $"User Id=C##{username};Password={password};Data Source=localhost:1521/orcl1;";
            if (string.Compare(username, "sys", true) == 0 || string.Compare(username, "system", true) == 0)
            {
                connectionString = $"User Id={username};Password={password};Data Source=localhost:1521/orcl1;";
                //connectionString = $"User Id={username};Password={password};Data Source=localhost:1521/orcl;";
            }
            else
            {
                connectionString = $"User Id=C##{username};Password={password};Data Source=localhost:1521/orcl1;pooling=false";
                //connectionString = $"User Id=C##{username};Password={password};Data Source=localhost:1521/orcl;pooling=false";
            }
            if(string.Compare(username,"sys", true)==0)
            {
                connectionString += "DBA Privilege=SYSDBA;";
            } 
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

                    await _sessionStorage.SetItemAsync("currentUserName", username);
                    await _sessionStorage.SetItemAsync("currentPassWord", password);
                    return true;
                }
                catch (OracleException)
                {
                    await killSession(username.ToUpper());
                    return false;
                }
            }
        }


        public async Task<bool> isTheUserLocked(string userName)
        {
            bool result = false;
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    string sqlKillSession = "isTheUserLocked";
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sqlKillSession;
                    oracleCommand.CommandType = CommandType.StoredProcedure;

                    // Chuyển đổi tên người dùng thành chữ hoa
                    oracleCommand.Parameters.Add("userNameIn", OracleDbType.Varchar2).Value = "C##"+userName.ToUpper();
                    oracleCommand.Parameters.Add("tableInfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    await kn.OpenAsync();
                    OracleDataReader read = await oracleCommand.ExecuteReaderAsync();
                    if (read.HasRows)
                    {
                        while (await read.ReadAsync())
                        {
                            string name = read["USERNAME"].ToString();
                            string status = read["ACCOUNT_STATUS"].ToString();
                            result = true;
                        }
                        await killSession(userName.ToUpper());
                    }
                    kn.Close();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần
            }
            return result;
        }

        public async Task<bool> ChangePassword(string username, string oldPassword, string newPassword)
        {
            string connectionString = $"User Id=C##{username};Password={oldPassword};Data Source=localhost:1521/orcl;";
            //string connectionString = $"User Id={username};Password={oldPassword};Data Source=localhost:1521/chkb;";

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
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin","oracle"))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("SELECT * FROM SYS.ALL_USERS WHERE USERNAME LIKE 'C##%'", conn))
                {
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new Users
                            {
                                username= reader["USERNAME"].ToString(),

                            };
                            dataList.Add(data);
                        }

                    }
                }
            }
            return dataList;
        }

        public async Task<string> GetCurrentUserName()
        {
            return await _sessionStorage.GetItemAsync<string>("currentUserName");
        }

        public async Task<string> GetCurrentPassWord()
        {
            return await _sessionStorage.GetItemAsync<string>("currentPassWord");
        }

        public async Task Logout()
        {
            string username = await _sessionStorage.GetItemAsync<string>("currentUserName");
            await _sessionStorage.RemoveItemAsync("currentUserName");
            await _sessionStorage.RemoveItemAsync("currentPassWord");
            currentUserName = null;
            currentPassWord = null;
        }


        public async Task ExecuteSqlCommand(string sqlCommand)
        {
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin", "oracle"))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sqlCommand, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<TablePermission>> GetUserPermissions(string username)
        {
            List<TablePermission> permissions = new List<TablePermission>();
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin", "oracle"))
            {
                conn.Open();
                string query = $"SELECT TABLE_NAME FROM SYS.USER_TAB_PRIVS WHERE GRANTEE = '{username}' AND PRIVILEGE = 'EXECUTE'";
                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            string procedureName = reader["TABLE_NAME"].ToString();
                            var tablePermission = new TablePermission { TableName = procedureName };
                            permissions.Add(tablePermission);
                        }
                    }
                }
            }
            return permissions;
        }

        public async Task killSession(string userName)
        {
            if (string.Compare(userName, "sys", true) == 0 || string.Compare(userName, "system", true) == 0)
            {

            }
            else
            {
                userName = "C##" + userName;
            }
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    string sqlKillSession = "killSessionUser";
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sqlKillSession;
                    oracleCommand.CommandType = CommandType.StoredProcedure;

                    oracleCommand.Parameters.Add("userName", OracleDbType.Varchar2).Value = userName;

                    //kn.OpenAsync();
                    kn.Open();
                    oracleCommand.ExecuteNonQuery();
                    kn.Close();
                    //kn.CloseAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
