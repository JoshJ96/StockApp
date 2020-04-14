using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LoginTest.Models
{
    public class Product
    {
        [Range(100000, 999999, ErrorMessage = "Please enter a 6 digit number")]
        public int Number { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Brand { get; set; }
    }
}