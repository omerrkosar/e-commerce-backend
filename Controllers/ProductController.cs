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
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _hostEnvironment;

        public ProductController(ApplicationDbContext context,IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return _context.Product.Include(p => p.category).Include(p => p.images).ToList();
        }


        public ActionResult<Product> GetById(long id)
        {
            var item = _context.Product.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }


        [HttpGet("{category}", Name="product")]
        public ActionResult<List<Category>> GetByCategory(long category)
        {
            var items = _context.Category.Include(c => c.products).ThenInclude(p => p.images).Where(x => x.id == category).ToList();
            if(items == null)
            {
                return NotFound();
            }
            return items;
        }
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] AddProduct product)
        {
            if(product==null)
            {
                return BadRequest();
            }
            Category categoryItem = _context.Category.Find(product.categoryid);
            Product insertItem = new Product 
            {
                price = product.price,
                category = categoryItem,
                name = product.name

            };
            _context.Product.Add(insertItem);
            _context.SaveChanges();
            return CreatedAtAction("GetById",new { id=insertItem.id },insertItem);
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(long id)
        {
            Product item = _context.Product.Include(p=>p.images).FirstOrDefault(item => item.id == id);
            if(item == null)
            {
                return NotFound();
            }

            foreach(var productImage in item.images)
            {
                DeleteImage(productImage.name);
            }
            _context.Product.Remove(item);
            _context.SaveChanges();
            return Ok();

        }

        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(long id,[FromBody] AddProduct product)
        {
            var item = _context.Product.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            Category categoryItem = _context.Category.Find(product.categoryid);
            item.name = product.name;
            item.category = categoryItem;
            item.price = product.price;
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