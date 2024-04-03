using infraAlerta.Models;
using System.Collections.Generic;   

namespace infraAlerta.Repositories
{
    public interface IUserRepositorie
    {
        List<User> SearchAll();
        User SearchById(int id);
        User Add(User user);
        User Update(User user);
        bool Delete(int id);
    }
}
