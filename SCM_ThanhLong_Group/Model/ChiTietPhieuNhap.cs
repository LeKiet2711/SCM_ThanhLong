using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class ChiTietPhieuNhap
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống số phiếu nhập")]
        public string SoPhieuNhap { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn loại thanh long")]
        
        public int MaLoaiThanhLong { get; set; }
        public string TenLoaiThanhLong { get; set;}
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double TongTien { get;set; }

        public LoaiThanhLong LoaiThanhLong { get; set; }
    }
}
