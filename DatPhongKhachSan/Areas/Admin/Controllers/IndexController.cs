using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatPhongKhachSan.Models;

namespace DatPhongKhachSan.Areas.Admin.Controllers
{
    public class IndexController : Controller
    {
        // GET: Admin
        DatPhongKhachSanEntities db = new DatPhongKhachSanEntities();
        public ActionResult Index()
        {
            int so_phong_trong = 0, so_phong_sd = 0, so_phong_don = 0;
            var listPhongs = db.Phong.Where(t=>t.MaTinhTrang<3).ToList();
            foreach(var item in listPhongs)
            {
                if (item.MaTinhTrang == 1)
                    so_phong_trong++;
                else if (item.MaTinhTrang == 2)
                    so_phong_sd++;
                else
                    so_phong_don++;
            }
            ViewBag.so_phong_trong = so_phong_trong;
            ViewBag.so_phong_sd = so_phong_sd;
            ViewBag.so_phong_don = so_phong_don;
            return View(listPhongs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tblAdmin01 objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.tblAdmin01.Where(a => a.TenDangNhap.Equals(objUser.TenDangNhap) && a.MatKhau.Equals(objUser.MatKhau)).FirstOrDefault();
                if (obj != null)
                {
                    Session["AD"] = obj;
                    return RedirectToAction("Index", "DichVu");
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
            if (Session["AD"] != null)
                return RedirectToAction("Index", "DichVu");
            return View();
        }
        public ActionResult Logout()
        {
            Session["AD"] = null;
            return RedirectToAction("Login","Index");
        } 

        public ActionResult CaNhan()
        {
            tblAdmin01 ad = (tblAdmin01)Session["AD"];
            if (ad != null)
            {
                ad = db.tblAdmin01.Find(ad.IDTaiKhoan);
                ViewBag.MaCV = new SelectList(db.ChucVuAdmin, "MaCV", "TenCV", ad.MaCV);
                return View(ad);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaNhan([Bind(Include = "IDTaiKhoan,TenDangNhap,MatKhau,Email,Sdt,TinhTrang,MaCV")] tblAdmin01 tblAdmin01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAdmin01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCV = new SelectList(db.ChucVuAdmin, "MaCV", "TenCV", tblAdmin01.MaCV);
            return View(tblAdmin01);
        }


        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string code = "";
                    List<String> dsImg = new List<string>();
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        String filename = Path.Combine(Server.MapPath("~/Content/Images/Phong/"), fname);
                        file.SaveAs(filename);
                        dsImg.Add("/Content/Images/Phong/" + fname);
                    }
                    // Returns message that successfully uploaded
                    code = Newtonsoft.Json.JsonConvert.SerializeObject(dsImg);
                    return Json(code);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}