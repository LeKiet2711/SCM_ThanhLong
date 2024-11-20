using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class LoThanhLong_Service
    {

        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;
        public LoThanhLong_Service(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }
        public async Task<List<LoThanhLong>> getAllData()
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

        public async Task addData(LoThanhLong loThanhLong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();

                using (OracleCommand cmd = new OracleCommand("C##Admin.AddLoThanhLong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số cho stored procedure
                    cmd.Parameters.Add("p_MaLo", OracleDbType.Varchar2).Value = loThanhLong.MaLo;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Int32).Value = loThanhLong.HoTrongID;
                    cmd.Parameters.Add("p_NgayThuHoach", OracleDbType.Date).Value = loThanhLong.NgayThuHoach;
                    cmd.Parameters.Add("p_TrangThai", OracleDbType.Varchar2).Value = loThanhLong.TrangThai;
                    cmd.Parameters.Add("p_MoTa", OracleDbType.Varchar2).Value = loThanhLong.MoTa;

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task updateData(LoThanhLong loThanhLong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();

                using (OracleCommand cmd = new OracleCommand("C##Admin.UpdateLoThanhLong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số cho stored procedure
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = loThanhLong.Auto_ID;
                    cmd.Parameters.Add("p_MaLo", OracleDbType.Varchar2).Value = loThanhLong.MaLo;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Int32).Value = loThanhLong.HoTrongID;
                    cmd.Parameters.Add("p_NgayThuHoach", OracleDbType.Date).Value = loThanhLong.NgayThuHoach;
                    cmd.Parameters.Add("p_TrangThai", OracleDbType.Varchar2).Value = loThanhLong.TrangThai;
                    cmd.Parameters.Add("p_MoTa", OracleDbType.Varchar2).Value = loThanhLong.MoTa;

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteData(string id)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.DELETELOTHANHLONG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> isMaLoExist(LoThanhLong loThanhLong, List<LoThanhLong> lstData, string ma)
        {
            if (lstData.Any(k => k.MaLo == loThanhLong.MaLo && loThanhLong.MaLo != ma))
            {
                return true;
            }
            return false;
        }
    }
}
