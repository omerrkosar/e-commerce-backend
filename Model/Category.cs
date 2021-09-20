using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E-Commerce_Backend.Model
{
    [Table(name: "Category")]
    public class Category
    {
        [Key]
        public long id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        public List<Product> products { get; set; } = new List<Product>();

        



    }
}