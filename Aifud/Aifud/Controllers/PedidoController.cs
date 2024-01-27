using Aifud.Models;
using Aifud.Repositories.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Aifud.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository pedidoRepository;
        private readonly CarrinhoCompra carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            this.pedidoRepository = pedidoRepository;
            this.carrinhoCompra = carrinhoCompra;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0;

            //obtem itens do carrino de compra
            var items = carrinhoCompra.GetCarrinhoCompraItems();
            carrinhoCompra.CarrinhoCompraItems = items;

            if (!items.Any())
            {
                ModelState.AddModelError("empty", "Carrinho de compras vazio");
            }


            //calcula valor do pedido
            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += item.Quantidade * item.Lanche.Preco;
            }

            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;


            if (ModelState.IsValid)
            {
                pedidoRepository.CriarPedido(pedido);

                ViewBag.CheckoutCompletoMensagem = "Pedido concluído!";
                ViewBag.TotalPedido = carrinhoCompra.GetCarrinhoCompraTotal();

                carrinhoCompra.LimparCarrinho();

                return View("CheckoutCompleto", pedido);
            }

            return View(pedido);
        }
    }
}
