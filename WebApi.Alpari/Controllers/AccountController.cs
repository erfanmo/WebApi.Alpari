using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Alpari.Helper;
using WebApi.Alpari.Models.Dto;
using WebApi.Alpari.Models.Services;

namespace WebApi.Alpari.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserRepository _userrepositry;
        private readonly UserTokenRepository _usertokens;
        public AccountController(IConfiguration config, UserTokenRepository usertokens,UserRepository userRepository)
        {
            _config = config;
            _usertokens = usertokens;
            _userrepositry = userRepository;
        }

        private LoginResultDto CreateToken(string UserName,string Password)
        {
            SecurityHelper securityHelper = new SecurityHelper();
             var user = _userrepositry.GetUser(Guid.Parse("669B8E6-319C-48D2-9D36-6D40E205AEFE"));

                var claims = new List<Claim>
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("Name", user.Name),
                };
                string key = _config["JWtConfig:key"];
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var expired = DateTime.Now.AddMinutes(int.Parse(_config["JWtConfig:expires"]));

                var token = new JwtSecurityToken(
                    issuer: _config["JWtConfig:issuer"],
                    audience: _config["JWtConfig:audience"],
                    expires: expired,
                    notBefore: DateTime.Now,
                    claims: claims,
                    signingCredentials: credentials
                    );

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                var refreshToken = Guid.NewGuid();

                _usertokens.SaveToken(new Models.Entities.UserToken
                {
                    TokenExpired = expired,
                    VersionOS = "Windows",
                    TokenHash = securityHelper.Getsha256Hash(jwtToken),
                    user = user,
                    RefreshToken =securityHelper.Getsha256Hash(refreshToken.ToString()),
                    RefreshTokenExpir = DateTime.Now.AddDays(30)
                });
            return new LoginResultDto
            {
                RefreshToken = refreshToken.ToString(),
                Token = jwtToken,
            };
        }

        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefrshToken([FromBody] string refreshToken)
        {
            var userToken = _usertokens.FindRefreshToken(refreshToken);
            if(userToken == null)
                return Unauthorized();
            if(userToken.RefreshTokenExpir < DateTime.Now)
                return Unauthorized("Token Expired");

            return Ok(CreateToken(null, null));
        }

        [HttpPost]
        public IActionResult Post(string UserName, string Password) 
        {
           return Ok(CreateToken(UserName, Password));
        }
    }
}
