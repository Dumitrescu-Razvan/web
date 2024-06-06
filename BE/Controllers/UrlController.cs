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
    public class UrlController : Controller
    {
        private readonly Contex _context;

        public UrlController(Contex context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetUrls")]
        public IActionResult GetUrls(int page = 1, string category = "All")
        {
            var urls = _context.Urls.Where(u => u.category == category).Skip((page - 1) * 10).Take(10).ToList();

            if (urls == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(urls);
            }
        }

        [HttpGet(Name = "GetCategories")]
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

        [HttpPost(Name = "AddUrl")]
        public IActionResult AddUrl(string url, string description, string category, int userId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                Url newUrl = new Url();
                newUrl.url = url;
                newUrl.description = description;
                newUrl.category = category;
                newUrl.UserId = userId;
                _context.Urls.Add(newUrl);
                _context.SaveChanges();
                return Ok(newUrl);
            }
        }

        [HttpPost(Name = "DeleteUrl")]
        public IActionResult DeleteUrl(int urlId)
        {
            var url = _context.Urls.Where(u => u.Id == urlId).FirstOrDefault();

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

        [HttpPost(Name = "EditUrl")]
        public IActionResult EditUrl(int urlId, string url, string description, string category)
        {
            var urlToEdit = _context.Urls.Where(u => u.Id == urlId).FirstOrDefault();

            if (urlToEdit == null)
            {
                return NotFound();
            }
            else
            {
                urlToEdit.url = url;
                urlToEdit.description = description;
                urlToEdit.category = category;
                _context.SaveChanges();
                return Ok(urlToEdit);
            }
        }


    }
}
