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
    
    public partial class Phong
    {
        public Phong()
        {
            this.DonDatPhong = new HashSet<DonDatPhong>();
        }
    
        public int MaP { get; set; }
        public Nullable<int> MaLP { get; set; }
        public string SoP { get; set; }
        public Nullable<decimal> GiaP { get; set; }
        public Nullable<int> SoNguoi { get; set; }
        public Nullable<int> Giuong { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public string Hinh1 { get; set; }
        public string Hinh2 { get; set; }
        public Nullable<int> MaTinhTrang { get; set; }
    
        public virtual ICollection<DonDatPhong> DonDatPhong { get; set; }
        public virtual LoaiPhong LoaiPhong { get; set; }
        public virtual TT_Phong TT_Phong { get; set; }
    }
}