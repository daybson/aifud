using Aifud.Areas.Admin.Services;

using Microsoft.AspNetCore.Mvc;

namespace Aifud.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGraficoController : Controller
    {
        private readonly GraficoVendasService graficoVendasService;

        public AdminGraficoController(GraficoVendasService graficoVendasService)
        {
            this.graficoVendasService = graficoVendasService;
        }


        public JsonResult VendasLanches(int dias)
        {
            var lanchesVendas = graficoVendasService.GetVendasLanche(dias);
            return Json(lanchesVendas);
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult VendasMensal()
        {
            return View();
        }


        public IActionResult VendasSemanal()
        {
            return View();
        }

    }
}
