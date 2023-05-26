using mpolaris.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static mpolaris.Models.rpPagosRealizadosModel;

namespace mpolaris.Interfaces
{
    public interface IrpPagosRealizados
    {
        Task<(IEnumerable<rpPagosRealizadosModel.r1_pagos>, IEnumerable<rpPagosRealizadosModel.r1_geodetalle>)> getPagosRealizados(int BD, string fini, string ffin, int companyid);
    }
}
