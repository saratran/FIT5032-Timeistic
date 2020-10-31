using FIT5032Project.Models;
using FIT5032Project.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FIT5032Project.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult ManageUsers()
        {
            return View(db.Users.ToList());
        }

        // GET: Admin/DeleteUser/5
        public async System.Threading.Tasks.Task<ActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/DeleteUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            var logins = user.Logins;
            var rolesForUser = await UserManager.GetRolesAsync(id);
            using (var transaction = db.Database.BeginTransaction())
            {
                foreach (var login in logins.ToList())
                {
                    await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                    }
                }

                await UserManager.DeleteAsync(user);
                transaction.Commit();
            }
            return RedirectToAction("ManageUsers");
        }
        // GET: Email
        public ActionResult Email()
        {
            if (TempData["result"] != null)
            {
                ViewBag.Result = TempData["result"].ToString();
            }
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(SendEmailViewModel model, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var users = GetUsersInRole("User");
                    var emails = users.Select(u => u.Email).ToList();

                    //String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    EmailSender es = new EmailSender();

                    es.SendMultipleAsync(emails, subject, contents, fileUploader);

                    ModelState.Clear();

                    TempData["result"] = "Email has been sent.";
                    return RedirectToAction("Email");
                }
                catch
                {
                    return RedirectToAction("Email");
                }
            }

            return RedirectToAction("Email");
        }
        public List<ApplicationUser> GetUsersInRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = roleManager.FindByName(roleName).Users.First();
            var usersInRole = UserManager.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();
            return usersInRole;
        }
    }

}