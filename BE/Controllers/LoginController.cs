using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost(Name = "Login")]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.Where(u => u.Name == username && u.Password == password).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost(Name = "Register")]
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
