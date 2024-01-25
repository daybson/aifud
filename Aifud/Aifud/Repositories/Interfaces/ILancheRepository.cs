using Aifud.Models;

namespace Aifud.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> GetLanches();
        IEnumerable<Lanche> GetLanchesPreferidos();
        Lanche GetLanche(int id);
    }
}
