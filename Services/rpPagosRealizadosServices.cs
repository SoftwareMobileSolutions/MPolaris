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
    public class rpPagosRealizadosServices: IrpPagosRealizados
    {

        private readonly SqlCnConfigMain _context;
        private readonly SqlCnConfigMainB _context2;


        public rpPagosRealizadosServices(SqlCnConfigMain context, SqlCnConfigMainB context2)
        {
            _context = context;
            _context2 = context2;
        }

        public async Task<(IEnumerable<rpPagosRealizadosModel.r1_pagos>, IEnumerable<rpPagosRealizadosModel.r1_geodetalle>)> getPagosRealizados(int BD, string fini, string ffin, int companyid)
        {
            IEnumerable<rpPagosRealizadosModel.r1_pagos> r1;
            IEnumerable<rpPagosRealizadosModel.r1_geodetalle> r2;

            var conn = BD == 0 ? _context.CreateConnection() : _context2.CreateConnection();
            using (conn)
            {
                string query = @"exec mpolaris_rppagosRealizados @fini, @ffin, @companyid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var reader = await conn.QueryMultipleAsync(query, new { fini, ffin, companyid }, commandType: CommandType.Text);
                r1 = reader.Read<rpPagosRealizadosModel.r1_pagos>().ToList();
                r2 = reader.Read<rpPagosRealizadosModel.r1_geodetalle>().ToList();



                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return (r1, r2);
        }
    }
}
