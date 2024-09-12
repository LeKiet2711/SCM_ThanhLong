using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class LoaiThanhLong
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage ="Vui lòng không để trống mã loại thanh long")]
        public string MaLoaiThanhLong{get;set; }
        [Required(ErrorMessage = "Vui lòng không để trống tên loại thanh long")]
        public string TenLoaiThanhLong { get; set; }
        public LoaiThanhLong Shallowcopy()
        {
            return (LoaiThanhLong)this.MemberwiseClone();
        }
    }
}
