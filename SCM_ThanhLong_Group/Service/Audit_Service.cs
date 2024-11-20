using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class Audit_Service
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public Audit_Service(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<UnifiedAuditTrail>> GetAuditKhuVucTrong(string loaiAudit)
        {
            List<UnifiedAuditTrail> lst = new List<UnifiedAuditTrail>();
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    string sqlKillSession = "GetAuditKhuVucTrong";
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sqlKillSession;
                    oracleCommand.CommandType = CommandType.StoredProcedure;

                    oracleCommand.Parameters.Add("dieuKien", OracleDbType.Varchar2).Value = loaiAudit;
                    oracleCommand.Parameters.Add("tableInfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    kn.Open();
                    OracleDataReader read = oracleCommand.ExecuteReader();
                    if(read.HasRows)
                    {
                        while(read.Read())
                        {
                            UnifiedAuditTrail audit = new UnifiedAuditTrail();
                            audit.EVENT_TIMESTAMP = read["EVENT_TIMESTAMP"].ToString();
                            audit.SQL_TEXT = read["SQL_TEXT"].ToString();
                            audit.OBJECT_SCHEMA = read["OBJECT_SCHEMA"].ToString();
                            audit.OBJECT_NAME = read["OBJECT_NAME"].ToString();
                            audit.ACTION_NAME = read["ACTION_NAME"].ToString();
                            audit.DBUSERNAME = read["DBUSERNAME"].ToString();
                            lst.Add(audit);
                        }    
                    }    
                    kn.Close();
                }
            }
            catch (Exception e)
            {

            }
            return lst;
        }

    }
}
