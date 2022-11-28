using mpolaris.Models;

namespace mpolaris.Interfaces
{
    public interface Imp_vermapa
    {
        Task<IEnumerable<mp_vermapaModel.nivelpeligrosidad>> Get_peligrosidadDelictiva(int BD, int companyid);
        Task<(IEnumerable<mp_vermapaModel.geodelictiva_r1>, IEnumerable<mp_vermapaModel.geodelictiva_r2>)> Get_geodelicitvas(int BD, int companyid);
    }
}
