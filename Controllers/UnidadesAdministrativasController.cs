using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Models;
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
        public IActionResult Agregar(Unidadadministrativa unidad)
        {
            if (ModelState.IsValid)
            {
                repository = new UnidadAdministrativaRepository();
                try
                {
                    if (repository.ValidarUnidadAdministrativa(unidad))
                    {
                        repository.Insert(unidad);
                        return new RedirectToActionResult("Index","UnidadesAdministrativas", null);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(unidad);
                }
                return View(unidad);
            }
            else
            {
                return View(unidad);
            }
        }

        public IActionResult Eliminar(int id)
        {
            repository = new UnidadAdministrativaRepository();
            var consulta = repository.GetById(id);
                repository.Delete(consulta);
                return RedirectToAction("Index");
            //ModelState.AddModelError("", "La unidad administrativa no existe o ya ha sido eliminada.");
            //return View(consulta);
        }
    }
}