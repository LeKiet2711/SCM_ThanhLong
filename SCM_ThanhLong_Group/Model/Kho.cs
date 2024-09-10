using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class Kho
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage="Vui lòng không để trống mã kho")]
        public string MaKho { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống tên kho")]
        public string TenKho { get; set; }
        public string DiaChi { get; set; }

        public Kho Shallowcopy()
        {
            return (Kho)this.MemberwiseClone();
        }
    }
}
