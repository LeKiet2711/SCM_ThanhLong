using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class NhaPhanPhoi
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống mã nhà phân phối")]
        public string MaNhaPhanPhoi { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống tên nhà phân phối")]
        public string TenNhaPhanPhoi { get; set; }
        
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }

        public NhaPhanPhoi ShallowCopy()
        {
            return (NhaPhanPhoi)this.MemberwiseClone();
        }
    }
}
