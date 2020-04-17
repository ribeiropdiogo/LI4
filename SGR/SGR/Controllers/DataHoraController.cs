using System.Diagnostics;
using System.Threading.Tasks;
using SGR.Models;
using SGR.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace SGR.Controllers
{
    public class DataHoraController : Controller
    {

        private SGRContext db;

        public DataHoraController(SGRContext context)
        {
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.Horarios = GetHorarios();
            return View(await db.DataHora.ToListAsync());
        }

        [Authorize]
        // GET: DataHora/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DataHora dataHora = db.DataHora.Find(id);
            if (dataHora == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Horarios = db.Horario.Find(dataHora.IdHorario);
            return View(dataHora);
        }

        [Authorize]
        // GET: DataHora/Adicionar
        public ActionResult Adicionar()
        {
            ViewBag.Horarios = GetHorarios();
            return View();
        }

        [Authorize]
        // POST: DataHora/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(DataHora d)
        {
            if (!ModelState.IsValid)
                return View(d);

            db.Add(d);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: DataHora/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DataHora f = db.DataHora.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Horarios = GetHorarios();
            return View(f);
        }

        [Authorize]
        // POST: DataHora/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, DataHora d)
        {
            if (id != d.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(d);
                await db.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            return View(d);
        }

        [Authorize]
        // GET: DataHora/Eliminar/5
        public ActionResult Eliminar(int? id, bool? saveChangesError = false)
        {
            System.Diagnostics.Debug.WriteLine("Mostrou");
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Eliminar falhou. Tente outra vez, e se o problema persistir contacte o administrador.";
            }
            DataHora f = db.DataHora.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            ViewBag.Horarios = db.Horario.Find(f.IdHorario);
            return View(f);
        }

        [Authorize]
        // POST: DataHora/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            System.Diagnostics.Debug.WriteLine("Deteta post");
            try
            {
                System.Diagnostics.Debug.WriteLine("Inicio");
                DataHora f = db.DataHora.Find(id);
                System.Diagnostics.Debug.WriteLine("Encontrou");
                db.DataHora.Remove(f);
                System.Diagnostics.Debug.WriteLine("Apagou");
                await db.SaveChangesAsync();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                System.Diagnostics.Debug.WriteLine("Deu erro");
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

        private IEnumerable<Horario> GetHorarios()
        {
            return db.Horario.ToList();
        }
    }
}


