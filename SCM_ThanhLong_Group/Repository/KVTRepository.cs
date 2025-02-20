using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;
using SCM_ThanhLong_Group.Interface;

namespace SCM_ThanhLong_Group.Repository
{
    public class KVTRepository:IKhuVucTrongRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public KVTRepository(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<KhuVucTrong>> getAllData()
        {
            List<KhuVucTrong> dataList = new List<KhuVucTrong>();

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetAllKhuVucTrong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var data = new KhuVucTrong
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                MaKhuVuc = reader["MaKhuVuc"].ToString(),
                                TenKhuVuc = reader["TenKhuVuc"].ToString(),
                                MoTa = reader["MoTa"].ToString(),
                                LinkQR = reader["LinkQR"].ToString()
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }
            return dataList;
        }

        public KhuVucTrong getDataByID(string id)
        {
            KhuVucTrong data = new KhuVucTrong();

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetKhuVucTrongByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số đầu vào
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);

                    // Thêm các tham số đầu ra
                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_TenKhuVuc", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_MoTa", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_LinkQR", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;

                    // Thực thi stored procedure
                    cmd.ExecuteNonQuery();

                    data.MaKhuVuc = cmd.Parameters["p_MaKhuVuc"].Value?.ToString() ?? string.Empty;
                    data.TenKhuVuc = cmd.Parameters["p_TenKhuVuc"].Value?.ToString() ?? string.Empty;
                    data.MoTa = cmd.Parameters["p_MoTa"].Value?.ToString() ?? string.Empty;
                    data.LinkQR = cmd.Parameters["p_LinkQR"].Value?.ToString() ?? string.Empty;
                }
            }
            return data;
        }

        public async Task addData(KhuVucTrong khuVucTrong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.AddKhuVucTrong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Varchar2).Value = khuVucTrong.MaKhuVuc;
                    cmd.Parameters.Add("p_TenKhuVuc", OracleDbType.Varchar2).Value = khuVucTrong.TenKhuVuc;
                    cmd.Parameters.Add("p_MoTa", OracleDbType.Varchar2).Value = khuVucTrong.MoTa;
                    cmd.Parameters.Add("p_LinkQR", OracleDbType.Varchar2).Value = khuVucTrong.LinkQR;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task updateData(KhuVucTrong khuVucTrong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.UpdateKhuVucTrong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = khuVucTrong.Auto_ID;
                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Varchar2).Value = khuVucTrong.MaKhuVuc;
                    cmd.Parameters.Add("p_TenKhuVuc", OracleDbType.Varchar2).Value = khuVucTrong.TenKhuVuc;
                    cmd.Parameters.Add("p_MoTa", OracleDbType.Varchar2).Value = khuVucTrong.MoTa;
                    cmd.Parameters.Add("p_LinkQR", OracleDbType.Varchar2).Value = khuVucTrong.LinkQR;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteData(string Auto_ID)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.DeleteKhuVucTrong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = Auto_ID;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> isMaKhuVucExist(KhuVucTrong khuVucTrong, List<KhuVucTrong> lstData, string ma)
        {
            return lstData.Any(k => k.MaKhuVuc == khuVucTrong.MaKhuVuc && khuVucTrong.MaKhuVuc != ma);
        }

        public async Task<bool> isTenKhuVucExist(KhuVucTrong khuVucTrong, List<KhuVucTrong> lstData, string ma)
        {
            return lstData.Any(k => k.TenKhuVuc == khuVucTrong.TenKhuVuc && khuVucTrong.TenKhuVuc != ma);
        }
    }
}
