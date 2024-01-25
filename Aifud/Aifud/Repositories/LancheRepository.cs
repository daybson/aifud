using Aifud.Context;
using Aifud.Models;
using Aifud.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Aifud.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext context;

        public LancheRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Lanche GetLanche(int id)
        {
            return context.Lanches
                .Include(c => c.Categoria)
                .FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Lanche> GetLanches()
        {
            return context.Lanches
                .Include(c => c.Categoria)
                .ToList();
        }

        public IEnumerable<Lanche> GetLanchesPreferidos()
        {
            return context.Lanches
                .Where(l => l.IsLanchePreferido)
                .Include(c => c.Categoria)
                .ToList();
        }
    }
}
