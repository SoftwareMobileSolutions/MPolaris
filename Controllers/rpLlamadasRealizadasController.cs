using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mpolaris.Interfaces;
using System.Linq;
using System.Threading.Tasks;


namespace mpolaris.Controllers
{
    public class rpLlamadasRealizadasController : Controller
    {
        private readonly IrpLlamadas IrpLlamadas;
        public rpLlamadasRealizadasController(IrpLlamadas _IrpLlamadas)
        {
            IrpLlamadas = _IrpLlamadas;
        }

        public async Task<IActionResult> rpLlamadasRealizadas()
        {
            return await Task.Run(() =>
            {
                return PartialView();
            });
        }

        public async Task<JsonResult> GetLlamadasRealizadas(int BD, string fini, string ffin, int companyid, int tipoLlamada)
        {
            var data = await IrpLlamadas.GetLlamadasRealizadas(BD, fini, ffin, companyid, tipoLlamada);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

    }
}
