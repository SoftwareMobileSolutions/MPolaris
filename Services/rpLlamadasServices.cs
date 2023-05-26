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
using static mpolaris.Models.rpLlamadasRealizadasModel;

namespace mpolaris.Services
{
    public class rpLlamadasServices : IrpLlamadas
    {
        private readonly SqlCnConfigMain _context;
        private readonly SqlCnConfigMainB _context2;
        public rpLlamadasServices(SqlCnConfigMain context, SqlCnConfigMainB context2)
        {
            _context = context;
            _context2 = context2;
        }

        //DDL Tipo incidente
        public async Task<IEnumerable<rpLlamadasRealizadasModel>> GetLlamadasRealizadas(int BD, string fini, string ffin, int companyid)
        {
            IEnumerable<rpLlamadasRealizadasModel> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec mpolaris_llamadasrealizadas_mobile @fini, @ffin, @companyid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<rpLlamadasRealizadasModel>(query, new { fini, ffin, companyid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }
    }

    /*
     EXEC SP_KontrolAReporteWeb 7208, 'A', 2, -6

exec Fleets_ObtenerEstadoInconveniente_Polaris 1

-- EXEC Fleets_InsertarEventosZonasDelictivas

exec Fleets_ObtenerTiposPago_RentaPolaris

EXEC Fleets_InsertarEventosZonasDelictivas

EXEC mpolaris_llamadasrealizadas '20220123 00:00:00', '20230123 00:00:00'
     */
}
