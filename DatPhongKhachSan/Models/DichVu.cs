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
    
    public partial class DichVu
    {
        public DichVu()
        {
            this.CT_SuDungDV = new HashSet<CT_SuDungDV>();
        }
    
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public Nullable<bool> TinhTrang { get; set; }
        public Nullable<decimal> GiaDV { get; set; }
    
        public virtual ICollection<CT_SuDungDV> CT_SuDungDV { get; set; }
    }
}
