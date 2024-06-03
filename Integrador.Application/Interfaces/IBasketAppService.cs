using Integrador.Application.ViewModel;

namespace Integrador.Application.Interfaces
{
    public interface IBasketAppService
    {
        BasketViewModel AddBasket(BasketViewModel viewModel);
        BasketViewModel GetById(Guid id);
        IEnumerable<BasketItemViewModel> AddItemBasket(BasketItemViewModel viewModel);
        IEnumerable<BasketItemViewModel> RemoveItemBasket(Guid idBasketItem);
        void UpdateItemQuantity(BasketItemViewModel item, int quantity);
        void ClearBasket(Guid basketId);
        IEnumerable<BasketItemViewModel> RemoveItemBasket(Guid basketId, Guid productId);
    }
}
