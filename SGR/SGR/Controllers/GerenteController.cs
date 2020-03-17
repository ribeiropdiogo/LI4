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

namespace SGR.Controllers
{
    public class GerenteController : Controller
    {

        private SGRContext db;

        public GerenteController(SGRContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await db.Gerente.ToListAsync());
        }


        // GET: Gerente/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Gerente gerente = db.Gerente.Find(id);
            if (gerente == null)
            {
                return RedirectToAction("Index");
            }
            return View(gerente);
        }

        // GET: Gerente/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Gerente/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(Gerente gerente)
        {
            if (!ModelState.IsValid)
                return View(gerente);

            db.Add(gerente);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Gerente/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Gerente f = db.Gerente.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }

        // POST: Gerente/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, Gerente gerente)
        {
            if (id != gerente.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(gerente);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(gerente);
        }

        // GET: Gerente/Eliminar/5
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
            Gerente f = db.Gerente.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }

        // POST: Gerente/Eliminar/5
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Gerente f = db.Gerente.Find(id);
                db.Gerente.Remove(f);
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


