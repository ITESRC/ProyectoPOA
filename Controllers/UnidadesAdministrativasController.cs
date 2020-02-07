using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Controllers
{
    public class UnidadesAdministrativasController : Controller
    {
        public IActionResult Index()
        {
            UnidadAdministrativaRepository repository = new UnidadAdministrativaRepository();
            return View(repository.GetAll());
        }
    }
}