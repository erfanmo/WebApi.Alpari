using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Alpari.Models.Services.Validator
{
    public interface ITokenValidator
    {
        Task Execute(TokenValidatedContext context);
    }

    public class TokenValidate : ITokenValidator
    {
        private UserRepository _userRepository;
        public TokenValidate(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(TokenValidatedContext context)
        {
            var claimidentity = context.Principal.Identities as ClaimsIdentity;
            if (claimidentity == null || !claimidentity.Claims.Any())
            {
                context.Fail("claim not found");
            }
            var userId = claimidentity.FindFirst("UserId").Value;
            if (!Guid.TryParse(userId, out Guid userGuid))
            {
                context.Fail("claim not found ....");
                return;
            }
            var user =_userRepository.GetUser(userGuid);
            if(user.isActive == false)
            {
                context.Fail("claim not found ....");
                return;
            }
        }
    }
}
