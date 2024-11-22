using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using System.Data;
using System.Data.Common;
namespace SCM_ThanhLong_Group.Service
{
    public class OracleAuditService
    {
        private readonly OracleDbConnection _dbConnection;
        private readonly Users _user;
        public OracleAuditService(OracleDbConnection dbConnection, Users user)
        {
            _dbConnection = dbConnection;
            _user = user;
        }

        public async Task<List<AuditPolicy>> GetAuditPoliciesAsync()
        {
            var auditPolicies = new List<AuditPolicy>();
            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
            {
                await conn.OpenAsync();
                string query = "SELECT OBJECT_SCHEMA, OBJECT_NAME, POLICY_NAME, AUDIT_COLUMN FROM DBA_FGA_POLICIES";

                using (var command = new OracleCommand(query, conn))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var policy = new AuditPolicy
                            {
                                ObjectSchema = reader.GetString(reader.GetOrdinal("OBJECT_SCHEMA")),
                                ObjectName = reader.GetString(reader.GetOrdinal("OBJECT_NAME")),
                                PolicyName = reader.GetString(reader.GetOrdinal("POLICY_NAME")),
                                AuditColumn = reader.GetString(reader.GetOrdinal("AUDIT_COLUMN"))
                            };

                            auditPolicies.Add(policy);
                        }
                    }
                }
            }

            return auditPolicies;
        }

        // Thêm chính sách FGA
        public async Task AddFGAAsync(string objectSchema, string objectName, string policyName, string auditColumn, string condition)
        {
            string dbaPrivilege = _user.username.Equals("sys", StringComparison.OrdinalIgnoreCase) ? "SYSDBA" : null;
            using (OracleConnection conn = _dbConnection.GetConnection(_user.username, _user.password, dbaPrivilege))
            {
                await conn.OpenAsync();
                string commandText = "BEGIN " +
                                     "  DBMS_FGA.add_policy(" +
                                     "    object_schema   => :p_object_schema, " +
                                     "    object_name     => :p_object_name, " +
                                     "    policy_name     => :p_policy_name, " +
                                     "    audit_column    => :p_audit_column, " +
                                     "    audit_condition => :p_condition, " +
                                     "    enable          => TRUE); " +
                                     "END;";

                using (var command = new OracleCommand(commandText, conn))
                {
                    command.Parameters.Add(new OracleParameter("p_object_schema", objectSchema));
                    command.Parameters.Add(new OracleParameter("p_object_name", objectName));
                    command.Parameters.Add(new OracleParameter("p_policy_name", policyName));
                    command.Parameters.Add(new OracleParameter("p_audit_column", auditColumn));
                    command.Parameters.Add(new OracleParameter("p_condition", condition));

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }

    public class AuditPolicy
    {
        public string ObjectSchema { get; set; }
        public string ObjectName { get; set; }
        public string PolicyName { get; set; }
        public string AuditColumn { get; set; }
        public string Condition { get; set; }  // Thêm thuộc tính này
    }
}
