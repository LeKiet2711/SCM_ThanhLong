using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Interface;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Repository
{
    public class PhieuNhapRepository:IPhieuNhapRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public PhieuNhapRepository(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<PhieuNhap>> getAllData(int khoId, string userId)
        {
            List<PhieuNhap> dataList = new List<PhieuNhap>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GETALLPHIEUNHAP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_khoId", OracleDbType.Int32).Value = khoId;
                    cmd.Parameters.Add("p_userId", OracleDbType.Varchar2).Value = userId;

                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var data = new PhieuNhap
                            {
                                Auto_ID = int.Parse(reader["AUTO_ID"].ToString()),
                                SoPhieuNhap = reader["SOPHIEUNHAP"].ToString(),
                                KhoID = int.Parse(reader["MAKHO"].ToString()),
                                TenKho = reader["TENKHO"].ToString(),
                                NgayNhap = DateTime.Parse(reader["NGAYNHAP"].ToString()),
                                TrangThai = int.Parse(reader["TrangThai"].ToString())
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

        public PhieuNhap getDataByID(int id)
        {
            PhieuNhap data = new PhieuNhap();

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetPhieuNhapByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số đầu vào
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = id;

                    // Thêm các tham số đầu ra
                    cmd.Parameters.Add("p_SoPhieuNhap", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_TenHoTrong", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Date).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    data.Auto_ID = id;
                    data.SoPhieuNhap = cmd.Parameters["p_SoPhieuNhap"].Value?.ToString() ?? string.Empty;
                    data.NgayNhap = DateTime.Parse(cmd.Parameters["p_NgayNhap"].Value?.ToString() ?? string.Empty);

                    if (cmd.Parameters["p_NgayNhap"].Value != DBNull.Value)
                    {
                        Oracle.ManagedDataAccess.Types.OracleDate oracleDate = (Oracle.ManagedDataAccess.Types.OracleDate)cmd.Parameters["p_NgayNhap"].Value;
                        data.NgayNhap = oracleDate.Value;
                    }
                }
            }
            return data;
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

        public async Task<int> addData(PhieuNhap phieuNhap)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.AddPhieuNhap", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_SoPhieuNhap", OracleDbType.Varchar2, 50).Value = phieuNhap.SoPhieuNhap;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Int32).Value = phieuNhap.KhoID;
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Date).Value = phieuNhap.NgayNhap;

                    cmd.Parameters.Add("p_TongTien", OracleDbType.Decimal).Value = 0;
                    var autoIDParam = new OracleParameter("p_AUTO_ID", OracleDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(autoIDParam);
                    await cmd.ExecuteNonQueryAsync();

                    // Chuyển đổi OracleDecimal sang int
                    OracleDecimal autoIDDecimal = (OracleDecimal)autoIDParam.Value;
                    return autoIDDecimal.ToInt32();  // Chuyển đổi OracleDecimal thành int
                }
            }
        }



        public async Task updateData(PhieuNhap phieuNhap)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.UPDATEPHIEUNHAP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = phieuNhap.Auto_ID;
                    cmd.Parameters.Add("p_SoPhieuNhap", OracleDbType.Varchar2, 50).Value = phieuNhap.SoPhieuNhap;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Int32, 50).Value = phieuNhap.KhoID;
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Date).Value = phieuNhap.NgayNhap;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<decimal> tinhTongTienPhieuNhap(int phieuNhapId)
        {
            decimal tongTien = 0;

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetTongTienPhieuNhap", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_phieuNhapId", OracleDbType.Int32).Value = phieuNhapId;
                    var tongTienParam = new OracleParameter("p_tongTien", OracleDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(tongTienParam);

                    await cmd.ExecuteNonQueryAsync();

                    tongTien = ((OracleDecimal)tongTienParam.Value).Value;
                }
            }

            return tongTien;
        }

        public async Task deleteData(string id)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.DELETEPHIEUNHAP", conn))
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

        public async Task<string> getSoPhieuNhapById(int nhapKhoId)
        {
            string soPhieuNhap = string.Empty;

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetPhieuNhapByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = nhapKhoId;
                    cmd.Parameters.Add("p_SoPhieuNhap", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;

                    await cmd.ExecuteNonQueryAsync();

                    soPhieuNhap = cmd.Parameters["p_SoPhieuNhap"].Value?.ToString() ?? string.Empty;
                }
            }

            return soPhieuNhap;
        }

        public async Task<List<PhieuNhap>> getAllPhieuNhapForException()
        {
            List<PhieuNhap> result = new List<PhieuNhap>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("SELECT * FROM C##Admin.PhieuNhap", conn))
                {
                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new PhieuNhap
                            {
                                SoPhieuNhap = reader["SoPhieuNhap"].ToString(),
                                // Các thuộc tính khác
                            });
                        }
                    }
                }
            }
            return result;
        }

        public async Task updateTrangThai(string autoId, int trangThai)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("UPDATE PHIEUNHAP SET TrangThai = :trangThai WHERE AUTO_ID = :autoId", conn))
                {
                    cmd.Parameters.Add(new OracleParameter("trangThai", trangThai));
                    cmd.Parameters.Add(new OracleParameter("autoId", autoId));
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
