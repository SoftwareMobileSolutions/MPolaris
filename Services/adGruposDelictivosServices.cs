using Dapper;
using Microsoft.Extensions.Hosting;
using mpolaris.Data;
using mpolaris.Interfaces;
using mpolaris.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using static mpolaris.Models.adGruposDelictivosModel;

namespace mpolaris.Services
{
    public class adGruposDelictivosServices: IadGruposDelictivos
    {
        private readonly SqlCnConfigMain _context;
        private readonly SqlCnConfigMainB _context2;
        public adGruposDelictivosServices(SqlCnConfigMain context, SqlCnConfigMainB context2)
        {
            _context = context;
            _context2 = context2;
        }

        //DDL Tipo incidente
        public async Task<IEnumerable<adGruposDelictivosModel.DDL_CatIncidentes>> Get_CatIncidentes(int BD, int companyid)
        {
            IEnumerable<adGruposDelictivosModel.DDL_CatIncidentes> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_ObtenerIncidentesDelictivos @companyid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.DDL_CatIncidentes>(query, new { companyid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        //DDL Vehículos
        public async Task<IEnumerable<adGruposDelictivosModel.DDL_vehiculos>> Get_VehiculosSubflotas(int BD, int userid)
        {
            IEnumerable<adGruposDelictivosModel.DDL_vehiculos> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec mpolaris_rpMoviles_ObtenerSubflota @userid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.DDL_vehiculos>(query, new { userid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        //DDL motoristas
        public async Task<IEnumerable<adGruposDelictivosModel.DDL_motoristas>> Get_Motoristas(int BD, int objecid, int tipo)
        {
            IEnumerable<adGruposDelictivosModel.DDL_motoristas> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec mpolaris_ObtenerMotoristaConFechaIngreso @objecid, @tipo";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.DDL_motoristas>(query, new { objecid, tipo }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        //DDL Geozonas Delictivas
        public async Task<(IEnumerable<adGruposDelictivosModel.DDL_geozonas>, IEnumerable<adGruposDelictivosModel.DDL_geozonas_Detalle>)> mpolaris_obtenerGeoDelictivos(int BD, int companyid)
        {
            //IEnumerable<adGruposDelictivosModel.DDL_motoristas> data;
            IEnumerable<DDL_geozonas> r1;
            IEnumerable<DDL_geozonas_Detalle> r2;


            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec mpolaris_obtenerGeoDelictivos @companyid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //  data = await conn.QueryAsync<adGruposDelictivosModel.DDL_motoristas>(query, new { companyid }, commandType: CommandType.Text);
                var reader = await conn.QueryMultipleAsync(query, new { companyid }, commandType: CommandType.Text);
                r1 = reader.Read<DDL_geozonas>().ToList();
                r2 = reader.Read<DDL_geozonas_Detalle>().ToList();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return (r1, r2);
        }

        //DDL Rutas
        public async Task<IEnumerable<adGruposDelictivosModel.DDL_Ruta>> Get_Ruta(int BD, int userid, int companyid)
        {
            IEnumerable<adGruposDelictivosModel.DDL_Ruta> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec mpolaris_ObtenerRutasConDuracion @userid, @companyid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.DDL_Ruta>(query, new { userid, companyid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<adGruposDelictivosModel.Grid_ListadoEventos>> getDatosGridDelictivos(int BD, int companyid, int userid)
        {
            IEnumerable<adGruposDelictivosModel.Grid_ListadoEventos> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_mpolarisGruposDelictivos @companyid, @userid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.Grid_ListadoEventos>(query, new { companyid, userid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }


        public async Task<IEnumerable<adGruposDelictivosModel.tipoinconvenientes_model>> getTipoInconvenientes(int BD, int tipo)
        {
            IEnumerable<adGruposDelictivosModel.tipoinconvenientes_model> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_ObtenerEstadoInconveniente_Polaris @tipo";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.tipoinconvenientes_model>(query, new { tipo }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }
        public async Task<IEnumerable<adGruposDelictivosModel.incidentescat_model>> getincidentescat(int BD, int companyid)
        {
            IEnumerable<adGruposDelictivosModel.incidentescat_model> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_ObtenerIncidentesDelictivos @companyid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.incidentescat_model>(query, new { companyid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<adGruposDelictivosModel.pagorenta_model>> getpagorentaDDL(int BD)
        {
            IEnumerable<adGruposDelictivosModel.pagorenta_model> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_ObtenerTiposPago_RentaPolaris";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.pagorenta_model>(query, new {  }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        //Agregar Evento delictivo
        public async Task<IEnumerable<adGruposDelictivosModel.msj>> AgregarIncidenteDelictivo(
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
        )
        {
            IEnumerable<adGruposDelictivosModel.msj> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_InsertarEventosZonasDelictivas @incidenteid, @motoristaid, @mobileid, @companyid, @geozonaid, @fecha, @descripcion, @costo, @longitud, @latitud, @rutaid, @contacto, @estado, @pagorentaid, @fechaPagoRenta, @estadoInconvenienteId, @esTipoRenta";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.msj>(query, new {
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
                }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        //Modificar Eventos delictivos
        public async Task<IEnumerable<adGruposDelictivosModel.msj>> ModificarIncidenteDelictivo(
           int BD,
           int eventoid ,
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
            IEnumerable<adGruposDelictivosModel.msj> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec mpolaris_ActualizarEventosZonasDelictivas @eventoid, @incidenteid, @motoristaid, @mobileid, @companyid, @geozonaid, @fecha, @descripcion, @costo, @pagorentaid, @estado, @estadoInconvenienteId";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.msj>(query, new
                {
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
                }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }



        //Modificar Eventos delictivos
        public async Task<IEnumerable<adGruposDelictivosModel.msj>> EliminarIncidenteDelictivo(
           int BD,
           int companyid,
           int eventoid
       )
        {
            IEnumerable<adGruposDelictivosModel.msj> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_EliminarEventoZonaDelictiva @companyid, @eventoid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adGruposDelictivosModel.msj>(query, new
                {
                    companyid,
                    eventoid
                }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

    }
}
