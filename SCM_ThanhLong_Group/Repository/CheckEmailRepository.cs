using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Interface;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Repository
{
    public class CheckEmailRepository:ICheckEmail
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public CheckEmailRepository(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }
        public async Task<bool> checkEmailUser(string gmail)
        {
            try
            {
                bool isEmail = false;
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();

                    using (OracleCommand command = new OracleCommand("C##Admin.Check_Email_code", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Thêm tham số đầu vào (userKey)
                        command.Parameters.Add(new OracleParameter("p_user_key", OracleDbType.Varchar2)
                        {
                            Value = gmail
                        });

                        // Thêm tham số đầu ra (p_is_valid)
                        var outputParam = new OracleParameter("p_is_valid", OracleDbType.Int32)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        await command.ExecuteNonQueryAsync();

                        // Kiểm tra kết quả đầu ra
                        var oracleDecimal = (Oracle.ManagedDataAccess.Types.OracleDecimal)outputParam.Value;
                        int result = oracleDecimal.ToInt32(); // Chuyển đổi sang int
                        isEmail = result == 1;
                        Console.WriteLine($"Kết quả kiểm tra EMAIL: {result}");


                    }
                }
                return isEmail;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kiểm tra mã xác minh: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> GrantUserAccess(string username)
        {

            try
            {
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();

                    using (OracleCommand command = new OracleCommand("grant_user_access", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Thêm tham số đầu vào (username)
                        command.Parameters.Add(new OracleParameter("p_username", OracleDbType.Varchar2)
                        {
                            Value = username
                        });

                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Cấp quyền thành công cho người dùng: {username}");

                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                if (username == null)
                {
                    Console.WriteLine($"gia tri user băng null {ex.Message}");
                }
                Console.WriteLine($"Lỗi khi cấp quyền: {ex.Message}");
                return false;
            }

        }
        public async Task<string> layGmail(string username)
        {
            string email = string.Empty;
            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "C##Admin.get_user_email";
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số đầu vào
                    var usernameParam = new OracleParameter("p_username", OracleDbType.Varchar2, 50)
                    {
                        Value = username,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(usernameParam);

                    // Thêm tham số đầu ra
                    var emailParam = new OracleParameter("p_email", OracleDbType.Varchar2, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(emailParam);

                    // Thực thi thủ tục
                    await command.ExecuteNonQueryAsync();

                    // Lấy kết quả từ tham số đầu ra
                    email = emailParam.Value?.ToString() ?? "Không có dữ liệu";
                    Console.WriteLine($"da lay duoc gmail:{email}");
                }
            }
            return email;
        }
    }
}
