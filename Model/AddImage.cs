using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace E-Commerce_Backend.Model
{
    public class AddImage
    {

        
        public long productid { get; set; }

        public IList<IFormFile> imageFiles { get; set; }



    }
}