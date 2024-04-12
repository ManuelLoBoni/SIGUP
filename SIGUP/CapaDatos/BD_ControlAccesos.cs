using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using System.IO;

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
        public List<EN_ControlAccesos> ListarAccesosParaBitacora()
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
                    sb.AppendLine("INNER JOIN Areas a on a.IdArea = CU.Area where TA.IdActividad = 1 order by CU.IdRegistro DESC");

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
                    if (registroCU.HoraSalida == null)
                    {
                        cmd.Parameters.AddWithValue("@HoraSalida", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@HoraSalida", registroCU.HoraSalida);
                    }
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
        public bool SalidaRCU(EN_ControlAccesos registroCU, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_SalidaCU", oConexion);
                    cmd.Parameters.AddWithValue("@IdRegistro", registroCU.IdRegistro);
                    cmd.Parameters.AddWithValue("@HoraSalida", registroCU.HoraSalida);
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

        public byte[] GenerarPDF()
        {
            List<EN_ControlAccesos> oLista = new List<EN_ControlAccesos>();

            //oLista = new RN_Administrador().Listar();
            oLista = new BD_ControlAccesos().ListarAccesosCompleto();

            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    var colors = "#1B396A";
                    // page content
                    page.Margin(30);
                    // page.Header().Height(100).Background(Colors.Blue.Medium);
                    page.Header().ShowOnce().Row(row =>
                    {//el ShowOnce sirve para que el header solo aparezca en la primera hoja

                        var basePath = AppDomain.CurrentDomain.BaseDirectory; //Obtenemos la ruta del servidor
                        var rutaImagen = Path.Combine(basePath, "Images", "logotec.png"); //Le damos la ruta del servidor, la carpeta y la imagen.
                        
                        byte[] imageData = System.IO.File.ReadAllBytes(rutaImagen);

                        row.ConstantItem(150).Image(imageData);

                        //row.ConstantItem(140).Height(60).Placeholder();//Elegimos el ancho del item

                        row.RelativeItem().Column(col =>//El ancho se coloca relativamente automatica
                        {
                            col.Item().AlignCenter().Text("Unidad de Prácticas").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("ITSSNP - SIGUP").Bold().FontSize(9);
                            col.Item().AlignCenter().Text("Sistema de Gestión y Control").Bold().FontSize(9);
                            col.Item().AlignCenter().Text("de Préstamos y Laboratorios").Bold().FontSize(9);
                            //col.Item().Background(Colors.Orange.Medium).Height(10);
                            //col.Item().Background(Colors.Green.Medium).Height(10);
                        });
                        row.RelativeItem().Column(col =>
                        {
                            //verde institucional 91B22C
                            //azul institucional 1B396A

                            col.Item().Border(1).BorderColor(colors). //Se le pasa la variable colors para cambiar mas facil el color.
                           AlignCenter().Text("SIGUP").Bold();

                            col.Item().Background(colors).Border(1)
                            .BorderColor(colors).AlignCenter()
                            .Text("Control de Accesos").FontColor("#fff");

                            col.Item().Border(1).BorderColor(colors).
                            AlignCenter().Text(DateTime.Now.ToString("dd-MM-yyyy"));

                        });

                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        int totalRegistros = 0;
                        col1.Item().LineHorizontal(0.5f);
                        col1.Item().Table(tabla =>
                        {//Seccion de la tabla
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(65);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.ConstantColumn(80);
                                columns.ConstantColumn(80);
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background(colors)//Se le pasa la variable colors para cambiar mas facil el color.
                               .Padding(2).Text("Fecha").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Usuario").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Hora de Entrada").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Hora de Salida").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Tipo de Actividad").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Alumnos").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Semestre").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Carrera").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Área").FontColor("#fff").FontSize(10);
                            });

                            foreach (EN_ControlAccesos cat in oLista)
                            {
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.fecha).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.E_IdUsuario.NombreCompletoUsuario).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.HoraEntrada).FontSize(10);
                                if (cat.HoraSalida == "")
                                {
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).Text("-------").FontSize(10);
                                }
                                else
                                {
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).Text(cat.HoraSalida).FontSize(10);
                                }
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.E_TipoActividad.NombreActividad).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.CantidadAlumnos.ToString()).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.Semestre.ToString()).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.E_IdCarrera.nombreCarrera).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.E_IdArea.nombreArea).FontSize(10);
                                totalRegistros++;
                            }
                        });

                        col1.Item().AlignRight().Text($"Total de Accesos: {totalRegistros}").FontSize(12);

                        col1.Spacing(10);
                    });

                    page.Footer()
                    .AlignRight()
                    .Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);

                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();

            MemoryStream stream = new MemoryStream(data);
            return stream.ToArray();
            //return File(stream, "applicacion/pdf", "detallePrestamo.pdf");
            //return View();
        }
    }
}
