using Integrador.Application.ViewModel;
using Integrador.Core.Enums;

namespace Integrador.Application.Interfaces
{
    public interface IOrderAppService
    {
        OrderViewModel SetCreateNewOrder(OrderViewModel viewModel);
        IEnumerable<OrderItemViewModel> SetInsertNewItem(OrderItemViewModel model,
            Guid orderId);
        IEnumerable<OrderItemViewModel> DeleteItemInOrder(Guid orderItemId, Guid orderId);
        void UpdateQuantityItemInOrder(Guid orderItemId, int newQuantity);
        OrderViewModel UpdateStatusOrder(Guid orderId, OrderStatus newStatus);
        OrderViewModel SetAddressDelivery(Guid orderId, AddressViewModel addresViewModel);
        OrderViewModel SetApplyVoucher(Guid orderId, string code);
        OrderViewModel GetById(Guid orderId);
        OrderViewModel GetLastOrderByClient(Guid clientId);
        IEnumerable<OrderViewModel> GetOrdersByClient(Guid clientId);
    }
}
