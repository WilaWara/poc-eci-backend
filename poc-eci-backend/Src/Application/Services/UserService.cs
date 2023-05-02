using Microsoft.IdentityModel.Tokens;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _secretKey;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public Task<User> Create(User user)
        {
            return _userRepository.Create(user);
        }

        public string EncodeMD5(string password)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            data = x.ComputeHash(data);
            string encryptedPassword = "";
            for (int i = 0; i < data.Length; i++)
            {
                encryptedPassword += data[i].ToString("x2").ToLower();
            }
            return encryptedPassword;
        }

        public async Task<IDictionary<User, string>> Login(string email, string password)
        {
            //string encryptedPassword = EncodeMD5(password);
            User existingUser = await _userRepository.Login(email, password);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Incorrect email or password");
            }
            return GetToken(existingUser);
        }

        public IDictionary<User, string> GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, user.Role)
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            IDictionary<User, string> userToken = new Dictionary<User, string>
            {
                { user, tokenHandler.WriteToken(token) }
            };

            return userToken;
        }

    }
}
