using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class HoTrong_Service
    {
        private readonly OracleDbConnection _dbConnection;

        public HoTrong_Service(OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<HoTrong>> getAllData()
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

        public async Task<List<KhuVucTrong>> getKhuVucTrongData()
        {
            List<KhuVucTrong> dataList = new List<KhuVucTrong>();

            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetAllKhuVucTrong", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new KhuVucTrong
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                MaKhuVuc = reader["MaKhuVuc"].ToString(),
                                TenKhuVuc = reader["TenKhuVuc"].ToString(),
                                //MoTa = reader["FD_FATHER"] != DBNull.Value ? int.Parse(reader["FD_FATHER"].ToString()) : 0,
                                MoTa = reader["MoTa"].ToString(),
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public HoTrong getDataByID(string id)
        {
            HoTrong data = new HoTrong();

            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetHoTrongByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Tham số đầu vào
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);

                    //Tham số đầu ra
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_TenHoTrong", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_SoDienThoai", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_HoTrongID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    data.Auto_ID = int.Parse(id);
                    data.MaHoTrong = cmd.Parameters["p_MaHoTrong"].Value?.ToString() ?? string.Empty;
                    data.TenHoTrong = cmd.Parameters["p_TenHoTrong"].Value?.ToString() ?? string.Empty;
                    data.DiaChi = cmd.Parameters["p_DiaChi"].Value?.ToString() ?? string.Empty;
                    data.SoDienThoai = cmd.Parameters["p_SoDienThoai"].Value?.ToString() ?? string.Empty;
                    data.KhuVucTrongID = cmd.Parameters["p_HoTrongID"].Value != DBNull.Value
                        ? Convert.ToInt32(cmd.Parameters["p_HoTrongID"].Value)
                        : 0;
                }
            }
            return data;
        }

        public async Task addData(HoTrong hoTrong)
        {
            using(OracleConnection conn=_dbConnection.GetConnection())
            {
                await conn.OpenAsync();
                using(OracleCommand cmd= new OracleCommand("ADDHOTRONG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Varchar2, 50).Value = hoTrong.MaHoTrong;
                    cmd.Parameters.Add("p_TenHoTrong", OracleDbType.Varchar2, 50).Value = hoTrong.TenHoTrong;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2, 50).Value = hoTrong.DiaChi;
                    cmd.Parameters.Add("p_SoDienThoai", OracleDbType.Varchar2, 10).Value = hoTrong.SoDienThoai;
                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Int32, 50).Value = hoTrong.KhuVucTrongID;
                    await cmd.ExecuteNonQueryAsync();
                }

            }
        }

        public async Task updateData(HoTrong hoTrong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("UPDATEHOTRONG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = hoTrong.Auto_ID;
                    cmd.Parameters.Add("p_MaHoTrong", OracleDbType.Varchar2, 50).Value = hoTrong.MaHoTrong;
                    cmd.Parameters.Add("p_TenHoTrong", OracleDbType.Varchar2, 50).Value = hoTrong.TenHoTrong;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2, 50).Value = hoTrong.DiaChi;
                    cmd.Parameters.Add("p_SoDienThoai", OracleDbType.Varchar2, 10).Value = hoTrong.SoDienThoai;
                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Int32, 50).Value = hoTrong.KhuVucTrongID;
                    await cmd.ExecuteNonQueryAsync();
                }

            }
        }

        public async Task deleteData(string id)
        {
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("DELETEHOTRONG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> isMaHoTrongExist(HoTrong hoTrong, List<HoTrong> lstData, string ma)
        {
            if (lstData.Any(k => k.MaHoTrong == hoTrong.MaHoTrong && hoTrong.MaHoTrong != ma))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> isTenHoTrongExist(HoTrong hoTrong, List<HoTrong> lstData, string ma)
        {
            if (lstData.Any(k => k.TenHoTrong == hoTrong.TenHoTrong && hoTrong.TenHoTrong != ma))
            {
                return true;
            }
            return false;
        }

    }
}
