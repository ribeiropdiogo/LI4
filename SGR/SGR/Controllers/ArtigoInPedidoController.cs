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
    public class ArtigoInPedidoController : Controller
    {

        private SGRContext db;

        public ArtigoInPedidoController(SGRContext context)
        {
            db = context;
        }

        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.Artigos = GetArtigos();
            return View(await db.ArtigoInPedido.ToListAsync());
        }
        
        
        [Authorize]
        public async Task<ActionResult> List(int? id)
        {
            ViewBag.Artigos = GetArtigos();
            TempData["NPedido"] = id;
            return View(await db.ArtigoInPedido.Where(a => a.IdPedido == id).ToListAsync());
        }
        

        [Authorize]
        // GET: ArtigoInPedido/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ArtigoInPedido a = db.ArtigoInPedido.Find(id);
            if (a == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Artigos = db.Artigo.Find(a.IdArtigo);
            return View(a);
        }

        [Authorize]
        // GET: ArtigoInPedido/Adicionar
        public ActionResult Adicionar()
        {
            ViewBag.Artigos = GetArtigos();
            return View();
        }

        [Authorize]
        // POST: ArtigoInPedido/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(ArtigoInPedido a)
        {
            if (!ModelState.IsValid)
                return View(a);
            ViewBag.NPedido = TempData["NPedido"];
            a.IdPedido = ViewBag.NPedido;
            db.Add(a);
            await db.SaveChangesAsync();
            return RedirectToAction("List", new { id = TempData["NPedido"] });
        }

        [Authorize]
        // GET: ArtigoInPedido/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ArtigoInPedido f = db.ArtigoInPedido.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Artigos = GetArtigos();
            return View(f);
        }

        [Authorize]
        // POST: ArtigoInPedido/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, ArtigoInPedido a)
        {
            if (id != a.Id)
            {
                return NotFound();
            }
            ViewBag.NPedido = TempData["NPedido"];
            a.IdPedido = ViewBag.NPedido;
            if (ModelState.IsValid)
            {
                db.Update(a);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(a);
        }

        [Authorize]
        // GET: ArtigoInPedido/Eliminar/5
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
            ArtigoInPedido f = db.ArtigoInPedido.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            ViewBag.Artigos = db.Artigo.Find(f.IdArtigo);
            return View(f);
        }

        [Authorize]
        // POST: ArtigoInPedido/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                ArtigoInPedido f = db.ArtigoInPedido.Find(id);
                /*
                List<Reserva> rs = await db.Reserva.ToListAsync();
                foreach (Reserva r in rs)
                    if (r.IdGerente.Equals(id))
                        db.Reserva.Remove(r);*/
                db.ArtigoInPedido.Remove(f);
                await db.SaveChangesAsync();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Eliminar", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("List", new { id = TempData["NPedido"] });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<Artigo> GetArtigos()
        {
            return db.Artigo.ToList().OrderBy(c => c.Nome);
        }
    }
}


