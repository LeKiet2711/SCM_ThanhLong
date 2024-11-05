using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class PhieuXuat_Service
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public PhieuXuat_Service(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<PhieuXuat>> getAllData(int? maKho = null)
        {
            List<PhieuXuat> dataList = new List<PhieuXuat>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetAllPhieuXuat", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Int32).Value = maKho.HasValue ? (object)maKho.Value : DBNull.Value;

                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var data = new PhieuXuat
                            {
                                Auto_ID = int.Parse(reader["AUTO_ID"].ToString()),
                                SoPhieuXuat = reader["SOPHIEUXUAT"].ToString(),
                                KhoID = int.Parse(reader["MAKHO"].ToString()),
                                TenKho = reader["TENKHO"].ToString(),
                                NgayXuat = DateTime.Parse(reader["NGAYXUAT"].ToString()),
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }


        public async Task<List<HoTrong>> getHoTrongData()
        {
            List<HoTrong> dataList = new List<HoTrong>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetAllHoTrong", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new HoTrong
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                MaHoTrong = reader["MaHoTrong"].ToString(),
                                TenHoTrong = reader["TenHoTrong"].ToString(),
                                KhuVucTrongID = int.Parse(reader["MaKhuVuc"].ToString()),
                                TenKhuVucTrong = reader["TenKhuVuc"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public async Task<List<Kho>> getKhoData()
        {
            List<Kho> dataList = new List<Kho>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetAllKho", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new Kho
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                MaKho = reader["MaKho"].ToString(),
                                TenKho = reader["TenKho"].ToString(),
                                //MoTa = reader["FD_FATHER"] != DBNull.Value ? int.Parse(reader["FD_FATHER"].ToString()) : 0,
                                DiaChi = reader["DiaChi"].ToString(),
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public async Task<List<LoThanhLong>> getLoThanhLongData()
        {
            List<LoThanhLong> loThanhLongList = new List<LoThanhLong>();

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();

                using (OracleCommand cmd = new OracleCommand("C##Admin.GetAllLoThanhLong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var loThanhLong = new LoThanhLong
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                MaLo = reader["MaLo"].ToString(),
                                HoTrongID = int.Parse(reader["MaHoTrong"].ToString()),
                                TenHoTrong = reader["TenHoTrong"].ToString(),
                                NgayThuHoach = reader.GetDateTime(reader.GetOrdinal("NgayThuHoach")),
                                //QRCode = reader["QRCode"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                MoTa = reader["Mota"].ToString(),
                            };
                            loThanhLongList.Add(loThanhLong);
                        }
                    }
                }
            }

            return loThanhLongList;
        }
        public async Task<int> addData(PhieuXuat phieuXuat)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.ADDPHIEUXUAT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào stored procedure
                    cmd.Parameters.Add("p_SoPhieuXuat", OracleDbType.Varchar2).Value = phieuXuat.SoPhieuXuat;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Int32).Value = phieuXuat.KhoID;
                    cmd.Parameters.Add("p_NgayXuat", OracleDbType.Date).Value = phieuXuat.NgayXuat;
                    cmd.Parameters.Add("p_TongTien", OracleDbType.Decimal).Value = 0;

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

        public async Task updateData(PhieuXuat phieuXuat)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.UPDATEPHIEUXUAT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào stored procedure
                    cmd.Parameters.Add("p_AUTO_ID", OracleDbType.Int32).Value = phieuXuat.Auto_ID;
                    cmd.Parameters.Add("p_SoPhieuXuat", OracleDbType.Varchar2).Value = phieuXuat.SoPhieuXuat;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Int32).Value = phieuXuat.KhoID;
                    cmd.Parameters.Add("p_NgayXuat", OracleDbType.Date).Value = phieuXuat.NgayXuat;

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteData(int autoID)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.DELETEPHIEUXUAT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_AUTO_ID", OracleDbType.Int32).Value = autoID;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> isSoPhieuXuatExist(PhieuXuat phieuXuat, List<PhieuXuat> lstData, string ma)
        {
            if (lstData.Any(k => k.SoPhieuXuat == phieuXuat.SoPhieuXuat && phieuXuat.SoPhieuXuat != ma))
            {
                return true;
            }
            return false;
        }

    }
}
