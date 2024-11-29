using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class PhieuXuat
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống số phiếu xuất")]
        public string SoPhieuXuat { get; set; }
        public string TenKho { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn kho")]
        public int KhoID { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày xuất hợp lệ")]
        public DateTime? NgayXuat { get; set; }
        public int isDeleted { get; set; }
        public Kho Kho { get; set; }
        public PhieuXuat Shallowcopy()
        {
            return (PhieuXuat)this.MemberwiseClone();
        }
    }
}
