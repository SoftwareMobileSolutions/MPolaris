using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpolaris.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace mpolaris.Controllers
{
    public class adGruposDelictivosController : Controller
    {
        private readonly IadGruposDelictivos IadGruposDelictivos;
        public adGruposDelictivosController(IadGruposDelictivos _IadGruposDelictivos)
        {
            IadGruposDelictivos = _IadGruposDelictivos;
        }
        public IActionResult adGruposDelictivos()
        {
            return PartialView();
        }

        //Tipo Inicidentes
        public async Task<JsonResult> get_cataglogo_tipoincidente(int BD, int companyid)
        {
            var data = await IadGruposDelictivos.Get_CatIncidentes(BD, companyid);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        //Vehículos
        public async Task<JsonResult> Get_VehiculosSubflotas(int BD, int userid)
        {
            var data = await IadGruposDelictivos.Get_VehiculosSubflotas(BD, userid);


            var flotas = data.Select(t => new
            {
                Name = t.name,
                codigo = t.cod,
                parent = t.parent,
                granparent = t.grandparent,
                Nivel = t.nivel,
                ImageUrl = t.urlimagen,
                Placa = t.plate
            }).Where(t => t.Nivel == 1).OrderBy(t => t.Name).ToList();

            var vehiculos = data.Select(t => new
            {
                Name = t.name,
                codigo = t.cod,
                parent = t.parent,
                granparent = t.grandparent,
                Nivel = t.nivel,
                Placa = t.plate,
                ImageUrl = t.urlimagen,
                motoristaid = t.motoristaid
            }).Where(t => t.Nivel == 2).OrderBy(t => t.Name).ToList();

            var ddlGroup =
            (from v in vehiculos
            join f in flotas on v.parent equals f.codigo
            select new
            {
                Name = v.Name,
                codigo = v.codigo,
                placa = v.Placa,
                parent = v.parent,
                granparent = v.granparent,
                Nivel = v.Nivel,
                ImageUrl = v.ImageUrl,
                Subflota = f.Name,
                placaname = (v.codigo + "£" + v.Name + v.Placa),
                Motoristaid = v.motoristaid
            }).ToList();

            return await Task.Run(() =>
            {
                return Json(ddlGroup);
            });
        }

        //Vehículos
        public async Task<JsonResult> Get_Motoristas(int BD, int userid)
        {
            var data = await IadGruposDelictivos.Get_Motoristas(BD, userid, 0);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        //Geozonas
        public async Task<JsonResult> Get_Geozonas(int BD, int companyid)
        {
            var data = await IadGruposDelictivos.mpolaris_obtenerGeoDelictivos(BD, companyid);
            var r1 = data.Item1;
            var r2 = data.Item2;

            return await Task.Run(() =>
            {
                var result = r1.Select(x =>
                    new {
                        x.id,
                        x.nombre,
                        detalle = r2/*.Select(y => new
                        {
                            y.id,
                            y.orden,
                            y.lat,
                            y.lng,
                        })*/.Where(y => y.id == x.id).OrderBy( z => z.orden).ToList()
                    });
                return Json(result);
            });
        }

        //Rutas
        public async Task<JsonResult> Get_Ruta(int BD, int userid, int companyid)
        {
            var data = await IadGruposDelictivos.Get_Ruta(BD, userid, companyid);
        

            return await Task.Run(() =>
            { 
                return Json(data);
            });
        }

        //Grid
        public async Task<JsonResult> get_DataGrid(int BD, int companyid, int userid)
        {
            var data = await IadGruposDelictivos.getDatosGridDelictivos(BD, companyid, userid);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        //TipoInconvenientes
        public async Task<JsonResult> getTipoInconvenientes(int BD, int tipo)
        {
            var data = await IadGruposDelictivos.getTipoInconvenientes(BD, tipo);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        //TipoIncidenteCategoria
        public async Task<JsonResult> getincidentescat(int BD, int companyid)
        {
            var data = await IadGruposDelictivos.getincidentescat(BD, companyid);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        //PagoRenta
        public async Task<JsonResult> getincidentescat(int BD)
        {
            var data = await IadGruposDelictivos.getpagorentaDDL(BD);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        public async Task<JsonResult> agegarincidenteDelictivo(
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
            bool esTipoRenta)
        {
            var data = await IadGruposDelictivos.AgregarIncidenteDelictivo(BD,
            incidenteid,
            motoristaid,
            mobileid,
            companyid,
            geozonaid,
            fecha,
            descripcion,
            costo,
            longitud,
            latitud,
            rutaid,
            contacto,
            estado,
            pagorentaid,
            fechaPagoRenta,
            estadoInconvenienteId,
            esTipoRenta
            
            );
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        public async Task<JsonResult> modificarincidenteDelictivo(
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
           )
           {
            var data = await IadGruposDelictivos.ModificarIncidenteDelictivo(
               BD,
               eventoid,
               incidenteid,
               motoristaid,
               mobileid,
               companyid,
               geozonaid,
               fecha,
               descripcion,
               costo,
               pagorentaid,
               estado,
               estadoInconvenienteId
            );
            return await Task.Run(() =>
            {
                return Json(data);
            });

        }


        public async Task<JsonResult> eliminarincidenteDelictivo(
                int BD,
                int companyid ,
                int eventoid
        )
        {
            var data = await IadGruposDelictivos.EliminarIncidenteDelictivo(
               BD,
               companyid,
               eventoid
            );
            return await Task.Run(() =>
            {
                return Json(data);
            });

        }

    }
}
