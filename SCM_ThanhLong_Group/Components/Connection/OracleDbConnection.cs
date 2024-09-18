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

        public OracleConnection GetConnection(string userId, string password)
        {
            string connectionString = _connectionStringManager.GetConnectionString(userId, password);
            return new OracleConnection(connectionString);
        }
    }
}
