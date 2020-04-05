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
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SGR.Controllers
{
    public class MesaController : Controller
    {

        private SGRContext db;

        public MesaController(SGRContext context)
        {
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await db.Mesa.ToListAsync());
        }

        [Authorize]
        // GET: Mesa/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Mesa m = db.Mesa.Find(id);
            if (m == null)
            {
                return RedirectToAction("Index");
            }
            return View(m);
        }

        [Authorize]
        // GET: Mesa/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        [Authorize]
        // POST: Mesa/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(Mesa d)
        {
            if (!ModelState.IsValid)
                return View(d);

            db.Add(d);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: Mesa/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Mesa f = db.Mesa.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }

        [Authorize]
        // POST: Mesa/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, Mesa d)
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
        // GET: Mesa/Eliminar/5
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
            Mesa f = db.Mesa.Find(id);
            if (f == null)
            {
                return NotFound();
            }

            return View(f);
        }

        [Authorize]
        // POST: Mesa/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Mesa f = db.Mesa.Find(id);
                List<Pedido> ps = await db.Pedido.ToListAsync();
                foreach (Pedido p in ps)
                    if (p.Mesa.Equals(id))
                        p.Mesa = 0;
                db.Mesa.Remove(f);
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
    }
}


