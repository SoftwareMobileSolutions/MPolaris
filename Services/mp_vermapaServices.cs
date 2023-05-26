using Dapper;
using mpolaris.Data;
using mpolaris.Interfaces;
using mpolaris.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace mpolaris.Services
{
    public class mp_vermapaServices : Imp_vermapa
    {
        private readonly SqlCnConfigMain _context;
        private readonly SqlCnConfigMainB _context2;


        public mp_vermapaServices(SqlCnConfigMain context, SqlCnConfigMainB context2)
        {
            _context = context;
            _context2 = context2;
        }

        public async Task<IEnumerable<mp_vermapaModel.nivelpeligrosidad>> Get_peligrosidadDelictiva(int BD, int companyid)
        {
            IEnumerable<mp_vermapaModel.nivelpeligrosidad> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection() ;
            using (conn)
            {
                string query = @"exec Fleets_ObtenerPeligrosidadGrupoDelictivo @companyid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<mp_vermapaModel.nivelpeligrosidad>(query, new { companyid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }



        public async Task<(IEnumerable<mp_vermapaModel.geodelictiva_r1>, IEnumerable<mp_vermapaModel.geodelictiva_r2>)> Get_geodelictivas(int BD, int companyid)
        {
            IEnumerable<mp_vermapaModel.geodelictiva_r1> r1;
            IEnumerable<mp_vermapaModel.geodelictiva_r2> r2;

            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_mpolaris_geozonasdelictivas @companyid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var reader = await conn.QueryMultipleAsync(query, new { companyid }, commandType: CommandType.Text);
                r1 = reader.Read<mp_vermapaModel.geodelictiva_r1>().ToList();
                r2 = reader.Read<mp_vermapaModel.geodelictiva_r2>().ToList();


                
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return (r1, r2);
        }

        //Eventos Anuales
        public async Task<IEnumerable<mp_vermapaModel.rpGeoEventoAnual>> getEventosAnuales(int BD, int companyid, string fechaini, string fechafin, int geozoneid)
        {
            IEnumerable<mp_vermapaModel.rpGeoEventoAnual> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_mpolariosEventoDelictivoPorGeozona @companyid, @fechaini, @fechafin, @geozoneid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<mp_vermapaModel.rpGeoEventoAnual>(query, new { companyid, fechaini, fechafin, geozoneid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }
    }
}
