using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FIT5032Project.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ReportController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }


        // GET: Report
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var items = db.Items.Where(i => i.User.Id == userId).OrderByDescending(item => item.Date).ThenBy(item => item.StartTime).ToList();
            return View(items);
        }


    }
}
