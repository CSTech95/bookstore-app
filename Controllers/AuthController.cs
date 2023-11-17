using System.Security.Cryptography;
using System.Text;
using BookStoreApp.Data;
using BookStoreApp.Dto;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.Controllers
{
   public class AuthController : ControllerBase 
    {
        private readonly DataContextDapper _dapper;
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _config = config;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if(userForRegistration.Password == userForRegistration.PasswordConfirm)
            {
                string sqlCheckUserExists = "SELECT Email FROM BookAppSchema.Auth WHERE Email = '" + 
                    userForRegistration.Email + "'";
                IEnumerable<string> existingUser = _dapper.LoadData<string>(sqlCheckUserExists);
                if (existingUser.Count()==0)
                {
                    byte[] passwordSalt = new byte[128 / 8];
                    using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordSalt);
                    }

                    byte[] passwordHash = GetPasswordHash(userForRegistration.Password, passwordSalt);

                    string sqlAddAuth = @"
                        INSERT INTO BookAppSchema.Auth ([Email],
                        [PasswordHash],
                        [PasswordSalt]) VALUES ('" + userForRegistration.Email +
                        "', @PasswordHash, @PasswordSalt)";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSalt", System.Data.SqlDbType.VarBinary);
                    passwordSaltParameter.Value = passwordSalt;

                    SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarBinary);
                    passwordHashParameter.Value = passwordHash;

                    sqlParameters.Add(passwordSaltParameter);
                    sqlParameters.Add(passwordHashParameter);
                    if (_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                    {
                         string sqlAddUser = @"INSERT INTO BookAppSchema.Users(
                           [FirstName],
                                [LastName],
                                [Email],
                                [Gender],
                                [Active] 
                        ) VALUES (" +
                                "'" + userForRegistration.FirstName +
                                "', '" + userForRegistration.LastName +
                                "', '" + userForRegistration.Email +
                                "', '" + userForRegistration.Gender +
                                "', 1)";
                        if(_dapper.Execute(sqlAddUser))
                        {
                        return Ok();
                        }
            throw new Exception("Failed to Add user"); 
                    }
            throw new Exception("Failed to Register user"); 
                }
            throw new Exception("This email already exists"); 
            }
            throw new Exception("Passwords do not match"); 
        }

        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = @"SELECT
                [PasswordHash],
                [PasswordSalt] FROM BookAppSchema.Auth WHERE Email = '" + 
                userForLogin.Email + "'";
            UserForLoginConfirmationDto userForLoginConfirmation = _dapper.LoadDataSingle<UserForLoginConfirmationDto>(sqlForHashAndSalt);

            byte[] passwordHash = GetPasswordHash(userForLogin.Password, userForLoginConfirmation.PasswordSalt);
            for (int i = 0; i < passwordHash.Length; i++)
            {
               if (passwordHash[i] != userForLoginConfirmation.PasswordHash[i])
               {
                    return StatusCode(401, "Incorrect Password");
               } 
            }
            return Ok();
        }
        private byte[] GetPasswordHash(string password, byte[] passwordSalt)
        {
            string passwordSaltPlusString = _config.GetSection("AppSettings: PasswordKey").Value 
                        + Convert.ToBase64String(passwordSalt);

            return KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256/8
            );
        }
    }
}