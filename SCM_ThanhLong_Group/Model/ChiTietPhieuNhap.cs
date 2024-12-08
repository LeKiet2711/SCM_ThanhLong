using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class ChiTietPhieuNhap
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống số phiếu nhập")]
        public int SoPhieuNhap { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn lô thanh long")]
        public int MaLo { get; set; }
        public string MaLoSTR { get; set; }
        public double SoKg { get; set; }
        public double DonGia { get; set; }
        public double TongTien { get;set; }


        public ChiTietPhieuNhap Shallowcopy()
        {
            return (ChiTietPhieuNhap)this.MemberwiseClone();
        }

    }
}
