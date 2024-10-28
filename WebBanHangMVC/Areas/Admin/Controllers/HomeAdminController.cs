using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHangMVC.Models;
using WebBanHangMVC.Models.Authentication;
using X.PagedList;

namespace WebBanHangMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        MasterContext db = new MasterContext();
        [Route("")]
        [Route("index")]
        [Authentication]
        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucsanpham")]
        [Authentication]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 16;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listProduct = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listProduct, pageNumber, pageSize);
            return View(list);
        }

        [Route("danhsachnguoidung")]
        [Authentication]
        public IActionResult DanhSachNguoiDung(int? page)
        {
            int pageSize = 16;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listUser = db.TUsers.AsNoTracking().OrderBy(x => x.Username);
            PagedList<TUser> list = new PagedList<TUser>(listUser, pageNumber, pageSize);
            return View(list);
        }

        [Route("ThemSanPhamMoi")]
        [HttpGet]
        [Authentication]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");

            return View();
        }

        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [Authentication]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPham(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid) { 
                db.TDanhMucSps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }


        [Route("SuaSanPham")]
        [HttpGet]
        [Authentication]
        public IActionResult SuaSanPham(string maSp)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var EditedProduct = db.TDanhMucSps.Find(maSp);
            if (EditedProduct != null) { 
                return View(EditedProduct);
            }

            return View();
        }

        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authentication]
        public IActionResult SuaSanPham(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Update(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }

        [Route("XoaSanPham")]
        [HttpGet]
        [Authentication]
        public IActionResult XoaSanPham(string maSp)
        {
            TempData["Message"] = "";
            var productDetail = db.TChiTietSanPhams.Where(x => x.MaSp == maSp).ToList();
            if (productDetail.Count > 0)
            {
                TempData["Message"] = "Không xóa được sản phẩm này";
                return RedirectToAction("DanhMucSanPham");
            }
            var productImages = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            if (productImages.Any()) {
                db.RemoveRange(productImages);
            }
            db.Remove(db.TDanhMucSps.Find(maSp));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";

            return RedirectToAction("DanhMucSanPham");
        }
    }
}
