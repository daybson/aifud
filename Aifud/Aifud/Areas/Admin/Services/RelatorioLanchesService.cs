using Aifud.Context;
using Aifud.Models;

using Microsoft.EntityFrameworkCore;

namespace Aifud.Areas.Admin.Services
{
    public class RelatorioLanchesService
    {
        private readonly AppDbContext context;

        public RelatorioLanchesService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Lanche>> GetLanchesReport()
        {
            var lanches = await context.Lanches.ToListAsync();
            if (lanches is null)
                return Enumerable.Empty<Lanche>();

            return lanches;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriaReport()
        {
            var categorias = await context.Categorias.ToListAsync();
            if (categorias is null)
                return Enumerable.Empty<Categoria>();

            return categorias;
        }
    }
}
