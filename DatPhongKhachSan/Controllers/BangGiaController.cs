using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatPhongKhachSan.Controllers
{
    public class BangGiaController : Controller
    {
        // GET: BangGia
        DatPhongKhachSan.Models.DatPhongKhachSanEntities db = new Models.DatPhongKhachSanEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GiaPhong()
        {
            return View(db.Phong.ToList());
        }
        public ActionResult GiaDichVu()
        {
            return View(db.DichVu.ToList());
        }
    }
}