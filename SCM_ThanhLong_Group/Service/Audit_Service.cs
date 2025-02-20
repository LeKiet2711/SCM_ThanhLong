using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;

namespace SCM_ThanhLong_Group.Service
{
    public class Audit_Service
    {
        private readonly IAuditRepository _auditRepository;
        public Audit_Service(IAuditRepository auditRepository)
        {
            _auditRepository=auditRepository;
        }

        public async Task<List<UnifiedAuditTrail>> GetAuditKhuVucTrong(string loaiAudit)
        {
            return await _auditRepository.GetAuditKhuVucTrong(loaiAudit);
        }

        public async Task<List<KhuVucTrongAudit>> GetTriggerAuditKhuVucTrong()
        {
            return await _auditRepository.GetTriggerAuditKhuVucTrong();
        }

        public async Task<List<string>> GET_ALL_TABLE_NAME_OF_USER()
        {
            return await _auditRepository.GET_ALL_TABLE_NAME_OF_USER();
        }
        
        public async Task<bool> create_audit_policy_standard(string auditName, string auditActions, string auditTable)
        {
            return await _auditRepository.create_audit_policy_standard(auditName, auditActions, auditTable);
        }
    }
}
