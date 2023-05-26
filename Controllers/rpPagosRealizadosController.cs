using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpolaris.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace mpolaris.Controllers
{
    public class rpPagosRealizadosController : Controller
    {
        private readonly IrpPagosRealizados IrpPagosRealizados;
        public rpPagosRealizadosController(IrpPagosRealizados _IrpPagosRealizados)
        {
            IrpPagosRealizados = _IrpPagosRealizados;
        }
        public async Task<IActionResult> rpPagosRealizados()
        {
            return await Task.Run(() =>
            {
                return PartialView();
            });
        }

        public async Task<JsonResult> GetPagosRealizados(int BD, string fini, string ffin, int companyid)
        {
            var data = await IrpPagosRealizados.getPagosRealizados(BD, fini, ffin, companyid);
            return await Task.Run(() =>
            {
                return Json(new { r1 = data.Item1, r2 = data.Item2});
            });
        }

    }
}
