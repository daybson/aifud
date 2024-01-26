using Aifud.Models;
using Aifud.ViewModel;

using Microsoft.AspNetCore.Mvc;

namespace Aifud.Components
{
    public class CarrinhoCompraResumo : ViewComponent
    {
        private readonly CarrinhoCompra carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            this.carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
        {
            var itens = carrinhoCompra.GetCarrinhoCompraItems();
            carrinhoCompra.CarrinhoCompraItems = itens;
             

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = carrinhoCompra,
                CarrinhoCompraTotal = carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }
    }
}
