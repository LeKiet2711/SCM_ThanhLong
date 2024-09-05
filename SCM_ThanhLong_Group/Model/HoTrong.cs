using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class HoTrong
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống mã hộ trồng")]
        public string MaHoTrong { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống tên hộ trồng")]
        public string TenHoTrong { get; set; }
        public string DiaChi { get; set; }
        [Required(ErrorMessage ="Vui lòng không để trống số điện thoại")]
        public string SoDienThoai { get; set; }
        public int isDeleted { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn khu vực")]
        public int KhuVucTrongID { get; set; }
        public string TenKhuVucTrong { get; set; }
        public KhuVucTrong KhuVucTrong { get; set; }

        public HoTrong ShallowCopy()
        {
            return (HoTrong)this.MemberwiseClone();
        }
    }
}
