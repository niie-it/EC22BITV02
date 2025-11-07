using Microsoft.AspNetCore.Mvc;
using MyEStore.Entities;
using MyEStore.Models;

namespace MyEStore.Controllers
{
	public class CustomerController : Controller
	{
		private readonly MyeStoreContext _ctx;

		public CustomerController(MyeStoreContext ctx)
		{
			_ctx = ctx;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}


		[HttpPost]
		public IActionResult Register(RegisterVM model, IFormFile FileHinh)
		{
			try
			{
				var khachHang = new KhachHang
				{
					MaKh = model.MaKh,
					HoTen = model.HoTen,
					NgaySinh = model.NgaySinh,
					DiaChi = model.DiaChi,
					GioiTinh = model.GioiTinh,
					DienThoai = model.DienThoai,
					Email = model.Email,
					Hinh = MyTool.UploadFileToFolder(FileHinh, "KhachHang"),

					HieuLuc = true, //false + gửi mail active tài khoản
					RandomKey = MyTool.GetRandom()
				};
				khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
				_ctx.Add(khachHang);
				_ctx.SaveChanges();
				return RedirectToAction("Login");
			}
			catch (Exception ex)
			{
				return View();
			}
		}


		public IActionResult Login()
		{
			return View();
		}



	}
}
