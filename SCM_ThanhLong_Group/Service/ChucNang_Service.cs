using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Service
{
    public class ChucNang_Service
    {
        private readonly OracleDbConnection _dbConnection;

        public ChucNang_Service(OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<ChucNang>> getAllData()
        {
            List<ChucNang> dataList = new List<ChucNang>();
            using (OracleConnection conn = _dbConnection.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GetAllChucNang", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new ChucNang
                            {
                                MaCN = int.Parse(reader["MaCN"].ToString()),
                                TenCN = reader["TenCN"].ToString(),
                                Xem = int.Parse(reader["Xem"].ToString()) == 1,
                                Them = int.Parse(reader["Them"].ToString()) == 1,
                                Sua = int.Parse(reader["Sua"].ToString()) == 1,
                                Xoa = int.Parse(reader["Xoa"].ToString()) == 1,
                            };
                            dataList.Add(data);
                        }

                    }
                }
            }
            return dataList;
        }

        public async Task<List<ChucNang>> updateData(ChucNang chucnang)
        {
            List<ChucNang> dataList = new List<ChucNang>();

            return dataList;
        }

    }
}
