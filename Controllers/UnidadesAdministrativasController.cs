using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Models;
using ProyectoPOA.Repositories;
using System.Text;
using System.Text.RegularExpressions;
using ProyectoPOA.Helpers;
using ProyectoPOA.Helpers.OptionEnum;

namespace ProyectoPOA.Controllers
{
    public class UnidadesAdministrativasController : Controller
    {
        UnidadAdministrativaRepository repository;
        public IActionResult Index()
        {
            ViewBag.Message = mensaje;
            mensaje = null;
            repository = new UnidadAdministrativaRepository();
            var most = repository.GetUnidadesAdministrativas();
            return View(most);
        }

        //Mostrar el formulario de Agregar
        public IActionResult Agregar()
        {
            return View();
        }
        public static dynamic mensaje;
        //Agregar todos los campos del formulario
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    repository.Insert(vm);
                    ViewBag.Message = Notification.Show("Se ha agregado correctamente", "Aviso", position: Position.TopRight, type: ToastType.Success);
                    mensaje = ViewBag.Message;
                    return new RedirectToActionResult("Index", "UnidadesAdministrativas", null);
                }
            }
            else
            {
                ViewBag.Message = Notification.Show("No se pudo agregar el elemento", "Aviso", position: Position.TopRight, type: ToastType.Error);
                mensaje = ViewBag.Message;
                return View(vm);
            }
        }

        public IActionResult Editar(int id)
        {
            repository = new UnidadAdministrativaRepository();
            var unidad = repository.EditarUnidadById(id);

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
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Unidadadministrativa vm)
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
                    repository.Update(varEntidad);
                    ViewBag.Message = Notification.Show("Se ha editado correctamente","Aviso", position: Position.TopRight, type: ToastType.Success);
                    mensaje = ViewBag.Message;
                    return new RedirectToActionResult("Index", "UnidadesAdministrativas", null);
                }
            }
            else
            {
                ViewBag.Message = Notification.Show("No se pudo editar el elemento", "Error", position: Position.TopRight, type: ToastType.Error);
                mensaje = ViewBag.Message;
                return View(vm);
            }
        }

        public IActionResult Eliminar(int id)
        {
            try
            {
                repository = new UnidadAdministrativaRepository();
                repository.EliminarUnidad(id);

                ViewBag.Message = Notification.Show("Se ha eliminado correctamente","Aviso", position: Position.TopRight, type: ToastType.Success);
                mensaje = ViewBag.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = Notification.Show(ex.Message, "Error", position: Position.TopRight, type: ToastType.Error);
                mensaje = ViewBag.Message;
                return RedirectToAction("Index");
            }
        }
    }
}