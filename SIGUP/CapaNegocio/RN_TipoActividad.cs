using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class RN_TipoActividad
    {
        BD_TipoActividad objActividad = new BD_TipoActividad();
        public List<EN_TipoActividad> ListarTipoActividad()
        {
            return objActividad.ListarTipoActividad();
        }
        public int RegistrarActividad(EN_TipoActividad actividad, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(actividad.NombreActividad) || string.IsNullOrWhiteSpace(actividad.NombreActividad))
            {
                Mensaje = "El nombre de la Actividad no puede estar vacío";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objActividad.añadir_actividad(actividad, out Mensaje);
            }
            else
            {
                return 0;//Es para saber que es un nuevo registro
            }
        }
        public bool EditarActividad(EN_TipoActividad actividad, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(actividad.NombreActividad) || string.IsNullOrWhiteSpace(actividad.NombreActividad))
            {
                Mensaje = "El nombre de la Actividad no puede estar vacío";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objActividad.modificar_actividad(actividad, out Mensaje);
            }
            else
            {
                return false;
            }
        }
        public bool EliminarActividad(int id, out string Mensaje)
        {
            return objActividad.eliminar_actividad(id, out Mensaje);
        }
    }
}
