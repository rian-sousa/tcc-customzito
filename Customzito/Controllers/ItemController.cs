using CustomBancoLib;
using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customzito.Controllers
{
    public class ItemController : Controller
    {
        private readonly CZContext _czContext;

        public ItemController(CZContext context)
        {
            _czContext = context;
        }

        public async Task<ActionResult> Index()
        {
            var obj = new TB_Produto
            {
                Titulo = "Broca-Shirt",
                Descricao = "Blusa do brocasito, versão exclusiva, usada num show em brasília",
                Preco = 420,
                qtd = 1,
                Avaliacao = 3.22M,
            };

            return View(obj);
        }
    }
}