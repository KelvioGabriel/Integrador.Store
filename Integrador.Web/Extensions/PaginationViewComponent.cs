using Microsoft.AspNetCore.Mvc;
using Integrador.Web.Models;

namespace Integrador.Web.Extensions
{
    public class PaginacaoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList pagedModel)
        {
            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return base.View(pagedModel);
        }
    }
}
