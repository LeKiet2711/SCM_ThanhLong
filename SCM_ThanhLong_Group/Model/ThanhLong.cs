using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class ThanhLong
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống mã nhà phân phối")]
        public string MaThanhLong { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống tên nhà phân phối")]
        public int LoaiThanhLongID { get; set; }
        public string TenLoaiThanhLong { get; set; }
        public int HoTrongID { get; set; }
        public string TenHoTrong { get; set; }
        public DateTime NgayThuHoach { get; set; }
        public string QRCode { get; set; }
        public string TrangThai { get;set; }
        public int isDeleted { get; set; }
        public HoTrong HoTrong { get; set; }
        public ThanhLong ShallowCopy()
        {
            return (ThanhLong)this.MemberwiseClone();
        }
    }
}
