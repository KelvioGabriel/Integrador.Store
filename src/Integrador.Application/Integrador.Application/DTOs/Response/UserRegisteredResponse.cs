namespace Integrador.Application.DTOs.Response
{
    public class UserRegisteredResponse
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }

        public UserRegisteredResponse() =>
            Errors = new List<string>();

        public UserRegisteredResponse(bool success = true) : this() =>
            Success = success;

        public void AddErrors(IEnumerable<string> errors) =>
            Errors.AddRange(errors);
    }
}
