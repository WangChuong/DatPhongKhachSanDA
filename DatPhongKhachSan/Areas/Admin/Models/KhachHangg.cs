using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatPhongKhachSan.Areas.Admin.Models
{
    public class KhachHangg
    {
        public String hoten { get; set; }
        public String socmt { get; set; }
        public String tuoi { get; set; }
        public String sodt { get; set; }
        public KhachHangg()
        {
        }
        public KhachHangg(String hoten, String tuoi)
        {
            this.hoten = hoten;
            this.tuoi = tuoi;
        }
        public KhachHangg(String hoten,String socmt, String tuoi,String sodt)
        {
            this.hoten = hoten;
            this.socmt = socmt;
            this.tuoi = tuoi;
            this.sodt = sodt;
        }
    }
    
}