using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Alpari.Helper;
using WebApi.Alpari.Models.Context;
using WebApi.Alpari.Models.Entities;

namespace WebApi.Alpari.Models.Services
{
    public class UserTokenRepository
    {
        private readonly DataBaseContext _context;
        public UserTokenRepository(DataBaseContext context)
        {
            _context = context;
        }
        public void SaveToken(UserToken token)
        {
            _context.userTokens.Add(token);
            _context.SaveChanges();
        }

        public UserToken FindRefreshToken(string refreshTOken)
        {
            SecurityHelper helper= new SecurityHelper();
            string hashRefreshToken = helper.Getsha256Hash(refreshTOken);

           var user  = _context.userTokens.Include(p=> p.user).SingleOrDefault(x => x.RefreshToken == hashRefreshToken);
            return user;

        }

        public void DeleteToken(string RefreshToken)
        {
            var token = FindRefreshToken(RefreshToken);
            if (token != null)
            {
                _context.userTokens.Remove(token);
                _context.SaveChanges();
            }

        }
    }
}
