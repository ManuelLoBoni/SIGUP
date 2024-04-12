using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class RN_Edificio
    {
        private BD_Edificio objEdificio = new BD_Edificio();
        public List<EN_Edificio> ListarEdificios()
        {
            return objEdificio.ListarEdificios();
        }
        public int RegistrarEdificio(EN_Edificio edificio, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(edificio.nombreEdificio) || string.IsNullOrWhiteSpace(edificio.nombreEdificio))
            {
                Mensaje = "El nombre del Edificio no puede estar vacío";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objEdificio.añadir_edificio(edificio, out Mensaje);
            }
            else
            {
                return 0;//Es para saber que es un nuevo registro
            }
        }
        public bool EditarEdificio(EN_Edificio edificio, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(edificio.nombreEdificio) || string.IsNullOrWhiteSpace(edificio.nombreEdificio))
            {
                Mensaje = "El nombre del edificio no puede estar vacío";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objEdificio.modificar_edifcio(edificio, out Mensaje);
            }
            else
            {
                return false;
            }
        }
        public bool EliminarEdificio(int id, out string Mensaje)
        {
            return objEdificio.eliminar_edifcio(id, out Mensaje);
        }
    }
}
