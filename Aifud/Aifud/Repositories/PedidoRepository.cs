using Aifud.Context;
using Aifud.Models;
using Aifud.Repositories.Interfaces;

namespace Aifud.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext context;
        private readonly CarrinhoCompra carrinhoCompra;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            this.context = context;
            this.carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            //Grava o pedido para gerar o ID dele
            pedido.PedidoEnviado = DateTime.Now;
            context.Pedidos.Add(pedido);
            context.SaveChanges();

            //Monta o detalhe do pedido
            foreach (var carrinhoItem in carrinhoCompra.CarrinhoCompraItems)
            {
                var detalhe = new PedidoDetalhe
                {
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.Id,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Lanche.Preco
                };

                context.PedidosDetalhes.Add(detalhe);
            }

            context.SaveChanges();
        }
    }
}
