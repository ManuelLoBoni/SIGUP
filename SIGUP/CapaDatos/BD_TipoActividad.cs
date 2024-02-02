using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class BD_TipoActividad
    {
        public List<EN_TipoActividad> ListarTipoActividad()
        {
            List<EN_TipoActividad> actividad = new List<EN_TipoActividad>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(BD_Conexion.cn))
                {
                    string query = "SELECT * FROM TipoActividad";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlConnection.Open();
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                EN_TipoActividad act = new EN_TipoActividad
                                {
                                    IdActividad = Convert.ToInt32(sqlDataReader["IdActividad"]),
                                    NombreActividad = sqlDataReader["NombreActividad"].ToString()
                                };
                                actividad.Add(act);
                            }
                        }
                    }
                }
                return actividad;
            }
            catch
            {
                return null;
            }
        }
    }
}
