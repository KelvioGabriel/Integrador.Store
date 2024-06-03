using Microsoft.AspNetCore.Mvc;
using Integrador.Core.Communication;

namespace Integrador.Web.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseWithError (ResponseResult response)
        {
            if(response != null && response.Errors.Messages.Any())
            {
                foreach(var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, errorMessage: message);
                }

                return true;
            }

            return false;
        }

        protected void AddErrorValidation(string message) 
        {
            ModelState.AddModelError(key: string.Empty, errorMessage: message);
        }

        protected bool ValidOperation()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
