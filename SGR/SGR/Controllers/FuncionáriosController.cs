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
    public class FuncionáriosController : Controller
    {

        private SGRContext db;

        public FuncionáriosController(SGRContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        { 
            return View(await db.Funcionários.ToListAsync());
        }


        // GET: Funcionários/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Funcionário funcionario = db.Funcionários.Find(id);
            if (funcionario == null)
            {
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // GET: Funcionário/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Funcionário/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(Funcionário funcionario)
        {
            if (!ModelState.IsValid)
                return View(funcionario);

            db.Add(funcionario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Funcionário/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Funcionário f = db.Funcionários.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }

        // POST: Funcionário/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, Funcionário funcionario)
        {
            if (id != funcionario.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(funcionario);
                await db.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // GET: Funcionário/Eliminar/5
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
            Funcionário f = db.Funcionários.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }

        // POST: Funcionário/Eliminar/5
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Funcionário f = db.Funcionários.Find(id);
                db.Funcionários.Remove(f);
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


