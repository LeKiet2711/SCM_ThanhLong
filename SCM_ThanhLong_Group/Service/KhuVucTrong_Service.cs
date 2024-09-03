using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Components.Connection;
using System.Data;


namespace SCM_ThanhLong_Group.Service
{
    public class KhuVucTrong_Service
    {

        private readonly OracleDbConnection _dbConnection;

        public KhuVucTrong_Service(OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<KhuVucTrong>> getAllData()
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
                                Auto_ID= int.Parse(reader["Auto_ID"].ToString()),
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


        public KhuVucTrong getDataByID(string id)
        {
            KhuVucTrong data = new KhuVucTrong();

            using (OracleConnection conn = _dbConnection.GetConnection())
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

                    if (cmd.Parameters["p_MaKhuVuc"].Value.ToString() == "null")
                    {
                        data.MaKhuVuc = "";
                    }
                    else
                    {
                        data.MaKhuVuc = cmd.Parameters["p_MaKhuVuc"].Value.ToString();
                    }

                    if (cmd.Parameters["p_TenKhuVuc"].Value.ToString() == "null")
                    {
                        data.TenKhuVuc = "";
                    }
                    else
                    {
                        data.TenKhuVuc = cmd.Parameters["p_TenKhuVuc"].Value.ToString();
                    }

                    if (cmd.Parameters["p_MoTa"].Value.ToString() == "null")
                    {
                        data.MoTa="";
                    }
                    else
                    {
                        data.MoTa = cmd.Parameters["p_MoTa"].Value?.ToString() ?? string.Empty;
                    }
                }
            }
            return data;
        }


        public async Task addData(KhuVucTrong khuVucTrong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection())
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
            using (OracleConnection conn = _dbConnection.GetConnection())
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
            using (OracleConnection conn = _dbConnection.GetConnection())
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
            if (lstData.Any(k => k.MaKhuVuc == khuVucTrong.MaKhuVuc && khuVucTrong.MaKhuVuc != ma))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> isTenKhuVucExist(KhuVucTrong khuVucTrong, List<KhuVucTrong> lstData, string ma)
        {
            if (lstData.Any(k => k.TenKhuVuc == khuVucTrong.TenKhuVuc && khuVucTrong.TenKhuVuc != ma))
            {
                return true;
            }
            return false;
        }

    }
}
