using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Interface;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Repository
{
    public class CTPXRepository:ICTPXRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public CTPXRepository(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<ChiTietPhieuXuat>> getAllData(int sophieuXuat)
        {
            List<ChiTietPhieuXuat> dataList = new List<ChiTietPhieuXuat>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetAllChiTietPhieuXuat", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("p_SOPHIEUXuat", OracleDbType.Int32).Value = sophieuXuat;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new ChiTietPhieuXuat
                            {
                                Auto_ID = int.Parse(reader["MACHITIET"].ToString()),
                                MaLoSTR = reader["MALO"].ToString(),
                                SoKg = double.Parse(reader["SOKG"].ToString()),
                                DonGia = double.Parse(reader["DONGIA"].ToString())

                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public async Task<int> addData(ChiTietPhieuXuat chitietPhieuXuat)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.AddChiTietPhieuXuat", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào stored procedure
                    cmd.Parameters.Add("p_SOPHIEUXUAT", OracleDbType.Int32).Value = chitietPhieuXuat.SoPhieuXuat;
                    cmd.Parameters.Add("p_DONGIA", OracleDbType.Double).Value = chitietPhieuXuat.DonGia;
                    cmd.Parameters.Add("p_SOKG", OracleDbType.Double).Value = chitietPhieuXuat.SoKg;
                    cmd.Parameters.Add("p_MALOHANG", OracleDbType.Int32).Value = chitietPhieuXuat.MaLo;

                    // Thêm tham số đầu ra
                    var autoIDParam = new OracleParameter("p_AUTO_ID", OracleDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(autoIDParam);

                    await cmd.ExecuteNonQueryAsync();

                    // Kiểm tra giá trị trả về
                    OracleDecimal autoIDDecimal = (OracleDecimal)autoIDParam.Value;
                    int autoID = autoIDDecimal.ToInt32();  // Chuyển đổi OracleDecimal sang int
                    return autoID;
                }
            }
        }


        public async Task deleteData(int machitiet)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.DELETECHITIETPHIEUXUAT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_MACHITIET", OracleDbType.Int32).Value = machitiet;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
