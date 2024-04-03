using infraAlerta.Models;


namespace infraAlerta.Helper
{
    public interface ISession
    {
        void CreateUserSession(User user);
        void DestroyUserSession();
        User GetUserSession();
    }
}
