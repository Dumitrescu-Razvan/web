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

        [HttpGet]
        [Route("GetUrls")]
        public IActionResult GetUrls(int page = 1, string category = "All")
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier").Value;
            var urls = new List<Url>();
            if (category == "All")
            {
                //get all urls of the user
                urls = _context.Urls.Where(u => u.UserId == int.Parse(userId)).Skip((page - 1) * 4).Take(4).ToList();
            }
            else
            {
                urls = _context.Urls.Where(u => u.UserId == int.Parse(userId) && u.category == category).Skip((page - 1) * 4).Take(4).ToList();
            }

            if (urls == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(urls);
            }
        }

        [HttpGet]
        [Route("GetCategories")]
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
        [Route("AddUrl")]
        public IActionResult AddUrl(string url, string description, string category)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier").Value;
                
            Url newUrl = new Url();
            newUrl.url = url;
            newUrl.description = description;
            newUrl.category = category;
            newUrl.UserId = int.Parse(userId);
            _context.Urls.Add(newUrl);
            _context.SaveChanges();
            return Ok(newUrl);
            
        }

        [HttpPost]
        [Route("DeleteUrl")]
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

        [HttpPost]
        [Route("EditUrl")]
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
