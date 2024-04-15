using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class RN_BitacoraAula
    {
        private BD_Bitacora objCD = new BD_Bitacora();
        private BD_ControlAccesos ControlAccesos = new BD_ControlAccesos();
        public List<EN_Bitacora> RN_ListarBitacora()
        {
            return objCD.ListarBitacora();
        }
        public List<EN_ControlAccesos> RN_ListarAccesosBitacora()
        {
            return ControlAccesos.ListarAccesosParaBitacora();
        }
        public List<EN_ControlAccesos> RN_ListarEditar()
        {
            return ControlAccesos.ListarEditarParaBitacora();
        }

        public bool EliminarRegBit(int id, out string mensaje)
        {
            return objCD.EliminarRegBit(id, out mensaje);
        }
        public int GuardarBit(EN_Bitacora registroBit, out string mensaje)
        {
            mensaje = string.Empty;
            if (string.IsNullOrEmpty(registroBit.NombreActividad) || string.IsNullOrWhiteSpace(registroBit.NombreActividad))
            {
                mensaje = "Coloca el nombre de la actividad.";
            }
            else if (registroBit.E_ControlAccesos.IdRegistro == 0)
            {
                mensaje = "Selecciona un acceso.";
            }
            if (string.IsNullOrEmpty(mensaje))
            {
                return objCD.GuardarBit(registroBit, out mensaje);
            }
            else
            {
                return 0;
            }
        }
        public bool EditarBit(EN_Bitacora registroBit, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(registroBit.NombreActividad) || string.IsNullOrWhiteSpace(registroBit.NombreActividad))
            {
                mensaje = "Coloca el nombre de la actividad.";
            }
            else if (registroBit.E_ControlAccesos.IdRegistro == 0)
            {
                mensaje = "Selecciona un acceso.";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCD.EditarBit(registroBit, out mensaje);
            }
            else
            {
                return false;
            }
        }
        public byte[] GenerarPDF()
        {
            return objCD.GenerarPDF();
        }
    }
}
