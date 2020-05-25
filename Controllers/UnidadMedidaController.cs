using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoPOA.Models;
using ProyectoPOA.Models.ViewModels;
using ProyectoPOA.Repositories;

namespace ProyectoPOA.Controllers
{
    public class UnidadMedidaController : Controller
    {
        UnidadMedidaRepository unidadMedidaRepository;
        public IActionResult Index()
        {
            unidadMedidaRepository = new UnidadMedidaRepository();
            UnidadMedidaViewModel unidadMedidaViewModel = new UnidadMedidaViewModel();
            unidadMedidaViewModel.ListaUnidadMedida = unidadMedidaRepository.GetUnidadmedidas();
            return View(unidadMedidaViewModel);
        }


        [HttpPost]
        public JsonResult Agregar(Unidadmedida unidadmedida) 
        {
            JsonResult jsonResult = null;
            unidadMedidaRepository = new UnidadMedidaRepository();
            try
            {
                if (unidadMedidaRepository.Validar(unidadmedida, out List<String> errores))
                {
                    unidadMedidaRepository.Insert(unidadmedida);
                    jsonResult = Json(true);
                }
                else
                {
                    String mensajes = String.Join("<br/>", errores);
                    jsonResult = Json(mensajes);
                }

            }
            catch (Exception ex)
            {
                jsonResult = Json(ex.Message);
            }

            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetUnidadById(Int32 Id) 
        {
            JsonResult jsonResult = null;
            unidadMedidaRepository = new UnidadMedidaRepository();
            Unidadmedida unidadmedida = unidadMedidaRepository.GetUnidadmedidaById(Id);

            if (unidadmedida==null)
            {
                jsonResult = Json("La unidad de medida no existe o ya ha sido eliminada");
            }
            else
            {
                jsonResult = Json(new
                {
                    unidadmedida.Id,
                    unidadmedida.Nombre,
                    unidadmedida.Eliminado
                });
            }
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Editar(Unidadmedida unidadmedida)
        {
            JsonResult jsonResult = null;
            unidadMedidaRepository = new UnidadMedidaRepository();
            try
            {
                if (unidadMedidaRepository.Validar(unidadmedida, out List<String> errores))
                {
                    unidadMedidaRepository.Insert(unidadmedida);
                    jsonResult = Json(true);
                }
                else
                {
                    String mensajes = String.Join("<br/>", errores);
                    jsonResult = Json(mensajes);
                }

            }
            catch (Exception ex)
            {
                jsonResult = Json(ex.Message);
            }

            return jsonResult;
        }

        [HttpPost]
        public JsonResult Eliminar(Int32 Id)
        {
            JsonResult jsonResult = null;
            unidadMedidaRepository = new UnidadMedidaRepository();
            try
            {
                if (unidadMedidaRepository.Eliminar(Id))
                {
                    jsonResult = Json(true);
                }
            }

            catch (Exception ex)
            {
                jsonResult = Json(ex.Message);
            }

            return jsonResult;
        }

    }
}