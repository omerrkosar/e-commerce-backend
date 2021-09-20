using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E-Commerce_Backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;
namespace E-Commerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _hostEnvironment;
        public CategoryController(ApplicationDbContext context,IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            return _context.Category.ToList();
        }

        [HttpGet("{id}", Name="category")]
        public ActionResult<Category> GetById(long id)
        {
            var item = _context.Category.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpPost]
        public ActionResult<Category> CreateCategory([FromBody] AddCategory category)
        {
            if(category==null)
            {
                return BadRequest();
            }
            Category insertItem = new Category
            {
                name = category.name,
            };
            
            
            _context.Category.Add(insertItem);
            _context.SaveChanges();
            return CreatedAtAction("GetById",new { id=insertItem.id },insertItem);
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> DeleteCategory(long id)
        {
            Category item = _context.Category.Include(c=>c.products).ThenInclude(p=>p.images).FirstOrDefault(item => item.id == id);

            if(item == null)
            {
                return NotFound();
            }
            foreach(var product in item.products)
            {
                foreach(var productImage in product.images)
                {
                    DeleteImage(productImage.name);
                }
            }
            _context.Category.Remove(item);
            _context.SaveChanges();
            return Ok();

        }

        [HttpPut("{id}")]
        public ActionResult<Category> UpdateCategory(long id,AddCategory category)
        {
            var item = _context.Category.FirstOrDefault(item => item.id == id);
            if(item == null)
            {
                return NotFound();
            }
            item.name = category.name;
            try
            {
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }

            return item;

        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images",imageName);
            if(System.IO.File.Exists(imagePath))
            {   
                System.IO.File.Delete(imagePath);
            }
        }

    }
}