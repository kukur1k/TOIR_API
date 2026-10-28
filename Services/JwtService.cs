using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TOIR_API.Models;

namespace TOIR_API.Services
{
    public class JwtService(IConfiguration config)
    {
        public string Generate(User user)
        {
            // шифруем ключ (в байты) семмитрично из строки для шифроввания (симметрично - один на пордпись и расшифровку)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SignKey"]!));

            // учетные данные - по сути сопоставление ключа и метода шифрования
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // данные что можно получить из токена
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // токен формируем
            var token = new JwtSecurityToken(
                issuer: config["JwtSettings:Issuer"],
                audience: config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ClaimsPrincipal - то, что хранится в HttpContext после авторизации по токену
        public int GetUserId(ClaimsPrincipal principal) =>
            int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)!);

    }
}