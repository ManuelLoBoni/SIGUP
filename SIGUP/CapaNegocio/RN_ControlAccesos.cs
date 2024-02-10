using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class RN_ControlAccesos
    {
        private BD_ControlAccesos objCD = new BD_ControlAccesos();
        public List<EN_ControlAccesos> ListarAccesosCompleto()
        {
            return objCD.ListarAccesosCompleto();
        }
        public bool EliminarRegistroCU(int id, out string Mensaje)
        {
            return objCD.ElimnarRegistroCU(id, out Mensaje);
        }
        public int GuardarRCU(EN_ControlAccesos registroCU, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(registroCU.fecha) || string.IsNullOrWhiteSpace(registroCU.fecha))
            {
                Mensaje = "Debes seleccionar una fecha.";
            }
            else if (string.IsNullOrEmpty(registroCU.E_IdUsuario.idUsuario) || string.IsNullOrWhiteSpace(registroCU.E_IdUsuario.idUsuario))
            {
                Mensaje = "Debes seleccionar un usuario.";
            }
            else if (string.IsNullOrEmpty(registroCU.HoraEntrada) || string.IsNullOrWhiteSpace(registroCU.HoraEntrada))
            {
                Mensaje = "Debes seleccionar una hora de entrada.";
            }
            else if (registroCU.E_TipoActividad.IdActividad == 0)
            {
                Mensaje = "Debes seleccionar una actividad.";
            }
            else if (registroCU.CantidadAlumnos == 0)
            {
                Mensaje = "Debes añadir alumnos al registro.";
            }
            else if (registroCU.Semestre == 0)
            {
                Mensaje = "Debes añadir un semestre entre 1 y 12";
            }
            else if (registroCU.E_IdCarrera.idCarrera == 0)
            {
                Mensaje = "Debes seleccionar una carrera.";
            }
            else if (registroCU.E_IdArea.idArea == 0)
            {
                Mensaje = "Debe seleccionar un área.";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCD.GuardarRCU(registroCU, out Mensaje);
            }
            else
            {
                return 0;//Es para saber que es un nuevo registro
            }
        }
        public bool EditarRCU(EN_ControlAccesos registroCU, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(registroCU.fecha) || string.IsNullOrWhiteSpace(registroCU.fecha))
            {
                Mensaje = "Debes seleccionar una fecha.";
            }
            else if (string.IsNullOrEmpty(registroCU.E_IdUsuario.idUsuario) || string.IsNullOrWhiteSpace(registroCU.E_IdUsuario.idUsuario))
            {
                Mensaje = "Debes seleccionar un usuario.";
            }
            else if (string.IsNullOrEmpty(registroCU.HoraEntrada) || string.IsNullOrWhiteSpace(registroCU.HoraEntrada))
            {
                Mensaje = "Debes seleccionar una hora de entrada.";
            }
            else if (registroCU.E_TipoActividad.IdActividad == 0)
            {
                Mensaje = "Debes seleccionar una actividad.";
            }
            else if (registroCU.CantidadAlumnos == 0)
            {
                Mensaje = "Debes añadir alumnos al registro.";
            }
            else if (registroCU.Semestre == 0)
            {
                Mensaje = "Debes añadir un semestre entre 1 y 12";
            }
            else if (registroCU.E_IdCarrera.idCarrera == 0)
            {
                Mensaje = "Debes seleccionar una carrera.";
            }
            else if (registroCU.E_IdArea.idArea == 0)
            {
                Mensaje = "Debe seleccionar un área.";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCD.EditarRCU(registroCU, out Mensaje);
            }
            else
            {
                return false;
            }
        }
        public bool SalidaRCU(EN_ControlAccesos salidaCU, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(salidaCU.HoraSalida) || string.IsNullOrWhiteSpace(salidaCU.HoraSalida))
            {
                Mensaje = "Debes seleccionar una hora de salida.";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCD.SalidaRCU(salidaCU, out Mensaje);
            }
            else
            {
                return false;
            }
        }
    }
}
