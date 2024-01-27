﻿using Aifud.Models;
using Aifud.Repositories.Interfaces;
using Aifud.ViewModel;

using Microsoft.AspNetCore.Mvc;

namespace Aifud.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            this.lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual;

            if (string.IsNullOrWhiteSpace(categoria))
            {
                lanches = lancheRepository.GetLanches()
                    .OrderBy(l => l.Nome);
            }
            else
            {
                lanches = lancheRepository.GetLanches()
                    .Where(l => l.Categoria.Nome.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(l => l.Nome)
                    .ToList();
            }
            categoriaAtual = categoria?.ToUpperInvariant() ?? "Todos";

            var lanchesVM = new LancheListViewModel
            {
                Lanches = lanches,
                Categoria = categoriaAtual
            };

            return View(lanchesVM);
        }


        public IActionResult Details(int lancheId)
        {
            var lanche = lancheRepository.GetLanche(lancheId);
            return View(lanche);
        }


        public IActionResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual;

            if (string.IsNullOrWhiteSpace(searchString))
            {
                lanches = lancheRepository.GetLanches()
                    .OrderBy(l => l.Nome);
                categoriaAtual = "Todos";
            }
            else
            {
                lanches = lancheRepository.GetLanches()
                    .Where(l => l.Nome.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(l => l.Nome)
                    .ToList();

                if (lanches.Any())
                {
                    categoriaAtual = "Lanches";
                }
                else
                {
                    categoriaAtual = "Nenhum lanche encontrado";
                }
            }            

            var lanchesVM = new LancheListViewModel
            {
                Lanches = lanches,
                Categoria = categoriaAtual
            };

            return View("List", lanchesVM);
        }
    }
}
