using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGR.Data;
using SGR.Models;

namespace SGR.Controllers
{
    public class HomeController : Controller
    {
        private SGRContext db;

        public HomeController(SGRContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.TotalPedidos = db.Pedido.Count();
            ViewBag.PedidosHoje = PedidosHoje();
            ViewBag.ReservasHoje = ReservasHoje();
            ViewBag.MercadoriasEmFalta = MercadoriasEmFalta();
            ViewBag.TotalHoje = 0;
            ViewBag.ObjetivoHoje = 0;
            ViewBag.Total10Dias = 0;
            ViewBag.Pedidos10Dias = 0;
            ViewBag.FaturacaoDiaria = faturacaoPassada();
            ViewBag.PedidosDiarios = pedidosPassados();
            ViewBag.MaisVendidos = maisVendidos();

            ViewBag.Dias = past10Days();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private int PedidosHoje()
        {
            DateTime hoje = DateTime.Today;
            return db.Pedido.Where(p => p.DataHora.Year.Equals(hoje.Year) && p.DataHora.Month.Equals(hoje.Month) && p.DataHora.Day.Equals(hoje.Day)).ToList().Count();
        }

        private int ReservasHoje()
        {
            DateTime hoje = DateTime.Today;
            return db.Reserva.Where(p => p.DataHora.Year.Equals(hoje.Year) && p.DataHora.Month.Equals(hoje.Month) && p.DataHora.Day.Equals(hoje.Day)).ToList().Count();
        }

        private IEnumerable<string> MercadoriasEmFalta()
        {
            return db.Mercadoria.Where(p => p.Stock < p.QuantidadeMinima).Select(p => p.Nome).ToList();
        }

        private String past10Days() {
            DateTime[] last10Days = Enumerable.Range(0, 10).Select(i => DateTime.Now.Date.AddDays(-i)).ToArray();
            String s = "[";
            for (int i = 9; i > 0; i--) {
                s = s +"\'"+last10Days[i].ToString("dd/MM") + "\',";
            }
            s = s+ "\'" + last10Days[0].ToString("dd/MM") + "\']";
            return s;
        }

        

        private double faturacaoDiaria(DateTime data)
        {
            double faturacao = 0;
            int[] idPedidos = db.Pedido.Where(p => p.DataHora.Date.Equals(data.Date)).Select(p => p.Id).ToArray();

            if (idPedidos.Length > 0)
            {
                foreach (int p in idPedidos)
                {
                    double total = 0;
                    List<ArtigoInPedido> artigos = db.ArtigoInPedido.Where(d => d.IdPedido.Equals(p)).ToList();
                    if (artigos.Count > 0)
                    {
                        foreach (ArtigoInPedido a in artigos)
                        {
                            double preco = (double)db.Artigo.Where(p => p.Id.Equals(a.IdArtigo)).Select(p => p.Preco).FirstOrDefault();
                            total += a.Quantidade * preco;
                        }
                    }
                    faturacao += total;
                }
            }
            if (data.Equals(DateTime.Now.Date))
            {
                ViewBag.TotalHoje = faturacao;
                ViewBag.ObjetivoHoje = (faturacao*100)/500;
            }

            ViewBag.Total10Dias += faturacao;
            return faturacao;
        }

        private String faturacaoPassada()
        {
            DateTime[] last10Days = Enumerable.Range(0, 10).Select(i => DateTime.Now.Date.AddDays(-i)).ToArray();


            String s = "[";
            for (int i = 9; i > 0; i--)
            {
                s = s + "\'" + faturacaoDiaria(last10Days[i]) + "\',";
            }
            s = s + "\'" + faturacaoDiaria(last10Days[0]) + "\']";
            return s;
        }

        private int pedidosDiarios(DateTime data)
        {
            int[] idPedidos = db.Pedido.Where(p => p.DataHora.Date.Equals(data.Date)).Select(p => p.Id).ToArray();

            ViewBag.Pedidos10Dias += idPedidos.Length;
            return idPedidos.Length;
        }

        private String pedidosPassados()
        {
            DateTime[] last10Days = Enumerable.Range(0, 10).Select(i => DateTime.Now.Date.AddDays(-i)).ToArray();


            String s = "[";
            for (int i = 9; i > 0; i--)
            {
                s = s + "\'" + pedidosDiarios(last10Days[i]) + "\',";
            }
            s = s + "\'" + pedidosDiarios(last10Days[0]) + "\']";
            return s;
        }

        private IEnumerable<KeyValuePair<Artigo,int>> maisVendidos()
        {
            Dictionary<Artigo, int> vendas = new Dictionary<Artigo, int>();

            List<ArtigoInPedido> artigos = db.ArtigoInPedido.ToList();
                   
            foreach (ArtigoInPedido a in artigos){
                Artigo artigo = db.Artigo.Where(p => p.Id.Equals(a.IdArtigo)).FirstOrDefault();
                int quant = a.Quantidade;

                if (vendas.Keys.Where(p => p.Id.Equals(artigo.Id)).Count() > 0)
                    vendas[artigo] += quant;
                else
                    vendas.Add(artigo, quant);
            }

            var sortedVendas = from entry in vendas orderby entry.Value descending select entry;
            var sortedVendas5 = sortedVendas.Take(5);

            return sortedVendas5;
        }
    }
}
