using Integrador.Application.Interfaces;
using Integrador.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Integrador.Store.API.Controllers.Ruler
{
    [ApiController]
    [Route("api/v1/basket")]
    public class BasketController : Controller
    {
        protected readonly IBasketAppService _basketAppService;

        public BasketController(IBasketAppService basketAppService)
        {
            _basketAppService = basketAppService;
        }

        //Novo
        [HttpGet("amount-items/{basketId}")]
        public ActionResult<int> GetAmountBasket(Guid basketId)
        {
            var result = _basketAppService.GetById(basketId)?.Items.Count();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<BasketViewModel> AddBasket([FromBody] BasketViewModel viewModel)
        {
            var result = _basketAppService.AddBasket(viewModel);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<BasketViewModel> GetById(Guid id)
        {
            var result = _basketAppService.GetById(id);
            return Ok(result);
        }

        [HttpPost("add-item")]
        public ActionResult<IEnumerable<BasketItemViewModel>> AddItemBasket(
            [FromBody] BasketItemViewModel viewModel)
        {
            var result = _basketAppService.AddItemBasket(viewModel);
            return Ok(result);
        }

        //TODO: Alterar aqui
        [HttpDelete("remove-item/{basketId}/{productId}")]
        public ActionResult<IEnumerable<BasketItemViewModel>> RemoveItemBasket(Guid basketId,
            Guid productId)
        {
            var result = _basketAppService.RemoveItemBasket(basketId, productId);
            return Ok(result);
        }

        //TODO - Alterar aqui
        [HttpPut("update-item/{quantity}")]
        public ActionResult UpdateItemQuantity([FromBody] BasketItemViewModel item, int quantity)
        {
            _basketAppService.UpdateItemQuantity(item, quantity);
            return Ok();
        }

        [HttpPut("clear")]
        public ActionResult ClearBasket(Guid basketId)
        {
            _basketAppService.ClearBasket(basketId);
            return Ok();
        }
    }
}