using System.Diagnostics;
using System.Threading.Tasks;
using SGR.Models;
using SGR.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SGR.Controllers
{
    public class HorarioController : Controller
    {

        private SGRContext db;

        public HorarioController(SGRContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await db.Horario.ToListAsync());
        }

        [Authorize]
        // GET: Horario/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Horario horario = db.Horario.Find(id);
            if (horario == null)
            {
                return RedirectToAction("Index");
            }
            return View(horario);
        }

        [Authorize]
        // GET: Horario/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        [Authorize]
        // POST: Horario/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(Horario horario)
        {
            if (!ModelState.IsValid)
                return View(horario);

            db.Add(horario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: Horario/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Horario f = db.Horario.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }

        [Authorize]
        // POST: Horario/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, Horario horario)
        {
            if (id != horario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(horario);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(horario);
        }

        [Authorize]
        // GET: Horario/Eliminar/5
        public ActionResult Eliminar(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Eliminar falhou. Tente outra vez, e se o problema persistir contacte o administrador.";
            }
            Horario f = db.Horario.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }

        [Authorize]
        // POST: Horario/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Horario f = db.Horario.Find(id);
                List<DataHora> dhs = await db.DataHora.ToListAsync();
                foreach (DataHora dh in dhs)
                    if (dh.IdHorario.Equals(id))
                        db.DataHora.Remove(dh);
                List<Funcionario> fs = await db.Funcionario.ToListAsync();
                foreach (Funcionario fu in fs)
                    if (fu.IdHorario.Equals(id)) {
                        fu.IdHorario = 0;
                        db.Update(fu);
                    }
                db.Horario.Remove(f);
                await db.SaveChangesAsync();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Eliminar", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


