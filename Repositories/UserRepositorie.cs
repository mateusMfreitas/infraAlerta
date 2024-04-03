using infraAlerta.Models;
using infraAlerta.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace infraAlerta.Repositories
{
    public class UserRepositorie
    {
        private readonly ApiDbContext _context;

        public UserRepositorie(ApiDbContext dbContext)
        {
            this._context = dbContext;
        }

        public User SearchById(int id)
        {
            return _context.User.FirstOrDefault(x => x.user_id == id);
        }

        public List<User> SearchAll()
        {
            return _context.User.ToList();
        }

        public User Add(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Update(User user)
        {
            _context.User.Update(user);
            _context.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var user = _context.User.FirstOrDefault(x => x.user_id == id);
            if (user == null)
            {
                return false;
            }
            _context.User.Remove(user);
            _context.SaveChanges();
            return true;
        }

        
    }
}
