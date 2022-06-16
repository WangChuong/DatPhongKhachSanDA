using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatPhongKhachSan.Models;

namespace DatPhongKhachSan.Areas.Admin.Controllers.Admin
{
    public class PhongController : Controller
    {
        private DatPhongKhachSanEntities db = new DatPhongKhachSanEntities();

        // GET: Phong
        public ActionResult Index()
        {
            var Phong = db.Phong.Where(t=>t.MaTinhTrang<3).Include(t => t.LoaiPhong).Include(t => t.TT_Phong);
            return View(Phong.ToList());
        }

      

        // GET: Phong/Create
        public ActionResult Create()
        {
            ViewBag.MaLP = new SelectList(db.LoaiPhong, "MaLP", "TenLP");
            ViewBag.MaTinhTrang = new SelectList(db.TT_Phong, "MaTinhTrang", "TenTinhTrang");
            return View();
        }

        // POST: Phong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaP,MaLP,SoP,GiaP,SoNguoi,Giuong,MoTa,MaTinhTrang,HinhAnh,Hinh1,Hinh2")] Phong Phong)
        {
            if (ModelState.IsValid)
            {
                db.Phong.Add(Phong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLP = new SelectList(db.LoaiPhong, "MaLP", "TenLP", Phong.MaLP);
            ViewBag.MaTinhTrang = new SelectList(db.TT_Phong, "MaTinhTrang", "TenTinhTrang", Phong.MaTinhTrang);
            return View(Phong);
        }

        // GET: Phong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong Phong = db.Phong.Find(id);
            if (Phong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLP = new SelectList(db.LoaiPhong, "MaLP", "TenLP", Phong.MaLP);
            ViewBag.MaTinhTrang = new SelectList(db.TT_Phong, "MaTinhTrang", "TenTinhTrang", Phong.MaTinhTrang);
            return View(Phong);
        }

        // POST: Phong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaP,MaLP,SoP,GiaP,SoNguoi,Giuong,MoTa,MaTinhTrang,HinhAnh,Hinh1,Hinh2")] Phong Phong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Phong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLP = new SelectList(db.LoaiPhong, "MaLP", "TenLP", Phong.MaLP);
            ViewBag.MaTinhTrang = new SelectList(db.TT_Phong, "MaTinhTrang", "TenTinhTrang", Phong.MaTinhTrang);
            return View(Phong);
        }

        // GET: Phong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phong Phong = db.Phong.Find(id);
            if (Phong == null)
            {
                return HttpNotFound();
            }
            return View(Phong);
        }

        // POST: Phong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Phong Phong = db.Phong.Find(id);
                Phong.MaTinhTrang = 3;
                db.Entry(Phong).State = EntityState.Modified;
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
    }
}
