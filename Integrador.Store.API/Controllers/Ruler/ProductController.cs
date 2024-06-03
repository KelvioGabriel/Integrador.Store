using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Integrador.Application.Interfaces;
using Integrador.Application.ViewModel;
using Integrador.Domain.Entities;
using System.Linq.Expressions;
using Integrador.API.Models;

namespace Integrador.Store.API.Controllers.Ruler
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : Controller
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductViewModel>> Get()
        {
            var result = _productAppService.Search(a => true);

            return Ok(result);
        }

        [HttpGet("{pageSize}/{pageIndex}/{nameProduct}")]
        public ActionResult<PagedResult<ProductViewModel>> Get(int pageSize, 
            int pageIndex, string nameProduct)
        {
            Expression<Func<Product, bool>> filter = p => true;

            if (nameProduct != null)
            {
                filter = filter.And(p => p.Name.Contains(nameProduct));
            }

            var result = _productAppService.Search(filter, pageIndex, pageSize);

            var total = _productAppService.Search(filter).Count();

            var pagedReturn = new PagedResult<ProductViewModel>
            {
                List = result,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = nameProduct
            };

            return pagedReturn;
        }

        [HttpGet("{pageSize}/{pageIndex}")]
        public ActionResult<PagedResult<ProductViewModel>> Get(int pageSize,
            int pageIndex)
            => Get(pageSize, pageIndex, null);

        [HttpGet("{id}")]
        public ActionResult<ProductViewModel> Get(Guid id)
        {
            var result = _productAppService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody] ProductViewModel model)
        {
            var result = _productAppService.Add(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] ProductViewModel model)
        {
            return Ok(_productAppService.Update(model));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _productAppService.Remove(id);
            return Ok();
        }

        [HttpPost("update-stock/{productId}/{quantity}")]
        public ActionResult SetUpdateStock(Guid productId, int quantity)
        {
            try
            {
                _productAppService.UpdateStock(productId, quantity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("check-quantity-stock")]
        public ActionResult<int> CheckQuantityStock(Guid productId)
        {
            return Ok(_productAppService.CheckQuantityStock(productId));
        }
    }
}