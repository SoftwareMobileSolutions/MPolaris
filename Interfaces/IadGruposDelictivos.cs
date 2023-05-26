using mpolaris.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static mpolaris.Models.adGruposDelictivosModel;

namespace mpolaris.Interfaces
{
    public interface IadGruposDelictivos
    {
        //Tipo de Incidentes
        Task<IEnumerable<adGruposDelictivosModel.DDL_CatIncidentes>> Get_CatIncidentes(int BD, int companyid);
        //Vehículos
        Task<IEnumerable<adGruposDelictivosModel.DDL_vehiculos>> Get_VehiculosSubflotas(int BD, int userid);
        //Motoristas
        Task<IEnumerable<adGruposDelictivosModel.DDL_motoristas>> Get_Motoristas(int BD, int objecid, int tipo);
        //Geozonas delictivas
        Task<(IEnumerable<adGruposDelictivosModel.DDL_geozonas>, IEnumerable<DDL_geozonas_Detalle>)> mpolaris_obtenerGeoDelictivos(int BD, int companyid);

        //DDL Rutas
        Task<IEnumerable<adGruposDelictivosModel.DDL_Ruta>> Get_Ruta(int BD, int userid, int companyid);

        Task<IEnumerable<adGruposDelictivosModel.Grid_ListadoEventos>> getDatosGridDelictivos(int BD, int companyid, int userid);
        Task<IEnumerable<adGruposDelictivosModel.tipoinconvenientes_model>> getTipoInconvenientes(int BD, int tipo);
        Task<IEnumerable<adGruposDelictivosModel.incidentescat_model>> getincidentescat(int BD, int companyid);
        Task<IEnumerable<adGruposDelictivosModel.pagorenta_model>> getpagorentaDDL(int BD);

        Task<IEnumerable<adGruposDelictivosModel.msj>> AgregarIncidenteDelictivo(
            int BD,
            int incidenteid,
            int motoristaid,
            int mobileid,
            int companyid,
            int geozonaid,
            string fecha,
            string descripcion,
            float costo,
            float longitud,
            float latitud,
            int rutaid,
            string contacto,

            string estado,
            int pagorentaid,
            string fechaPagoRenta,
            int estadoInconvenienteId,
            bool esTipoRenta
        );

        Task<IEnumerable<adGruposDelictivosModel.msj>> ModificarIncidenteDelictivo(
           int BD,
           int eventoid,
           int incidenteid,
           int motoristaid,
           int mobileid,
           int companyid,
           int geozonaid,
           string fecha,
           string descripcion,
           float costo,
           int pagorentaid,
           string estado,
           int estadoInconvenienteId
        );

        Task<IEnumerable<adGruposDelictivosModel.msj>> EliminarIncidenteDelictivo(
           int BD,
           int companyid,
           int eventoid
        );
    }
}
