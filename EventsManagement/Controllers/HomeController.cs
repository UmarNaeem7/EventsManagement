using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;


namespace EventsManagement.Models
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(string email, string password, string fullname)
        {
            int result = CRUD.UserSignUpProc(email, password, fullname);
            //Session["fullname"] = fullname;
            if (result == 1)
            {
                ViewBag.Title = "SIGNUP SUCCESSFUL";
                Session["email"] = null;
                Session["password"] = null;
            }
            else
            {
                ViewBag.Title = "SIGNUP NOT SUCCESSFUL";
                Session["email"] = null;
                Session["password"] = null;
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Post(string email, string password, string fullname)
        {
            int result = CRUD.UserSignUpProc(email, password, fullname);
            //Session["fullname"] = fullname;
            if (result == 1)
            {
                ViewBag.Title = "SIGNUP SUCCESSFUL";
                Session["email"] = null;
                Session["password"] = null;
            }
            else
            {
                ViewBag.Title = "SIGNUP NOT SUCCESSFUL";
                Session["email"] = null;
                Session["password"] = null;
            }
            return View();
        }

        public ActionResult Authenticate(string email, string password)
        {
            if (Session["email"] == null)//If User is not already logged in
            {
                int res = CRUD.UserLoginProc(email, password);
                if (res > 0)
                {
                    ViewBag.message = "Login Successful";
                    Session["email"] = email;
                    Session["password"] = password;

                }
                else
                {
                    ViewBag.Message = "WRONG USERNAME OR PASSWORD";
                    return View("Login");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserLogout()
        {
            Session.Clear();
            return View();
        }

        public ActionResult Profile()
        {
            if (Session["email"] != null)
            {
                string a = CRUD.Getfullname(Session["email"].ToString(), Session["password"].ToString());
                Session["fullname"] = a;
            }
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}