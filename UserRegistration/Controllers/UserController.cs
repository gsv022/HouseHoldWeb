using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using UserRegistration.Models;
using System.Web.Security;


namespace UserRegistration.Controllers
{
    public class UserController : Controller
    {
       
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            User userModel = new User();

            return View(userModel);
        }
        [HttpPost]
        public ActionResult AddOrEdit(User UserModel)
        {
            using (DbModels dbModel=new DbModels())
            {
                if(dbModel.Users.Any(x=>x.Username==UserModel.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exists";
                    return View("AddOrEdit", UserModel);
                }
                dbModel.Users.Add(UserModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful.";
            return View("AddOrEdit",new User());
        }



        [HttpGet]
        public ActionResult LogOn()
        {
         return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(User UserModel)
        {
            using (DbModels db = new DbModels())
            {
                var usr = db.Users.Single(u => u.Username == UserModel.Username && u.Password == UserModel.Password);
                if (usr != null)
                {
                    Session["UserId"] = usr.UserID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }

            }
            return View();
        }
        public ActionResult LoggedIn()
        {
            if(Session["UserId"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogOn");
            }
           
        }



        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn", "User");
        }




    }
}