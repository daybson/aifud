using Aifud.Context;
using Aifud.Models;

namespace Aifud.Areas.Admin.Services
{
    public class GraficoVendasService
    {
        private readonly AppDbContext context;

        public GraficoVendasService(AppDbContext context)
        {
            this.context = context;
        }

        public List<LancheGrafico> GetVendasLanche(int dias = 365)
        {
            var data = DateTime.Now.AddDays(-dias);

            var lanches = from pd in context.PedidosDetalhes
                          join l in context.Lanches
                            on pd.LancheId equals l.Id
                          where pd.Pedido.PedidoEnviado >= data
                          group pd by new { pd.LancheId, l.Nome }
                        into g
                          select new
                          {
                              LancheNome = g.Key.Nome,
                              LanchesQuantidade = g.Sum(l => l.Quantidade),
                              LanchesValorTotal = g.Sum(l => l.Preco * l.Quantidade)

                          };
            var lista = new List<LancheGrafico>();
            foreach (var l in lanches)
            {
                lista.Add(
                    new LancheGrafico
                    {
                        LancheNome = l.LancheNome,
                        LanchesQuantidade = l.LanchesQuantidade,
                        LanchesValorTotal = l.LanchesValorTotal
                    });
            }

            return lista;
        }
    }
}
