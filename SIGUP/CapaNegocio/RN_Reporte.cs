﻿using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class RN_Reporte
    {
        private BD_Reporte objCapaDato = new BD_Reporte();

        public List<EN_Reporte> Prestamos(string fechaInicio, string fechaFin, string codigoUsuario, string estado, string herramienta)
        {
            return objCapaDato.Prestamos(fechaInicio, fechaFin, codigoUsuario, estado, herramienta);
        }

        public EN_Dashboard VerDashBoard() /*Usa una clase de la capa entidad*/
        {
            return objCapaDato.VerDashBoard();/*Retorna el metodo listar de la instancia de la capa Datos*/
        }

        public byte[] GenerarPDF(string fechaInicio, string fechaFin, string codigoUsuario, string estado, string herramienta)
        {
            return objCapaDato.GenerarPDF(fechaInicio, fechaFin, codigoUsuario, estado, herramienta);
        }
    }
}
