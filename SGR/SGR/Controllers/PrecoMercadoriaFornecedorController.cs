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
    public class PrecoMercadoriaFornecedorController : Controller
    {

        private SGRContext db;

        public PrecoMercadoriaFornecedorController(SGRContext context)
        {
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.Mercadorias = GetMercadorias();
            ViewBag.Fornecedores = GetFornecedores();
            return View(await db.PrecoMercadoriaFornecedor.ToListAsync());
        }

        [Authorize]
        // GET: PrecoMercadoriaFornecedor/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            PrecoMercadoriaFornecedor a = db.PrecoMercadoriaFornecedor.Find(id);
            if (a == null)
            {
                return RedirectToAction("Index");
            }


            ViewBag.Mercadorias = db.Mercadoria.Find(a.Mercadoria);
            ViewBag.Fornecedores = db.Fornecedor.Find(a.Fornecedor);

            return View(a);
        }

        [Authorize]
        // GET: PrecoMercadoriaFornecedor/Adicionar
        public ActionResult Adicionar()
        {
            ViewBag.Mercadorias = GetMercadorias();
            ViewBag.Fornecedores = GetFornecedores();
            return View();
        }

        [Authorize]
        // POST: PrecoMercadoriaFornecedor/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(PrecoMercadoriaFornecedor a)
        {
            if (!ModelState.IsValid)
                return View(a);

            db.Add(a);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: PrecoMercadoriaFornecedor/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            PrecoMercadoriaFornecedor f = db.PrecoMercadoriaFornecedor.Find(id);

            ViewBag.Mercadorias = GetMercadorias();
            ViewBag.Fornecedores = GetFornecedores();

            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }

        [Authorize]
        // POST: PrecoMercadoriaFornecedor/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, PrecoMercadoriaFornecedor a)
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
        // GET: PrecoMercadoriaFornecedor/Eliminar/5
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
            PrecoMercadoriaFornecedor f = db.PrecoMercadoriaFornecedor.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }

        [Authorize]
        // POST: PrecoMercadoriaFornecedor/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                PrecoMercadoriaFornecedor f = db.PrecoMercadoriaFornecedor.Find(id);
                /*
                List<Reserva> rs = await db.Reserva.ToListAsync();
                foreach (Reserva r in rs)
                    if (r.IdGerente.Equals(id))
                        db.Reserva.Remove(r);*/
                db.PrecoMercadoriaFornecedor.Remove(f);
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
            return db.Mercadoria.ToList();
        }

        private IEnumerable<Fornecedor> GetFornecedores()
        {
            return db.Fornecedor.ToList();
        }
    }
}


