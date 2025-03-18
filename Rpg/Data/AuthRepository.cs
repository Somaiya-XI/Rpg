using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public AuthRepository(DataContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));

            if (user is null)
            {
                response.IsOk = false;
                response.Message = "User with this username does not exist";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.IsOk = false;
                response.Message = "Incorrect Password";
            }
            else
            {
                string token = GenerateToken(user);
                if (!string.IsNullOrEmpty(token))
                    response.Data = GenerateToken(user);
                else
                {
                    response.IsOk = false;
                    response.Message = "Token generation error!";
                }
            }
            return response;

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            if (await UserExists(user.Username))
            {
                response.IsOk = false;
                response.Message = "User already exists";
                return response;
            }
            CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(hash);
            }
        }

        private string GenerateToken(User user)
        {
            try
            {
                if (user is null) throw new ArgumentNullException(nameof(user), "User object cannot be null.");

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

                var appSettingsToken = _config.GetSection("AppSettings:Token").Value;

                if (string.IsNullOrEmpty(appSettingsToken) || appSettingsToken.Length < 64)
                    throw new Exception("JWT Secret Key is too short! It must be at least 64 characters long.");

                SymmetricSecurityKey key = new SymmetricSecurityKey
                (System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

                SigningCredentials creds = new SigningCredentials
                (key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds,
                };

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
            return string.Empty;

        }
    }
}