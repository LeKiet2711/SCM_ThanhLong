using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class Users
    {
        [Required(ErrorMessage = "Vui lòng không để trống tên đăng nhập")]
        public string name { get; set; }
        
        string email{ get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống mật khẩu")]
        public string password;

        [Required(ErrorMessage = "Vui lòng nhập đúng mật khẩu")]
        public string repassword;
        bool rememberMe;

       

    }
}
