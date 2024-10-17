using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class ChiTietPhieuXuat
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống số phiếu xuất")]
        public int SoPhieuXuat { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn loại thanh long")]
        public int MaLo { get; set; }
        public string MaLoSTR { get; set; }
        public double SoKg { get; set; }
        public double DonGia { get; set; }
        public double TongTien { get; set; }


        public ChiTietPhieuXuat Shallowcopy()
        {
            return (ChiTietPhieuXuat)this.MemberwiseClone();
        }
    }
}
