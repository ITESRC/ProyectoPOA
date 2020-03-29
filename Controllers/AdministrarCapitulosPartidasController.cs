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
    public class AdministrarCapitulosPartidasController : Controller
    {

        CapitulosRepository capitulosRepository;

        public IActionResult Index()
        {
            capitulosRepository = new CapitulosRepository();
            CapitulosPartidasViewModel cPView = new CapitulosPartidasViewModel();
            cPView.ListaCapitulos = capitulosRepository.GetCapitulos();
            return View(cPView);
        }

        [HttpPost]
        public JsonResult Agregar(CapitulosPartidasViewModel c)
        {
            JsonResult json = null;
            capitulosRepository = new CapitulosRepository();
            try
            {
                if (capitulosRepository.Validar(c.Capitulo, out List<String> errores))
                {
                    capitulosRepository.Insert(c.Capitulo);
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
        public JsonResult GetCapitulo(Int32 Id)
        {
            JsonResult jsonResult = null;
            CapitulosRepository capitulosRepository = new CapitulosRepository();
            Capitulo capit = capitulosRepository.GetById(Id);
            if (capit == null)
            {
                jsonResult = Json(false);
            }
            else
            {
                jsonResult = Json(new
                {
                    capit.Id,
                    capit.Nombre,
                    capit.Clave,
                    capit.Eliminado,
                    capit.Partida
                }
                    );
            }
            return jsonResult;
        }



        [HttpPost]
        public JsonResult Editar(CapitulosPartidasViewModel c)
        {
            JsonResult json = null;
            capitulosRepository = new CapitulosRepository();
            try
            {
                if (capitulosRepository.Validar(c.Capitulo, out List<String> errores))
                {
                    capitulosRepository.Update(c.Capitulo);
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
        public JsonResult Eliminar(Int32 Id)
        {
            JsonResult jsonResult = null;

            capitulosRepository = new CapitulosRepository();
            try
            {
                if (capitulosRepository.Eliminar(Id) == true)
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