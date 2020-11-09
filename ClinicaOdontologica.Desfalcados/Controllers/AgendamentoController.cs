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
    public class AgendamentoController : Controller
    {
        private ClinicaEntities db = new ClinicaEntities();

        // GET: Agendamento
        public ActionResult Index()
        {
            var agendamento = db.Agendamento.Include(a => a.Funcionario).Include(a => a.Paciente).Include(a => a.Servico);
            return View(agendamento.ToList());
        }

        // GET: Agendamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = db.Agendamento.Find(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            return View(agendamento);
        }

        // GET: Agendamento/Create
        public ActionResult Create()
        {

            //ViewBag.ID_FUNCIONARIO = new SelectList(db.Funcionario, "ID_FUNCIONARIO", "NOME", "Selecione um Funcionário");

            ViewBag.ID_PACIENTE = new SelectList(db.Paciente, "ID_PACIENTE", "NOME", "Selecione um Paciente");

            ViewBag.ID_SERVICO = new SelectList(db.Servico, "ID_SERVICO", "SERVICO1", "Selecione um Serviço");
            return View();
        }

        // POST: Agendamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Agendamento agendamento)
        {
            agendamento.DT_INCLUSAO = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Agendamento.Add(agendamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_FUNCIONARIO = new SelectList(db.Funcionario, "ID_FUNCIONARIO", "NOME", agendamento.ID_FUNCIONARIO);
            ViewBag.ID_PACIENTE = new SelectList(db.Paciente, "ID_PACIENTE", "NOME", agendamento.ID_PACIENTE);
            ViewBag.ID_SERVICO = new SelectList(db.Servico, "ID_SERVICO", "SERVICO1", agendamento.ID_SERVICO);
            return View(agendamento);
        }

        // GET: Agendamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = db.Agendamento.Find(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_FUNCIONARIO = new SelectList(db.Funcionario, "ID_FUNCIONARIO", "NOME", agendamento.ID_FUNCIONARIO);
            ViewBag.ID_PACIENTE = new SelectList(db.Paciente, "ID_PACIENTE", "NOME", agendamento.ID_PACIENTE);
            ViewBag.ID_SERVICO = new SelectList(db.Servico, "ID_SERVICO", "SERVICO1", agendamento.ID_SERVICO);
            return View(agendamento);
        }

        // POST: Agendamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_AGENDAMENTO,DT_HORA_ATENDIMENTO,DT_INCLUSAO,FL_CANCELADO,MOTIVO_CANCELAMENTO,VL_ATENDIMENTO,ID_PACIENTE,ID_SERVICO,ID_FUNCIONARIO")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agendamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_FUNCIONARIO = new SelectList(db.Funcionario, "ID_FUNCIONARIO", "NOME", agendamento.ID_FUNCIONARIO);
            ViewBag.ID_PACIENTE = new SelectList(db.Paciente, "ID_PACIENTE", "NOME", agendamento.ID_PACIENTE);
            ViewBag.ID_SERVICO = new SelectList(db.Servico, "ID_SERVICO", "SERVICO1", agendamento.ID_SERVICO);
            return View(agendamento);
        }

        // GET: Agendamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = db.Agendamento.Find(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            return View(agendamento);
        }

        // POST: Agendamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agendamento agendamento = db.Agendamento.Find(id);
            db.Agendamento.Remove(agendamento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual JsonResult DoutoresPorServico(int id)
        {
            var xpto = db.Funcionario
                            .Where(x => x.Cargo.ID_ESPECIALIZACAO == id && x.Cargo.FL_DENTISTA)
                            .OrderBy(x => x.NOME)
                            .Select(x => new { ID_FUNCIONARIO = x.ID_FUNCIONARIO, NOME = x.NOME })
                            .ToList();

            return Json(xpto, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetConvenioServicoValor(int ID_SERVICO, int ID_PACIENTE)
        {



            var servico = db.Servico
                            .Where(x => x.ID_SERVICO == ID_SERVICO)
                            .Select(x => new { ID_P = 1, SERVICO_ID = x.ID_SERVICO, VALOR_SERVICO = x.VALOR })
                            .ToList();

            var convenio = db.Paciente
                                .Where(x => x.ID_PACIENTE == ID_PACIENTE)
                                .Select(x => new { ID_P = 1, CONVENIO = x.Convenio.CONVENIO1, x.Convenio.VALOR_DESCONTO })
                                .ToList();

            var xpto = (from s in servico
                        join c in convenio
                        on s.ID_P equals c.ID_P
                        select new { SERVICO = servico, CONVENIO = convenio }
                        ).ToList();


            return Json(xpto, JsonRequestBehavior.AllowGet);
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
