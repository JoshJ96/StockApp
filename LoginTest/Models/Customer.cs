using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LoginTest.Models
{
    public class Customer
    {
        [Required]
        [Range(100000,999999, ErrorMessage = "Please enter a 6 digit customer ID.")]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        
    }
}