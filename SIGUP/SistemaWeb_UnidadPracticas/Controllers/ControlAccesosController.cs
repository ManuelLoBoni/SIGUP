﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;

namespace SistemaWeb_UnidadPracticas.Controllers
{
    public class ControlAccesosController : Controller
    {
        // GET: ControlAccesos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Accesos()
        {
            return View();
        }
        public ActionResult Carreras()
        {
            return View();
        }

        /*--------------Control de Accesos--------------------*/
        #region Control de Accesos
        [HttpGet]
        public JsonResult ListarAccesosCompleto()
        {
            List<EN_ControlAccesos> oLista = new List<EN_ControlAccesos>();
            oLista = new RN_ControlAccesos().ListarAccesosCompleto();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarRCU(EN_ControlAccesos accesos)
        {
            object resultado;
            string mensaje = string.Empty;

            if (accesos.IdRegistro == 0)
            {
                resultado = new RN_ControlAccesos().GuardarRCU(accesos, out mensaje);
            }
            else
            {
                resultado = new RN_ControlAccesos().EditarRCU(accesos,out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SalidaCU(EN_ControlAccesos salida)
        {
            object resultado;
            string mensaje = string.Empty;

            resultado = new RN_ControlAccesos().SalidaRCU(salida, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarrCU(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new RN_ControlAccesos().EliminarRegistroCU(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Carreras
        [HttpGet]
        public JsonResult ListarCarrerasCU()
        {
            List<EN_Carrera> oLista = new List<EN_Carrera>();
            oLista = new RN_Carrera().ListarCarreras();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarCarreraCU(EN_Carrera carrera)
        {
            object resultado;
            string mensaje = string.Empty;

            if (carrera.idCarrera == 0)
            {
                resultado = new RN_Carrera().RegistrarCarrera(carrera, out mensaje);
            }
            else
            {
                resultado = new RN_Carrera().EditarCarrera(carrera, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult EliminarCarreraCU(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new RN_Carrera().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Edificio
        [HttpGet]
        public JsonResult ListarEdificiosCU()
        {
            List<EN_Edificio> oLista = new List<EN_Edificio>();
            oLista = new RN_Edificio().ListarEdificios();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarEdificiosCU(EN_Edificio edificio)
        {
            object resultado;
            string mensaje = string.Empty;

            if (edificio.idEdificio == 0)
            {
                resultado = new RN_Edificio().RegistrarEdificio(edificio, out mensaje);
            }
            else
            {
                resultado = new RN_Edificio().EditarEdificio(edificio, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult EliminarEdificiosCU(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new RN_Edificio().EliminarEdificio(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Areas

        [HttpGet] /*Una URL que devuelve datos, un httpost se le pasan los valores y despues devuelve los datos  */
        public JsonResult ListarAreaParaCU() /*D este json se puede controlar que mas ver, igualar elementos, etc*/
        {
            List<EN_Area> oLista = new List<EN_Area>();
            oLista = new RN_Area().ListarAreaParaPrestamo();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            /*El json da los datos, jala los datos de esa lista, en data*/
        }
        [HttpGet] /*Una URL que devuelve datos, un httpost se le pasan los valores y despues devuelve los datos  */
        public JsonResult ListarAreaCCU() /*D este json se puede controlar que mas ver, igualar elementos, etc*/
        {
            List<EN_Area> oLista = new List<EN_Area>();
            oLista = new RN_Area().ListarAreaCompleta();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            /*El json da los datos, jala los datos de esa lista, en data*/
        }
        [HttpPost]
        public JsonResult GuardarAreasCU(EN_Area area)
        {
            object resultado;
            string mensaje = string.Empty;

            if (area.idArea == 0)
            {
                resultado = new RN_Area().RegistrarArea(area, out mensaje);
            }
            else
            {
                resultado = new RN_Area().EditarArea(area, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult EliminarAreasCU(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new RN_Area().EliminarArea(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Actividades
        [HttpGet]
        public JsonResult ListarActividadCU()
        {
            List<EN_TipoActividad> oLista = new List<EN_TipoActividad>();
            oLista = new RN_TipoActividad().ListarTipoActividad();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarActividadesCU(EN_TipoActividad actividad)
        {
            object resultado;
            string mensaje = string.Empty;

            if (actividad.IdActividad == 0)
            {
                resultado = new RN_TipoActividad().RegistrarActividad(actividad, out mensaje);
            }
            else
            {
                resultado = new RN_TipoActividad().EditarActividad(actividad, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult EliminarActividadesCU(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new RN_TipoActividad().EliminarActividad(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Metodos Listar
        public JsonResult ListarUsuarioCU()
        {
            List<EN_Usuario> oLista = new List<EN_Usuario>();
            oLista = new RN_Usuarios().ListarUsuarioParaCU();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}