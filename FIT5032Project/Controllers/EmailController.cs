using FIT5032Project.Models;
using FIT5032Project.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FIT5032Project.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            if(TempData["result"] != null)
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
                    //String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    EmailSender es = new EmailSender();
                    if (fileUploader != null)
                    {
                        //await es.SendAsync(toEmail, subject, contents, fileUploader);
                    }
                    else
                    {
                        //await es.SendAsync(toEmail, subject, contents);
                    }
                    ModelState.Clear();

                    TempData["result"] = "Email has been sent.";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}