using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto_Rumo;

namespace Projeto_Rumo.Controllers
{
    public class CopaController : Controller
    {
        private barEntities db = new barEntities();

        // GET: Copa
        public ActionResult Index()
        {
            var copa = db.Copa.Include(c => c.Pedido);
            return View(copa.ToList());
        }

        // GET: Copa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copa copa = db.Copa.Find(id);
            if (copa == null)
            {
                return HttpNotFound();
            }
            return View(copa);
        }

        // GET: Copa/Create
        public ActionResult Create()
        {
            ViewBag.Idpedido = new SelectList(db.Pedido, "IdPedido", "PratoDisponivel");
            return View();
        }

        // POST: Copa/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCopa,BebidaEscolhida,Idpedido")] Copa copa)
        {
            if (ModelState.IsValid)
            {
                db.Copa.Add(copa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idpedido = new SelectList(db.Pedido, "IdPedido", "PratoDisponivel", copa.Idpedido);
            return View(copa);
        }

        // GET: Copa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copa copa = db.Copa.Find(id);
            if (copa == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idpedido = new SelectList(db.Pedido, "IdPedido", "PratoDisponivel", copa.Idpedido);
            return View(copa);
        }

        // POST: Copa/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCopa,BebidaEscolhida,Idpedido")] Copa copa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(copa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idpedido = new SelectList(db.Pedido, "IdPedido", "PratoDisponivel", copa.Idpedido);
            return View(copa);
        }

        // GET: Copa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copa copa = db.Copa.Find(id);
            if (copa == null)
            {
                return HttpNotFound();
            }
            return View(copa);
        }

        // POST: Copa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Copa copa = db.Copa.Find(id);
            db.Copa.Remove(copa);
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
