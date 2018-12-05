using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using InventarioMedicina.Models;

namespace InventarioMedicina.Controllers
{
    public class ProveedorMVCController : Controller
    {
        private InventarioDBEntities db = new InventarioDBEntities();

        // GET: ProveedorMVC
        public ActionResult Index()
        {
            IEnumerable<TBLPROVEEDOR> proveedores = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54469/api/");
                //GET GETAlumnos
                //el siguente codigo obtiene la informacion de manera asincrona y espera hata obtener la data
                var reponseTask = client.GetAsync("tblproveedorapi");
                reponseTask.Wait();
                var result = reponseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // leer todo el cotenido y lo parseamos a una lista de alumno
                    var leer = result.Content.ReadAsAsync<IList<TBLPROVEEDOR>>();
                    leer.Wait();
                    proveedores = leer.Result;
                }
                else
                {
                    proveedores = Enumerable.Empty<TBLPROVEEDOR>();
                    ModelState.AddModelError(string.Empty, "Error...");
                }

            }
            return View(proveedores.ToList());
            //  return View(db.TBLPROVEEDOR.ToList());
        }

        // GET: ProveedorMVC/Details/5
        public ActionResult Details(int? id)
        {
            //Codigo para el detalle  como restfull

            TBLPROVEEDOR proveedores = new TBLPROVEEDOR();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54469/api/");

                // Obtiene asincronamente y esepera hata obteneer la data
                var responsetask = client.GetAsync("tblproveedorapi/" + id);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //leer todo el contenido y parsearlo a la lista
                    var leer = result.Content.ReadAsAsync<TBLPROVEEDOR>();
                    leer.Wait();
                    proveedores = leer.Result;
                }
                else
                {
                    proveedores = null;
                    ModelState.AddModelError(string.Empty, "Error...");
                }
            }
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);

        }

        // GET: ProveedorMVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProveedorMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Proveedor,Nombre,Nit,Telefono")] TBLPROVEEDOR tBLPROVEEDOR)
        {
            if (ModelState.IsValid)
            {
                db.TBLPROVEEDOR.Add(tBLPROVEEDOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBLPROVEEDOR);
        }

        // GET: ProveedorMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLPROVEEDOR tBLPROVEEDOR = db.TBLPROVEEDOR.Find(id);
            if (tBLPROVEEDOR == null)
            {
                return HttpNotFound();
            }
            return View(tBLPROVEEDOR);
        }

        // POST: ProveedorMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Proveedor,Nombre,Nit,Telefono")] TBLPROVEEDOR tBLPROVEEDOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLPROVEEDOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBLPROVEEDOR);
        }

        // GET: ProveedorMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLPROVEEDOR tBLPROVEEDOR = db.TBLPROVEEDOR.Find(id);
            if (tBLPROVEEDOR == null)
            {
                return HttpNotFound();
            }
            return View(tBLPROVEEDOR);
        }

        // POST: ProveedorMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBLPROVEEDOR tBLPROVEEDOR = db.TBLPROVEEDOR.Find(id);
            db.TBLPROVEEDOR.Remove(tBLPROVEEDOR);
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
    }
}
