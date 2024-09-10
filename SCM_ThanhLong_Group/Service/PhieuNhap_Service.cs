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
                                TenNhaPhanPhoi = reader["TenNhaPhanPhoi"].ToString(),
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
                    cmd.Parameters.Add("p_TenNhaPhanPhoi", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_MaNhaPhanPhoi", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    data.Auto_ID = int.Parse(id);
                    data.SoPhieuNhap = cmd.Parameters["p_SoPhieuNhap"].Value?.ToString() ?? string.Empty;
                    data.TenNhaPhanPhoi = cmd.Parameters["p_TenNhaPhanPhoi"].Value?.ToString() ?? string.Empty;
                    data.NgayNhap = DateTime.Parse(cmd.Parameters["p_NgayNhap"].Value?.ToString() ?? string.Empty);
                    
                    data.NhaPhanPhoiID = cmd.Parameters["p_NhaPhanPhoiID"].Value != DBNull.Value
                        ? Convert.ToInt32(cmd.Parameters["p_NhaPhanPhoiID"].Value)
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
                    cmd.Parameters.Add("p_TenNhaPhanPhoi", OracleDbType.Varchar2, 50).Value = phieuNhap.TenNhaPhanPhoi;
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Varchar2, 50).Value = phieuNhap.NgayNhap;
                    cmd.Parameters.Add("p_NhaPhanPhoiID", OracleDbType.Int32, 50).Value = phieuNhap.NhaPhanPhoiID;
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
                    cmd.Parameters.Add("p_TenNhaPhanPhoi", OracleDbType.Varchar2, 50).Value = phieuNhap.TenNhaPhanPhoi;
                    cmd.Parameters.Add("p_NgayNhap", OracleDbType.Varchar2, 50).Value = phieuNhap.NgayNhap;
                    cmd.Parameters.Add("p_NhaPhanPhoiID", OracleDbType.Int32, 50).Value = phieuNhap.NhaPhanPhoiID;
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

    }
}
