using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class LoThanhLong
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống Mã lô hàng")]
        public string MaLo { get; set; }

        [Required(ErrorMessage = "Vui lòng không để trống hộ trồng")]
        public int HoTrongID { get; set; }

        public string TenHoTrong { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày thu hoạch hợp lệ")]
        public DateTime NgayThuHoach { get; set; }
        //public string QRCode { get; set; }
        public string TrangThai { get;set; }
        public string MoTa { get; set; }
        public int isDeleted { get; set; }
        public HoTrong HoTrong { get; set; }
        public LoThanhLong ShallowCopy()
        {
            return (LoThanhLong)this.MemberwiseClone();
        }
    }
}
