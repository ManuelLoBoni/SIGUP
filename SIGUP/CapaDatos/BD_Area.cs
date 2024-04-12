using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using QuestPDF.Fluent;//Para exportar a pdf
//using QuestPDF.Helpers;
using System.IO;

namespace CapaDatos
{
    public class BD_Area
    {
        public List<EN_Area> ListarAreaParaPrestamo()
        {
            List<EN_Area> lista = new List<EN_Area>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    //string query = "select IDArea, CONCAT(Nombre,' ',Apellidos) [NombreArea],  Activo from Area where Activo  = 1";
                    string query = "select IdArea, NombreArea from Areas";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;/*En este caso es de tipo Text (no usamos para este ejemplo, procedimientos almacenados*/

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())/*Lee todos los resultados que aparecen en la ejecucion del select anter ior*/
                    {
                        while (dr.Read())/*Mientras reader esta leyendo, ira agregando a la lista dicha lectura*/
                        {
                            lista.Add(/*Agrega un nuevo Lector a la lista*/
                                new EN_Area()
                                {
                                    idArea = Convert.ToInt32(dr["IdArea"]),
                                    nombreArea = dr["NombreArea"].ToString()
                                    //activo = Convert.ToBoolean(dr["Activo"])
                                }
                                );
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<EN_Area>();
            }

            return lista;
        }
        public List<EN_Area> ListarAreaCompleta()
        {
            List<EN_Area> lista = new List<EN_Area>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    //string query = "select IDArea, CONCAT(Nombre,' ',Apellidos) [NombreArea],  Activo from Area where Activo  = 1";
                    string query = "SELECT IdArea, NombreArea, A.IdEdificio AS IdEd, E.NombreEdificio AS NEdi FROM Areas A INNER JOIN Edificio E ON E.IdEdificio = A.IdEdificio";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;/*En este caso es de tipo Text (no usamos para este ejemplo, procedimientos almacenados*/

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())/*Lee todos los resultados que aparecen en la ejecucion del select anter ior*/
                    {
                        while (dr.Read())/*Mientras reader esta leyendo, ira agregando a la lista dicha lectura*/
                        {
                            lista.Add(/*Agrega un nuevo Lector a la lista*/
                                new EN_Area()
                                {
                                    idArea = Convert.ToInt32(dr["IdArea"]),
                                    nombreArea = dr["NombreArea"].ToString(),
                                    e_edificio = new EN_Edificio() { idEdificio = Convert.ToInt32(dr["IdEd"]), nombreEdificio = dr["NEdi"].ToString() }
                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<EN_Area>();
            }

            return lista;
        }
        public int añadir_area(EN_Area area, out string Mensaje)
        {
            int IdAutogenerado = 0; /*Recibe el id autogenerado*/

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarAreas", oConexion);
                    cmd.Parameters.AddWithValue("@IdArea", area.idArea);
                    cmd.Parameters.AddWithValue("@NombreArea", area.nombreArea);
                    cmd.Parameters.AddWithValue("@IdEdificio", area.e_edificio.idEdificio);
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

        public bool modificar_area(EN_Area area, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarAreas", oConexion);
                    cmd.Parameters.AddWithValue("@IdArea", area.idArea);
                    cmd.Parameters.AddWithValue("@NombreArea", area.nombreArea);
                    cmd.Parameters.AddWithValue("@IdEdificio", area.e_edificio.idEdificio);
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

        public bool eliminar_area(int id, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarAreas", oConexion);
                    cmd.Parameters.AddWithValue("@IdArea", id);
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
