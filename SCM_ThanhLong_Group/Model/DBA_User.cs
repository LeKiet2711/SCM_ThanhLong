namespace SCM_ThanhLong_Group.Model
{
    public class DBA_User
    {
        public string USERNAME { get; set; }
        public string ACCOUNT_STATUS { get; set; }
        public string LOCK_DATE { get; set; }
        public string EXPIRY_DATE { get; set; }
        public string CREATED { get; set; }
        public string PROFILE { get; set; }
        public string LAST_LOGIN { get; set; }
        public DBA_User(string username, string account_status, string lock_date, string expiry_date, string created, string profile, string last_login)
        {
            USERNAME = username;
            ACCOUNT_STATUS = account_status;
            LOCK_DATE = lock_date;
            EXPIRY_DATE = expiry_date;
            CREATED = created;
            PROFILE = profile;
            LAST_LOGIN = last_login;
        }

        public DBA_User()
        {
        }


    }
}
