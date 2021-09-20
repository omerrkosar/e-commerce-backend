using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E-Commerce_Backend.Model
{
    public class AddCategory
    {
        public string name { get; set; }

        public long parentid { get; set; }

        



    }
}