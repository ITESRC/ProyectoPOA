using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Helpers;
using ProyectoPOA.Helpers.OptionEnum;
using ProyectoPOA.Models;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Controllers
{
    public class PIIDController : Controller
    {
        ObjetivoRepository repo;
        Objetivo obj = new Objetivo();
        public static dynamic mensaje;
        public IActionResult Index()
        {
            repo = new ObjetivoRepository();
            var nov = repo.GetAll();
            ViewBag.Lista = repo.GetEstrategias();

            return View(nov);
        }
        public IActionResult Nuevo(Objetivo no)
        {

            if (ModelState.IsValid)
            {
                repo = new ObjetivoRepository();



                var cons = repo.Context.Objetivo.FirstOrDefault(x => x.Id == no.Id);
                cons.Eliminado = false;
                repo.Update(cons);
                ViewBag.Message = Notification.Show("Objetivo Creado", "Aviso", position: Position.TopRight, type: ToastType.Success);
                mensaje = ViewBag.Message;
                return new RedirectToActionResult("Index", "Objetivos", null);

            }
            else
            {
                ViewBag.Message = Notification.Show("No se pudo crear el objetivo", "Aviso", position: Position.TopRight, type: ToastType.Error);
                mensaje = ViewBag.Message;
                return View("Index", no);
            }

        }
        public IActionResult Editar(Objetivo eo)
        {
            if (ModelState.IsValid)
            {

                repo = new ObjetivoRepository();
                var cons = repo.Context.Objetivo.FirstOrDefault(x => x.Id == eo.Id);
                cons.Id = eo.Id;
                cons.Nombre = eo.Nombre;
                cons.Eliminado = eo.Eliminado;
                cons.Estrategia = eo.Estrategia;
                repo.Update(cons);
                ViewBag.Message = Notification.Show("Objetivo editado exitosamente", "Aviso", position: Position.TopRight, type: ToastType.Success);
                mensaje = ViewBag.Message;
                return new RedirectToActionResult("Index", "Objetivos", null);
            }
            else
            {
                ViewBag.Message = Notification.Show("No se pudo editar el objetivo", "Aviso", position: Position.TopRight, type: ToastType.Error);
                mensaje = ViewBag.Message;
                return View(eo);
            }

        }
        public IActionResult Desactivar(Objetivo delO)
        {
            repo = new ObjetivoRepository();
            var result = repo.Context.Objetivo.FirstOrDefault(x => x.Id == delO.Id);
            repo.Delete(result);
            ViewBag.Message = Notification.Show("Objetivo Dado de Baja", "Aviso", position: Position.TopRight, type: ToastType.Error);
            mensaje = ViewBag.Message;
            return RedirectToAction("Index");
        }
    }
}