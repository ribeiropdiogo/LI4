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
    public class ArtigoController : Controller
    {

        private SGRContext db;

        public ArtigoController(SGRContext context)
        {
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await db.Artigo.ToListAsync());
        }


        [Authorize]
        // GET: Artigo/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Artigo a = db.Artigo.Find(id);
            if (a == null)
            {
                return RedirectToAction("Index");
            }
            return View(a);
        }

        [Authorize]
        // GET: Artigo/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        [Authorize]
        // POST: Artigo/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(Artigo a)
        {
            if (!ModelState.IsValid)
                return View(a);

            db.Add(a);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: Artigo/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Artigo f = db.Artigo.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }

        [Authorize]
        // POST: Artigo/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, Artigo a)
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

        [Authorize]
        // GET: Artigo/Eliminar/5
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
            Artigo f = db.Artigo.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }

        [Authorize]
        // POST: Artigo/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Artigo f = db.Artigo.Find(id);
                /*
                List<Reserva> rs = await db.Reserva.ToListAsync();
                foreach (Reserva r in rs)
                    if (r.IdGerente.Equals(id))
                        db.Reserva.Remove(r);*/
                db.Artigo.Remove(f);
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


