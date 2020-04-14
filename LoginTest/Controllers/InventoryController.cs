using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LoginTest.DataAccess.Business_Logic.ProductProcessor;
using System.Web.Mvc;
using LoginTest.Models;

namespace LoginTest.Controllers
{
    public class InventoryController : Controller
    {
        public ActionResult ViewInventory()
        {
            return View(LoadProducts());
        }

        public ActionResult Modify()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Modify(string submit, Product product)
        {
            //Button clicked state machine
            switch (submit)
            {
                //Search button clicked
                case "Search":
                    //Take the number input and grab product from DB
                    if (ModelState.IsValidField("Number"))
                    {
                        product = GetByID(product.Number);
                        //var _product = GetProductFromID(product.Number);

                        if (product.Name != null)
                            ViewBag.Valid = "True";
                        if (product.Name == null)
                            ViewBag.Message = "The number entered didn't match any records";
                        
                    }
                    break;

                //Submit button clicked
                case "Submit Changes":

                    //Still show the table even upon reload
                    ViewBag.Valid = "True";
                    //This function will execute only if the modified details are valid
                    if (ModelState.IsValid)
                    {
                        //SQL MODIFY FUNCTION
                        int recordsAffected = ModifyProduct(product);
                    }
                    break;
                default:
                    break;
            }
            return View(product);
        }

        public ActionResult Add()
        {
            return View();
        }

        //view modify
    }
}