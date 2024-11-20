namespace SCM_ThanhLong_Group.Model
{
    public class UnifiedAuditTrail
    {
        public string DBUSERNAME { get; set; }
        public string ACTION_NAME { get; set; }
        public string OBJECT_SCHEMA { get; set; }
        public string OBJECT_NAME { get; set; }
        public string EVENT_TIMESTAMP { get; set; }
        public string SQL_TEXT { get; set; }
        public UnifiedAuditTrail() { }

        public UnifiedAuditTrail(string dBUSERNAME, string aCTION_NAME, string oBJECT_SCHEMA, string oBJECT_NAME, string eVENT_TIMESTAMP, string sQL_TEXT)
        {
            DBUSERNAME = dBUSERNAME;
            ACTION_NAME = aCTION_NAME;
            OBJECT_SCHEMA = oBJECT_SCHEMA;
            OBJECT_NAME = oBJECT_NAME;
            EVENT_TIMESTAMP = eVENT_TIMESTAMP;
            SQL_TEXT = sQL_TEXT;
        }
    }
}
