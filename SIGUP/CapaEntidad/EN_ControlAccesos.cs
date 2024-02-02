using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EN_ControlAccesos
    {
        public int IdRegistro { get; set; }
        public string fecha { get; set; }
        public EN_Usuario E_IdUsuario { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }
        public EN_TipoActividad E_TipoActividad { get; set; }
        public int CantidadAlumnos { get; set; }
        public int Semestre { get; set; }
        public EN_Carrera E_IdCarrera { get; set; }
        public EN_Area E_IdArea { get; set; }
    }
}
