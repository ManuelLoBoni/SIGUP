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
        public int añadir_actividad(EN_TipoActividad actividad, out string Mensaje)
        {
            int IdAutogenerado = 0; /*Recibe el id autogenerado*/

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarActividad", oConexion);
                    cmd.Parameters.AddWithValue("@NombreActividad", actividad.NombreActividad);
                    //Dos parametros de salida, un entero de resultaado y un string de mensaje
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    IdAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IdAutogenerado = 0;
                Mensaje = ex.Message;
            }
            return IdAutogenerado;
        }

        public bool modificar_actividad(EN_TipoActividad actividad, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarActividad", oConexion);
                    cmd.Parameters.AddWithValue("@IdActividad", actividad.IdActividad);
                    cmd.Parameters.AddWithValue("@NombreActividad", actividad.NombreActividad);
                    //Dos parametros de salida, un entero de resultaado y un string de mensaje
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool eliminar_actividad(int id, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarActividad", oConexion);
                    cmd.Parameters.AddWithValue("@IdActividad", id);
                    //Dos parametros de salida, un entero de resultaado y un string de mensaje
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }
            return resultado;
        }
    }
}
