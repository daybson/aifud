using System.ComponentModel.DataAnnotations;

namespace Aifud.Models
{
    public class CarrinhoCompraItem
    {
        public int Id { get; set; }
        public Lanche Lanche { get; set; }
        public int Quantidade { get; set; }

        //[StringLength(100)]
        public Guid CarrinhoCompraId { get; set; }
    }
}
