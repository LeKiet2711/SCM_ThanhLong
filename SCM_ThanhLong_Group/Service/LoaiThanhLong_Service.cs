using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class LoaiThanhLong_Service
    {
        private readonly OracleDbConnection _dbConnection;

        public LoaiThanhLong_Service(OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<LoaiThanhLong>> getAllData() 
        {
            List<LoaiThanhLong> dataList=new List<LoaiThanhLong>();
            using(OracleConnection conn = _dbConnection.GetConnection("C##Admin","oracle"))
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

        public LoaiThanhLong getDataByID(string id)
        {
            LoaiThanhLong data = new LoaiThanhLong();

            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin","oracle"))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetLoaiThanhLongByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Tham số đầu vào
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);

                    //Tham số đầu ra
                    cmd.Parameters.Add("p_MaLoaiThanhLong", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_TenLoaiThanhLong", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    data.Auto_ID = int.Parse(id);
                    data.MaLoaiThanhLong = cmd.Parameters["p_MaLoaiThanhLong"].Value?.ToString() ?? string.Empty;
                    data.TenLoaiThanhLong = cmd.Parameters["p_TenLoaiThanhLong"].Value?.ToString() ?? string.Empty;
                }
            }
            return data;
        }

        public async Task addData(LoaiThanhLong loaiThanhLong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin","oracle")) 
            { 
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("AddLoaiThanhLong",conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_MaLoaiThanhLong", OracleDbType.Varchar2, 50).Value = loaiThanhLong.MaLoaiThanhLong;
                    cmd.Parameters.Add("p_TenLoaiThanhLong", OracleDbType.Varchar2, 50).Value = loaiThanhLong.TenLoaiThanhLong;
                    await cmd.ExecuteNonQueryAsync();
                }

            }
        }

        public async Task updateData(LoaiThanhLong loaiThanhLong)
        {
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin","oracle"))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("UpdateLoaiThanhLong", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = loaiThanhLong.Auto_ID;
                    cmd.Parameters.Add("p_MaLoaiThanhLong", OracleDbType.Varchar2, 50).Value = loaiThanhLong.MaLoaiThanhLong;
                    cmd.Parameters.Add("p_TenLoaiThanhLong", OracleDbType.Varchar2, 50).Value = loaiThanhLong.TenLoaiThanhLong;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteData(string id)
        {
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin","oracle"))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("DELETELOAITHANHLONG", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> isMaLoaiThanhLongExist(LoaiThanhLong loaiThanhLong, List<LoaiThanhLong> lstData, string ma)
        {
            if (lstData.Any(k => k.MaLoaiThanhLong == loaiThanhLong.MaLoaiThanhLong && loaiThanhLong.MaLoaiThanhLong != ma))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> isTenLoaiThanhLongExist(LoaiThanhLong loaiThanhLong, List<LoaiThanhLong> lstData, string ma)
        {
            if (lstData.Any(k => k.TenLoaiThanhLong == loaiThanhLong.TenLoaiThanhLong && loaiThanhLong.TenLoaiThanhLong != ma))
            {
                return true;
            }
            return false;
        }

    }
}
