using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using mpolaris.Interfaces;

namespace mpolaris.Controllers
{
    public class mp_VerMapaController : Controller
    {
        private readonly Imp_vermapa Imp_vermapa;

        public mp_VerMapaController(Imp_vermapa _Imp_vermapa)
        {
            Imp_vermapa = _Imp_vermapa;
        }

        public async Task<IActionResult> mp_VerMapa()
        {
            return await Task.Run(() =>
            {
                return PartialView();
            });
        }

        [HttpGet]
        public async Task<JsonResult> Get_peligrosidadDelictiva(int BD, int companyid)
        {
            var data = await Imp_vermapa.Get_peligrosidadDelictiva(BD, companyid);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        [HttpGet]
        public async Task<JsonResult> Get_geodelicitvas(int BD, int companyid)
        {
            var data = await Imp_vermapa.Get_geodelicitvas(BD, companyid);
            return await Task.Run(() =>
            {
                return Json(new { r1 = data.Item1, r2 =data.Item2 });
            });
        }

    }
}
