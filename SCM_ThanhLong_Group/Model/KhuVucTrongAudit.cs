namespace SCM_ThanhLong_Group.Model
{
    public class KhuVucTrongAudit
    {
        public string AUDIT_ID { get; set; }
        public string USER_NAME { get; set; }
        public string ACTION { get; set; }
        public string OLD_MAKHV { get; set; }
        public string NEW_MAKHV {  set; get; }
        public string OLD_TENKV { get; set; }
        public string NEW_TENKV {  set; get; }
        public string OLD_MOTA {  get; set; }
        public string NEW_MOTA { set; get; }
        public string OLD_ISDELETED { get; set; }
        public string NEW_ISDELETED { set; get; }
        public string CHANGE_DATE { get; set; }
        public KhuVucTrongAudit() { }

        public KhuVucTrongAudit(string aUDIT_ID, string uSER_NAME, string aCTION, string oLD_MAKHV, string nEW_MAKHV, string oLD_TENKHV, string nEW_TENKHV, string oLD_MOTA, string nEW_MOTA, string oLD_ISDELETED, string nEW_ISDELETED, string cHANGE_DATE)
        {
            AUDIT_ID = aUDIT_ID;
            USER_NAME = uSER_NAME;
            ACTION = aCTION;
            OLD_MAKHV = oLD_MAKHV;
            NEW_MAKHV = nEW_MAKHV;
            OLD_TENKV = oLD_TENKHV;
            NEW_TENKV = nEW_TENKHV;
            OLD_MOTA = oLD_MOTA;
            NEW_MOTA = nEW_MOTA;
            OLD_ISDELETED = oLD_ISDELETED;
            NEW_ISDELETED = nEW_ISDELETED;
            CHANGE_DATE = cHANGE_DATE;
        }
    }
}
