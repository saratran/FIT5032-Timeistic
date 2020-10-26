using FIT5032Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032Project.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

      
        public ActionResult ManageUsers()
        {
            return View(db.Users.ToList());
        }

    }
}