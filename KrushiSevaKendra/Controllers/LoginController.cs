using KrushiSevaKendra.Context;
using KrushiSevaKendra.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KrushiSevaKendra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly KrushiDBContext _context;

        public readonly IConfiguration _configuration1;
        public LoginController(KrushiDBContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration1 = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> login([FromBody] Admin adminToCheck)
        {
            //var admin = await _context.Admins.Where(admin => admin.Username == adminToCheck.Username).FirstAsync();
            //if (admin == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    if (admin.Password == adminToCheck.Password)
            //    {
            //        return Ok(admin);
            //    }
            //    else
            //    {
            //        return NotFound();
            //    }
            //}

            //jwt
            var admin =  _context.Admins.Where(admin => admin.Username == adminToCheck.Username).ToList();

            if (admin.Count > 0)
            {
                var issuer = _configuration1["Jwt:Issuer"];
                var audience = _configuration1["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes
                (_configuration1["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                         new Claim("Id",admin[0].Id.ToString()),
                         new Claim("Name",admin[0].Name.ToString()),
                         new Claim("Username",admin[0].Username.ToString()),
                         new Claim(JwtRegisteredClaimNames.Jti,
                         Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                //added one more field in admin model
                admin[0].TokenString = stringToken;
                return Ok(admin);
            }
            else
            {
                return Ok();
            }
        }





        //Change password
        //[HttpPost]
        //[Route("change_password/{id}/{password}/{changedPassword}")]
        //public async Task<IActionResult> ChangePasswordPrm([FromRoute] int id, [FromRoute] string password, [FromRoute] string changedPassword)
        //{
        //    var user = await _context.Admins.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    else if (user.Password.Equals(password))
        //    {
        //        user.Password= changedPassword;
        //        await _context.SaveChangesAsync();
        //        return Ok(user);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
            
        //}

        [HttpPost]
        [Route("change_password")]
        public async Task<IActionResult> changePassword([FromBody] UserVerify user)
        {

            Admin admin = await _context.Admins.Where(admin => admin.Username == user.UserName).FirstAsync();
            if (user.Password == admin.Password)
            {
                admin.Password = user.Changepassword;
                await _context.SaveChangesAsync();
                return Ok(admin);
            }
            else
            {
                return BadRequest();
            }
        }



    }
}
