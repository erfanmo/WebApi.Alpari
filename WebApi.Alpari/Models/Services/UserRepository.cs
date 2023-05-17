using System;
using System.Linq;
using WebApi.Alpari.Models.Context;
using WebApi.Alpari.Models.Entities;

namespace WebApi.Alpari.Models.Services
{
    public class UserRepository
    {
        private readonly DataBaseContext _context;
        public UserRepository(DataBaseContext context)
        {
            this._context = context;
        }

        public User GetUser(Guid id)
        {

            var user = _context.Users.SingleOrDefault(p=> p.Id == id);
            return user;
        }

        public bool ValidateUser(string username,string Password)
        {
            var user = _context.Users.FirstOrDefault();
            return user != null? true : false;
        }
    }
}
