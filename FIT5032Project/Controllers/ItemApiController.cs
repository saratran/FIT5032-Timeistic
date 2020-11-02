using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FIT5032Project.Models;

namespace FIT5032Project.Controllers
{
    public class ItemApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/item/rating/{id}")]
        public IHttpActionResult ItemRating(Guid id, [FromBody] int rating)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Rating = rating;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(Guid id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}