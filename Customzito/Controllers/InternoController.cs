﻿using CustomBancoLib;
using Customzito.Services.CZDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            var Materiais = await _czContext.Material
                .ToListAsync();

            var Tipos = await _czContext.TdTipoVestimenta
                .ToListAsync();

            ViewBag.MaterialDrop = new SelectList(Materiais, "IdMaterial", "Descricao");

            ViewBag.TipoDrop = new SelectList(Tipos, "IdTipoVestimenta", "Descricao");

            ViewBag.ColecoesDrop = new SelectList(Colecoes, "IdColecao", "Descricao");

            if (idColecao is not null)
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
            if (idColecao is not null)
            {
                var ProdColecao = await _czContext.TbProduto
                .Where(x => x.IdColecao == idColecao)
                .ToListAsync();

                foreach (var item in ProdColecao)
                {
                    Console.WriteLine("=" + item.IdProduto + "=");
                }

                return ProdColecao;
            }
            return default;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AdicionarColecao(string DescricaoColecao, bool checkLimitado)
        {
            if (DescricaoColecao is not null)
            {
                TB_Colecao ObjColecao = new TB_Colecao()
                {
                    Descricao = DescricaoColecao,
                    Limitado = checkLimitado
                };

                await _czContext.TbColecao
                    .AddAsync(ObjColecao);

                await _czContext.SaveChangesAsync();

                return PartialView("_OperacaoConfirmadaPartial");
            }

            return PartialView("_OperacaoFalhaPartial");

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoverColecao(int id)
        {
            var ObjColecao = await _czContext.TbColecao
                .Where(x => x.IdColecao == id)
                .FirstOrDefaultAsync();

            if (ObjColecao is not null)
            {
                _czContext.TbColecao
                .Remove(ObjColecao);

                await _czContext.SaveChangesAsync();

                return PartialView("_OperacaoConfirmadaPartial");
            }

            return PartialView("_OperacaoFalhaPartial");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditarColecao(int id, string descricao, bool limitado)
        {
            var ObjColecao = await _czContext.TbColecao
                .FirstOrDefaultAsync(x => x.IdColecao == id);

            ObjColecao.Descricao = descricao;
            ObjColecao.Limitado = limitado;

            _czContext.Update(ObjColecao);

            await _czContext.SaveChangesAsync();

            return PartialView("_OperacaoConfirmadaPartial");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AdicionarRoupa(string DescricaoRoupa, string Titulo, float preco, int IdTipoVestimenta, int IdColecao, decimal? avaliacao, int qtd, string? cor, string IdMaterial, string Marca, IFormFile? imagem)
        {
            if (imagem is not null)
            {
                var CaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", imagem.FileName);

                using (var stream = new FileStream(CaminhoArquivo, FileMode.Create))
                {
                    await imagem.CopyToAsync(stream);
                }

                TB_Produto ObjColecao = new TB_Produto()
                {
                    Descricao = DescricaoRoupa,
                    Titulo = Titulo,
                    Preco = preco,
                    IdTipoVestimenta = IdTipoVestimenta,
                    IdColecao = IdColecao,
                    Avaliacao = avaliacao,
                    qtd = qtd,
                    Cor = cor,
                    Material = IdMaterial,
                    Marca = Marca,
                    UrlImagem = CaminhoArquivo
                };
                await _czContext.TbProduto
                .AddAsync(ObjColecao);

                await _czContext.SaveChangesAsync();

                return PartialView("_OperacaoConfirmadaPartial");
            }
            else
            {
                TB_Produto ObjColecao = new TB_Produto()
                {
                    Descricao = DescricaoRoupa,
                    Titulo = Titulo,
                    Preco = preco,
                    IdTipoVestimenta = IdTipoVestimenta,
                    IdColecao = IdColecao,
                    Avaliacao = avaliacao,
                    qtd = qtd,
                    Cor = cor,
                    Material = IdMaterial,
                    Marca = Marca
                };

                await _czContext.TbProduto
                    .AddAsync(ObjColecao);

                await _czContext.SaveChangesAsync();

                return PartialView("_OperacaoConfirmadaPartial");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoverRoupa(int id)
        {
            var ObjRoupa = await _czContext.TbProduto
                .FirstOrDefaultAsync(x => x.IdProduto == id);

            _czContext.TbProduto
               .Remove(ObjRoupa);

            await _czContext.SaveChangesAsync();

            return PartialView("_OperacaoConfirmadaPartial");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditarRoupa(int idProduto,string DescricaoRoupa, string Titulo, float preco, int IdTipoVestimenta, int IdColecao, decimal? avaliacao, int qtd, string? cor, string IdMaterial, string Marca)
        {
            var ObjProduto = await _czContext.TbProduto
                .FirstOrDefaultAsync(x => x.IdProduto == idProduto);

            ObjProduto.Titulo = Titulo;
            ObjProduto.Preco = preco;
            ObjProduto.Descricao = DescricaoRoupa;
            ObjProduto.IdTipoVestimenta = IdTipoVestimenta;
            ObjProduto.IdColecao = IdColecao;
            ObjProduto.Avaliacao = avaliacao;
            ObjProduto.qtd = qtd;
            ObjProduto.Marca = Marca;
            ObjProduto.Cor = cor;
            ObjProduto.Material = IdMaterial;

            _czContext.Update(ObjProduto);

            await _czContext.SaveChangesAsync();

            return PartialView("_OperacaoConfirmadaPartial");
        }

        [Authorize]
        public async Task<IActionResult> RestaurarModalEditarColecao(int id)
        {
            var Colecao = await _czContext.TbColecao
                .FirstOrDefaultAsync(x => x.IdColecao == id);

            return PartialView("_EditarColecaoPartial", Colecao);
        }


        [Authorize]
        public IActionResult RestaurarModalAddColecao()
        {
            return PartialView("_ModalAdicionarColecaoPartial");
        }

        [Authorize]
        public IActionResult RestaurarModalRemoverColecao(string idColecao)
        {
            return PartialView("_ModalRemoverColecaoPartial", idColecao);
        }

        [Authorize]
        public async Task<IActionResult> RestaurarModalAddRoupa()
        {
            var Colecoes = await _czContext.TbColecao
                .ToListAsync();

            var Materiais = await _czContext.Material
                .ToListAsync();

            var Tipos = await _czContext.TdTipoVestimenta
                .ToListAsync();

            ViewBag.MaterialDrop = new SelectList(Materiais, "IdMaterial", "Descricao");

            ViewBag.TipoDrop = new SelectList(Tipos, "IdTipoVestimenta", "Descricao");

            ViewBag.ColecoesDrop = new SelectList(Colecoes, "IdColecao", "Descricao");

            return PartialView("_ModalAdicionarRoupaPartial");
        }

        [Authorize]
        public IActionResult RestaurarModalRemoverRoupa(string idRoupa)
        {
            return PartialView("_ModalRemoverRoupaPartial", idRoupa);
        }

        [Authorize]
        public async Task<IActionResult> RestaurarModalEditarRoupa(int id)
        {
            var Colecoes = await _czContext.TbColecao
                .ToListAsync();

            var Materiais = await _czContext.Material
                .ToListAsync();

            var Tipos = await _czContext.TdTipoVestimenta
                .ToListAsync();

            ViewBag.MaterialDrop = new SelectList(Materiais, "IdMaterial", "Descricao");

            ViewBag.TipoDrop = new SelectList(Tipos, "IdTipoVestimenta", "Descricao");

            ViewBag.ColecoesDrop = new SelectList(Colecoes, "IdColecao", "Descricao");

            var Produto = await _czContext.TbProduto
                .FirstOrDefaultAsync(x => x.IdProduto == id);

            return PartialView("_EditarRoupaPartial", Produto);
        }

    }
}
