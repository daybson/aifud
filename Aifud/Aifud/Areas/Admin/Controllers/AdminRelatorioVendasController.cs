using Aifud.Areas.Admin.Services;

using Microsoft.AspNetCore.Mvc;

namespace Aifud.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private RelatorioVendasService relatorioVendasService;

        public AdminRelatorioVendasController(RelatorioVendasService relatorioVendasService)
        {
            this.relatorioVendasService = relatorioVendasService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? min, DateTime? max)
        {
            if (!min.HasValue)
                min = new DateTime(DateTime.Now.Year, 1, 1);

            if (!max.HasValue)
                max = DateTime.Now;

            ViewData["mindate"] = min.Value.ToString("yyyy-MM-dd");
            ViewData["maxdate"] = max.Value.ToString("yyyy-MM-dd");

            var result = await relatorioVendasService.FindByDateAsync(min, max);
            return View(result);
        }

    }
}
