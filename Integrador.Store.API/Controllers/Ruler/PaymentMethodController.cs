using Microsoft.AspNetCore.Mvc;
using Integrador.Application.Interfaces;
using Integrador.Application.ViewModel;

namespace Integrador.Store.API.Controllers.Ruler
{
    [ApiController]
    [Route("api/v1/payment-method")]
    public class PaymentMethodController : Controller
    {
        private readonly IPaymentMethodAppService _paymentMethodAppService;

        public PaymentMethodController(IPaymentMethodAppService paymentMethodAppService)
        {
            _paymentMethodAppService = paymentMethodAppService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PaymentMethodViewModel>> Get()
        {
            var result = _paymentMethodAppService.Search(a => true);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<PaymentMethodViewModel> Get(Guid id)
        {
            var result = _paymentMethodAppService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody] PaymentMethodViewModel model)
        {
            var result = _paymentMethodAppService.Add(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] PaymentMethodViewModel model)
        {
            return Ok(_paymentMethodAppService.Update(model));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _paymentMethodAppService.Remove(id);
            return Ok();
        }
    }
}