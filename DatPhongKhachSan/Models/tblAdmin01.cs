//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatPhongKhachSan.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblAdmin01
    {
        public tblAdmin01()
        {
            this.DonDatPhong = new HashSet<DonDatPhong>();
        }
    
        public int IDTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string Sdt { get; set; }
        public Nullable<bool> TinhTrang { get; set; }
        public Nullable<int> MaCV { get; set; }
    
        public virtual ChucVuAdmin ChucVuAdmin { get; set; }
        public virtual ICollection<DonDatPhong> DonDatPhong { get; set; }
    }
}
