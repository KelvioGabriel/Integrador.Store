using Microsoft.AspNetCore.Mvc;
using Integrador.Web.Models;
using Integrador.Web.Services;

namespace Integrador.Web.Controllers
{
    public class ClientController : MainController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> NewAddress(AddressViewModel address, Guid clientId)
        {
            var response = await _clientService.SetAddress(address, clientId);

            if (ResponseWithError(response))
            {
                TempData["Errors"] = ModelState.Values.SelectMany(r => r.Errors.Select(e =>
                e.ErrorMessage)).ToList();
            }

            return RedirectToAction("AddressDelivery", "Order");

        }
    }
}
