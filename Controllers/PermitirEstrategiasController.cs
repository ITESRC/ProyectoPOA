using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Controllers
{
    public class PermitirEstrategiasController : Controller
    {
        PermitirEstrategiaRepository context;

        [HttpGet]
        public IActionResult Index([FromQuery]int id)
        {
            if (id > 0)
            {
                context = new PermitirEstrategiaRepository();
                var model = context.GetEstrategiasPermitidasDeUnidad(id);
                if (model != null)
                {
                    ViewBag.IdUnidad = id;
                    ViewBag.NombreUnidad = model.NombreUnidad;
                    return View(model);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}