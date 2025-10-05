using EmployeeDetailsWithTab.BusinessLayer;
using EmployeeDetailsWithTab.Filters;
using EmployeeDetailsWithTab.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDetailsWithTab.Controllers
{
    public class UserController : Controller
    {
        //[AllowAnonymus] it works in all action method weather it is authorized or not
        UserBusinessLayer user=new UserBusinessLayer();

        // GET: User
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        //Insert User SignUp Data
        [AllowAnonymous]
        public ActionResult InsertUserSignData(InsertSignDataModel data)
        {
            try
            {
                var response = user.InsertuserSignInData(data);
                return Json(new { success = response.IsSuccess,data = response.Data, message = response.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult UserSignInData(string password,string email)
        {
            try
            {
                var response = user.UserSignInData(password,email);
                return Json(new { success = response.IsSuccess, token = response.Data, message = response.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult ResetPassword()
        {
            return View();
        }


    }
}