using Integrador.Application.Interfaces;
using Integrador.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Integrador.Store.API.Controllers.Ruler
{
    [ApiController]
    [Route("api/ruler/address")]
    public class AddressController : Controller
    {
        protected readonly IAddressAppService _addressService;

        public AddressController(IAddressAppService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AddressViewModel>> Get()
        {
            var result = _addressService.Search(a => true);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<AddressViewModel> Get(Guid id)
        {
            var result = _addressService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody] AddressViewModel model)
        {
            var result = _addressService.Add(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] AddressViewModel model)
        {
            return Ok(_addressService.Update(model));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _addressService.Remove(id);
            return Ok();
        }
    }
}
