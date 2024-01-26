using Aifud.Models;
using Aifud.Repositories.Interfaces;
using Aifud.ViewModel;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace Aifud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository lancheRepository;

        public HomeController(ILancheRepository lancheRepository)
        {
            this.lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var homeVM = new HomeViewModel
            {
                LanchesPreferidos = lancheRepository.GetLanchesPreferidos()
            };

            return View(homeVM);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
