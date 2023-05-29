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
    public class rpEventosDelictivosServices: IrpEventosDelictivos
    {
        private readonly SqlCnConfigMain _context;
        private readonly SqlCnConfigMainB _context2;
        public rpEventosDelictivosServices(SqlCnConfigMain context, SqlCnConfigMainB context2)
        {
            _context = context;
            _context2 = context2;
        }

        public async Task<IEnumerable<rpEventosDelictivosModel.griddelictivo>> Get_GridrpDelictivos(int BD, int companyid, string fechai, string fechafin, /*string geozonasid,*/ string mobilesid, /*string tipoincidentesid, string motoristasid,*/ int userid)
        {
            IEnumerable<rpEventosDelictivosModel.griddelictivo> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec mpolaris_ObtenerReporteEventosDelictivosMobile @companyid, @fechai, @fechafin, @mobilesid, @userid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<rpEventosDelictivosModel.griddelictivo>(query, new { companyid, fechai, fechafin, /*geozonasid,*/ mobilesid, /*tipoincidentesid, motoristasid,*/ userid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<rpEventosDelictivosModel.treeDelictivos>> gettreeMobiles(int BD, int userid)
        {
            IEnumerable<rpEventosDelictivosModel.treeDelictivos> data;
            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec mpolaris_rpMoviles_getSubflota @userid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<rpEventosDelictivosModel.treeDelictivos>(query, new { userid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

    }
}
