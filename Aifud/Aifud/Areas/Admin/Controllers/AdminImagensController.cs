using Aifud.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aifud.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImages configurationImages;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdminImagensController(IOptions<ConfigurationImages> configurationImages,
                                        IWebHostEnvironment webHostEnvironment)
        {
            this.configurationImages = configurationImages.Value;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Nenhum arquivo selecionado";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Máximo de 10 arquivos";
                return View(ViewData);
            }

            var filePathsName = new List<string>();
            var filePath = Path.Combine(webHostEnvironment.WebRootPath,
                                        configurationImages.NomePastaImagensProdutos);

            foreach (var file in files)
            {
                if (file.FileName.Contains(".jpg") || file.FileName.Contains(".png"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", file.FileName);
                    filePathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} arquivos enviado(s)";

            ViewBag.Arquivos = filePathsName;
            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();
            var userImagesPath = Path.Combine(
                webHostEnvironment.WebRootPath,
                configurationImages.NomePastaImagensProdutos
                );

            var dir = new DirectoryInfo(userImagesPath);

            var files = dir.GetFiles();

            model.PathImagesProduto = configurationImages.NomePastaImagensProdutos;
            if (!files.Any())
            {
                ViewData["Erro"] = "Nenhum arquivo encontrado";
            }

            model.Files = files;

            return View(model);

        }


        public IActionResult Deletefile(string fname)
        {
            var imageDeleta = Path.Combine(
                webHostEnvironment.WebRootPath, configurationImages.NomePastaImagensProdutos
                + "\\", fname);

            if (System.IO.File.Exists(imageDeleta))
            {
                System.IO.File.Delete(imageDeleta);

                ViewData["Deletado"] = $"Arquivo {imageDeleta} deletado com sucesso";
            }

            return View("Index");
        }
    }
}
