using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace SCM_ThanhLong_Group.Components.Connection
{
    public class OracleDbConnection
    {
        private string _connectionString;

        public OracleDbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public OracleConnection GetConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}
