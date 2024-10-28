using WebBanHangMVC.Models;

namespace WebBanHangMVC.Repository
{
    public interface ILoaiSpRepository
    {
        TLoaiSp Add(TLoaiSp loaiSp);
        TLoaiSp Update(TLoaiSp loaiSp);
        TLoaiSp Delete(TLoaiSp loaiSp);
        TLoaiSp GetLoaiSp(TLoaiSp loaiSp);
        IEnumerable<TLoaiSp> GetAllLoaiSp();
    }
}
