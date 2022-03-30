using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranVanTri.Models;

namespace TranVanTri.Controllers
{
    public class SinhVienController : Controller
    {
        // GET: SinhVien
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_sinhvien = from tt in data.SinhViens select tt;
            return View(all_sinhvien);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var E_MaSV = collection["MaSV"];
            var E_HoTen = collection["HoTen"];
            var E_GioiTinh = collection["GioiTinh"];
            var E_NgaySinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_Hinh = collection["Hinh"];



            if (string.IsNullOrEmpty(E_HoTen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.MaSV = E_MaSV;
                s.HoTen = E_HoTen;
                s.NgaySinh = E_NgaySinh;
                s.GioiTinh = E_GioiTinh;
                s.Hinh = E_Hinh.ToString();


                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Listsinhvien");
            }
            return this.Create();
        }

        public ActionResult Edit(String id)
        {
            var E_sp = data.SinhViens.First(m => m.MaSV == id);
            return View(E_sp);
        }
        [HttpPost]
        public ActionResult Edit(String id, FormCollection collection)
        {
            var E_sv = data.SinhViens.First(m => m.MaSV == id);
            var E_tenSV = collection["HoTen"];
            var E_GioiTinh = collection["GioiTinh"];
            var E_ngaySinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_hinh = collection["Hinh"];
            var E_MaNganh = collection["MaNganh"];
            E_sv.MaSV = id;
            if (string.IsNullOrEmpty(E_sv.MaSV))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sv.MaSV = E_tenSV;
                E_sv.GioiTinh = E_GioiTinh;
                E_sv.NgaySinh = E_ngaySinh;
                E_sv.Hinh = E_hinh;
                E_sv.MaNganh = E_MaNganh;
                UpdateModel(E_sv);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
        public ActionResult Delete(String id)
        {
            var D_sach = data.SinhViens.First(m => m.MaSV == id);
            return View(D_sach);
        }
        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
        {
            var D_sach = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(D_sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detail(String id)
        {
            var D_sach = data.SinhViens.Where(m => m.MaSV == id).First();
            return View(D_sach);
        }
    }
}
