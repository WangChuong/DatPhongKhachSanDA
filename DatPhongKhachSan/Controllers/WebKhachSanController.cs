using DatPhongKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Data;
using System.Data.Entity;
using Newtonsoft.Json;

namespace DatPhongKhachSan.Controllers
{

    public class WebKhachSanController : Controller
    {
        private DatPhongKhachSanEntities db = new DatPhongKhachSanEntities();
        // GET: WebKhachSan
        public ActionResult DatPhong()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult LienHe()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult LienHe(String HoTen, String NoiDung)
        {
            if (NoiDung == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            YKienKhachHang YK = (YKienKhachHang)Session["YK"];
            if (YK == null)
            {
                if (HoTen == null )
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (NoiDung.Length >= 500)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            YKienKhachHang YK1 = new YKienKhachHang();
            if (YK1 == null)
            {
                YK1.HoTen = HoTen;
            }
            else
            {

            }
            YK1.HoTen = HoTen;
            YK1.NoiDung = NoiDung;
            YK1.NgayGui = DateTime.Now;
            try
            {
                db.YKienKhachHang.Add(YK1);
                db.SaveChanges();
                ModelState.AddModelError("", "Gửi ticket thành công !");
            }
            catch
            {
                ModelState.AddModelError("", "Có lỗi xảy ra!");
            }
            return View();
        }

        public ActionResult ChonPhong(string id)
        {
            //Session["ma_phong"] = id;
            try
            {
                List<int> ds;
                ds = (List<int>)Session["ds_MaP"];
                if (ds == null)
                    ds = new List<int>();
                ds.Add(Int32.Parse(id));
                Session["ds_MaP"] = ds;
                ViewBag.result = "success";
            }
            catch
            {
                ViewBag.result = "error";
            }
            return View();
            //return RedirectToAction("BookRoom", "Home");
        }
        public ActionResult HuyChon(string id)
        {
            try
            {
                List<int> ds;
                ds = (List<int>)Session["ds_MaP"];
                if (ds == null)
                    ds = new List<int>();
                ds.Remove(Int32.Parse(id));
                Session["ds_MaP"] = ds;
                ViewBag.result = "success";
            }
            catch
            {
                ViewBag.result = "error";
            }
            return View();
        }

        public ActionResult BookRoom()
        {
            if (Session["KH"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            KhachHang kh = (KhachHang)Session["KH"];
            ViewBag.MaKH = kh.MaKH;
            ViewBag.TenKH = kh.TenKH;
            ViewBag.NgayDen = DateTime.Now;
            ViewBag.NgayDen = (String)Session["NgayDen"];
            ViewBag.NgayDi = (String)Session["NgayDi"];


            String sp = "";
            List<int> ds;
            ds = (List<int>)Session["ds_MaP"];
            if (ds == null)
                ds = new List<int>();
            ViewBag.MaP = JsonConvert.SerializeObject(ds);
            foreach (var item in ds)
            {
                Phong p = (Phong)db.Phong.Find(Int32.Parse(item.ToString()));
                sp += p.SoP.ToString() + ", ";
            }
            ViewBag.SoP = sp;
            var liP = db.DonDatPhong.Where(u => u.MaKH == kh.MaKH && u.MaTinhTrang == 1).ToList();
            return View(liP);
        }




        public ActionResult Result(int MaKH, String NgayDen, String NgayDi, String MaP)
        {


#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (MaKH == null || MaP == null || NgayDen == null  )
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'

            {
                return RedirectToAction("GioiThieu", "WebKhachSan");
            }
            else
            {
                DonDatPhong ddp = new DonDatPhong();
                List<int> ds = JsonConvert.DeserializeObject<List<int>>(MaP);
                ddp.MaKH = MaKH;
                ddp.MaTinhTrang = 1;
                ddp.NgayDen = DateTime.Now;
                ddp.NgayDen = (DateTime.ParseExact(NgayDen, "dd/MM/yyyy", CultureInfo.InvariantCulture)).AddHours(12);
                //ddp.NgayDi = (DateTime.ParseExact(NgayDi, "dd/MM/yyyy", CultureInfo.InvariantCulture)).AddHours(12);
                try
                {
                    for (int i = 0; i < ds.Count; i++)
                    {
                        ddp.MaP = ds[i];
                        db.DonDatPhong.Add(ddp);
                        db.SaveChanges();
                        ViewBag.Result = "success";
                    }
                    ViewBag.NgayDen = ddp.NgayDen;
                    setNull();
                }
                catch
                {
                    ViewBag.Result = "error";
                }
            }
            return View();
        }

        public ActionResult HuyPhieuDatPhong()
        {
            setNull();
            return RedirectToAction("BookRoom", "WebKhachSan");
        }
        private void setNull()
        {
            Session["NgayDen"] = null;
            Session["NgayDi"] = null;
            Session["MaP"] = null;
            Session["ds_MaP"] = null;
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult Upload()
        {
            return View();
        }
        public ActionResult Slider(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //tblPhong p = db.tblPhongs.Include(a => a.tblLoaiPhong).Where(a=>a.ma_phong==id).First();
            LoaiPhong lp = db.LoaiPhong.Find(id);
            return View(lp);
        }


        public ActionResult FindRoom()
        {
            return RedirectToAction("Index", "WebKhachSan");
        }
        [HttpPost]
        public ActionResult FindRoom(String datestart, String dateend)
        {
            List<Phong> li = new List<Phong>();
            if (datestart.Equals("") || dateend.Equals(""))
            {
                li = db.Phong.ToList();
            }
            else
            {
                Session["ds_MaP"] = null;
                Session["NgayDen"] = datestart;
                Session["NgayDi"] = dateend;

                datestart = DateTime.ParseExact(datestart, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
                dateend = DateTime.ParseExact(dateend, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");

                DateTime dateS = (DateTime.Parse(datestart)).AddHours(12);
                DateTime dateE = (DateTime.Parse(dateend)).AddHours(12);
                li = db.Phong.Where(t => !(db.DonDatPhong.Where(m => (m.MaTinhTrang == 1 || m.MaTinhTrang == 2)
                    && m.NgayDi > dateS && m.NgayDen < dateE))
                    .Select(m => m.MaP).Contains(t.MaP)).ToList();
            }
            return View(li);
        }
    }
}