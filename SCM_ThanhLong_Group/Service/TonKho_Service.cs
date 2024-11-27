using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SCM_ThanhLong_Group.Service
{
    public class TonKho_Service
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public TonKho_Service(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<TonKho>> BaoCaoTonKho(DateTime ngayNhap, DateTime ngayXuat)
        {
            List<TonKho> result = new List<TonKho>();

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("BaoCaoTonKho", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền giá trị DateTime vào store procedure
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Date).Value = ngayNhap;
                    cmd.Parameters.Add("p_NgayXuat", OracleDbType.Date).Value = ngayXuat;

                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var tonKho = new TonKho
                            {
                                MaLoHang = reader["MaLoHang"].ToString(),
                                SLDauKy = reader["SLDauKy"] == DBNull.Value ? 0 : int.Parse(reader["SLDauKy"].ToString()),
                                SLNhap = reader["SLNhap"] == DBNull.Value ? 0 : int.Parse(reader["SLNhap"].ToString()),
                                SLXuat = reader["SLXuat"] == DBNull.Value ? 0 : int.Parse(reader["SLXuat"].ToString()),
                                SLCuoiKy = reader["SLCuoiKy"] == DBNull.Value ? 0 : int.Parse(reader["SLCuoiKy"].ToString())
                            };
                            result.Add(tonKho);
                        }
                    }
                }
            }
            return result;
        }

    }

    public class TonKho
    {
        public string MaLoHang { get; set; }
        public int SLDauKy { get; set; }
        public int SLNhap { get; set; }
        public int SLXuat { get; set; }
        public int SLCuoiKy { get; set; }
    }
}
