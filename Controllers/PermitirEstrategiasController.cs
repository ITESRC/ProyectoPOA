using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using ProyectoPOA.Models.ViewModels;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Controllers
{
    public class PermitirEstrategiasController : Controller
    {
        PermitirEstrategiaRepository context;

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult MostrarEstrategiasByUnidad(int id)
        {
            context = new PermitirEstrategiaRepository();
            var data = context.GetEstrategiasPermitidasDeUnidad(id);
            return Json(data);
        }

        public JsonResult MostrarEstrategiasParaPermitir(int id)
        {
            context = new PermitirEstrategiaRepository();
            var data = context.GetEstrategiasParaPermitir(id);
            return Json(data);
        }

        [HttpPost]
        public JsonResult EditarEstrategiasPermitidas(EditarEstrategiasPermitidasViewModel estrategias)
        {
            context = new PermitirEstrategiaRepository();
            var data = context.Editar(estrategias);
            return Json(data);
        }
    }
}