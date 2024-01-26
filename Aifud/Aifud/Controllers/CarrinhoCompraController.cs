using Aifud.Models;
using Aifud.Repositories.Interfaces;
using Aifud.ViewModel;

using Microsoft.AspNetCore.Mvc;

namespace Aifud.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository lancheRepository;
        private readonly CarrinhoCompra carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, 
                                        CarrinhoCompra carrinhoCompra)
        {
            this.lancheRepository = lancheRepository;
            this.carrinhoCompra = carrinhoCompra;
        }


        public IActionResult Index()
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


        public IActionResult AdicionarItemNoCarrinho(int lancheId)
        {
            var lanche = lancheRepository.GetLanche(lancheId);
            if(lanche != null)
            {
                carrinhoCompra.AdicionarAoCarrinho(lanche);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoverItemDoCarrinho(int lancheId)
        {
            var lanche = lancheRepository.GetLanche(lancheId);
            if(lanche != null)
            {
                carrinhoCompra.RemoverDoCarrinho(lanche);
            }
            return RedirectToAction("Index");
        }
    }
}
