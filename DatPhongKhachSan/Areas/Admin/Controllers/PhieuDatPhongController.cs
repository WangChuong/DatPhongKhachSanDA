using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DatPhongKhachSan.Areas.Admin.Models;
using DatPhongKhachSan.Models;

namespace DatPhongKhachSan.Areas.Admin.Controllers.Admin
{
    public class PhieuDatPhongController : Controller
    {
        private DatPhongKhachSanEntities db = new DatPhongKhachSanEntities();

        // GET: PhieuDatPhong
        public ActionResult Index()
        {
            AutoHuyPhieuDatPhong();
            var DonDatPhong = db.DonDatPhong.Include(t => t.KhachHang).Include(t => t.Phong).Include(t => t.TT_DonDatPhong);
            return View(DonDatPhong.ToList());
        }

        private void AutoHuyPhieuDatPhong()
        {
            var datenow = DateTime.Now;
            var DonDatPhong = db.DonDatPhong.Where(u=>u.MaTinhTrang == 1).Include(t => t.KhachHang).Include(t => t.Phong).Include(t => t.TT_DonDatPhong).ToList();
            foreach(var item in DonDatPhong)
            {
                System.Diagnostics.Debug.WriteLine((item.NgayDen - datenow).Value.Days);
                if ((item.NgayDen - datenow).Value.Days < 0)
                {
                    item.MaTinhTrang = 3;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }


        public ActionResult List()
        {
            AutoHuyPhieuDatPhong();
            var DonDatPhong = db.DonDatPhong.Where(t => t.MaTinhTrang == 1 && t.NgayDen.Value.Day == DateTime.Now.Day && t.NgayDen.Value.Month == DateTime.Now.Month && t.NgayDen.Value.Year == DateTime.Now.Year).Include(t => t.KhachHang).Include(t => t.Phong).Include(t => t.TT_DonDatPhong);
            return View(DonDatPhong.ToList());
        }


        // GET: PhieuDatPhong/Create

        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.select_MaP = id;
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "MaKH");
            ViewBag.MaP = new SelectList(db.Phong.Where(u => u.MaTinhTrang == 1), "MaP", "SoP");
            ViewBag.MaTinhTrang = new SelectList(db.TT_DonDatPhong, "MaTinhTrang", "TenTinhTrang");
            return View();
        }


       


        // POST: PhieuDatPhong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String radSelect, [Bind(Include = "MaDDP,MaKH,MaP,NgayDen,NgayDi,TongTien,TienDichVu,TienPhong,GhiChu,MaTinhTrang,IDTaiKhoan")] DonDatPhong DonDatPhong)
        {
            System.Diagnostics.Debug.WriteLine("SS :"+radSelect);
            if (ModelState.IsValid)
            {

                db.DonDatPhong.Add(DonDatPhong);

            }

            DonDatPhong.MaTinhTrang = 1;
                DonDatPhong.NgayDen = DateTime.Now;
                db.DonDatPhong.Add(DonDatPhong);
                db.SaveChanges();
                int ma = DonDatPhong.MaDDP;
                return RedirectToAction("Index", new { id = ma });

            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "MaKH", DonDatPhong.MaKH);
            ViewBag.MaP = new SelectList(db.Phong, "MaP", "SoP", DonDatPhong.MaP);
            ViewBag.MaTinhTrang = new SelectList(db.TT_DonDatPhong, "MaTinhTrang", "TenTinhTrang", DonDatPhong.MaTinhTrang);
            return View(DonDatPhong);
        }

        // GET: PhieuDatPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatPhong DonDatPhong = db.DonDatPhong.Find(id);
            if (DonDatPhong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "MaKH", DonDatPhong.MaKH);
            ViewBag.MaP = new SelectList(db.Phong, "MaP", "SoP", DonDatPhong.MaP);
            ViewBag.MaTinhTrang = new SelectList(db.TT_DonDatPhong, "MaTinhTrang", "TenTinhTrang", DonDatPhong.MaTinhTrang);
            return View(DonDatPhong);
        }

        // POST: PhieuDatPhong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDDP,MaKH,MaP,NgayDen,NgayDi,TongTien,TienDichVu,TienPhong,GhiChu,MaTinhTrang,IDTaiKhoan")] DonDatPhong DonDatPhong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(DonDatPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "MaKH", DonDatPhong.MaKH);
            ViewBag.MaP = new SelectList(db.Phong, "MaP", "SoP", DonDatPhong.MaP);
            ViewBag.MaTinhTrang = new SelectList(db.TT_DonDatPhong, "MaTinhTrang", "TenTinhTrang", DonDatPhong.MaTinhTrang);
            return View(DonDatPhong);
        }

        // GET: PhieuDatPhong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatPhong DonDatPhong = db.DonDatPhong.Find(id);
            if (DonDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(DonDatPhong);
        }

        // POST: PhieuDatPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                DonDatPhong DonDatPhong = db.DonDatPhong.Find(id);
                db.DonDatPhong.Remove(DonDatPhong);
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

    
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatPhong DonDatPhong = db.DonDatPhong.Find(id);
            if (DonDatPhong == null)
            {
                return HttpNotFound();
            }
            return View(DonDatPhong);
        }



        public ActionResult ChiTietPhieuDatPhong(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatPhong DonDatPhong = db.DonDatPhong.Find(id);
            if (DonDatPhong == null)
            {
                return HttpNotFound();
            }

            var TienPhong = (DonDatPhong.NgayDi - DonDatPhong.NgayDen).Value.TotalDays * ((float?)DonDatPhong.Phong.GiaP);
            ViewBag.TienPhong = TienPhong;

            ViewBag.time_now = DateTime.Now.ToString();

            List<CT_SuDungDV> sddv = db.CT_SuDungDV.Where(u => u.MaDDP == id).ToList();
            ViewBag.list_dv = sddv;
            double tongtiendv = 0;
            List<double> tt = new List<double>();
            foreach (var item in sddv)
            {
                double t = (double)(item.SoLuong * item.DichVu.GiaDV);
                tongtiendv += t;
                tt.Add(t);
            }
            ViewBag.list_tt = tt;
            ViewBag.TienDichVu = tongtiendv;
            ViewBag.TongTien = TienPhong + tongtiendv;
            return View(DonDatPhong);
        }










    }
}
