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
    public class MercadoriaInArtigoController : Controller
    {

        private SGRContext db;

        public MercadoriaInArtigoController(SGRContext context)
        {
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.Artigos = GetArtigos();
            ViewBag.Mercadorias = GetMercadorias();
            return View(await db.MercadoriaInArtigo.ToListAsync());
        }

        [Authorize]
        // GET: MercadoriaInArtigo/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MercadoriaInArtigo a = db.MercadoriaInArtigo.Find(id);
            if (a == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Artigos = db.Artigo.Find(a.IdArtigo);
            ViewBag.Mercadorias = db.Mercadoria.Find(a.IdMercadoria);

            return View(a);
        }

        [Authorize]
        // GET: MercadoriaInArtigo/Adicionar
        public ActionResult Adicionar()
        {
            ViewBag.Artigos = GetArtigos();
            ViewBag.Mercadorias = GetMercadorias();
            return View();
        }

        [Authorize]
        // POST: MercadoriaInArtigo/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(MercadoriaInArtigo a)
        {
            if (!ModelState.IsValid)
                return View(a);

            db.Add(a);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: MercadoriaInArtigo/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            MercadoriaInArtigo f = db.MercadoriaInArtigo.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Artigos = GetArtigos();
            ViewBag.Mercadorias = GetMercadorias();
            return View(f);
        }

        [Authorize]
        // POST: MercadoriaInArtigo/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, MercadoriaInArtigo a)
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
        // GET: MercadoriaInArtigo/Eliminar/5
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
            MercadoriaInArtigo f = db.MercadoriaInArtigo.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }

        [Authorize]
        // POST: MercadoriaInArtigo/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                MercadoriaInArtigo f = db.MercadoriaInArtigo.Find(id);
                /*
                List<Reserva> rs = await db.Reserva.ToListAsync();
                foreach (Reserva r in rs)
                    if (r.IdGerente.Equals(id))
                        db.Reserva.Remove(r);*/
                db.MercadoriaInArtigo.Remove(f);
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

        private IEnumerable<Mercadoria> GetMercadorias()
        {
            return db.Mercadoria.ToList().OrderBy(c => c.Nome);
        }

        private IEnumerable<Artigo> GetArtigos()
        {
            return db.Artigo.ToList().OrderBy(c => c.Nome);
        }
    }
}


