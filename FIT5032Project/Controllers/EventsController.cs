using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using FIT5032Project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FIT5032Project.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected UserManager<ApplicationUser> UserManager { get; set; }

        public EventsController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }
        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            //if (TempData["error"] != null)
            //{
            //    ViewBag.Result = TempData["error"].ToString();
            //}
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "Id,Name,StartTime,EndTime,Date")] Event @event)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var events = db.Events.Where(e => e.User.Id == user.Id).ToList();
            foreach (Event e in events)
            {
                // Check if new event overlap with any others from the same user
                if (OverlappingEvents(@event, e))
                {
                    ViewBag.Result = "Cannot create overlapping event";
                    return View(@event);
                }
            }

            @event.Id = Guid.NewGuid();
            @event.User = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            ModelState.Clear();
            TryValidateModel(@event);
            if (ModelState.IsValid)
            {
                @event.Id = Guid.NewGuid();
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StartTime,EndTime,Date")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static bool OverlappingPeriods(DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
        {
            if (aStart > aEnd)
                throw new ArgumentException("A start can not be after its end.");

            if (bStart > bEnd)
                throw new ArgumentException("B start can not be after its end.");

            return !((aEnd < bStart && aStart < bStart) ||
                        (bEnd < aStart && bStart < aStart));
        }

        public static bool OverlappingEvents(Event e1, Event e2)
        {
            var e1Start = new DateTime(e1.Date.Year, e1.Date.Month, e1.Date.Day, e1.StartTime.Hour, e1.StartTime.Minute, 0);
            var e1End= new DateTime(e1.Date.Year, e1.Date.Month, e1.Date.Day, e1.EndTime.Hour, e1.EndTime.Minute, 0);
            if (e1.EndTime < e1.StartTime) {
                e1End.AddDays(1);
            }
            var e2Start = new DateTime(e2.Date.Year, e2.Date.Month, e2.Date.Day, e2.StartTime.Hour, e2.StartTime.Minute, 0);
            var e2End = new DateTime(e2.Date.Year, e2.Date.Month, e2.Date.Day, e2.EndTime.Hour, e2.EndTime.Minute, 0);
            if (e2.EndTime < e2.StartTime)
            {
                e2End.AddDays(1);
            }

            return OverlappingPeriods(e1Start, e1End, e2Start, e1Start);
        }
    }
}
