
using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;
using System.Security.AccessControl;
using System.Transactions;
namespace SCM_ThanhLong_Group.Service
{
    public class OracleFgaService
    {
       
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public OracleFgaService(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }


        // 1. Thêm chính sách FGA
        public async Task AddPolicyAsync(string objectName, string policyName)
        {
            try
            {
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();
                    using (OracleTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (OracleCommand command = new OracleCommand("add_fga_policy", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Transaction = transaction;

                                command.Parameters.Add(new OracleParameter("objectName", OracleDbType.Varchar2) { Value = objectName });
                                command.Parameters.Add(new OracleParameter("policyName", OracleDbType.Varchar2) { Value = policyName });

                                await command.ExecuteNonQueryAsync();
                                Console.WriteLine("ADD thành công");
                            }
                            await transaction.CommitAsync();
                        }
                        catch
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }


        // 2. Bật chính sách FGA
        public async Task EnablePolicyAsync(string objectName, string policyName)
        {
            try
            {
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();
                    using (OracleTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (OracleCommand command = new OracleCommand("enable_fga_policy", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Transaction = transaction;

                                command.Parameters.Add(new OracleParameter("objectName", OracleDbType.Varchar2) { Value = objectName });
                                command.Parameters.Add(new OracleParameter("policyName", OracleDbType.Varchar2) { Value = policyName });

                                await command.ExecuteNonQueryAsync();
                                Console.WriteLine("Bật thành công");
                            }
                            await transaction.CommitAsync();
                        }
                        catch
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        // 3. Tắt chính sách FGA
        public async Task DisablePolicyAsync(string objectName, string policyName)
        {
            try
            {
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();
                    using (OracleTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (OracleCommand command = new OracleCommand("disable_fga_policy", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Transaction = transaction;

                                command.Parameters.Add(new OracleParameter("objectName", OracleDbType.Varchar2) { Value = objectName });
                                command.Parameters.Add(new OracleParameter("policyName", OracleDbType.Varchar2) { Value = policyName });

                                await command.ExecuteNonQueryAsync();
                                Console.WriteLine("Tắt thành công");
                            }
                            await transaction.CommitAsync();
                        }
                        catch
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }


        // 4. Xóa chính sách FGA
        public async Task DropPolicyAsync(string objectName, string policyName)
        {
            try
            {
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();
                    using (OracleTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (OracleCommand command = new OracleCommand("drop_fga_policy", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Transaction = transaction;

                                command.Parameters.Add(new OracleParameter("objectName", OracleDbType.Varchar2) { Value = objectName });
                                command.Parameters.Add(new OracleParameter("policyName", OracleDbType.Varchar2) { Value = policyName });

                                await command.ExecuteNonQueryAsync();
                                Console.WriteLine("Xóa thành công");
                            }
                            await transaction.CommitAsync();
                        }
                        catch
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        // 5. Lấy dữ liệu từ bảng
        public async Task<DataTable> FetchTableDataAsync()
        {
            try
            {
                // Xác định quyền SYSDBA nếu người dùng là "sys"
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

                // Tạo kết nối Oracle
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();

                    // Truy vấn dữ liệu từ bảng 
                    string query = "SELECT TIMESTAMP,DB_USER,OS_USER,USERHOST,OBJECT_SCHEMA ,OBJECT_NAME,SQL_TEXT,STATEMENT_TYPE FROM DBA_FGA_AUDIT_TRAIL";

                    using (OracleCommand command = new OracleCommand(query, conn))
                    {
                        command.CommandType = CommandType.Text;

                        // Sử dụng DataReader để đọc dữ liệu
                        using (OracleDataReader reader = await command.ExecuteReaderAsync())
                        {
                            var table = new DataTable();
                            table.Load(reader); // Chuyển dữ liệu từ reader vào DataTable
                            return table;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // In ra lỗi nếu có
                Console.WriteLine($"Lỗi: {ex.Message}");
                return null; // Trả về null nếu xảy ra lỗi
            }
        }
        public async Task DeleteTableDataAsync()
        {
            try
            {
                string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

                // Tạo kết nối Oracle
                using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
                {
                    await conn.OpenAsync();
                    using (OracleTransaction transaction = conn.BeginTransaction())
                    {
                        // Truy vấn dữ liệu từ bảng 
                        string query = "DELETE FROM DBA_FGA_AUDIT_TRAIL";

                        using (OracleCommand command = new OracleCommand(query, conn))
                        {
                            command.CommandType = CommandType.Text;

                            
                            command.Transaction = transaction;

                            // Thực thi lệnh DELETE
                            await command.ExecuteNonQueryAsync();

                        }
                        await transaction.CommitAsync();
                    }
                }
                
            }
            catch (Exception ex) { }
        }


    }
}
