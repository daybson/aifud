using Aifud.Context;
using Aifud.Models;
using Aifud.Repositories.Interfaces;

namespace Aifud.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext context;

        public CategoriaRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            return context.Categorias;
        }
    }
}
