using Aifud.Areas.Admin.FastReportUtils;
using Aifud.Areas.Admin.Services;

using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;

using Microsoft.AspNetCore.Mvc;

namespace Aifud.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLanchesReportController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly RelatorioLanchesService relatorioLanchesService;

        public AdminLanchesReportController(IWebHostEnvironment environment, RelatorioLanchesService relatorioLanchesService)
        {
            this.environment = environment;
            this.relatorioLanchesService = relatorioLanchesService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [Route("LanchesCategoriaReport")]
        public async Task<IActionResult> LanchesCategoriaReport()
        {
            var report = new WebReport();

            var mssqlDataConnection = new MsSqlDataConnection();

            report.Report.Dictionary.AddChild(mssqlDataConnection);

            report.Report.Load(Path.Combine(environment.ContentRootPath, "wwwroot/reports", "lanchesCategoria.frx"));
            var lanches = HelperFastReport.GetTable(await relatorioLanchesService.GetLanchesReport(), "LanchesReport");
            var categorias = HelperFastReport.GetTable(await relatorioLanchesService.GetCategoriaReport(), "CategoriasReport");

            report.Report.RegisterData(lanches, "LanchesReport");
            report.Report.RegisterData(categorias, "CategoriasReport");

            return View(report);
        }


        [Route("LanchesCategoriaReportPdf")]
        public async Task<IActionResult> LanchesCategoriaReportPdf()
        {
            var report = new WebReport();

            var mssqlDataConnection = new MsSqlDataConnection();

            report.Report.Dictionary.AddChild(mssqlDataConnection);

            report.Report.Load(Path.Combine(environment.ContentRootPath, "wwwroot/reports", "lanchesCategoria.frx"));
            var lanches = HelperFastReport.GetTable(await relatorioLanchesService.GetLanchesReport(), "LanchesReport");
            var categorias = HelperFastReport.GetTable(await relatorioLanchesService.GetCategoriaReport(), "CategoriasReport");

            report.Report.RegisterData(lanches, "LanchesReport");
            report.Report.RegisterData(categorias, "CategoriasReport");

            report.Report.Prepare();

            var stream = new MemoryStream();
            report.Report.Export(new PDFSimpleExport(), stream);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/pdf");
        }
    }
}
