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
    public class ProductoMVCController : Controller
    {
        private InventarioDBEntities db = new InventarioDBEntities();

        // GET: ProductoMVC
        public ActionResult Index()
        {
            IEnumerable<TBLPRODUCTO> productos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54469/api/");
                //GET GETAlumnos
                //el siguente codigo obtiene la informacion de manera asincrona y espera hata obtener la data
                var reponseTask = client.GetAsync("tblproductoapi");
                reponseTask.Wait();
                var result = reponseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // leer todo el cotenido y lo parseamos a una lista de alumno
                    var leer = result.Content.ReadAsAsync<IList<TBLPRODUCTO>>();
                    leer.Wait();
                    productos = leer.Result;
                }
                else
                {
                    productos = Enumerable.Empty<TBLPRODUCTO>();
                    ModelState.AddModelError(string.Empty, "Error...");
                }

            }
            return View(productos.ToList());


            //var tBLPRODUCTO = db.TBLPRODUCTO.Include(t => t.TBLPROVEEDOR);
            //return View(tBLPRODUCTO.ToList());
        }

        // GET: ProductoMVC/Details/5
        public ActionResult Details(int? id)
        {
            //Codigo para el detalle  como restfull

            TBLPRODUCTO productos = new TBLPRODUCTO();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54469/api/");

                // Obtiene asincronamente y esepera hata obteneer la data
                var responsetask = client.GetAsync("tblproductoapi/" + id);
                responsetask.Wait();
                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //leer todo el contenido y parsearlo a la lista
                    var leer = result.Content.ReadAsAsync<TBLPRODUCTO>();
                    leer.Wait();
                    productos = leer.Result;
                }
                else
                {
                    productos = null;
                    ModelState.AddModelError(string.Empty, "Error...");
                }
            }
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);



            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //TBLPRODUCTO tBLPRODUCTO = db.TBLPRODUCTO.Find(id);
            //if (tBLPRODUCTO == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(tBLPRODUCTO);
        }

        // GET: ProductoMVC/Create
        public ActionResult Create()
        {
            ViewBag.Proveedor = new SelectList(db.TBLPROVEEDOR, "Id_Proveedor", "Nombre");
            return View();
        }

        // POST: ProductoMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Producto,Nombre,Descripcion,Cantidad,Precio,Estado,Proveedor")] TBLPRODUCTO tBLPRODUCTO)
        {
            if (ModelState.IsValid)
            {
                db.TBLPRODUCTO.Add(tBLPRODUCTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Proveedor = new SelectList(db.TBLPROVEEDOR, "Id_Proveedor", "Nombre", tBLPRODUCTO.Proveedor);
            return View(tBLPRODUCTO);
        }

        // GET: ProductoMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLPRODUCTO tBLPRODUCTO = db.TBLPRODUCTO.Find(id);
            if (tBLPRODUCTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Proveedor = new SelectList(db.TBLPROVEEDOR, "Id_Proveedor", "Nombre", tBLPRODUCTO.Proveedor);
            return View(tBLPRODUCTO);
        }

        // POST: ProductoMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Producto,Nombre,Descripcion,Cantidad,Precio,Estado,Proveedor")] TBLPRODUCTO tBLPRODUCTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBLPRODUCTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Proveedor = new SelectList(db.TBLPROVEEDOR, "Id_Proveedor", "Nombre", tBLPRODUCTO.Proveedor);
            return View(tBLPRODUCTO);
        }

        // GET: ProductoMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLPRODUCTO tBLPRODUCTO = db.TBLPRODUCTO.Find(id);
            if (tBLPRODUCTO == null)
            {
                return HttpNotFound();
            }
            return View(tBLPRODUCTO);
        }

        // POST: ProductoMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBLPRODUCTO tBLPRODUCTO = db.TBLPRODUCTO.Find(id);
            db.TBLPRODUCTO.Remove(tBLPRODUCTO);
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
