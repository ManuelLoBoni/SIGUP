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
        BD_TipoActividad actividad = new BD_TipoActividad();
        public List<EN_TipoActividad> ListarTipoActividad()
        {
            return actividad.ListarTipoActividad();
        }
    }
}
