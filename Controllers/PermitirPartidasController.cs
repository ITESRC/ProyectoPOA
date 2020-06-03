using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Models.ViewModels.UaPartidasCapitulos;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Controllers
{
    public class PermitirPartidasController : Controller
    {
        PermitirPartidasRepository data;

        public IActionResult Index()
        {
            data = new PermitirPartidasRepository();
            ViewBag.SupList = data.GetSuperiores();
            return View();
        }
        [HttpPost]
        public JsonResult Mostrar(int id)
        {
            data = new PermitirPartidasRepository();
            var datas = data.GetPartidasPermitidas(id);
            //TempData["Partidas"] = datas;
            return Json(datas);
        }
        [HttpPost]
        public JsonResult Agregar()
        {
            data = new PermitirPartidasRepository();
            var res = data.GetPartidasPermitidas(1);
            var list = res.CapitulosPermitidos.Select(x => x.PartidasPermitidas.Where(y => y.IdCapitulo == x.IdCapituloPermitido));

            return Json(list);
        }
        [HttpGet]
        public JsonResult Eliminar(int[] partidas)
        {
            data = new PermitirPartidasRepository();
            for (int i = 0; i < partidas.Length; i++)
            {
                UnidadPartidasPermitidasViewModel das = (UnidadPartidasPermitidasViewModel)TempData["Partidas"];
                var partida = das.CapitulosPermitidos.Select(x => x.PartidasPermitidas);
                var filt = data.Context.Partida.FirstOrDefault(x => x.Clave == partidas[i]);
                foreach (var item in partida)
                {
                    var cafe = item.Select(x => x.IdPartida);
                    foreach (var item2 in cafe)
                    {
                        if (item2 == filt.Clave)
                        {
                            var ult = data.Context.Uapartidas.FirstOrDefault(x => x.IdPartida == filt.Clave);
                            data.Delete(ult);
                            return Json(true);
                        }
                    }

                }
            }
            return Json(false);
        }

    }
}
