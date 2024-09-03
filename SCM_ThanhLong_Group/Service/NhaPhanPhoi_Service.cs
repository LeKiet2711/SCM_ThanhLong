using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class NhaPhanPhoi_Service
    {
        private readonly OracleDbConnection _dbConnection;

        public NhaPhanPhoi_Service (OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<NhaPhanPhoi>> getAllData()
        {
            List<NhaPhanPhoi> dataList= new List<NhaPhanPhoi>();
            using(OracleConnection conn= _dbConnection.GetConnection())
            {
                conn.Open();
                using(OracleCommand cmd= new OracleCommand("GetAllNhaPhanPhoi",conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new NhaPhanPhoi
                            {
                                Auto_ID = int.Parse(reader["Auto_ID"].ToString()),
                                MaNhaPhanPhoi = reader["MaNhaPhanPhoi"].ToString(),
                                TenNhaPhanPhoi = reader["TenNhaPhanPhoi"].ToString(),
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

        public NhaPhanPhoi getDataByID(string id)
        {
            NhaPhanPhoi data = new NhaPhanPhoi();

            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetNhaPhanPhoiByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);
                    cmd.Parameters.Add("p_MaNhaPhanPhoi", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_TenNhaPhanPhoi", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_SoDienThoai", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["p_MaNhaPhanPhoi"].Value.ToString()=="null")
                    {
                        data.MaNhaPhanPhoi = "";
                    }
                    else
                    {
                        data.MaNhaPhanPhoi = cmd.Parameters["p_MaNhaPhanPhoi"].Value.ToString();
                    }

                    if (cmd.Parameters["p_TenNhaPhanPhoi"].Value.ToString() == "null")
                    {
                        data.TenNhaPhanPhoi="";
                    }
                    else
                    {
                        data.TenNhaPhanPhoi = cmd.Parameters["p_TenNhaPhanPhoi"].Value.ToString();
                    }

                    if (cmd.Parameters["p_DiaChi"].Value.ToString() == "null")
                    {
                        data.DiaChi = "";
                    }
                    else
                    {
                        data.DiaChi = cmd.Parameters["p_DiaChi"].Value.ToString();
                    }

                    if (cmd.Parameters["p_SoDienThoai"].Value.ToString() == "null")
                    {
                        data.SoDienThoai = "";
                    }
                    else
                    {
                        data.SoDienThoai = cmd.Parameters["p_SoDienThoai"].Value.ToString();
                    }
                   
                }
            }
            return data;
        }


        public async Task addData(NhaPhanPhoi NhaPhanPhoi)
        {
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("AddNhaPhanPhoi", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_MaNhaPhanPhoi", OracleDbType.Varchar2).Value = NhaPhanPhoi.MaNhaPhanPhoi;
                    cmd.Parameters.Add("p_TenNhaPhanPhoi", OracleDbType.Varchar2).Value = NhaPhanPhoi.TenNhaPhanPhoi;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2).Value = NhaPhanPhoi.DiaChi;
                    cmd.Parameters.Add("p_SoDienThoai", OracleDbType.Varchar2).Value = NhaPhanPhoi.SoDienThoai;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task updateData(NhaPhanPhoi data)
        {
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("UpdateNhaPhanPhoi", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = data.Auto_ID;
                    cmd.Parameters.Add("p_MaNhaPhanPhoi", OracleDbType.Varchar2, 50).Value = data.MaNhaPhanPhoi;
                    cmd.Parameters.Add("p_TenNhaPhanPhoi", OracleDbType.Varchar2, 50).Value = data.TenNhaPhanPhoi;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2, 100).Value = data.DiaChi;
                    cmd.Parameters.Add("p_SoDienThoai", OracleDbType.Varchar2, 20).Value = data.SoDienThoai;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public async Task deleteData(string Auto_ID)
        {
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("DeleteNhaPhanPhoi", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = Auto_ID;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> isMaNhaPhanPhoiExist(NhaPhanPhoi NhaPhanPhoi, List<NhaPhanPhoi> lstData, string ma)
        {
            if (lstData.Any(k => k.MaNhaPhanPhoi == NhaPhanPhoi.MaNhaPhanPhoi && NhaPhanPhoi.MaNhaPhanPhoi != ma))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> isTenNhaPhanPhoiExist(NhaPhanPhoi NhaPhanPhoi, List<NhaPhanPhoi> lstData, string ma)
        {
            if (lstData.Any(k => k.MaNhaPhanPhoi == NhaPhanPhoi.MaNhaPhanPhoi && NhaPhanPhoi.TenNhaPhanPhoi != ma))
            {
                return true;
            }
            return false;
        }

    }
}
