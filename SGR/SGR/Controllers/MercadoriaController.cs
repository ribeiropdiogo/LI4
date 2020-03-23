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

namespace SGR.Controllers
{
    public class MercadoriaController : Controller
    {

        private SGRContext db;

        public MercadoriaController(SGRContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await db.Mercadoria.ToListAsync());
        }


        // GET: Mercadoria/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Mercadoria a = db.Mercadoria.Find(id);
            if (a == null)
            {
                return RedirectToAction("Index");
            }
            return View(a);
        }

        // GET: Mercadoria/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Mercadoria/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(Mercadoria a)
        {
            if (!ModelState.IsValid)
                return View(a);

            db.Add(a);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Mercadoria/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Mercadoria f = db.Mercadoria.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }

        // POST: Mercadoria/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, Mercadoria a)
        {
            if (id != a.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(a);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(a);
        }

        // GET: Mercadoria/Eliminar/5
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
            Mercadoria f = db.Mercadoria.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }

        // POST: Mercadoria/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Mercadoria f = db.Mercadoria.Find(id);
                /*
                List<Reserva> rs = await db.Reserva.ToListAsync();
                foreach (Reserva r in rs)
                    if (r.IdGerente.Equals(id))
                        db.Reserva.Remove(r);*/
                db.Mercadoria.Remove(f);
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


