using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Models;
using ProyectoPOA.Repositories;
using System.Text;
using System.Text.RegularExpressions;

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

        [HttpPost]
        public IActionResult Index(string dato)
        {
            ViewBag.Busqueda = dato;
            if (!string.IsNullOrWhiteSpace(dato))
            {
                repository = new UnidadAdministrativaRepository();
                return View(repository.FiltrarUnidades(dato));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //Mostrar el formulario de Agregar
        public IActionResult Agregar()
        {
            return View();
        }
        //Agregar todos los campos del formulario
        [HttpPost]
        public IActionResult Agregar(Unidadadministrativa vm)
        {
            if (ModelState.IsValid)
            {
                repository = new UnidadAdministrativaRepository();
                List<string> errores = repository.Validar(vm);

                if (errores != null)
                {
                    for (int i = 0; i < errores.Count; i++)
                    {
                        ModelState.AddModelError(i.ToString(), errores[i]);
                    }
                    return View(vm);
                }
                else
                {
                    var varEntidad = repository.GetById(vm.Id);
                    varEntidad.Nombre = vm.Nombre;
                    varEntidad.Clave = vm.Clave;
                    varEntidad.Encargado = vm.Encargado;
                    varEntidad.IdUnidadSuperior = vm.IdUnidadSuperior;
                    repository.Insert(varEntidad);
                    return new RedirectToActionResult("Index", "UnidadesAdministrativas", null);
                }
            }
            else
            {
                return View(vm);
            }
        }

        public IActionResult Editar(int id)
        {
            repository = new UnidadAdministrativaRepository();
            var unidad = repository.GetById(id);

            if (unidad != null)
            {
                return View(unidad);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(Unidadadministrativa vm)
        {
            if (ModelState.IsValid)
            {
                repository = new UnidadAdministrativaRepository();
                List<string> errores = repository.Validar(vm);

                if(errores != null)
                {
                    for (int i = 0; i < errores.Count; i++)
                    {
                        ModelState.AddModelError(i.ToString(), errores[i]);
                    }
                    return View(vm);
                }
                else
                {
                    var varEntidad = repository.GetById(vm.Id);
                    varEntidad.Nombre = vm.Nombre;
                    varEntidad.Clave = vm.Clave;
                    varEntidad.Encargado = vm.Encargado;
                    varEntidad.IdUnidadSuperior = vm.IdUnidadSuperior;
                    repository.Update(varEntidad);
                    return new RedirectToActionResult("Index", "UnidadesAdministrativas", null);
                }
            }
            else
            {
                return View(vm);
            }
        }

        public IActionResult Eliminar(int id)
        {
            try
            {
                repository = new UnidadAdministrativaRepository();
                repository.EliminarUnidad(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}