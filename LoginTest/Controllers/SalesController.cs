using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LoginTest.Models;
using static LoginTest.DataAccess.Business_Logic.ProductProcessor;
using static LoginTest.DataAccess.Business_Logic.CustomerProcessor;

namespace LoginTest.Controllers
{
    public class SalesController : Controller
    {
        public ActionResult Initialize()
        {
            //Initialize the new sale
            Order order = new Order();
            order.lineItems = new List<LineItem>();
            Session["Error"] = "";

            //Add an empty line item
            order.lineItems.Add(new LineItem { Number = 0, Quantity = 0 });

            //Save the lineitems list to the session
            Session["LineItems"] = order.lineItems;

            //Move to create tab
            return RedirectToAction("NewSale", order);
        }

        public ActionResult NewSale()
        {
            return View();
        }







        public ActionResult Create(Order order)
        {
            if (order.lineItems == null)
            {
                return RedirectToAction("Initialize");
            }

            //Get line items from session data
            order.lineItems = Session["LineItems"] as List<LineItem>;
            return View(order);
        }

        [HttpPost]
        public ActionResult Create(string submit, Order order, LineItem newLine)
        {
            //Get line items from session data
            order.lineItems = Session["LineItems"] as List<LineItem>;

            //If the "add line" button is clicked
            if (Request.Form["addLine"] != null)
            {
                //Check if form fields are valid
                if (ViewData.ModelState.IsValidField("Number") && ViewData.ModelState.IsValidField("Quantity"))
                {
                    //Find product data from DB
                    var data = GetProductFromID(newLine.Number);

                    //If we found product data
                    if (data.Count != 0) 
                    {
                        //Check quantity of product
                        var quantityOnHand = data[0].Quantity;
                        var quantityWanted = newLine.Quantity;

                        //If the user wants more than we have
                        //TODO: message
                        if (quantityWanted > quantityOnHand)
                        {
                            Session["Error"] = "QuantityTooHigh";
                            return RedirectToAction("Create", order);
                        }

                        //Otherwise, let's update the current order
                        order.lineItems[order.lineItems.Count - 1] = new LineItem
                        { Number = newLine.Number,
                          Quantity = newLine.Quantity};

                        //Add an empty line item for the next entry
                         order.lineItems.Add(new LineItem { Number = 0, Quantity = 0 });

                        //Write it to the session data
                        Session["LineItems"] = order.lineItems;
                    }
                }
                else
                {
                    Session["Error"] = "InvalidProduct";
                    return RedirectToAction("Create", order);
                }
            }

            //If the "finalize" button is clicked
            if (Request.Form["finalize"] != null)
            {
                return RedirectToAction("ReviewOrder", order);
            }

            Session["Error"] = "";
            
            return RedirectToAction("Create", order);
        }

        public ActionResult ReviewOrder(Order order)
        {
            //Get line items from session data
            order.lineItems = Session["LineItems"] as List<LineItem>;

            //Calculate price per line
            foreach (LineItem line in order.lineItems)
            {
                var productData = GetByID(line.Number);

                if (productData != null)
                {   line.Price = productData.Price;
                    line.Location = productData.Location;
                    Session["LineItems"] = order.lineItems;
                }

                var customerData = GetCustomerByID(order.customerID);
                if (customerData != null)
                { Session["CustomerInfo"] = customerData; }                
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult ReviewOrder(string submit, Order order)
        {
            return RedirectToAction("Print", order);
        }



        public ActionResult Print(Order order)
        {
            //Get line items from session data
            order.lineItems = Session["LineItems"] as List<LineItem>;
            order.customerInfo = Session["CustomerInfo"] as Customer;

            return View(order);
        }



        public ActionResult Manage()
        {
            return View();
        }
    }
}