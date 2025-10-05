using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDetailsWithTab.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult EmployeeCreation()
        {
            return View();
        }
        public ActionResult InsertEmployee()
        {
            return null;
        }
    }
}