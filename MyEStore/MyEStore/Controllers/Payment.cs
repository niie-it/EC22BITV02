using Microsoft.AspNetCore.Mvc;
using MyEStore.Models;

namespace MyEStore.Controllers
{
	public class PaymentController : Controller
	{
		private readonly PaypalClient _paypalClient;

		public PaymentController(PaypalClient paypalClient)
		{
			_paypalClient = paypalClient;
		}


		[HttpPost("/payment/create-paypal-order")]

		public async Task<IActionResult> CreateOrder([FromBody] Amount amount)

		{
			//Tạo đơn hàng
			var refId = DateTime.Now.Ticks.ToString();
			try
			{

				var response = await _paypalClient.CreateOrder(amount.value, amount.currency_code, refId);
				return Ok(response);
			}

			catch (Exception ex)

			{

				var error = new { ex.GetBaseException().Message };

				return BadRequest(error);

			}

		}



		[HttpPost("/payment/capture-paypal-order")]
		public IActionResult CaptureOrder(string orderID)

		{

			try

			{

				var response = _paypalClient.CaptureOrder(orderID);



				//Lưu đơn hàng vô database



				return Ok(response);

			}

			catch (Exception ex)

			{

				var error = new { ex.GetBaseException().Message };

				return BadRequest(error);

			}

		}
	}
}
