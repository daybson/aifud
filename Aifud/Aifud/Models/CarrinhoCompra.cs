using Aifud.Context;

using Microsoft.EntityFrameworkCore;

namespace Aifud.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext context;

        public CarrinhoCompra(AppDbContext context)
        {
            this.context = context;
        }

        public Guid Id { get; set; }
        public IEnumerable<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            var carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            return new CarrinhoCompra(context)
            {
                Id = Guid.Parse(carrinhoId)
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            //Já existe esse lanche na tabela relacional?
            CarrinhoCompraItem? carrinhoCompraItem = LancheExisteNoCarrinho(lanche);

            //Não existe: insere a primeira vez
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = Id,
                    Lanche = lanche,
                    Quantidade = 1
                };

                context.CarrinhoCompraItems.Add(carrinhoCompraItem);
            }
            else
            {
                //Já existe: aumenta a quantidade
                carrinhoCompraItem.Quantidade++;
            }

            context.SaveChanges();

        }


        public int RemoverDoCarrinho(Lanche lanche)
        {
            //Já existe esse lanche na tabela relacional?
            CarrinhoCompraItem? carrinhoCompraItem = LancheExisteNoCarrinho(lanche);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                //Se existe mais de 1 vez no carrinho, decrementa quantidade em 1
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    //Só existe 1 unidade, remove do banco
                    context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
                }
            }

            context.SaveChanges();
            return quantidadeLocal;
        }

        public IEnumerable<CarrinhoCompraItem> GetCarrinhoCompraItems()
        {
            return CarrinhoCompraItems ??
                (CarrinhoCompraItems =
                    context.CarrinhoCompraItems
                    .Where(c => c.CarrinhoCompraId == Id)
                    .Include(l => l.Lanche)
                    .ToList()
                    );
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = context.CarrinhoCompraItems
                .Where(c => c.CarrinhoCompraId == Id);

            context.RemoveRange(carrinhoItens);
            context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = context.CarrinhoCompraItems
                .Where(c => c.CarrinhoCompraId == Id)
                .Select(i => i.Lanche.Preco * i.Quantidade)
                .Sum();
            return total;
        }
        
        private CarrinhoCompraItem? LancheExisteNoCarrinho(Lanche lanche)
        {
            return context.CarrinhoCompraItems
                            .SingleOrDefault(s =>
                                s.Lanche.Id == lanche.Id &&
                                s.CarrinhoCompraId == Id);
        }
    }
}
