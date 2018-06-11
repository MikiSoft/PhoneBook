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
using PhoneBook.Models;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace PhoneBook.Controllers
{
    public class ItemsController : ApiController
    {
        private ItemContext db = new ItemContext();

        // GET: api/Items(?last_name=)
        public IQueryable<Item> GetItems(string last_name = null)
        {
            return last_name != null ? db.Items.Where(e => e.LastName == last_name) : db.Items;
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.ID)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

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

        // POST: api/Items
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Items.Count(e => e.PhoneNum == item.PhoneNum) > 0)
            {
                return BadRequest("Specified phone number already exists.");
            }

            db.Items.Add(item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
        }

        // DELETE: api/Items/?ids=5(,6,...)
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteItems(string ids)
        {
            List<string> a_ids = ids.Split(',').ToList();
            IQueryable<Item> items = db.Items.Where(e => a_ids.Contains(e.ID.ToString()));
            if (items.Count() == 0)
            {
                return NotFound();
            }

            foreach (var item in items) {
                db.Items.Remove(item);
            }
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.ID == id) > 0;
        }
    }
}