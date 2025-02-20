using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;

namespace SCM_ThanhLong_Group.Repository
{
    public class AuditRepository:IAuditRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public AuditRepository(OracleDbConnection dbConnection, Users user)
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
                    if (read.HasRows)
                    {
                        while (read.Read())
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

        public async Task<List<KhuVucTrongAudit>> GetTriggerAuditKhuVucTrong()
        {
            List<KhuVucTrongAudit> lst = new List<KhuVucTrongAudit>();
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    string sqlKillSession = "GetTriggerAuditKhuVucTrong";
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sqlKillSession;
                    oracleCommand.CommandType = CommandType.StoredProcedure;

                    oracleCommand.Parameters.Add("tableInfor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    kn.OpenAsync();
                    OracleDataReader read = oracleCommand.ExecuteReader();

                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            KhuVucTrongAudit audit = new KhuVucTrongAudit();
                            audit.AUDIT_ID = read["AUDIT_ID"].ToString();
                            audit.USER_NAME = read["USER_NAME"].ToString();
                            audit.ACTION = read["ACTION"].ToString();
                            audit.OLD_MAKHV = read["OLD_MAKHV"].ToString();
                            audit.NEW_MAKHV = read["NEW_MAKHV"].ToString();
                            audit.OLD_TENKV = read["OLD_TENKV"].ToString();
                            audit.NEW_TENKV = read["NEW_TENKV"].ToString();
                            audit.OLD_MOTA = read["OLD_MOTA"].ToString();
                            audit.NEW_MOTA = read["NEW_MOTA"].ToString();
                            audit.OLD_ISDELETED = read["OLD_ISDELETED"].ToString();
                            audit.NEW_ISDELETED = read["NEW_ISDELETED"].ToString();
                            audit.CHANGE_DATE = read["CHANGE_DATE"].ToString();
                            lst.Add(audit);
                        }
                    }
                    kn.CloseAsync();

                }
            }
            catch (Exception e)
            {

            }
            return lst;
        }

        public async Task<List<string>> GET_ALL_TABLE_NAME_OF_USER()
        {
            List<string> lst = new List<string>();
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    OracleCommand oracleCommand = new OracleCommand("GET_ALL_TABLE_NAME_OF_USER", kn);
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add("USERNAMEIN", OracleDbType.Varchar2).Value = "C##ADMIN";
                    oracleCommand.Parameters.Add("TABLEOUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    kn.Open();
                    OracleDataReader r = oracleCommand.ExecuteReader();
                    if (r.HasRows)
                    {
                        while (r.Read())
                        {
                            lst.Add(r["TABLE_NAME"].ToString());
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

        public async Task<bool> create_audit_policy_standard(string auditName, string auditActions, string auditTable)
        {
            bool result = false;
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("C##ADMIN", "oracle"))//"sys", "sys", "SYSDBA"
                {
                    OracleCommand oracleCommand = new OracleCommand("create_audit_policy_standard", kn);
                    oracleCommand.Parameters.Add("audit_name", OracleDbType.Varchar2).Value = auditName;
                    oracleCommand.Parameters.Add("audit_actions", OracleDbType.Varchar2).Value = auditActions;
                    oracleCommand.Parameters.Add("audit_object_schema", OracleDbType.Varchar2).Value = "C##ADMIN";
                    oracleCommand.Parameters.Add("audit_object_name", OracleDbType.Varchar2).Value = auditTable;
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    kn.Open();
                    oracleCommand.ExecuteNonQuery();
                    kn.Close();
                    result = true;
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
    }
}
