using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E-Commerce_Backend.Model
{
    [Table(name: "Product")]
    public class Product
    {
        [Key]
        public long id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        [Required]
        public Category category {get; set; }

        [Required]
        public double price { get; set; }

        public List<Image> images { get; set; } = new List<Image>();
    }
}