using SCM_ThanhLong_Group.Model;

namespace SCM_ThanhLong_Group.Interface
{
    public interface IAuditRepository
    {
        Task<List<UnifiedAuditTrail>> GetAuditKhuVucTrong(string loaiAudit);
        Task<List<KhuVucTrongAudit>> GetTriggerAuditKhuVucTrong();
        Task<List<string>> GET_ALL_TABLE_NAME_OF_USER();
        Task<bool> create_audit_policy_standard(string auditName, string auditActions, string auditTable);

    }
}
