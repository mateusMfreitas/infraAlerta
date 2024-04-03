using infraAlerta.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace infraAlerta.Helper
{
    public class Session : ISession
    {
        private readonly IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public void CreateUserSession(User user)
        {
            string value = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("sessionUser", value);
        }

        public void DestroyUserSession()
        {
            _httpContext.HttpContext.Session.Remove("sessionUser");
        }

        public User GetUserSession()
        {
            string sessionUser = _httpContext.HttpContext.Session.GetString("sessionUser");
            if (sessionUser == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<User>(sessionUser);
        }
    }
}
