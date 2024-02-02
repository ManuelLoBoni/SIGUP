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
    public class BD_ControlAccesos
    {
        public List<EN_ControlAccesos> ListarAccesosCompleto()
        {
            List<EN_ControlAccesos> lista = new List<EN_ControlAccesos>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder(); //Permite hacer saltos de linea

                    sb.AppendLine("SELECT CU.IdRegistro, CONVERT(char(10), CU.fecha, 103)Fecha, Us.IdUsuario, CONCAT(Us.Nombre,' ',Us.Apellidos)Usuario, CU.HoraEntrada,");
                    sb.AppendLine("CU.HoraSalida, TA.IdActividad, TA.NombreActividad, CU.CantidadAlumnos AS Alumnos,");
                    sb.AppendLine("CU.Semestre,Ca.IdCarrera, Ca.NombreCarrera AS Carrera,a.IdArea,a.NombreArea as Area");
                    sb.AppendLine("from ControlUsuario CU");
                    sb.AppendLine("INNER JOIN TipoActividad TA ON TA.IdActividad = CU.TipoActividad");
                    sb.AppendLine("INNER JOIN usuario Us ON Us.IdUsuario = CU.IdUsuario");
                    sb.AppendLine("INNER JOIN Carreras Ca ON CU.IdCarrera = Ca.IdCarrera");
                    sb.AppendLine("INNER JOIN Areas a on a.IdArea = CU.Area order by CU.IdRegistro DESC");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    //cmd.Parameters.AddWithValue("@idLector", idLector);
                    cmd.CommandType = CommandType.Text;/*En este caso es de tipo Text (no usamos para este ejemplo, procedimientos almacenados*/

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())/*Lee todos los resultados que aparecen en la ejecucion del select anterior*/
                    {
                        while (dr.Read())/*Mientras reader esta leyendo, ira agregando a la lista dicha lectura*/
                        {
                            lista.Add(
                                new EN_ControlAccesos()
                                {
                                    IdRegistro = Convert.ToInt32(dr["IdRegistro"]),
                                    fecha = dr["Fecha"].ToString(),
                                    E_IdUsuario = new EN_Usuario() { idUsuario = dr["IdUsuario"].ToString(), NombreCompletoUsuario = dr["Usuario"].ToString() },
                                    HoraEntrada = dr["HoraEntrada"].ToString(),
                                    HoraSalida = dr["HoraSalida"].ToString(),
                                    E_TipoActividad = new EN_TipoActividad() { IdActividad = Convert.ToInt32(dr["IdActividad"]), NombreActividad = dr["NombreActividad"].ToString() },
                                    CantidadAlumnos = Convert.ToInt32(dr["Alumnos"]),
                                    Semestre = Convert.ToInt32(dr["Semestre"]),
                                    E_IdCarrera = new EN_Carrera() { idCarrera = Convert.ToInt32(dr["IdCarrera"]), nombreCarrera = dr["Carrera"].ToString() },
                                    E_IdArea = new EN_Area() { idArea = Convert.ToInt32(dr["IdArea"]), nombreArea = dr["Area"].ToString() }
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EN_ControlAccesos>();
            }

            return lista;
        }
        public int GuardarRCU(EN_ControlAccesos registroCU, out string Mensaje)
        {
            int IdAutogenerado = 0; /*Recibe el id autogenerado*/

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCU", oConexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", registroCU.E_IdUsuario.idUsuario);
                    cmd.Parameters.AddWithValue("@fecha", registroCU.fecha);
                    cmd.Parameters.AddWithValue("@HoraEntrada", registroCU.HoraEntrada);
                    cmd.Parameters.AddWithValue("@TipoActividad", registroCU.E_TipoActividad.IdActividad);
                    cmd.Parameters.AddWithValue("@CantidadAlumnos", registroCU.CantidadAlumnos);
                    cmd.Parameters.AddWithValue("@Semestre", registroCU.Semestre);
                    cmd.Parameters.AddWithValue("@IdCarrera", registroCU.E_IdCarrera.idCarrera);
                    cmd.Parameters.AddWithValue("@Area", registroCU.E_IdArea.idArea);
                    //Dos parametros de salida, un entero de resultaado y un string de mensaje
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    IdAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                IdAutogenerado = 0;
                Mensaje = ex.Message;
            }
            return IdAutogenerado;
        }
        public bool ElimnarRegistroCU(int id, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarCU", oConexion);
                    cmd.Parameters.AddWithValue("@IdRegistro", id);
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
        public bool EditarRCU(EN_ControlAccesos registroCU, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarCU", oConexion);
                    cmd.Parameters.AddWithValue("@IdRegistro", registroCU.IdRegistro);
                    cmd.Parameters.AddWithValue("@IdUsuario", registroCU.E_IdUsuario.idUsuario);
                    cmd.Parameters.AddWithValue("@fecha", registroCU.fecha);
                    cmd.Parameters.AddWithValue("@HoraEntrada", registroCU.HoraEntrada);
                    cmd.Parameters.AddWithValue("@HoraSalida", registroCU.HoraSalida);
                    cmd.Parameters.AddWithValue("@TipoActividad", registroCU.E_TipoActividad.IdActividad);
                    cmd.Parameters.AddWithValue("@CantidadAlumnos", registroCU.CantidadAlumnos);
                    cmd.Parameters.AddWithValue("@Semestre", registroCU.Semestre);
                    cmd.Parameters.AddWithValue("@IdCarrera", registroCU.E_IdCarrera.idCarrera);
                    cmd.Parameters.AddWithValue("@Area", registroCU.E_IdArea.idArea);
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
