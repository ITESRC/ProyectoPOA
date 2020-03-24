using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Models;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Controllers
{
    public class AdministrarCapitulosPartidasController : Controller
    {

        CapitulosRepository capitulosRepository;

        public IActionResult Index()
        {
            capitulosRepository = new CapitulosRepository();
            IEnumerable<Capitulo> listCap = capitulosRepository.GetCapitulos();
            //ViewBag.ListaCap = listCap;
            return View(listCap);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(Capitulo c)
        {
            var error = ModelState.Values.Select(x => x.Errors);
            if (ModelState.IsValid)
            {
                capitulosRepository = new CapitulosRepository();
                List<String> errores = capitulosRepository.Validar(c);
                if (errores != null)
                {
                    for (Int32 i = 0; i < errores.Count; i++)
                    {
                        ModelState.AddModelError(i.ToString(), errores[i]);
                    }
                    return RedirectToAction("AdministrarPIIDPartidasCapitulos");
                }
                else
                {
                    c.Nombre = c.Nombre.Trim().ToUpper();
                    capitulosRepository.Insert(c);
                    return new RedirectToActionResult("AdministrarPIIDPartidasCapitulos", "Capitulos", null);
                }
            }
            else
            {
                return RedirectToAction("AdministrarPIIDPartidasCapitulos");
            }
        }


        public IActionResult Editar(Int32 Id)
        {
            if (Id > 2147483647 && Id < 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                capitulosRepository = new CapitulosRepository();
                Capitulo capitulo = capitulosRepository.GetById(Id);
                if (capitulo != null && capitulo.Eliminado == false)
                {
                    return View(capitulo);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Capitulo c)
        {
            if (ModelState.IsValid && c.Eliminado == false)
            {
                capitulosRepository = new CapitulosRepository();
                List<String> errores = capitulosRepository.Validar(c);
                if (errores != null)
                {
                    for (Int32 i = 0; i < errores.Count; i++)
                    {
                        ModelState.AddModelError(i.ToString(), errores[i]);
                    }
                    return View(errores);
                }
                else
                {
                    Capitulo capitulo = capitulosRepository.GetById(c.Id);
                    capitulo.Nombre = c.Nombre;
                    capitulosRepository.Update(capitulo);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(c);
            }
        }

        [HttpPost]
        public IActionResult Eliminar(Int32 Id)
        {
            capitulosRepository = new CapitulosRepository();
            capitulosRepository.Eliminar(Id);
            return RedirectToAction("Index");

        }

    }
}