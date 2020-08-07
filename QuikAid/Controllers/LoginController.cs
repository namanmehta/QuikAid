using QuikAid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using QuikAid.Models;
namespace QuikAid.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Authorize(QuikAid.Models.UserTable userModels)
        {
            using (LoginDBEntities db = new LoginDBEntities())
            {
                var userDetails = db.UserTables.Where(x => x.userName == userModels.userName && x.password == userModels.password).FirstOrDefault();
                if(userDetails == null)
                {
                    ViewBag.errorLogin = "Please enter correct credentials";
                    return View("Index", userModels);
                }
                else
                {
                    Session["userID"] = userDetails.UserId;
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
    }
}