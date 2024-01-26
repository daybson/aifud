using Aifud.Models;

namespace Aifud.ViewModel
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; } 
        public string Categoria { get; set; }
    }
}
