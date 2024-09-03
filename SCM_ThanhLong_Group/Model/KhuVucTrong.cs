using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class KhuVucTrong
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Mã khu vực không được để trống.")]
        public string MaKhuVuc { get; set; }
        [Required(ErrorMessage = "Tên khu vực không được để trống.")]
        public string TenKhuVuc { get; set; }
        public string MoTa { get; set; }

        public ICollection<HoTrong> HoTrongs { get; set; }
        public KhuVucTrong ShallowCopy()
        {
            return (KhuVucTrong)this.MemberwiseClone();
        }
    }
}
