using mpolaris.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mpolaris.Interfaces
{
    public interface IrpEventosDelictivos
    {
        Task<IEnumerable<rpEventosDelictivosModel.griddelictivo>> Get_GridrpDelictivos(int BD, int companyid, string fechai, string fechafin, string geozonasid, string mobilesid, string tipoincidentesid, string motoristasid, int userid);
        Task<IEnumerable<rpEventosDelictivosModel.treeDelictivos>> gettreeMobiles(int BD, int userid);
    }
}
