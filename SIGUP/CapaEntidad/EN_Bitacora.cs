using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EN_Bitacora
    {
        public int IdPractica { get; set; }
        public string Fecha { get; set; }
        public EN_Usuario E_IdUsuario { get; set; }
        public string NombreActividad { get; set; }
        public int CantidadAlumnos { get; set; }
        public string Carrera { get; set; }
        public int Semestre { get; set; }
        public string Observaciones { get; set; }
        public EN_ControlAccesos E_ControlAccesos { get; set; }
    }
}
