using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCM_ThanhLong_Group.Service
{
    public class Profile_Service
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;

        public Profile_Service(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<Profile>> GetAllProfiles()
        {
            List<Profile> profiles = new List<Profile>();

            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;

            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
            {
                await conn.OpenAsync();
                string sql = "SELECT * FROM dba_profiles";
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var profile = new Profile
                            {
                                ProfileName = reader["PROFILE"].ToString(),
                                ResourceName = reader["RESOURCE_NAME"].ToString(),
                                ResourceType = reader["RESOURCE_TYPE"].ToString(),
                                Limit = reader["LIMIT"].ToString(),
                                Common = reader["COMMON"].ToString()
                            };
                            profiles.Add(profile);
                        }
                    }
                }
            }

            return profiles;
        }
    }
}
