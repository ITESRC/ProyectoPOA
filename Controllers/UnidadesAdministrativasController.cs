using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Models.ViewModels;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Controllers
{
    public class UnidadesAdministrativasController : Controller
    {
        UnidadAdministrativaRepository repository;
        public IActionResult Index()
        {
             repository = new UnidadAdministrativaRepository();
            return View(repository.GetAll());
        }
        //Mostrar el formulario de Agregar
        public IActionResult Agregar()
        {
            return View();
        }
        //Agregar todos los campos del formulario
        public IActionResult Agregar(UnidadAdministrativasViewModel unidad)
        {
            repository = new UnidadAdministrativaRepository();
            if (ModelState.IsValid)
            {
                repository.Context.Add(unidad);
                repository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(unidad);
            }
        }

    }
}