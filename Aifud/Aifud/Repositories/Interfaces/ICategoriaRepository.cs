using Aifud.Models;

namespace Aifud.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetCategorias();
    }
}
