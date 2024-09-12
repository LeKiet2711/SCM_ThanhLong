using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class PhieuNhap
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống số phiếu nhập")]
        public string SoPhieuNhap { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống tên nhà phân phối")]
        public string TenHoTrong { get; set; }
        public int HoTrongID { get; set; }
        public string TenKho { get; set; }
        public int KhoID { get; set; }
        public DateTime NgayNhap { get; set; }
        public int isDeleted { get; set; }
        public HoTrong HoTrong { get; set; }
        public Kho Kho { get; set; }
        public PhieuNhap Shallowcopy()
        {
           return (PhieuNhap)this.MemberwiseClone();
        }
    }
}
