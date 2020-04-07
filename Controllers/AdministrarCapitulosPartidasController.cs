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
        PartidasRepository partidasRepository;

        public IActionResult Index()
        {
            capitulosRepository = new CapitulosRepository();
            CapitulosPartidasViewModel cPView = new CapitulosPartidasViewModel();
            cPView.ListaCapitulos = capitulosRepository.GetCapitulos();
            return View(cPView);
        }

        [HttpPost]
        public JsonResult AgregarCapitulo(CapitulosPartidasViewModel c)
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
        public JsonResult AgregarPartida(CapitulosPartidasViewModel p)
        {
            JsonResult json = null;
            partidasRepository = new PartidasRepository();
            try
            {
                if (partidasRepository.Validar(p.Partida, out List<String> errores))
                {
                    partidasRepository.Insert(p.Partida);
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
            capitulosRepository = new CapitulosRepository();
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
        public JsonResult GetPartida(Int32 Id)
        {
            JsonResult jsonResult = null;
            partidasRepository = new PartidasRepository();
            Partida part = partidasRepository.GetById(Id);
            if (part == null)
            {
                jsonResult = Json(false);
            }
            else
            {
                jsonResult = Json(new
                {
                    part.Id,
                    part.Concepto,
                    part.Clave,
                    part.Eliminado,
                    part.Capitulo,
                });
            }
            return jsonResult;
        }

        [HttpPost]
        public JsonResult EditarCapitulo(CapitulosPartidasViewModel c)
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
        public JsonResult EditarPartida(CapitulosPartidasViewModel p)
        {
            JsonResult json = null;
            partidasRepository = new PartidasRepository();
            try
            {
                if (partidasRepository.Validar(p.Partida, out List<String> errores))
                {
                    partidasRepository.Update(p.Partida);
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
        public JsonResult EliminarCapitulo(Int32 Id)
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

        [HttpPost]
        public JsonResult EliminarPartida(Int32 Id)
        {
            JsonResult jsonResult = null;

            partidasRepository = new PartidasRepository();
            try
            {
                if (partidasRepository.Eliminar(Id) == true)
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