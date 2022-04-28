using WebUsuarionet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text;


using Newtonsoft.Json;

using WebUsuarionet.Servicios;

namespace WebUsuarionet.Controllers
{
    public class HomeController : Controller
    {

        private IServicio_Api _servicioApi;

        public HomeController(IServicio_Api servicio_Api)
        {
            _servicioApi = servicio_Api;
        }

        public async Task<IActionResult> Index()
        {
            List<Usuario> lista = await _servicioApi.lista();
            return View(lista);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> Usuario(int IdUsuario)
        {

            Usuario modelo_usuario =new Usuario();

            ViewBag.Accion = "Nuevo Usuario";

            if (IdUsuario != 0) { 

                ViewBag.Accion = "Editar Usuario";
                modelo_usuario = await _servicioApi.Obtener(IdUsuario);
            }

            return View(modelo_usuario);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Usuario ob_usuario)
        {

            bool respuesta;

            if (ob_usuario.IdUsuario == 0)
            {
                respuesta = await _servicioApi.Guardar(ob_usuario);
            }
            else
            {
                respuesta = await _servicioApi.Editar(ob_usuario);
            }


            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdUsuario)
        {

            var respuesta = await _servicioApi.Eliminar(IdUsuario);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
