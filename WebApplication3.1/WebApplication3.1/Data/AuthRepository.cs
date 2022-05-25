//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using WebApplication3._1.Models;

//namespace WebApplication3._1.Data
//{
//    public class AuthRepository : IAuthRepository
//    {
//        private readonly DataContext _db;
//        private readonly IConfiguration _configuration;

//        public AuthRepository(DataContext db, IConfiguration configuration)
//        {
//            _db = db;
//            _configuration = configuration;
//        }

//        public async Task<ServiceResponse<string>> Login(string username, string password)
//        {
//            var response = new ServiceResponse<string>();
//            var user = await _db.User.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());

//            if (user == null)
//            {
//                response.Success = false;
//                response.Message = "User not found.";
//            }
//            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
//            {
//                response.Success = false;
//                response.Message = "Wrong password.";
//            }
//            else
//            {
//                response.Data = CreateToken(user);
//            }

//            return response;


//        }

//        public async Task<ServiceResponse<int>> Register(User user, string password)
//        {
//            ServiceResponse<int> response = new ServiceResponse<int>();

//            if (await UserExists(user.Username))
//            {
//                response.Success = false;
//                response.Message = "User already exists.";
//                return response;
//            }

//            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
//            user.PasswordHash = passwordHash;
//            user.PasswordSalt = passwordSalt;

//            _db.User.Add(user);
//            await _db.SaveChangesAsync();


//            response.Data = user.Id;
//            return response;
//        }

//        public async Task<bool> UserExists(string username)
//        {
//            if (await _db.User.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
//                return true;
//            return false;
//        }



//        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//        {
//            using (var hmac = new System.Security.Cryptography.HMACSHA512())
//            {
//                passwordSalt = hmac.Key;
//                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

//            }
//        }

//        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
//        {
//            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
//            {
//                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//                for (int i = 0; i < computedHash.Length; i++)
//                {
//                    if (computedHash[i] != passwordHash[i])
//                        return false;

//                }

//                return true;
//            }
//        }

//        private string CreateToken(User user)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.Username),

//            };

//            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

//            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                Expires = System.DateTime.Now.AddDays(1),
//                SigningCredentials = credentials
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();

//            var token = tokenHandler.CreateToken(tokenDescriptor);

//            return tokenHandler.WriteToken(token);
//        }

//    }
//}
