using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginTest.Models
{
    public class Order
    {
        //[Required]
        //[Range(100000, 999999, ErrorMessage = "Please enter a 6-digit customer ID number")]
        public int customerID { get; set; }

        public Customer customerInfo { get; set; }

        public List<LineItem> lineItems { get; set; }

        [Required]
        public string shippingMethod { get; set; }

        [Required]
        public string paymentMethod { get; set; }
    }
}