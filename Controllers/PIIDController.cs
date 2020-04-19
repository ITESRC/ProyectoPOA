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
        ObjetivoRepository Orepo;
        EstrategiasRepository Erepo;
        public static dynamic mensaje;
        public IActionResult Index()
        {
            ViewBag.Message = mensaje;
            mensaje = null;
            Orepo = new ObjetivoRepository();
            //var nov = Orepo.GetEstrategias();
            ViewBag.NoVigentes = Orepo.ObjetivosInactivos();
            ViewBag.Lista = Orepo.GetEstrategias();

            return View();
        }
        //El objetivo se habilita preguntando si desea Reahabilitar las estrategias que tenia
        [HttpPost]
        public JsonResult Nuevo(Objetivo ob)
        {
            JsonResult json = null;
            Orepo = new ObjetivoRepository();
            try
            {
                List<string> errores = Orepo.Validar(ob.Id);
                if (errores.Count>0)
                {
                    for (int i = 0; i < errores.Count; i++)
                    {
                        ModelState.AddModelError(i.ToString(), errores[i]);
                    }
                    json = Json(false);
                }
                else
                {

                    var cons = Orepo.Context.Objetivo.FirstOrDefault(x => x.Id == ob.Id);
                    cons.Eliminado = false;
                    Orepo.Update(cons);
                    json = Json(true);
                    ViewBag.Message = Notification.Show("Objetivo Creado", "Aviso", position: Position.TopRight, type: ToastType.Success);
                    mensaje = ViewBag.Message;

                    
                }
            }
            catch (Exception)
            {
                ViewBag.Message = Notification.Show("No se pudo crear el objetivo", "Aviso", position: Position.TopRight, type: ToastType.Error);
                mensaje = ViewBag.Message;
                json = Json(mensaje);
            }
            return json;
        }
        [HttpPost]
        public JsonResult GetObjetivo(int idObjetivo)
        {
            JsonResult json = null;
            Erepo = new EstrategiasRepository();
            var Estrategias = Erepo.GetEstrategiasByObjetivo(idObjetivo);
            try
            {
                if (Estrategias != null)
                {
                    json = Json(new { Estrategias });
                }
                else
                {
                    json = Json(false);
                }

            }
            catch (Exception ex)
            {
                json = Json(ex);
            }
            return json;
        }
        //Editar las estrategias agregadas y agregar
        [HttpPost]
        public JsonResult Editar(int[] eo)
        {
            Orepo = new ObjetivoRepository();
            Erepo = new EstrategiasRepository();
            JsonResult json = null;
            try
            {
                if (eo != null)
                {
                    var res = Erepo.DesHabEstrategias(eo);
                    if (res)
                    {
                        ViewBag.Message = Notification.Show("Estrategias Actualizadas", "Aviso", position: Position.TopRight, type: ToastType.Success);
                        mensaje = ViewBag.Message;
                        json = Json(true);
                    }
                    else
                    {
                        var error = "Alguna de las estrategias no existe";
                        json = Json(error);
                    }
                }
            }
            catch (Exception ex)
            {
                json = Json(false);
                ViewBag.Message = Notification.Show(ex.Message, "Aviso", position: Position.TopRight, type: ToastType.Error);
                mensaje = ViewBag.Message;
                
            }
            return json;
        }
        //da de baja el objetivo
        [HttpPost]
        public JsonResult Desactivar(int delO)
        {
            JsonResult json = null;
            Orepo = new ObjetivoRepository();
            try
            {
                var result = Orepo.Context.Objetivo.FirstOrDefault(x => x.Id == delO);
                result.Eliminado = true;
                Orepo.Update(result);
                json = Json(true);
                ViewBag.Message = Notification.Show("Objetivo Dado de Baja", "Aviso", position: Position.TopRight, type: ToastType.Success);
                mensaje = ViewBag.Message;
                
            }
            catch (Exception ex)
            {
                ViewBag.Message = Notification.Show(ex.Message, "Aviso", position: Position.TopRight, type: ToastType.Error);
                mensaje = ViewBag.Message;
                json = Json(mensaje);
            }

            return json;
        }
    }
}