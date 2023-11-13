using CustomBancoLib;
using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    public class InternoController : Controller
    {
        private CZContext _czContext;
        public InternoController(CZContext contexto) 
        {
            _czContext = contexto;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ControleEstoque(int? idColecao)
        {
            Dictionary<string, List<TB_Produto>> DictRoupasEstoques = new Dictionary<string, List<TB_Produto>>();

            var Colecoes = await _czContext.TbColecao
                .ToListAsync();

            var Produtos = await _czContext.TbProduto
                .ToListAsync();

            if(idColecao is not null)
            {
                var lstRoupas = await RecuperarRoupasColecao(idColecao);

                DictRoupasEstoques.Add(idColecao.ToString(), lstRoupas);

                Console.WriteLine(DictRoupasEstoques.Values);

                ViewBag.lstProdutosColecao = lstRoupas;

                return PartialView("_VisualizerRoupasPartial", lstRoupas);

            }
            

            TempData["RoupasDaColecao"] = DictRoupasEstoques;

            ViewBag.lstProdutos = Produtos;
            ViewBag.lstColecoes = Colecoes;

            return View();
        }

        [Authorize]
        public async Task<List<TB_Produto>> RecuperarRoupasColecao(int? idColecao)
        {
            if(idColecao is not null)
            {
                var ProdColecao = await _czContext.TbProduto
                .Where(x => x.IdColecao == idColecao)
                .ToListAsync();

                foreach (var item in ProdColecao)
                {
                    Console.WriteLine("="+item.IdProduto+"=");
                }                

                return ProdColecao;
            }
            return default;
        }

        [Authorize]
        public async Task<IActionResult> Adicionar()
        {
            return default;
        }

        [Authorize]
        public async Task<IActionResult> RemoverColecao()
        {
            return default;
        }


    }
}
