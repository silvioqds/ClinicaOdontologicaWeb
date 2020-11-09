using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaOdontologica.Desfalcados.Models;

namespace ClinicaOdontologica.Desfalcados.Controllers
{
    [Authorize]
    public class ServicoController : Controller
    {
        private ClinicaEntities db = new ClinicaEntities();

        // GET: Servico
        public ActionResult Index()
        {
            var servico = db.Servico.Include(s => s.Especializacao);
            return View(servico.ToList());
        }

        // GET: Servico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servico.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // GET: Servico/Create
        public ActionResult Create()
        {
            ViewBag.ID_ESPECIALIZACAO = new SelectList(db.Especializacao, "ID_ESPECIALIZACAO", "ESPECIALIZACAO1");
            return View();
        }

        // POST: Servico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SERVICO,SERVICO1,VALOR,ID_ESPECIALIZACAO")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                db.Servico.Add(servico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_ESPECIALIZACAO = new SelectList(db.Especializacao, "ID_ESPECIALIZACAO", "ESPECIALIZACAO1", servico.ID_ESPECIALIZACAO);
            return View(servico);
        }

        // GET: Servico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servico.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ESPECIALIZACAO = new SelectList(db.Especializacao, "ID_ESPECIALIZACAO", "ESPECIALIZACAO1", servico.ID_ESPECIALIZACAO);
            return View(servico);
        }

        // POST: Servico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SERVICO,SERVICO1,VALOR,ID_ESPECIALIZACAO")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ESPECIALIZACAO = new SelectList(db.Especializacao, "ID_ESPECIALIZACAO", "ESPECIALIZACAO1", servico.ID_ESPECIALIZACAO);
            return View(servico);
        }

        // GET: Servico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servico.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servico servico = db.Servico.Find(id);
            db.Servico.Remove(servico);
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
