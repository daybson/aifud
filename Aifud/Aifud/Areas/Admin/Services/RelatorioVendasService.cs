using Aifud.Context;
using Aifud.Models;

using Microsoft.EntityFrameworkCore;

namespace Aifud.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext context;

        public RelatorioVendasService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? mindate,
            DateTime? maxdate)
        {
            var result = from obj in context.Pedidos select obj;

            if (mindate.HasValue)
                result = result.Where(x => x.PedidoEnviado >= mindate.Value);

            if (maxdate.HasValue)
                result = result.Where(x => x.PedidoEnviado <= maxdate.Value);

            return await result
                .Include(l => l.PedidoItens)
                .ThenInclude(l => l.Lanche)
                .OrderByDescending(p => p.PedidoEnviado)
                .ToListAsync();
        }
    }
}
