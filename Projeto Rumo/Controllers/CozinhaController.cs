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
    public class CozinhaController : Controller
    {
        private barEntities db = new barEntities();

        // GET: Cozinha
        public ActionResult Index()
        {
            var cozinha = db.Cozinha.Include(c => c.Pedido);
            return View(cozinha.ToList());
        }

        // GET: Cozinha/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cozinha cozinha = db.Cozinha.Find(id);
            if (cozinha == null)
            {
                return HttpNotFound();
            }
            return View(cozinha);
        }

        // GET: Cozinha/Create
        public ActionResult Create()
        {
            ViewBag.Idpedido = new SelectList(db.Pedido, "IdPedido", "PratoDisponivel");
            return View();
        }

        // POST: Cozinha/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCozinha,PratoEscolhido,Idpedido")] Cozinha cozinha)
        {
            if (ModelState.IsValid)
            {
                db.Cozinha.Add(cozinha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idpedido = new SelectList(db.Pedido, "IdPedido", "PratoDisponivel", cozinha.Idpedido);
            return View(cozinha);
        }

        // GET: Cozinha/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cozinha cozinha = db.Cozinha.Find(id);
            if (cozinha == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idpedido = new SelectList(db.Pedido, "IdPedido", "PratoDisponivel", cozinha.Idpedido);
            return View(cozinha);
        }

        // POST: Cozinha/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCozinha,PratoEscolhido,Idpedido")] Cozinha cozinha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cozinha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idpedido = new SelectList(db.Pedido, "IdPedido", "PratoDisponivel", cozinha.Idpedido);
            return View(cozinha);
        }

        // GET: Cozinha/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cozinha cozinha = db.Cozinha.Find(id);
            if (cozinha == null)
            {
                return HttpNotFound();
            }
            return View(cozinha);
        }

        // POST: Cozinha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cozinha cozinha = db.Cozinha.Find(id);
            db.Cozinha.Remove(cozinha);
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
