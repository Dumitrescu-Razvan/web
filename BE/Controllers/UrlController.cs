using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using BE.Data;
using BE.Model;
using System.Security.Claims;


namespace BE.Controllers
{
    [ApiController]
    public class UrlController : Controller
    {
        private readonly Contex _context;

        public UrlController(Contex context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("urls")]
        public IActionResult GetUrls(int page = 1, string category = "All")
        {
            bool hasPreviousPage = false;
            bool hasNextPage = false;
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var urls = new List<Url>();
            if (category == "All")
            {
                //get all urls of the user
                urls = _context.Urls.Where(u => u.UserId == userId).Skip((page - 1) * 4).Take(4).ToList();
            }
            else
            {
                urls = _context.Urls.Where(u => u.UserId == userId && u.category == category).Skip((page - 1) * 4).Take(4).ToList();
            }
            
            if (page > 1)
            {
                hasPreviousPage = true;
            }
            if (_context.Urls.Where(u => u.UserId == userId).Count() > page * 4)
            {
                hasNextPage = true;
            }

            if (urls == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Json(new { urls, hasPreviousPage, hasNextPage }));
            }
        }

        [HttpGet]
        [Route("categories")]
        public IActionResult GetCategories()
        {
            var categories = _context.Urls.Select(u => u.category).Distinct().ToList();

            if (categories == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(categories);
            }
        }

        [HttpPost]
        [Route("urls")]
        public IActionResult AddUrl([FromBody] Url url)
        {
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            Console.WriteLine(userId);
            var userExists = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (userExists == null)
            {
                return NotFound();
            }
                
            Url newUrl = new Url();
            newUrl.url = url.url;
            newUrl.description = url.description;
            newUrl.category = url.category;
            newUrl.UserId = userId;
            _context.Urls.Add(newUrl);
            _context.SaveChanges();
            return Ok(newUrl);
            
        }

        [HttpDelete]
        [Route("url")]
        public IActionResult DeleteUrl(int Id)
        {
            var url = _context.Urls.Where(u => u.Id == Id).FirstOrDefault();

            if (url == null)
            {
                return NotFound();
            }
            else
            {
                _context.Urls.Remove(url);
                _context.SaveChanges();
                return Ok(url);
            }
        }

        [HttpPut]
        [Route("url")]
        public IActionResult EditUrl(int Id, [FromBody] Url url)
        {
            var urlToEdit = _context.Urls.Where(u => u.Id == Id).FirstOrDefault();

            if (urlToEdit == null)
            {
                return NotFound();
            }
            else
            {
                urlToEdit.Id = Id;
                urlToEdit.url = url.url;
                urlToEdit.description = url.description;
                urlToEdit.category = url.category;
                _context.SaveChanges();
                return Ok(urlToEdit);
            }
        }
    }
}
