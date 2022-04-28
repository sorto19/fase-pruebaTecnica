using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usuarionet.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;

namespace Usuarionet.Controllers

{

    [EnableCors("ReglaCors")]

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly string cadenaSQL;

        public UsuarioController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");

        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_listar",conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while ( rd.Read())
                        {
                            lista.Add(new Usuario() { 
                            
                            IdUsuario = Convert.ToInt32(rd["IdUsuario"]),
                                Nombre = rd["Nombre"].ToString(),
                            Apellido=rd["Apellido"].ToString(),
                            Email=rd["Email"].ToString(),
                            Contraseña=rd["Contraseña"].ToString(),
                            Telefono=rd["Telefono"].ToString(),
                            Departamento=rd["Departamento"].ToString(),
                            Municipio=rd["Municipio"].ToString()
                            
                            });
                        }
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            } catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }

        }


        [HttpGet]
        [Route("Obtener/{IdUsuario:int}")]
        public IActionResult Obtener(int IdUsuario)
        {
            List<Usuario> lista = new List<Usuario>();

            Usuario usuario = new Usuario();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_obtener", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Usuario()
                            {

                                IdUsuario = Convert.ToInt32(rd["IdUsuario"]),
                                Nombre = rd["Nombre"].ToString(),
                                Apellido = rd["Apellido"].ToString(),
                                Email = rd["Email"].ToString(),
                                Contraseña = rd["Contraseña"].ToString(),
                                Telefono = rd["Telefono"].ToString(),
                                Departamento = rd["Departamento"].ToString(),
                                Municipio = rd["Municipio"].ToString()

                            });
                        }
                    }


                }

                usuario= lista.Where(item => IdUsuario == IdUsuario).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = usuario });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = usuario });
            }

        }
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Usuario objeto)
        {
          

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar", conexion);
                    cmd.Parameters.AddWithValue("Nombre",objeto.Nombre);
                    cmd.Parameters.AddWithValue("Apellido",objeto.Apellido);
                    cmd.Parameters.AddWithValue("Email", objeto.Email);
                    cmd.Parameters.AddWithValue("Contraseña", objeto.Contraseña);
                    cmd.Parameters.AddWithValue("Telefono", objeto.Telefono);   
                    cmd.Parameters.AddWithValue("Departamento", objeto.Departamento);
                    cmd.Parameters.AddWithValue("Municipio", objeto.Municipio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

               

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }

        }
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Usuario objeto)
        {


            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar", conexion);
                    cmd.Parameters.AddWithValue("IdUsuario", objeto.IdUsuario == 0 ? DBNull.Value : objeto.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre is null ? DBNull.Value: objeto.Nombre); 
                    cmd.Parameters.AddWithValue("Apellido", objeto.Apellido is null ? DBNull.Value : objeto.Apellido);
                    cmd.Parameters.AddWithValue("Email", objeto.Email is null ? DBNull.Value : objeto.Email);
                    cmd.Parameters.AddWithValue("Contraseña", objeto.Contraseña is null ? DBNull.Value : objeto.Contraseña);
                    cmd.Parameters.AddWithValue("Telefono", objeto.Telefono is null ? DBNull.Value : objeto.Telefono);
                    cmd.Parameters.AddWithValue("Departamento", objeto.Departamento is null ? DBNull.Value : objeto.Departamento);
                    cmd.Parameters.AddWithValue("Municipio", objeto.Municipio is null ? DBNull.Value : objeto.Municipio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }



                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }

        }
        [HttpPut]
        [Route("Eliminar/{IdUsuario:int}")]
        public IActionResult Eliminar(int IdUsuario)
        {


            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar",conexion );
                    cmd.Parameters.AddWithValue("IdUsuario", IdUsuario);
                   
                   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }



                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado" });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }

        }


    }


}

