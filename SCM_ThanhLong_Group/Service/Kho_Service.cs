using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class Kho_Service
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public Kho_Service(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<Kho>> getAllData()
        {
            List<Kho> dataList=new List<Kho>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username,_user.password))
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

        public Kho getDataByID(string id)
        {
            Kho data = new Kho();

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("C##Admin.GetKhoByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số đầu vào
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = int.Parse(id);

                    // Thêm các tham số đầu ra
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_TenKho", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;

                    // Thực thi stored procedure
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["p_MaKho"].Value.ToString() == "null")
                    {
                        data.MaKho = "";
                    }
                    else
                    {
                        data.MaKho= cmd.Parameters["p_MaKho"].Value.ToString();
                    }

                    if (cmd.Parameters["p_TenKho"].Value.ToString() == "null")
                    {
                        data.TenKho = "";
                    }
                    else
                    {
                        data.TenKho = cmd.Parameters["p_TenKho"].Value.ToString();
                    }

                    if (cmd.Parameters["p_DiaChi"].Value.ToString() == "null")
                    {
                        data.DiaChi = "";
                    }
                    else
                    {
                        data.DiaChi = cmd.Parameters["p_DiaChi"].Value?.ToString() ?? string.Empty;
                    }


                }
            }
            return data;
        }

        public async Task addData(Kho kho)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.AddKho", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_MaKho", OracleDbType.Varchar2).Value = kho.MaKho;
                    cmd.Parameters.Add("p_TenKho", OracleDbType.Varchar2).Value = kho.TenKho;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2).Value = kho.DiaChi;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task updateData(Kho kho)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.UpdateKho", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = kho.Auto_ID;
                    cmd.Parameters.Add("p_MaKho", OracleDbType.Varchar2).Value = kho.MaKho;
                    cmd.Parameters.Add("p_TenKho", OracleDbType.Varchar2).Value = kho.TenKho;
                    cmd.Parameters.Add("p_DiaChi", OracleDbType.Varchar2).Value = kho.DiaChi;

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task deleteData(string Auto_ID)
        {
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                await conn.OpenAsync();
                using (OracleCommand cmd = new OracleCommand("C##Admin.DeleteKho", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Auto_ID", OracleDbType.Int32).Value = Auto_ID;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> isMaKhoExist(Kho kho, List<Kho> lstData, string ma)
        {
            if (lstData.Any(k => k.MaKho == kho.MaKho && kho.MaKho != ma))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> isTenKhoExist(Kho kho, List<Kho> lstData, string ma)
        {
            if (lstData.Any(k => k.TenKho == kho.TenKho && kho.TenKho != ma))
            {
                return true;
            }
            return false;
        }

        public async Task<List<Users>> getAllData2()
        {
            List<Users> dataList = new List<Users>();
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("SELECT * FROM SYS.ALL_USERS", conn))
                {
                    cmd.CommandType = CommandType.Text;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new Users
                            {
                                username = reader["USERNAME"].ToString(),

                            };
                            dataList.Add(data);
                        }

                    }
                }
            }
            return dataList;
        }

    }
}
