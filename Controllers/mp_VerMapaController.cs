using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using mpolaris.Interfaces;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet] //BD: 0 = Kontrol, 1 = Bluefenyx
        public async Task<JsonResult> Get_peligrosidadDelictiva(int BD, int companyid)
        {
            var data = await Imp_vermapa.Get_peligrosidadDelictiva(BD, companyid);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        [HttpGet]
        public async Task<JsonResult> Get_geodelictivas(int BD, int companyid)
        {
            var data = await Imp_vermapa.Get_geodelictivas(BD, companyid);
            return await Task.Run(() =>
            {
                return Json(new { r1 = data.Item1, r2 =data.Item2 });
            });
        }

        //Eventos Anuales
        [HttpGet]
        public async Task<JsonResult> getEventosAnuales(int BD, int companyid, string fechaini, string fechafin, int geozoneid)
        {
            var data = await Imp_vermapa.getEventosAnuales(BD, companyid, fechaini, fechafin, geozoneid);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }



    }
}
