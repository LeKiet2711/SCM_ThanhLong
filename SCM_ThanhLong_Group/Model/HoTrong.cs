namespace SCM_ThanhLong_Group.Model
{
    public class HoTrong
    {
        public int Auto_ID { get; set; }
        public string MaHoTrong { get; set; }
        public string TenHoTrong { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public int KhuVucTrongID { get; set; }
        public KhuVucTrong KhuVucTrong { get; set; }

        public HoTrong ShallowCopy()
        {
            return (HoTrong)this.MemberwiseClone();
        }
    }
}
