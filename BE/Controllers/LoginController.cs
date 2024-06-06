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
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly Contex _context;

        public LoginController(Contex context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.Where(u => u.Name == username && u.Password == password).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)), authProperties);
            return Ok(user);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(string username, string password)
        {
            var user = _context.Users.Where(u => u.Name == username).FirstOrDefault();

            if (user != null)
            {
                return NotFound();
            }
            else
            {
                User newUser = new User();
                newUser.Name = username;
                newUser.Password = password;
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return Ok(newUser);
            }
        }

    }


}
