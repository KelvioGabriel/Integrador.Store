using Microsoft.Extensions.Options;
using Integrador.Web.Extensions;
using Integrador.Web.Models;

namespace Integrador.Web.Services
{
    public interface ICatalogService
    {
        Task<PagedViewModel<ProductViewModel>> GetAll(int pageSize, 
            int pageIndex, 
            string productName = null);
        Task<ProductViewModel> GetById(Guid id);
    }
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.Url);
        }

        public async Task<PagedViewModel<ProductViewModel>> GetAll(int pageSize, 
            int pageIndex, 
            string productName = null)
        {
            var response = await _httpClient.GetAsync($"/api/v1/product/{pageSize}/" +
                $"{pageIndex}/{productName}");

            HandleErrosResponse(response);

            return await DeserializeObjectResponse<PagedViewModel<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/product/{id}");

            HandleErrosResponse(response);

            return await DeserializeObjectResponse<ProductViewModel>(response);
        }
    }
}
