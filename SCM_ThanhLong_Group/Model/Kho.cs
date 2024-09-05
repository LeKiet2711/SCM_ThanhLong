namespace SCM_ThanhLong_Group.Model
{
    public class Kho
    {
        public int Auto_ID { get; set; }
        public string MaKho { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }

        public Kho Shallowcopy()
        {
            return (Kho)this.MemberwiseClone();
        }
    }
}
