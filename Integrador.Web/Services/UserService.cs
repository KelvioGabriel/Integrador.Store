using Integrador.Core.User;

namespace Integrador.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) :
                Guid.Empty;
        }

        public string GetUserToken()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : "";
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext;
        }
    }
}
