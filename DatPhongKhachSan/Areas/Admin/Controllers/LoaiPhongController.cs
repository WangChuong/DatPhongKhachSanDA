using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatPhongKhachSan.Models;

namespace DatPhongKhachSan.Areas.Admin.Controllers
{
    public class LoaiPhongController : Controller
    {
        private DatPhongKhachSanEntities db = new DatPhongKhachSanEntities();

        // GET: LoaiPhong
        public ActionResult Index()
        {
            return View(db.LoaiPhong.ToList());
        }

       

        // GET: LoaiPhong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiPhong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenLP,MoTa,TinhTrang")] LoaiPhong LoaiPhong)
        {
            if (ModelState.IsValid)
            {
                
                db.LoaiPhong.Add(LoaiPhong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(LoaiPhong);
        }

        // GET: LoaiPhong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhong LoaiPhong = db.LoaiPhong.Find(id);
            if (LoaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(LoaiPhong);
        }

        // POST: LoaiPhong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLP,TenLP,MoTa,TinhTrang")] LoaiPhong LoaiPhong)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(LoaiPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(LoaiPhong);
        }

        // GET: LoaiPhong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiPhong LoaiPhong = db.LoaiPhong.Find(id);
            if (LoaiPhong == null)
            {
                return HttpNotFound();
            }
            return View(LoaiPhong);
        }

        // POST: LoaiPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LoaiPhong LoaiPhong = db.LoaiPhong.Find(id);
                db.LoaiPhong.Remove(LoaiPhong);
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
