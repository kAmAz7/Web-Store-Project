using DAL.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebProject.ModelDTO;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        UserManager _UserManager = new UserManager();

        [HttpGet]
        public ActionResult Registration()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string username = System.Web.HttpContext.Current.User.Identity.Name;
                UserDTO userDetails = _UserManager.GetUserByUserName(username);
                ViewBag.Title = "Update Details";

                return View(userDetails);
            }
            else
            {
                ViewBag.Title = "Registration";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Registration(UserDTO user)
        {
            if (!ModelState.IsValid)
                return View();
            else
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (_UserManager.UpdateUserDetails(user))
                        return View("SuccessRegister", user);
                }
                else
                {
                    if (_UserManager.CreateUser(user, out bool userExist))
                    {
                        ViewBag.Title = "Registration";
                        FormsAuthentication.SetAuthCookie(user.Username, true);
                        return View("SuccessRegister", user);
                    }
                    else if (userExist)
                    {
                        TempData["UserUnique"] = "This Username already exists, please choose another Username";
                        return View();
                    }
                }
            }
            return View("Error");
        }

        public ActionResult Login(UserDTO user)
        {
            if (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password))
            {
                Session["Cart"] = null;
                bool foundUser = _UserManager.LoginUser(user.Username, user.Password);
                if (foundUser)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["invalidMsg"] = "Invalid username or password";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["invalidMsg"] = "Username and password can not be empty";
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult LogOut(UserDTO user)
        {
            FormsAuthentication.SignOut();
            Session["Cart"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}