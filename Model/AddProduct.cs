using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E-Commerce_Backend.Model
{
    public class AddProduct
    {
        public string name { get; set; }
        public long categoryid {get; set; }
        public double price { get; set; }


    }
}