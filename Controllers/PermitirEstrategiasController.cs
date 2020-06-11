using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Controllers
{
    public class PermitirEstrategiasController : Controller
    {
        PermitirEstrategiaRepository context;

        //[HttpGet]
        //public IActionResult Index([FromQuery]int id)
        //{
        //    if (id > 0)
        //    {
        //        context = new PermitirEstrategiaRepository();
        //        var model = context.GetEstrategiasPermitidasDeUnidad(id);
        //        if (model != null)
        //        {
        //            ViewBag.IdUnidad = id;
        //            ViewBag.NombreUnidad = model.NombreUnidad;
        //            return View(model);
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

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
    }
}