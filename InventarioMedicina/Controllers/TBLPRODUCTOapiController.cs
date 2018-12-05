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
using InventarioMedicina.Models;

namespace InventarioMedicina.Controllers
{
    public class TBLPRODUCTOapiController : ApiController
    {
        private InventarioDBEntities db = new InventarioDBEntities();

        // GET: api/TBLPRODUCTOapi
        public IQueryable<TBLPRODUCTO> GetTBLPRODUCTO()
        {
            return db.TBLPRODUCTO;
        }

        // GET: api/TBLPRODUCTOapi/5
        [ResponseType(typeof(TBLPRODUCTO))]
        public IHttpActionResult GetTBLPRODUCTO(int id)
        {
            TBLPRODUCTO tBLPRODUCTO = db.TBLPRODUCTO.Find(id);
            if (tBLPRODUCTO == null)
            {
                return NotFound();
            }

            return Ok(tBLPRODUCTO);
        }

        // PUT: api/TBLPRODUCTOapi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTBLPRODUCTO(int id, TBLPRODUCTO tBLPRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tBLPRODUCTO.Id_Producto)
            {
                return BadRequest();
            }

            db.Entry(tBLPRODUCTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TBLPRODUCTOExists(id))
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

        // POST: api/TBLPRODUCTOapi
        [ResponseType(typeof(TBLPRODUCTO))]
        public IHttpActionResult PostTBLPRODUCTO(TBLPRODUCTO tBLPRODUCTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TBLPRODUCTO.Add(tBLPRODUCTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tBLPRODUCTO.Id_Producto }, tBLPRODUCTO);
        }

        // DELETE: api/TBLPRODUCTOapi/5
        [ResponseType(typeof(TBLPRODUCTO))]
        public IHttpActionResult DeleteTBLPRODUCTO(int id)
        {
            TBLPRODUCTO tBLPRODUCTO = db.TBLPRODUCTO.Find(id);
            if (tBLPRODUCTO == null)
            {
                return NotFound();
            }

            db.TBLPRODUCTO.Remove(tBLPRODUCTO);
            db.SaveChanges();

            return Ok(tBLPRODUCTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TBLPRODUCTOExists(int id)
        {
            return db.TBLPRODUCTO.Count(e => e.Id_Producto == id) > 0;
        }
    }
}