﻿using System.ComponentModel.DataAnnotations;

namespace SCM_ThanhLong_Group.Model
{
    public class PhieuNhap
    {
        public int Auto_ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống số phiếu nhập")]
        public string SoPhieuNhap { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống tên nhà phân phối")]
        public string TenHoTrong { get; set; }
        [Required(ErrorMessage ="Vui lòng chọn hộ trồng")]
        public int HoTrongID { get; set; }
        public string TenKho { get; set; }
        [Required(ErrorMessage ="Vui lòng chọn kho")]
        public int KhoID { get; set; }
        [Required(ErrorMessage ="Vui lòng không để trống ngày nhập")]
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
