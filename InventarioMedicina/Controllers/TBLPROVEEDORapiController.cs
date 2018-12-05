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
    public class TBLPROVEEDORapiController : ApiController
    {
        private InventarioDBEntities db = new InventarioDBEntities();

        // GET: api/TBLPROVEEDORapi
        public IQueryable<TBLPROVEEDOR> GetTBLPROVEEDOR()
        {
            return db.TBLPROVEEDOR;
        }

        // GET: api/TBLPROVEEDORapi/5
        [ResponseType(typeof(TBLPROVEEDOR))]
        public IHttpActionResult GetTBLPROVEEDOR(int id)
        {
            TBLPROVEEDOR tBLPROVEEDOR = db.TBLPROVEEDOR.Find(id);
            if (tBLPROVEEDOR == null)
            {
                return NotFound();
            }

            return Ok(tBLPROVEEDOR);
        }

        // PUT: api/TBLPROVEEDORapi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTBLPROVEEDOR(int id, TBLPROVEEDOR tBLPROVEEDOR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tBLPROVEEDOR.Id_Proveedor)
            {
                return BadRequest();
            }

            db.Entry(tBLPROVEEDOR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TBLPROVEEDORExists(id))
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

        // POST: api/TBLPROVEEDORapi
        [ResponseType(typeof(TBLPROVEEDOR))]
        public IHttpActionResult PostTBLPROVEEDOR(TBLPROVEEDOR tBLPROVEEDOR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TBLPROVEEDOR.Add(tBLPROVEEDOR);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tBLPROVEEDOR.Id_Proveedor }, tBLPROVEEDOR);
        }

        // DELETE: api/TBLPROVEEDORapi/5
        [ResponseType(typeof(TBLPROVEEDOR))]
        public IHttpActionResult DeleteTBLPROVEEDOR(int id)
        {
            TBLPROVEEDOR tBLPROVEEDOR = db.TBLPROVEEDOR.Find(id);
            if (tBLPROVEEDOR == null)
            {
                return NotFound();
            }

            db.TBLPROVEEDOR.Remove(tBLPROVEEDOR);
            db.SaveChanges();

            return Ok(tBLPROVEEDOR);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TBLPROVEEDORExists(int id)
        {
            return db.TBLPROVEEDOR.Count(e => e.Id_Proveedor == id) > 0;
        }
    }
}