using InetumAPP.Context;
using InetumAPP.Helper;
using InetumAPP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;

namespace InetumAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {   
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext appDbContext) 
        { 
            _authContext = appDbContext; //to get and save data from the database
        
        }
        [HttpPost("authenticate")]

        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null) 
            {
                return BadRequest();
            }

            var user = await _authContext.Users.
                FirstOrDefaultAsync(x=>x.Email == userObj.Email );

            if (user == null)
                return NotFound(new { Message = "User Not Found !" });

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }

            user.Token = CreateJWT(user);

            return Ok(new {
                Token = user.Token,                                                                       
                Message = "Login Success!" }) ;
             
        }

        [HttpPost("register")]

        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj== null) 
            {
                return BadRequest();
            }

            //Check Email

            if (await CheckEmailExistAsync(userObj.Email))
            {
                return BadRequest(new { Message = "Email Already Exists !" });
            }

            //Check Password Strength

         
            var pass= CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass });

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            await _authContext.Users.AddAsync(userObj); //send user to DB
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "User Registered !"
            }
            );
        }

        //Function to check if Email exists or not
        private Task<bool> CheckEmailExistAsync(string email)
            => _authContext.Users.AnyAsync(x => x.Email == email);

        //Function to check password strength
        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new  ();
            if (password.Length < 8)            
                sb.Append("Minimum Password Length Is 8"+Environment.NewLine);
            
            if (!(Regex.IsMatch(password,"[a-z]") && Regex.IsMatch(password,"[A-Z]")
                && Regex.IsMatch(password,"[0-9]")))
               sb.Append("Password Must Be Alphanumeric"+Environment.NewLine);
            
            if (!Regex.IsMatch(password, "[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]"))
                sb.Append("Password Must Have Special Characters"+Environment.NewLine);
            
            return sb.ToString(); 
        }

        private string CreateJWT (User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim (ClaimTypes.Role,user.Role),
                new Claim (ClaimTypes.Name,$"{user.FirstName}{user.LastName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            } ; 
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
      
        }

        [HttpGet]

        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(await _authContext.Users.ToListAsync());
        }

    }

}
