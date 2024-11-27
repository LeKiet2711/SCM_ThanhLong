using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class DBA_UserService
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public DBA_UserService(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<DBA_User>> GetAllDataUser()
        {
            List<DBA_User> lst = new List<DBA_User>();
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    string sql = "GetAllUserOfSystem";
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sql;
                    oracleCommand.CommandType = CommandType.StoredProcedure;

                    oracleCommand.Parameters.Add("tableInfo", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    kn.Open();
                    OracleDataReader read = oracleCommand.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            DBA_User user = new DBA_User();
                            user.USERNAME = read["USERNAME"].ToString();
                            user.ACCOUNT_STATUS = read["ACCOUNT_STATUS"].ToString();
                            user.LOCK_DATE = read["LOCK_DATE"].ToString();
                            user.EXPIRY_DATE = read["EXPIRY_DATE"].ToString();
                            user.CREATED = read["CREATED"].ToString();
                            user.PROFILE = read["PROFILE"].ToString();
                            user.LAST_LOGIN = read["LAST_LOGIN"].ToString();
                            lst.Add(user);
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

        public async Task<bool> UnlockAccount(string AccountName)
        {
            bool result = false;
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    string sql = "UnlockAccount";
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sql;
                    oracleCommand.CommandType = CommandType.StoredProcedure;

                    oracleCommand.Parameters.Add("AccountName", OracleDbType.Varchar2).Value = AccountName;

                    kn.Open();
                    oracleCommand.ExecuteNonQuery();
                    result = true;
                    kn.Close();
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }

        public async Task<bool> LockAccount(string AccountName)
        {
            bool result = false;
            try
            {
                using (OracleConnection kn = _dbConnection.GetConnection("sys", "sys", "SYSDBA"))
                {
                    string sql = "LockAccount";
                    OracleCommand oracleCommand = new OracleCommand();
                    oracleCommand.Connection = kn;
                    oracleCommand.CommandText = sql;
                    oracleCommand.CommandType = CommandType.StoredProcedure;

                    oracleCommand.Parameters.Add("AccountName", OracleDbType.Varchar2).Value = AccountName;

                    kn.Open();
                    oracleCommand.ExecuteNonQuery();
                    result = true;
                    kn.Close();
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }



    }
}
