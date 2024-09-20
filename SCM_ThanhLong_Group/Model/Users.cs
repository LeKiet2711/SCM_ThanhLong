using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using Telerik.SvgIcons;

namespace SCM_ThanhLong_Group.Model
{
    public class Users
    {
        [Required(ErrorMessage = "Vui lòng không để trống tên đăng nhập")]
        public string username { get; set; }
        
        string email{ get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống mật khẩu")]
        public string password;

        [Required(ErrorMessage = "Vui lòng nhập đúng mật khẩu")]
        public string repassword;
        bool rememberMe;

        public void Reset()
        {
            username = null;
            password = null;
        }
    }
}
