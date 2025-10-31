using Microsoft.AspNetCore.Mvc;
using MyEStore.Entities;
using MyEStore.Models;

namespace MyEStore.Controllers
{
	public class CartController : Controller
	{
		private readonly MyeStoreContext _ctx;
		public const string CART_NAME = "CART";

		public CartController(MyeStoreContext ctx)
		{
			_ctx = ctx;
		}

		public List<CartItem> CartItems
		{
			get
			{
				var carts = HttpContext.Session.Get<List<CartItem>>(CART_NAME);
				if (carts == null)
				{
					carts = new List<CartItem>();
				}
				return carts;
			}
		}

		public IActionResult AddToCart(int id, int qty = 1)
		{
			var gioHang = CartItems;

			//kiểm tra id (MaHH) truyền qua đã nằm trong giỏ hàng hay chưa
			var item = gioHang.SingleOrDefault(p => p.MaHh == id);
			if (item != null) //đã có
			{
				item.SoLuong += qty;
			}
			else
			{
				var hangHoa = _ctx.HangHoas.SingleOrDefault(p => p.MaHh == id);
				if (hangHoa == null)//id không có trong Database
				{
					return RedirectToAction("Index", "HangHoa");
				}
				item = new CartItem
				{
					MaHh = id,
					SoLuong = qty,
					TenHh = hangHoa.TenHh,
					Hinh = hangHoa.Hinh,
					DonGia = hangHoa.DonGia.Value,
					GiamGia = hangHoa.GiamGia
				};
				//thêm vào giỏ hàng
				gioHang.Add(item);
			}
			//cập nhật session
			HttpContext.Session.Set(CART_NAME, gioHang);
			return RedirectToAction("Index"); //để hiện giỏ hàng
		}

		public IActionResult Index()
		{
			return View(CartItems);
		}

		public IActionResult RemoveCart(int id)
		{
			var gioHang = CartItems;
			var item = gioHang.SingleOrDefault(p => p.MaHh == id);
			if (item != null)
			{
				gioHang.Remove(item);
				HttpContext.Session.Set(CART_NAME, gioHang);
			}
			return RedirectToAction("Index");
		}

	}
}
