using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;
using Blazored.SessionStorage;
using Oracle.ManagedDataAccess.Types;

namespace SCM_ThanhLong_Group.Service

{
    public class KeyValidationService
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public KeyValidationService(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<bool> ValidateKey(string userKey)
        {

            try
            {
                bool isValid = false;
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();
                    if (conn == null)
                    {
                        throw new Exception("Không thể tạo kết nối tới cơ sở dữ liệu.");
                    }
                    using (OracleCommand command = new OracleCommand("C##Admin.validate_user_key", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new OracleParameter("p_user_key", OracleDbType.Varchar2)
                        {
                            Value = userKey
                        });

                        var outputParameter = new OracleParameter("p_is_valid", OracleDbType.Int32)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParameter);

                        await command.ExecuteNonQueryAsync();
                        var oracleDecimal = (Oracle.ManagedDataAccess.Types.OracleDecimal)outputParameter.Value;
                        int result = oracleDecimal.ToInt32(); // Chuyển đổi sang int
                        isValid = result == 1;
                        Console.WriteLine($"Kết quả kiểm tra KHOA: {result}");
                    }
                }

                return isValid;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gọi ValidateKey: {ex.Message}");
                return false;
            }
        }
      
    }
}
