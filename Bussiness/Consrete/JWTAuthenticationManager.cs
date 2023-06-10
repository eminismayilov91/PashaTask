using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bussiness.Consrete
{
    public class JWTAuthenticationManager
    {
        private readonly string _key;
        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
        {
            {"user1","pass1" },
            {"user2","pass2" }
        };

        public JWTAuthenticationManager(string key)
        {
            _key = key;
        }

        public string Authenticate(string username, string password)
        {
            if (!_users.Any(u=>u.Key == username && u.Value == password))
            {
                return null;
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
