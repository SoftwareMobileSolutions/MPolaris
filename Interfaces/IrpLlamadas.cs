using mpolaris.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static mpolaris.Models.rpLlamadasRealizadasModel;

namespace mpolaris.Interfaces
{
    public interface IrpLlamadas
    {
        Task<IEnumerable<rpLlamadasRealizadasModel>> GetLlamadasRealizadas(int BD, string fini, string ffin, int companyid);
    }
}
