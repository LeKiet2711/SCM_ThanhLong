using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;
using Blazored.SessionStorage;
using Oracle.ManagedDataAccess.Types;

namespace SCM_ThanhLong_Group.Service

{
    public class KeyValidationService
    {
        private readonly IKeyValidationRepository _keyValidationRepository;
        public KeyValidationService(IKeyValidationRepository keyValidationRepository)
        {
            _keyValidationRepository= keyValidationRepository;
        }

        public async Task<bool> ValidateKey(string userKey)
        {
            return await _keyValidationRepository.ValidateKey(userKey);
        }
      
    }
}
