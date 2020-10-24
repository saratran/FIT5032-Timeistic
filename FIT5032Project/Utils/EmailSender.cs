using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032Project.Utils
{
    public class EmailSender
    {
        private const String API_KEY = "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";

        public async Task SendAsync(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("saraut1479@gmail.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            //System.Diagnostics.Debug.WriteLine(response.Body);
        }

        public async Task SendAsync(String toEmail, String subject, String contents, HttpPostedFileBase fileUploader)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("saraut1479@gmail.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            System.IO.Stream fs = fileUploader.InputStream;
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            var file = Convert.ToBase64String(bytes);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            msg.AddAttachment(fileUploader.FileName, base64String, fileUploader.ContentType, "attachment", "banner");
            var attachment = new SendGrid.Helpers.Mail.Attachment();
            var response = await client.SendEmailAsync(msg);
        }

    }
}