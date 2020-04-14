using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginTest.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult ViewCustomers()
        {
            return View();
        }
        public ActionResult Manage()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
    }
}