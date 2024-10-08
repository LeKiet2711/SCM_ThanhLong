using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace SCM_ThanhLong_Group.Components.Connection
{
    public class OracleDbConnection
    {
        private readonly ConnectionStringManager _connectionStringManager;

        public OracleDbConnection(ConnectionStringManager connectionStringManager)
        {
            _connectionStringManager = connectionStringManager;
        }

        public OracleConnection GetConnection(string userId, string password, string dbaPrivilege = null)
        {
            var connectionStringBuilder = new OracleConnectionStringBuilder(_connectionStringManager.GetConnectionString(userId, password))
            {
                UserID = userId,
                Password = password
            };

            if (!string.IsNullOrEmpty(dbaPrivilege))
            {
                connectionStringBuilder.DBAPrivilege = dbaPrivilege;
            }

            return new OracleConnection(connectionStringBuilder.ToString());
        }
    }
}
