using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class PhieuNhap_Service
    {
        private readonly OracleDbConnection _dbConnection;

        public PhieuNhap_Service(OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<PhieuNhap>> getAllData()
        {
            List<PhieuNhap> dataList = new List<PhieuNhap>();
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetAllPhieuNhap", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new PhieuNhap
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                SoPhieuNhap = reader["SoPhieuNhap"].ToString(),
                                TenHoTrong = reader["TenHoTrong"].ToString(),
                                TenKho = reader["TenKho"].ToString(),
                                NgayNhap = DateTime.Parse(reader["NgayNhap"].ToString()),
                                isDeleted = int.Parse(reader["isDeleted"].ToString()),
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public async Task<List<LoaiThanhLong>> getLoaiThanhLongData()
        {
            List<LoaiThanhLong> dataList = new List<LoaiThanhLong>();
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetAllLoaiThanhLong", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new LoaiThanhLong
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                MaLoaiThanhLong = reader["MaLoaiThanhLong"].ToString(),
                                TenLoaiThanhLong = reader["TenLoaiThanhLong"].ToString(),
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
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetAllHoTrong", conn))
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
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetAllKho", conn))
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

        public PhieuNhap getDataByID(string id)
        {
            PhieuNhap data = new PhieuNhap();

            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetPhieuNhapByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số đầu vào
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);

                    // Thêm các tham số đầu ra
                    cmd.Parameters.Add("p_SoPhieuNhap", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_TenHoTrong", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    data.Auto_ID = int.Parse(id);
                    data.SoPhieuNhap = cmd.Parameters["p_SoPhieuNhap"].Value?.ToString() ?? string.Empty;
                    data.TenHoTrong = cmd.Parameters["p_TenHoTrong"].Value?.ToString() ?? string.Empty;
                    data.NgayNhap = DateTime.Parse(cmd.Parameters["p_NgayNhap"].Value?.ToString() ?? string.Empty);
                    
                    data.HoTrongID = cmd.Parameters["p_HoTrongID"].Value != DBNull.Value
                        ? Convert.ToInt32(cmd.Parameters["p_HoTrongID"].Value)
                        : 0;

                }
            }
            return data;
        }

        public async Task addData(PhieuNhap phieuNhap)
        {
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("ADDPHIEUNHAP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_SoPhieuNhap", OracleDbType.Varchar2, 50).Value = phieuNhap.SoPhieuNhap;
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Varchar2, 50).Value = phieuNhap.NgayNhap;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Int32, 50).Value = phieuNhap.HoTrongID;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Int32, 50).Value = phieuNhap.KhoID;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task updateData(PhieuNhap phieuNhap)
        {
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("UPDATEPHIEUNHAP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = phieuNhap.Auto_ID;
                    cmd.Parameters.Add("p_SoPhieuNhap", OracleDbType.Varchar2, 50).Value = phieuNhap.SoPhieuNhap;
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Varchar2, 50).Value = phieuNhap.NgayNhap;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Int32, 50).Value = phieuNhap.HoTrongID;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Int32, 50).Value = phieuNhap.KhoID;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteData(string id)
        {
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("DELETEPHIEUNHAP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> isSoPhieuNhapExist(PhieuNhap phieuNhap, List<PhieuNhap> lstData, string ma)
        {
            if (lstData.Any(k => k.SoPhieuNhap == phieuNhap.SoPhieuNhap && phieuNhap.SoPhieuNhap != ma))
            {
                return true;
            }
            return false;
        }


    }
}
