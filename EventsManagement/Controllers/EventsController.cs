using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsManagement.Models
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEvent(string title, string date, string time, string duration, string description, string location, int? isPublic, int? upcoming)
        {   
            string a = CRUD.Getfullname(Session["email"].ToString(), Session["password"].ToString());
            Session["fullname"] = a;
            int p, u;
            string selected = Request.Form["isPublic"];
            if (selected == null)
                p = 0;
            else
                p = 1;
            if (upcoming == null)
                u = 0;
            else
                u = 1;
            int result = CRUD.AddEventToDB(title, date, time, duration, description, location, p, Session["fullname"].ToString(), u);
            if (result > 0)
            {
                //ViewBag.Message = "Event Successfully Added";
            }
            else
                ViewBag.Message = "Event could not be added";
            return View();
        }

        public ActionResult EditEvent(string title, string date, string time, string duration, string description, string location, int? isPublic, int? upcoming)
        {
            string a = CRUD.Getfullname(Session["email"].ToString(), Session["password"].ToString());
            Session["fullname"] = a;
            int p, u;
            string selected = Request.Form["isPublic"];
            if (selected == null)
                p = 0;
            else
                p = 1;
            if (upcoming == null)
                u = 0;
            else
                u = 1;
            int result = CRUD.EditEvent(title, date, time, duration, description, location, p, Session["fullname"].ToString(), u);
            if (result > 0)
            {
                //ViewBag.Message = "Event Successfully Edited";
            }
            else
                ViewBag.Message = "Event could not be edited";
            return View();
        }

        public ActionResult DeleteEvent(string title, string date)
        {
            string a = CRUD.Getfullname(Session["email"].ToString(), Session["password"].ToString());
            Session["fullname"] = a;
            int result = CRUD.DeleteEvent(title, date, a);
            if (result > 0)
            {
                
            }
            else
            {
                ViewBag.Message = "Event could not be deleted";
            }
            return View();
        }

        public ActionResult AdminEditEvent(string title, string date, string time, string duration, string description, string location, int? isPublic, int? upcoming)
        {
            int p, u;
            string selected = Request.Form["isPublic"];
            if (selected == null)
                p = 0;
            else
                p = 1;
            if (upcoming == null)
                u = 0;
            else
                u = 1;
            int result = CRUD.EditAdminEvent(title, date, time, duration, description, location, p, u);
            if (result > 0)
            {
                //ViewBag.Message = "Event Successfully Edited";
            }
            else
                ViewBag.Message = "Event could not be edited";
            return View();
        }

        public ActionResult AdminDeleteEvent(string title, string date)
        {
            int result = CRUD.DeleteAdminEvent(title, date);
            if (result > 0)
            {

            }
            else
            {
                ViewBag.Message = "Event could not be deleted";
            }
            return View();
        }
    }

   
}
