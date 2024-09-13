using SCM_ThanhLong_Group.Components.Connection;

namespace SCM_ThanhLong_Group.Service
{
    public class ChiTietPhieuNhap_Service
    {
        private readonly OracleDbConnection _dbConnection;

        public ChiTietPhieuNhap_Service(OracleDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }



    }
}
