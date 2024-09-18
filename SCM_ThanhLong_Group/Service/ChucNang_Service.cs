using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;
using Telerik.SvgIcons;

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
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin","oracle"))
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

        public async Task updateData(ChucNang data)
        {
            using (OracleConnection conn = _dbConnection.GetConnection("C##Admin","oracle"))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("UpdateChucNang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_MACN", OracleDbType.Int32).Value = data.MaCN;
                    cmd.Parameters.Add("p_TENCN", OracleDbType.Varchar2, 50).Value = data.TenCN;
                    cmd.Parameters.Add("p_XEM", OracleDbType.Int32).Value = data.Xem ? 1 : 0; 
                    cmd.Parameters.Add("p_THEM", OracleDbType.Int32).Value = data.Them ? 1 : 0; 
                    cmd.Parameters.Add("p_SUA", OracleDbType.Int32).Value = data.Sua ? 1 : 0;  
                    cmd.Parameters.Add("p_XOA", OracleDbType.Int32).Value = data.Xoa ? 1 : 0;  

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
