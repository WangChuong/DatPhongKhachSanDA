using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatPhongKhachSan.Models;
using Facebook;
using Newtonsoft.Json;


namespace DatPhongKhachSan.Controllers.WebKhachSan
{
    public class AccountController : Controller
    {
        private DatPhongKhachSanEntities db = new DatPhongKhachSanEntities();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View(db.KhachHang.ToList());
        }

        // GET: KhachHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang KhachHang = db.KhachHang.Find(id);
            if (KhachHang == null)
            {
                return HttpNotFound();
            }
            return View(KhachHang);
        }

        // GET: KhachHang/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "MaKH,TenKH,Sdt,Email,CCCD,QuocGia,DiaChi,TaiKhoan,MatKhau")] KhachHang KhachHang)
        {
            if (ModelState.IsValid)
            {
                if (db.KhachHang.Find(KhachHang.MaKH) == null)
                {
                    db.KhachHang.Add(KhachHang);
                    db.SaveChanges();
                    Session["KH"] = KhachHang;
                    return RedirectToAction("BookRoom", "WebKhachSan");
                }
                else
                {
                    ModelState.AddModelError("", "Tên tài khoản đã được sử dụng !");
                }
            }

            return View(KhachHang);
        }

        public ActionResult Add()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "MaKH,TenKH,Sdt,Email,CCCD,QuocGia,DiaChi,TaiKhoan,MatKhau")] KhachHang KhachHang)
        {
            if (ModelState.IsValid)
            {
                if (db.KhachHang.Find(KhachHang.MaKH) == null)
                {
                    db.KhachHang.Add(KhachHang);
                    db.SaveChanges();
                    return RedirectToAction("FindRoom", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }

            return View(KhachHang);
        }

        // GET: KhachHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang KhachHang = db.KhachHang.Find(id);
            if (KhachHang == null)
            {
                return HttpNotFound();
            }
            return View(KhachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenKH,Sdt,Email,CCCD,QuocGia,DiaChi,TaiKhoan,MatKhau")] KhachHang KhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(KhachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(KhachHang);
        }


        public ActionResult CaNhan()
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] == null)
            {
                return RedirectToAction("Index", "WebKhachSan");
            }
            else
            {
                kh = (KhachHang)Session["KH"];
            }
            return View(kh);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaNhan([Bind(Include = "MaKH,TenKH,Sdt,Email,CCCD,QuocGia,DiaChi,TaiKhoan,MatKhau")] KhachHang KhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(KhachHang).State = EntityState.Modified;
                db.SaveChanges();
                Session["KH"] = KhachHang;
                return RedirectToAction("Index", "WebKhachSan");
            }
            return View(KhachHang);
        }

        // GET: KhachHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang KhachHang = db.KhachHang.Find(id);
            if (KhachHang == null)
            {
                return HttpNotFound();
            }
            return View(KhachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                KhachHang KhachHang = db.KhachHang.Find(id);
                db.KhachHang.Remove(KhachHang);
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHang objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.KhachHang.Where(a => a.MaKH.Equals(objUser.MaKH) && a.MatKhau.Equals(objUser.MatKhau)).FirstOrDefault();
                if (obj != null)
                {
                    Session["KH"] = obj;
                    return RedirectToAction("Index", "WebKhachSan");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["KH"] = null;
            KhachHang kh = (KhachHang)Session["KH"];
            if (kh != null)
                return RedirectToAction("Index", "WebKhachSan");
            return View();
        }

        public ActionResult Logout()
        {
            Session["KH"] = null;
            return RedirectToAction("Login", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }









        public ActionResult SuaPhieuDatPhong(int? id)
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatPhong DonDatPhong = db.DonDatPhong.Find(id);
            if (DonDatPhong == null)
            {
                return HttpNotFound();
            }
            if (DonDatPhong.MaKH != kh.MaKH)
                return RedirectToAction("Index", "WebKhachSan");
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "MatKhau", DonDatPhong.MaKH);
            ViewBag.MaP = new SelectList(db.Phong, "MaP", "SoP", DonDatPhong.MaP);
            ViewBag.MaTinhTrang = new SelectList(db.TT_DonDatPhong, "MaTinhTrang", "TenTinhTrang", DonDatPhong.MaTinhTrang);
            return View(DonDatPhong);
        }

        // POST: PhieuDatPhong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaPhieuDatPhong([Bind(Include = "MaDDP,MaKH,NgayDen,NgayDi,MaP,MaTinhTrang")] DonDatPhong DonDatPhong)
        {
            if (ModelState.IsValid)
            {
                DonDatPhong.MaTinhTrang = 1;
                db.Entry(DonDatPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BookRoom", "WebKhachSan");
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "MatKhau", DonDatPhong.MaKH);
            ViewBag.MaP = new SelectList(db.Phong, "MaP", "SoP", DonDatPhong.MaP);
            ViewBag.MaTinhTrang = new SelectList(db.TT_DonDatPhong, "MaTinhTrang", "TenTinhTrang", DonDatPhong.MaTinhTrang);
            return RedirectToAction("BookRoom", "WebKhachSan");
        }

        // GET: PhieuDatPhong/Delete/5
        public ActionResult XoaPhieuDatPhong(int? id)
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DonDatPhong DonDatPhong = db.DonDatPhong.Find(id);
            if (DonDatPhong == null)
            {
                return HttpNotFound();
            }
            if (DonDatPhong.MaKH != kh.MaKH)
                return RedirectToAction("Index", "WebKhachSan");
            return View(DonDatPhong);
        }

        // POST: PhieuDatPhong/Delete/5
        [HttpPost, ActionName("XoaPhieuDatPhong")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmXoaPhieuDatPhong(int id)
        {
            DonDatPhong DonDatPhong = db.DonDatPhong.Find(id);




            DonDatPhong.MaTinhTrang = 2;
            db.Entry(DonDatPhong).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("BookRoom", "WebKhachSan");
        }


       
        public ActionResult PhieuDatPhong()
        {
            AutoHuyPhieuDatPhong();
            KhachHang kh = new KhachHang();
            if (Session["KH"] != null)
                kh = (KhachHang)Session["KH"];
            else
                return RedirectToAction("Index", "WebKhachSan");

            var dsDDP = db.DonDatPhong.Where(t => t.MaKH == kh.MaKH).ToList();
            return View(dsDDP);
        }
        private void AutoHuyPhieuDatPhong()
        {
            var datenow = DateTime.Now;
            var DonDatPhong = db.DonDatPhong.Where(u => u.MaTinhTrang == 1).Include(t => t.KhachHang).Include(t => t.Phong).Include(t => t.TT_DonDatPhong).ToList();
            foreach (var item in DonDatPhong)
            {
                System.Diagnostics.Debug.WriteLine((item.NgayDen - datenow).Value.Days);
                if ((item.NgayDen - datenow).Value.Days < 0)
                {
                    item.MaTinhTrang = 2;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
       

    }
}
