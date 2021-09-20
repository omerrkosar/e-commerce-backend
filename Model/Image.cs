using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace E-Commerce_Backend.Model
{
    [Table(name: "Image")]
    public class Image
    {
        [Key]
        public long id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        
        [Required]
        public Product product { get; set; }

        [NotMapped]
        public IFormFile imageFile { get; set; }
        



    }
}