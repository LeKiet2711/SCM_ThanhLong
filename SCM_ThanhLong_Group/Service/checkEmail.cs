using Oracle.ManagedDataAccess.Client;
using SCM_ThanhLong_Group.Components.Connection;
using SCM_ThanhLong_Group.Model;
using SCM_ThanhLong_Group.Interface;
using System.Data;
using Oracle.ManagedDataAccess.Types;
namespace SCM_ThanhLong_Group.Service
{
    public class checkEmail
    {
        private readonly ICheckEmail _checkEmailRepository;
        public checkEmail(ICheckEmail checkEmailRepository)
        {
            _checkEmailRepository=checkEmailRepository;
        }
        public async Task<bool> checkEmailUser(string gmail)
        {
            return await _checkEmailRepository.checkEmailUser(gmail);
        }
        public async Task<bool> GrantUserAccess(string username)
        {          
            return await _checkEmailRepository.GrantUserAccess(username);
        }
        public async Task<string> layGmail(string username)
        {
            return await _checkEmailRepository.layGmail(username);
        }
    }
}
    

