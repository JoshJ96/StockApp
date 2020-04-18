using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginTest.Models
{
    public class LineItem
    {
        [Range(100000, 999999, ErrorMessage = "Please enter a 6 digit number")]
        public int Number { get; set; }

        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)")]
        public int Quantity { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}