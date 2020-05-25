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
    public class ArticulosController : Controller
    {

        ArticulosRepository articulosRepository;

        public IActionResult Index()
        {
            articulosRepository = new ArticulosRepository();
            ArticulosViewModel articulosViewModel = new ArticulosViewModel();
            articulosViewModel.ListaArticulos = articulosRepository.GetArticulos();
            return View(articulosViewModel);
        }

        [HttpGet]
        public IActionResult Index(String desc)
        {
            articulosRepository = new ArticulosRepository();
            ArticulosViewModel articulosViewModel = new ArticulosViewModel();
            if (!String.IsNullOrWhiteSpace(desc))
            {
                articulosViewModel.ListaArticulos = articulosRepository.GetArticulosByDescripcion(desc);
                return View(articulosViewModel);
            }
            else
            {
                articulosViewModel.ListaArticulos = articulosRepository.GetArticulos();
                return View(articulosViewModel);
            }
        }

        [HttpPost]
        public JsonResult Agregar(Articulo articulo)
        {
            JsonResult json = null;
            articulosRepository = new ArticulosRepository();

            try
            {
                if (articulosRepository.Validar(articulo, out List<String> errores))
                {
                    articulosRepository.Insert(articulo);
                    json = Json(true);
                }
                else
                {
                    String mensajes = String.Join("<br/>", errores);
                    json = Json(mensajes);
                }
            }
            catch (Exception ex)
            {
                json = Json(ex.Message);
            }

            return json;

        }

        [HttpPost]
        public JsonResult GetArticuloById(Int32 Id)
        {
            JsonResult json = null;
            articulosRepository = new ArticulosRepository();
            Articulo articulo = articulosRepository.GetPartidaXId(Id);

            if (articulo == null)
            {
                json = Json(false);
            }
            else
            {
                json = Json(new
                {
                    articulo.Id,
                    articulo.CostoUnitario,
                    articulo.Descripcion,
                    articulo.Idpartida,
                    articulo.Idunidadmedida,
                    articulo.Eliminado
                }
                );
            }
            return json;
        }

        [HttpPost]
        public JsonResult Editar(Articulo articulo)
        {
            JsonResult json = null;
            articulosRepository = new ArticulosRepository();

            try
            {
                if (articulosRepository.Validar(articulo, out List<String> errores))
                {
                    articulosRepository.Update(articulo);
                    json = Json(true);
                }
                else
                {
                    String mensajes = String.Join("<br/>", errores);
                    json = Json(mensajes);
                }
            }
            catch (Exception ex)
            {
                json = Json(ex.Message);
            }

            return json;
        }

        [HttpPost]
        public JsonResult Eliminar(Int32 id)
        {
            JsonResult json = null;
            articulosRepository = new ArticulosRepository();
            try
            {
                if (articulosRepository.Eliminar(id))
                {
                    json = Json(true);

                }
            }
            catch (Exception ex)
            {
                json = Json(ex.Message);
            }
            return json;
        }






    }
}