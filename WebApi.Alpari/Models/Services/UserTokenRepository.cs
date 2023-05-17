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
    }
}
