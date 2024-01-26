using Aifud.Repositories.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Aifud.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository repository;

        public CategoriaMenu(ICategoriaRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = repository.GetCategorias()
                .OrderBy(c => c.Nome);
            return View(categorias);
        }
    }
}
