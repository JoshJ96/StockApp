using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginTest.Controllers
{
    public class AccountingController : Controller
    {
        public ActionResult ManageInvoices()
        {
            return View();
        }
    }
}