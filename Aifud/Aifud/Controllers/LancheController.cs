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

        public IActionResult List()
        {
            // ViewData["Title"] = "Lanches";
            // return View(lancheRepository.GetLanches());

            var lanchesListViewModel = new LancheListViewModel();
            lanchesListViewModel.Lanches = lancheRepository.GetLanches();
            lanchesListViewModel.Categoria = "Categoria atual";

            return View(lanchesListViewModel);
        }
    }
}
