using Microsoft.Extensions.Options;
using Integrador.Core.Communication;
using Integrador.Web.Extensions;
using Integrador.Web.Models;

namespace Integrador.Web.Services
{
    public interface IOrderBffService
    {
        #region Carrinho
        Task<BasketViewModel> GetBasket(Guid basketId);
        Task<int> GetAmountBasket(Guid basketId);
        Task<ResponseResult> AddBasketItem(BasketItemViewModel item);
        Task<ResponseResult> UpdateBasketItem(BasketItemViewModel item, int amount);
        Task<ResponseResult> DeleteBasketItem(Guid basketId, Guid productId);

        #endregion

        #region Pedido
        Task<ResponseResult> ApplyVoucherCode(Guid orderId, string voucher);
        Task<ResponseResult> CreateOrder(OrderTransactionViewModel orderTransaction);
        Task<OrderViewModel> GetLastOrderClient(Guid clientId);
        Task<IEnumerable<OrderViewModel>> GetOrdersByClient(Guid clientId);
        OrderTransactionViewModel MapToOrder(OrderViewModel order, AddressViewModel address);
        Task<ResponseResult> FinishOrder(OrderTransactionViewModel orderTransaction);

        #endregion
    }

    public class OrderBffService : Service, IOrderBffService
    {
        private readonly HttpClient _httpClient;

        public OrderBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.Url);
        }

        #region Carrinho

        public async Task<ResponseResult> AddBasketItem(BasketItemViewModel item)
        {
            var itemContent = GetContent(item);

            var response = await _httpClient.PostAsync($"/api/v1/basket/add-item",
                itemContent);

            if (!HandleErrosResponse(response))
                return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<BasketViewModel> GetBasket(Guid basketId)
        {

            var response = await _httpClient.GetAsync($"/api/v1/basket/{basketId}");

            HandleErrosResponse(response);

            return await DeserializeObjectResponse<BasketViewModel>(response);
        }
        public async Task<int> GetAmountBasket(Guid basketId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/basket/amount-items/{basketId}");

            HandleErrosResponse(response);

            return await DeserializeObjectResponse<int>(response);
        }

        public async Task<ResponseResult> UpdateBasketItem(BasketItemViewModel item, int quantity)
        {
            var itemContent = GetContent(item);

            var response = await _httpClient.PutAsync($"/api/v1/basket/update-item/{quantity}", 
                itemContent);

            if (!HandleErrosResponse(response)) 
                return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
        public async Task<ResponseResult> DeleteBasketItem(Guid basketId, Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/basket/remove-item/{basketId}/" +
                $"{productId}");

            if (!HandleErrosResponse(response)) 
                return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
        #endregion

        #region Pedido

        public async Task<ResponseResult> ApplyVoucherCode(Guid orderId, string voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync($"/api/v1/order/apply-voucher/{orderId}",
                itemContent);

            if (!HandleErrosResponse(response))
                return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> CreateOrder(OrderTransactionViewModel orderTransaction)
        {
            var orderContent = GetContent(orderTransaction);

            var response = await _httpClient.PostAsync("/api/v1/order/create-new-order", orderContent);

            if (!HandleErrosResponse(response)) 
                return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<OrderViewModel> GetLastOrderClient(Guid clientId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/order/last-order-client/{clientId}");

            HandleErrosResponse(response);

            return await DeserializeObjectResponse<OrderViewModel>(response);
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersByClient(Guid clientId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/order/list-orders/{clientId}");

            HandleErrosResponse(response);

            return await DeserializeObjectResponse<IEnumerable<OrderViewModel>>(response);
        }

        public OrderTransactionViewModel MapToOrder(OrderViewModel order, 
            AddressViewModel address)
        {
            var orderViewModel = new OrderTransactionViewModel
            {
                TotalValue = order.TotalValue,
                Itens = order.OrderItems,
                Discount = order.DiscountValue,
                VoucherUsed = order.Voucher != null,
                VoucherCode = order.Voucher?.Code
            };

            if (address != null)
            {
                orderViewModel.Address = new AddressViewModel
                {
                    Street = address.Street,
                    Number = address.Number,
                    Neighborhood = address.Neighborhood,
                    PostalCode = address.PostalCode,
                    Complement = address.Complement,
                    City = address.City,
                    State = address.State
                };
            }

            return orderViewModel;
        }
        
        public async Task<ResponseResult> FinishOrder(OrderTransactionViewModel orderTransaction)
        {
            var orderContent = GetContent(orderTransaction);

            var response = await _httpClient.PostAsync("/api/v1/order/create-new-order", orderContent);

            if (!HandleErrosResponse(response))
            {
                return await DeserializeObjectResponse<ResponseResult>(response);
            }

            return ReturnOk();
        }

        #endregion
    }
}
