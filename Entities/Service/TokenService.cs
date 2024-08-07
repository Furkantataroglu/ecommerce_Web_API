using Entities.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;
        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _userManager = userManager;
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };


            var roles = _userManager.GetRolesAsync(user).Result; // Synchronously wait for roles
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //token özellikleri
            var tokenDescriptor = new SecurityTokenDescriptor
            {    // Token'a eklenen claim'ler (kullanıcı bilgileri, roller vs.)
                Subject = new ClaimsIdentity(claims),

                // Token'ın son kullanma tarihi (7 gün sonra geçersiz hale geliyor)
                Expires = DateTime.Now.AddDays(7), // 7 gün sonra token expire oluyor.

                // Token'ı imzalamak için kullanılan güvenlik kimlik bilgileri (şifreleme anahtarı ve algoritması)
                SigningCredentials = creds,

                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
                
            };

            //token oluşturucu
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
