using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace SCM_ThanhLong_Group.Components.Connection
{
    public class ConnectionStringManager
    {
        private readonly IConfiguration _configuration;
        private string _baseConnectionString;

        public ConnectionStringManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public string GetConnectionString(string userId, string password)
        {
            var builder = new OracleConnectionStringBuilder(_baseConnectionString)
            {
                UserID = userId,
                Password = password
            };
            return builder.ToString();
        }
    }
}
