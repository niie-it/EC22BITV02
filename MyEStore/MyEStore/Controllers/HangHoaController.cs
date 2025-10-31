using Microsoft.AspNetCore.Mvc;
using MyEStore.Entities;
using MyEStore.Models;

namespace MyEStore.Controllers
{
	public class HangHoaController : Controller
	{
		private readonly MyeStoreContext _ctx;

		public HangHoaController(MyeStoreContext ctx)
		{
			_ctx = ctx;
		}

		public IActionResult Index()
		{
			var data = _ctx.HangHoas.AsQueryable();
			//filter
			var result = data.Select(hh => new HangHoaVM
			{
				MaHh = hh.MaHh,
				TenHh = hh.TenHh,
				DonGia = hh.DonGia ?? 0,
				Hinh = hh.Hinh,
				GiamGia = hh.GiamGia
			}).ToList();
			return View(result);
		}
	}
}
