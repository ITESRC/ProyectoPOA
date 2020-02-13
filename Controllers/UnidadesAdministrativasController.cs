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
            return View(repository.GetUnidadesAdministrativas());
        }
        //Mostrar el formulario de Agregar
        public IActionResult Agregar()
        {
            return View();
        }
        //Agregar todos los campos del formulario
        [HttpPost]
        public IActionResult Agregar(UnidadAdministrativasViewModel unidad)
        {

            if (ModelState.IsValid)
            {
                repository = new UnidadAdministrativaRepository();
                repository.Insert(unidad);
                return RedirectToAction("Index", "UnidadesAdministrativas");
            }
            else
            {
                return View(unidad);
            }
        }
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            repository = new UnidadAdministrativaRepository();
            var consulta = repository.GetById(id);

            if (consulta != null)
            {
                repository.Delete(consulta);
                return RedirectToAction("Index");
                //Toast
               
            }
            ModelState.AddModelError("", "La unidad administrativa no existe o ya ha sido eliminada.");
            return View(consulta);
            
        }
    }
}