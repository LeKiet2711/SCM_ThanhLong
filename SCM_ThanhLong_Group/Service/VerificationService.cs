using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;
using Oracle.ManagedDataAccess.Types;
namespace SCM_ThanhLong_Group.Service
{
    public class VerificationService
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public VerificationService(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        // Gọi thủ tục mã hóa mã xác minh
        //public async Task EncryptVerificationCodeAsync()
        //{
        //    try
        //    {
        //        using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
        //        {
        //            await conn.OpenAsync();

        //            using (OracleCommand command = new OracleCommand("C##Admin.encrypt_verification_code", conn))
        //            {
        //                command.CommandType = System.Data.CommandType.StoredProcedure;
        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Lỗi khi mã hóa mã xác minh: {ex.Message}");
        //    }
        //}

        // Gọi thủ tục kiểm tra mã xác minh
        public async Task<bool> CheckVerificationCodeAsync(string matext)
        {
            try
            {
                bool isVerified = false;
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();

                    using (OracleCommand command = new OracleCommand("C##Admin.Check_verification_code", conn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Thêm tham số đầu vào (userKey)
                        command.Parameters.Add(new OracleParameter("p_user_key", OracleDbType.Varchar2)
                        {
                            Value = matext
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
                        isVerified=result == 1;
                        Console.WriteLine($"Kết quả kiểm tra MA: {result}");
                        
                        
                    }
                }
                return isVerified;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kiểm tra mã xác minh: {ex.Message}");
                return false;
            }
        }
    }
}
