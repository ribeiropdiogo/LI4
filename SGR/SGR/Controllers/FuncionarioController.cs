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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;

namespace SGR.Controllers
{
    public class FuncionarioController : Controller
    {

        private SGRContext db;

        public FuncionarioController(SGRContext context)
        {
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.Horarios = GetHorarios();
            return View(await db.Funcionario.ToListAsync());
        }

        [Authorize]
        // GET: Funcionario/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Funcionario funcionario = db.Funcionario.Find(id);
            if (funcionario == null)
            {
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        [Authorize]
        // GET: Funcionario/Adicionar
        public ActionResult Adicionar()
        {
            ViewBag.Horarios = GetHorarios();
            return View();
        }

        [Authorize]
        // POST: Funcionario/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
                return View(funcionario);

            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            funcionario.IdGerente = int.Parse(claim.Value);

            db.Add(funcionario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: Funcionario/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Funcionario f = db.Funcionario.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Horarios = GetHorarios();
            return View(f);
        }

        [Authorize]
        // POST: Funcionario/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
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

        [Authorize]
        // GET: Funcionario/Eliminar/5
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
            Funcionario f = db.Funcionario.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            ViewBag.Horarios = db.Horario.Find(f.IdHorario);
            return View(f);
        }

        [Authorize]
        // POST: Funcionario/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Funcionario f = db.Funcionario.Find(id);
                db.Funcionario.Remove(f);
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

        private IEnumerable<Horario> GetHorarios()
        {
            return db.Horario.ToList();
        }
    }
}


