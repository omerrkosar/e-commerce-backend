using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E-Commerce_Backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;
namespace E-Commerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _hostEnvironment;

        public ImageController(ApplicationDbContext context,IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }


        [HttpGet]
        public ActionResult<List<Image>> GetAll()
        {
            return _context.Image.Include(i=>i.product).ToList();
        }

        [HttpGet("{id}", Name="image")]
        public ActionResult<Image> GetById(long id)
        {
            var item = _context.Image.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpPost]
        public ActionResult<Image> CreateImages([FromForm] AddImage image)
        {
            if(image==null)
            {
                return BadRequest();
            }
            Product productItem = _context.Product.Find(image.productid);
                foreach (var item in image.imageFiles)
                {
                    Image insertItem = new Image
                    {
                        
                        name = SaveImage(item),
                        product = productItem,
                    };
                    _context.Image.Add(insertItem);
                    _context.SaveChanges();
                }
                
            
            
            
            
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Image> RemoveImages(long id)
        {
            Product productItem = _context.Product.Include(p => p.images).FirstOrDefault(item => item.id == id);
            foreach (var productImage in productItem.images)
            {
                DeleteImage(productImage.name);
                _context.Image.Remove(productImage);

            }
            _context.SaveChanges();
            return Ok();

        }

        [HttpPut("{id}")]
        public ActionResult<Image> UpdateImages(long id, [FromForm] AddImage image)
        {
            Product productItem = _context.Product.Include(p => p.images).FirstOrDefault(item => item.id == id);
            if(productItem == null)
            {
                return NotFound();
            }
           
            if(productItem.images!=null)
            {
                foreach (var productImage in productItem.images)
                {
                    DeleteImage(productImage.name);
                    _context.Image.Remove(productImage);

                }
            }
            _context.SaveChanges();
            if(image.imageFiles!=null)
            {
                foreach (var item in image.imageFiles)
                {
                    Image insertItem = new Image
                    {
                        
                        name = SaveImage(item),
                        product = productItem,
                    };
                    _context.Image.Add(insertItem);
                    _context.SaveChanges();
                }
            }
            

            return Ok();

        }

        [NonAction]
        public string SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ','-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath,"Images",imageName);
            using (var fileStream = new FileStream(imagePath,FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }
            return imageName;
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