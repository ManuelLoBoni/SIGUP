using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class RN_Carrera
    {
        private BD_Carrera objBDC = new BD_Carrera();
        public List<EN_Carrera> ListarCarreras()
        {
            return objBDC.ListarCarreras();
        }
        public bool Eliminar(int id, out string Mensaje)
        {
            return objBDC.eliminar_carrera(id, out Mensaje);
        }
        public int RegistrarCarrera(EN_Carrera carrera, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(carrera.nombreCarrera) || string.IsNullOrWhiteSpace(carrera.nombreCarrera))
            {
                Mensaje = "El nombre de la carrera no puede estar vacío";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objBDC.añadir_carrera(carrera, out Mensaje);
            }
            else
            {
                return 0;//Es para saber que es un nuevo registro
            }
        }
        public bool EditarCarrera(EN_Carrera carrera, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(carrera.nombreCarrera) || string.IsNullOrWhiteSpace(carrera.nombreCarrera))
            {
                Mensaje = "El nombre de la carrera no puede estar vacío";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objBDC.modificar_carrera(carrera, out Mensaje);
            }
            else
            {
                return false;
            }
        }
    }
}
