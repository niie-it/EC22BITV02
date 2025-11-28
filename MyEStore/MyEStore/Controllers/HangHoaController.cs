using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		public IActionResult ChiTiet(string tenalias, int id)
		{
			var sp = _ctx.HangHoas.Include(p => p.MaLoaiNavigation).SingleOrDefault(p => p.MaHh == id);
			if (sp != null)
			{
				return View(sp);
			}
			return RedirectToAction("Index");
		}
	}
}
