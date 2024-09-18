using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Components.Connection;
using System.Data;
using System.Threading.Tasks;

namespace SCM_ThanhLong_Group.Service
{
    public class KhuVucTrong_Service
    {
        private readonly OracleDbConnection _dbConnection;

        public KhuVucTrong_Service(OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<KhuVucTrong>> getAllData(string userId, string password)
        {
            List<KhuVucTrong> dataList = new List<KhuVucTrong>();

            using (OracleConnection conn = _dbConnection.GetConnection(userId, password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("GetAllKhuVucTrong", conn))
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

            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin", "oracle"))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetKhuVucTrongByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số đầu vào
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);

                    // Thêm các tham số đầu ra
                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_TenKhuVuc", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_MoTa", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;

                    // Thực thi stored procedure
                    cmd.ExecuteNonQuery();

                    data.MaKhuVuc = cmd.Parameters["p_MaKhuVuc"].Value?.ToString() ?? string.Empty;
                    data.TenKhuVuc = cmd.Parameters["p_TenKhuVuc"].Value?.ToString() ?? string.Empty;
                    data.MoTa = cmd.Parameters["p_MoTa"].Value?.ToString() ?? string.Empty;
                }
            }
            return data;
        }

        public async Task addData(KhuVucTrong khuVucTrong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin", "oracle"))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("AddKhuVucTrong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Varchar2).Value = khuVucTrong.MaKhuVuc;
                    cmd.Parameters.Add("p_TenKhuVuc", OracleDbType.Varchar2).Value = khuVucTrong.TenKhuVuc;
                    cmd.Parameters.Add("p_MoTa", OracleDbType.Varchar2).Value = khuVucTrong.MoTa;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task updateData(KhuVucTrong khuVucTrong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin", "oracle"))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("UpdateKhuVucTrong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = khuVucTrong.Auto_ID;
                    cmd.Parameters.Add("p_MaKhuVuc", OracleDbType.Varchar2).Value = khuVucTrong.MaKhuVuc;
                    cmd.Parameters.Add("p_TenKhuVuc", OracleDbType.Varchar2).Value = khuVucTrong.TenKhuVuc;
                    cmd.Parameters.Add("p_MoTa", OracleDbType.Varchar2).Value = khuVucTrong.MoTa;

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteData(string Auto_ID)
        {
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin", "oracle"))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("DeleteKhuVucTrong", conn))
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
