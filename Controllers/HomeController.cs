using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDetailsWithTab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult BasicInformation()
        {
            return PartialView("_BasicInformation");
        }
        //Insert Basic Information
        public ActionResult InsertBasicInfromation()
        {
            return View();
        }

        public PartialViewResult EducationDetails()
        {
            return PartialView("_EducationDetails");
        }
        public PartialViewResult ExperianceDetails()
        {
            return PartialView("_ExperianceDetails");
        }
        public PartialViewResult CTCDetails()
        {
            return PartialView("_CTCDetails");
        }
        public PartialViewResult FamilyDetails()
        {
            return PartialView("_FamilyDetails");
        }
        public PartialViewResult AddressDetails()
        {
            return PartialView("_AddressDetails");
        }
        public PartialViewResult OtherDetails()
        {
            return PartialView("_OtherDetails");
        }
    }
}