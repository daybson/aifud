using Aifud.Models;
using Aifud.Services;

using Microsoft.AspNetCore.Mvc;

namespace Aifud.Controllers
{
    public class CepController : Controller
    {
        private readonly ConsultaCepService consultaCep;

        public CepController(ConsultaCepService consultaCep)
        {
            this.consultaCep = consultaCep;
        }

        public async Task<CEP> GetAddress(string cep)
        {
            try
            {
                return await consultaCep.ConsultaCep(cep);
            }
            catch (Exception ex)
            {
                return new CEP { };
            }
        }
    }
}
