﻿using System.Diagnostics;
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
    public class PedidoController : Controller
    {

        private SGRContext db;

        public PedidoController(SGRContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await db.Pedido.ToListAsync());
        }


        // GET: Pedido/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Pedido p = db.Pedido.Find(id);
            if (p == null)
            {
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // GET: Pedido/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Pedido/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(Pedido p)
        {
            if (!ModelState.IsValid)
                return View(p);

            db.Add(p);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Pedido/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Pedido f = db.Pedido.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }

        // POST: Pedido/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, Pedido p)
        {
            if (id != p.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(p);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(p);
        }

        // GET: Pedido/Eliminar/5
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
            Pedido f = db.Pedido.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }

        // POST: Pedido/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Pedido f = db.Pedido.Find(id);
                db.Pedido.Remove(f);
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

