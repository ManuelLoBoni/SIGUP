using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public  class RN_Area
    {
        private BD_Area objArea = new BD_Area(); /*Instancia una clase de la capa datos */
        public List<EN_Area> ListarAreaParaPrestamo()
        {
            return objArea.ListarAreaParaPrestamo();
        }
        public List<EN_Area> ListarAreaCompleta()
        {
            return objArea.ListarAreaCompleta();
        }

        public int RegistrarArea(EN_Area area, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (area.idArea == 0)
            {
                Mensaje = "El id del Área no puede estar vacío";
            }
            else if(string.IsNullOrEmpty(area.nombreArea) || string.IsNullOrWhiteSpace(area.nombreArea))
            {
                Mensaje = "El nombre del Área no puede estar vacío";
            }
            else if (area.e_edificio.idEdificio == 0)
            {
                Mensaje = "Debes seleccionar una edificio.";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objArea.añadir_area(area, out Mensaje);
            }
            else
            {
                return 0;//Es para saber que es un nuevo registro
            }
        }
        public bool EditarArea(EN_Area area, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (area.idArea == 0)
            {
                Mensaje = "El id del Área no puede estar vacío";
            }
            else if (string.IsNullOrEmpty(area.nombreArea) || string.IsNullOrWhiteSpace(area.nombreArea))
            {
                Mensaje = "El nombre del Área no puede estar vacío";
            }
            else if (area.e_edificio.idEdificio == 0)
            {
                Mensaje = "Debes seleccionar una edificio.";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objArea.modificar_area(area, out Mensaje);
            }
            else
            {
                return false;
            }
        }
        public bool EliminarArea(int id, out string Mensaje)
        {
            return objArea.eliminar_area(id, out Mensaje);
        }
    }
}
