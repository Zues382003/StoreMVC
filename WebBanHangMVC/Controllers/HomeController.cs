using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanHangMVC.Models;
using WebBanHangMVC.Models.Authentication;
using WebBanHangMVC.ViewModels;
using X.PagedList;

namespace WebBanHangMVC.Controllers
{
	public class HomeController : Controller
	{
        MasterContext db = new MasterContext();
        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[Authentication]
		public IActionResult Index(int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var listProduct = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
			PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listProduct, pageNumber, pageSize);
			return View(list);
		}

        [Authentication]
        public IActionResult SanPhamTheoLoai(String maloai, int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var listProduct = db.TDanhMucSps.AsNoTracking().Where(x => x.MaLoai == maloai).OrderBy(x => x.TenSp);
			PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(listProduct, pageNumber, pageSize);
			ViewBag.maLoai = maloai;
			return View(list);
		}

        [Authentication]
        public IActionResult ProductDetail(String maSp)
		{
			var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
			var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
			var productDetail = new HomeProductDetailViewModel
			{
				danhMucSp = sanPham,
				anhSps = anhSanPham,
			};
			return View(productDetail);	
		}

        [Authentication]
        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
