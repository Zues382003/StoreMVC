using Microsoft.AspNetCore.Mvc;
using WebBanHangMVC.Repository;

namespace WebBanHangMVC.ViewComponents
{
    public class LoaiSpMenuViewComponent:ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSp;
        public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSpRepository)
        {
            _loaiSp = loaiSpRepository;
        }

        public IViewComponentResult Invoke()
        {
            var loaiSp = _loaiSp.GetAllLoaiSp().OrderBy(x => x.Loai);
            return View(loaiSp);
        }
    }
}
