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
    public class BD_Bitacora
    {
        public List<EN_Bitacora> ListarBitacora()
        {
            List<EN_Bitacora> lista = new List<EN_Bitacora>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select IdPractica, CONVERT(char(10), CU.fecha, 103)Fecha,CU.IdUsuario, CONCAT(US.Nombre,' ',US.Apellidos)Usuario,Bitacora.NombreActividad, CU.CantidadAlumnos, CA.NombreCarrera,Semestre,CU.IdRegistro, Observaciones from Bitacora");
                    sb.AppendLine("inner join ControlUsuario CU on Bitacora.IdRegistro = CU.IdRegistro");
                    sb.AppendLine("inner join Carreras CA ON CA.IdCarrera = CU.IdCarrera");
                    sb.AppendLine("inner join usuario US ON CU.IdUsuario = US.IdUsuario");
                    sb.AppendLine("inner join TipoActividad TA ON TA.IdActividad = 1 order by Bitacora.IdPractica DESC");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    //cmd.Parameters.AddWithValue("@idLector", idLector);
                    cmd.CommandType = CommandType.Text;/*En este caso es de tipo Text (no usamos para este ejemplo, procedimientos almacenados*/

                    oConexion.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            lista.Add(new EN_Bitacora()
                            {
                                IdPractica = Convert.ToInt32(rdr["IdPractica"]),
                                Fecha = rdr["Fecha"].ToString(),
                                E_IdUsuario = new EN_Usuario() { idUsuario = rdr["IdUsuario"].ToString(), NombreCompletoUsuario = rdr["Usuario"].ToString() },
                                NombreActividad = rdr["NombreActividad"].ToString(),
                                CantidadAlumnos = Convert.ToInt32(rdr["CantidadAlumnos"]),
                                Carrera = rdr["NombreCarrera"].ToString(),
                                Semestre = Convert.ToInt32(rdr["Semestre"]),
                                Observaciones = rdr["Observaciones"].ToString(),
                                E_ControlAccesos = new EN_ControlAccesos() { IdRegistro = Convert.ToInt32(rdr["IdRegistro"]) }
                            }); ;
                        }
                    }
                }
            }

            catch (Exception e)
            {
                lista = new List<EN_Bitacora>();
                Console.WriteLine(e.Message);
            }
            return lista;
        }

        public int GuardarBit(EN_Bitacora registroBit, out string Mensaje)
        {
            int IdAutogenerado = 0; //Recibe el id autogenerado

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarBitacora", oConexion);
                    cmd.Parameters.AddWithValue("@NombreActividad", registroBit.NombreActividad);
                    cmd.Parameters.AddWithValue("@IdRegistro", registroBit.E_ControlAccesos.IdRegistro);
                    cmd.Parameters.AddWithValue("@Observaciones", registroBit.Observaciones);


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
        public bool EditarBit(EN_Bitacora registroBit, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarBitacora", oConexion);
                    cmd.Parameters.AddWithValue("@IdPractica", registroBit.IdPractica);
                    cmd.Parameters.AddWithValue("@NombreActividad", registroBit.NombreActividad);
                    cmd.Parameters.AddWithValue("@IdRegistro", registroBit.E_ControlAccesos.IdRegistro);
                    if (registroBit.Observaciones == null)
                    {
                        cmd.Parameters.AddWithValue("@Observaciones", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Observaciones", registroBit.Observaciones);
                    }

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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

        public bool EliminarRegBit(int id, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(BD_Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarBitacora", oConexion);
                    cmd.Parameters.AddWithValue("@IdPractica", id);
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
            List<EN_Bitacora> oLista = new List<EN_Bitacora>();

            //oLista = new RN_Administrador().Listar();
            oLista = new BD_Bitacora().ListarBitacora();

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
                            .Text("Bitácora").FontColor("#fff");

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
                                columns.ConstantColumn(50);
                                columns.RelativeColumn();
                                columns.ConstantColumn(50);
                                columns.RelativeColumn();
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background(colors)//Se le pasa la variable colors para cambiar mas facil el color.
                               .Padding(2).Text("Fecha").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Usuario").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Nómina").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Actividad").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Alumnos").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Carrera").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Semestre").FontColor("#fff").FontSize(10);
                                header.Cell().Background(colors)
                               .Padding(2).Text("Observaciones").FontColor("#fff").FontSize(10);
                            });

                            foreach (EN_Bitacora cat in oLista)
                            {
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.Fecha).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.E_IdUsuario.NombreCompletoUsuario).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.E_IdUsuario.idUsuario).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.NombreActividad.ToString()).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.CantidadAlumnos.ToString()).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.Carrera.ToString()).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.Semestre.ToString()).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(cat.Observaciones.ToString()).FontSize(10);
                                totalRegistros++;
                            }
                        });

                        col1.Item().AlignRight().Text($"Total de registros en bitácora: {totalRegistros}").FontSize(12);

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
