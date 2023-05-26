using mpolaris.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mpolaris.Interfaces
{
    public interface Imp_vermapa
    {
        Task<IEnumerable<mp_vermapaModel.nivelpeligrosidad>> Get_peligrosidadDelictiva(int BD, int companyid);
        Task<(IEnumerable<mp_vermapaModel.geodelictiva_r1>, IEnumerable<mp_vermapaModel.geodelictiva_r2>)> Get_geodelictivas(int BD, int companyid);
        Task<IEnumerable<mp_vermapaModel.rpGeoEventoAnual>> getEventosAnuales(int BD, int companyid, string fechaini, string fechafin, int geozoneid);
    }
}
