using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatPhongKhachSan.Models;

namespace DatPhongKhachSan.Areas.Admin.Controllers
{
    public class DichVuController : Controller
    {
        private DatPhongKhachSanEntities db = new DatPhongKhachSanEntities();

        // GET: DichVu
        public ActionResult Index()
        {
            return View(db.DichVu.ToList());
        }

 
       

        // GET: DichVu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DichVu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, [Bind(Include = "MaDV, TenDV, GiaDV, TinhTrang")] DichVu DichVu)
        {
            if (ModelState.IsValid)
            {
  
                db.DichVu.Add(DichVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(DichVu);
        }

        // GET: DichVu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichVu tblDichVu = db.DichVu.Find(id);
            if (tblDichVu == null)
            {
                return HttpNotFound();
            }
            return View(tblDichVu);
        }

        // POST: DichVu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, [Bind(Include = "MaDV, TenDV, GiaDV, TinhTrang")] DichVu DichVu)
        {
            DichVu dv = db.DichVu.Find(DichVu.MaDV);
            if (ModelState.IsValid)
            {
               
              

               
                dv.TinhTrang = DichVu.TinhTrang;
                dv.GiaDV = DichVu.GiaDV;
                dv.TenDV = DichVu.TenDV;
                db.Entry(dv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DichVu);
        }

        // GET: DichVu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DichVu tblDichVu = db.DichVu.Find(id);
            if (tblDichVu == null)
            {
                return HttpNotFound();
            }
            return View(tblDichVu);
        }

        // POST: DichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                DichVu DichVu = db.DichVu.Find(id);
                db.DichVu.Remove(DichVu);
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
