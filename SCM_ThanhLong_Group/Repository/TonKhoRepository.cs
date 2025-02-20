using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Interface;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Service;
using System.Data;

namespace SCM_ThanhLong_Group.Repository
{
    public class TonKhoRepository:ITonKhoRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public TonKhoRepository(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<TonKho>> BaoCaoTonKho(DateTime ngayNhap, DateTime ngayXuat)
        {
            var result = new List<TonKho>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("BaoCaoTonKho", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("NgayNhap", OracleDbType.Date).Value = ngayNhap.Date;
                    cmd.Parameters.Add("NgayXuat", OracleDbType.Date).Value = ngayXuat.Date;

                    // REF CURSOR để lấy dữ liệu
                    cmd.Parameters.Add("OutCursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var tonKho = new TonKho
                            {
                                MaLoHang = reader["MaLoHang"].ToString(),
                                SLDauKy = reader["SLDauKy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SLDauKy"]),
                                SLNhap = reader["SLNhap"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SLNhap"]),
                                SLXuat = reader["SLXuat"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SLXuat"]),
                                SLCuoiKy = reader["SLCuoiKy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SLCuoiKy"]),
                            };
                            result.Add(tonKho);
                            Console.WriteLine($"MaLoHang: {reader["MaLoHang"]}, SLDauKy: {reader["SLDauKy"]}, " +
                                  $"SLNhap: {reader["SLNhap"]}, SLXuat: {reader["SLXuat"]}, " +
                                  $"SLCuoiKy: {reader["SLCuoiKy"]}");
                        }
                    }
                }
            }
            return result;
        }
    }
}
