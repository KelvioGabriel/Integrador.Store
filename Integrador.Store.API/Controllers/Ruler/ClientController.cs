using Microsoft.AspNetCore.Mvc;
using Integrador.Application.Interfaces;
using Integrador.Application.ViewModel;

namespace Integrador.Store.API.Controllers.Ruler
{
    [ApiController]
    [Route("api/v1/client")]
    public class ClientController : Controller
    {
        private readonly IClientAppService _clientAppService;
        private readonly IAddressAppService _addressAppService;

        public ClientController(IClientAppService clientAppService, 
            IAddressAppService addressAppService)
        {
            _clientAppService = clientAppService;
            _addressAppService = addressAppService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientViewModel>> Get()
        {
            var result = _clientAppService.Search(a => true);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ClientViewModel> Get(Guid id)
        {
            var result = _clientAppService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody] ClientViewModel model)
        {
            var result = _clientAppService.Add(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] ClientViewModel model)
        {
            return Ok(_clientAppService.Update(model));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _clientAppService.Remove(id);
            return Ok();
        }

        [HttpPost("set-address-client/clientId")]
        public ActionResult<ClientViewModel> SetNewAddressClient(Guid clientId, 
            [FromBody]AddressViewModel address)
        {
            _clientAppService.SetAddAddressClient(clientId, address);
            return Ok();
        }

        [HttpGet("/address/{clientId}")]
        public ActionResult<AddressViewModel> GetClientAddress(Guid clientId)
        {
            var addressId  = _clientAppService.GetById(clientId).AddressId;
            var address = _addressAppService.GetById(addressId);

            return Ok(address);
        }
    }
}