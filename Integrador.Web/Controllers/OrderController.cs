using Microsoft.AspNetCore.Mvc;
using Integrador.Web.Models;
using Integrador.Web.Services;

namespace Integrador.Web.Controllers
{
    public class OrderController : MainController
    {
        private readonly IClientService _clientService;
        private readonly IOrderBffService _orderBffService;
        private readonly IUserService _userService;

        private Guid userId;

        public OrderController(IClientService clientService,
            IOrderBffService orderBffService,
                IUserService userService)
        {
            _clientService = clientService;
            _orderBffService = orderBffService;
            _userService = userService;

            userId = _userService.GetUserId();
        }
        

        [HttpGet]
        [Route("address-delivery")]
        public async Task<IActionResult> AddressDelivery()
        {
            var order = await _orderBffService.GetLastOrderClient(userId);
            if (!order.OrderItems.Any()) return RedirectToAction("Index", "Basket");
            

            var address = await _clientService.GetAddress(userId);
            var orderTransaction = _orderBffService.MapToOrder(order, address);

            return View(orderTransaction);
        }

        [HttpGet]
        [Route("payment")]
        public async Task<IActionResult> Payment()
        {
            var order = await _orderBffService.GetLastOrderClient(userId);
            if (!order.OrderItems.Any()) return RedirectToAction("Index", "Basket");

            var orderTransaction = _orderBffService.MapToOrder(order, null);

            return View(orderTransaction);
        }

        [HttpPost]
        [Route("finish-order")]
        public async Task<IActionResult> FinishOrder(OrderTransactionViewModel orderTransaction)
        {
            if (!ModelState.IsValid)
            {
                return View("Payment", _orderBffService.MapToOrder(
                await _orderBffService.GetLastOrderClient(userId), null));
            }

            var ret = await _orderBffService.FinishOrder(orderTransaction);

            if (ResponseWithError(ret))
            {
                var order = await _orderBffService.GetLastOrderClient(userId);
                if (order.OrderItems.Count() == 0)
                {
                    return RedirectToAction("Index", "Basket");
                }

                var orderMap = _orderBffService.MapToOrder(order, null);
                return View("Payment", orderMap);
            }

            return RedirectToAction("OrderFinished");
        }

        [HttpGet]
        [Route("order-finished")]
        public async Task<IActionResult> OrderFinished()
        {
            return View("ConfirmOrder", await _orderBffService.GetLastOrderClient(userId));
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> MyOrders()
        {
            return View(await _orderBffService.GetOrdersByClient(userId));
        }
        
        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(Guid orderId, string voucherCode)
        {
            var response = await _orderBffService.ApplyVoucherCode(orderId, voucherCode);

            return RedirectToAction("Payment");
        }
    }
}
