using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System;
using BE.Data;
using BE.Model;


namespace BE.Controllers
{


    [ApiController]
    public class LoginController : Controller
    {
        private readonly Contex _context;

        public LoginController(Contex context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] User user )
        {
            var userInDb = _context.Users.Where(u => u. username == user. username && u.Password == user.Password).FirstOrDefault();

            if (userInDb == null)
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userInDb.username),
                new Claim(ClaimTypes.NameIdentifier, userInDb.Id.ToString())
            };
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)), authProperties);
            return Ok(userInDb);
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register( [FromBody] User user)
        {
            var userInDb = _context.Users.Where(u => u. username == user. username).FirstOrDefault();
            if (userInDb != null)
            {
                return BadRequest();
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }
    }
}