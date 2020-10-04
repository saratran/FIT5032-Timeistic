using FIT5032Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace FIT5032Project.Controllers
{
    public class TrackerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public TrackerController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: Items
        [Authorize]
        public ActionResult Index()
        {
            var model = new ItemTracker();
            var userId = User.Identity.GetUserId();
            model.Items = db.Items.Where(i => i.User.Id == userId).ToList();
            return View(model);
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "NewItem, Items")] ItemTracker model)
        {
            var item = model.NewItem;
            /*var item = model.NewItem;*/
            item.Id = Guid.NewGuid();
            item.User = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            ModelState.Clear();
            TryValidateModel(item);

            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}