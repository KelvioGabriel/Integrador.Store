using Microsoft.AspNetCore.Mvc;
using Integrador.Application.Interfaces;
using Integrador.Application.ViewModel;
using Integrador.Core.Enums;

namespace Integrador.Store.API.Controllers.Ruler
{
    [ApiController]
    [Route("api/v1/order")]
    public class OrderController : Controller
    {
        protected readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost("create-new-order")]
        public ActionResult CreateNewOrderAsync([FromBody] OrderViewModel model)
        {
            var result = _orderAppService.SetCreateNewOrder(model);
            return Ok(result);
        }

        [HttpPost("insert-new-item/{orderId}")]
        public ActionResult<IEnumerable<OrderItemViewModel>> InsertNewItem([FromBody] 
        OrderItemViewModel model, Guid orderId)
        {
            var result = _orderAppService.SetInsertNewItem(model, orderId);
            return Ok(result);
        }

        [HttpDelete("{orderItemId}/{orderId}")]
        public ActionResult<IEnumerable<OrderItemViewModel>> DeleteItem(Guid orderItemId, 
            Guid orderId)
        {
            var result = _orderAppService.DeleteItemInOrder(orderItemId, orderId);
            return Ok(result);
        }

        [HttpPut("address-delivery/{orderId}")]
        public ActionResult<OrderViewModel> SetAddressDelivery(Guid orderId, 
            [FromBody] AddressViewModel addressModel)
        {
            var result = _orderAppService.SetAddressDelivery(orderId, addressModel);
            return result;
        }

        [HttpPut("update-quantity-item/{orderItemId}/{newQuantity}")]
        public bool UpdateQuantityItem(Guid orderItemId, int newQuantity)
        {
            _orderAppService.UpdateQuantityItemInOrder(orderItemId, newQuantity);
            return true;
        }

        [HttpPut("update-status-order/{orderId}/{newStatus}")]
        public ActionResult<OrderViewModel> UpdateStatus(Guid orderId, OrderStatus newStatus)
        {
            var result = _orderAppService.UpdateStatusOrder(orderId, newStatus);
            return result;
        }

        //TODO: alterar aqui
        [HttpPut("apply-voucher/{orderId}")]
        public ActionResult<OrderViewModel> SetApplyVoucher(Guid orderId, [FromBody]string code)
        {
            var result = _orderAppService.SetApplyVoucher(orderId, code);
            return result;
        }


        //TODO: Novo
        [HttpGet("{orderId}")]
        public ActionResult<OrderViewModel> GetOrderById(Guid orderId)
        {
            var result = _orderAppService.GetById(orderId);
            return result;
        }

        //TODO: Novo
        [HttpGet("last-order-client/{clientId}")]
        public ActionResult<OrderViewModel> GetLastOrderByClient(Guid clientId)
        {
            var result = _orderAppService.GetLastOrderByClient(clientId);
            return result;
        }

        //TODO: Novo
        [HttpGet("list-orders/{clientId}")]
        public ActionResult<IEnumerable<OrderViewModel>> GetOrdersByClient(Guid clientId)
        {
            var result = _orderAppService.GetOrdersByClient(clientId);

            return Ok(result);
        }

    }
}