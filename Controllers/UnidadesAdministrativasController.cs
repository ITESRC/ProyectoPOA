﻿using System;
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
                    if (repository.ValidarUnidadAdministrativa(unidad, false))
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
        public IActionResult Editar(Unidadadministrativa unidad)
        {
            if (ModelState.IsValid)
            {
                repository = new UnidadAdministrativaRepository();
                try
                {
                    if (repository.ValidarUnidadAdministrativa(unidad, true))
                    {
                        repository = new UnidadAdministrativaRepository();
                        repository.Update(unidad);
                        return new RedirectToActionResult("Index", "UnidadesAdministrativas", null);
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
            repository.EliminarUnidad(id);
            return RedirectToAction("Index");
        }
    }
}