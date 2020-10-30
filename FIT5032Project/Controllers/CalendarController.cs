using FIT5032Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032Project.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public CalendarController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: Items
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var items = db.Items.Where(i => i.User.Id == userId).ToList();
            return View(items);
        }
    }
}