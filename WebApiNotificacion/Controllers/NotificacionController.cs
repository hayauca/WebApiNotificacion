using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiNotificacion.Controllers
{
    [Route("api/notificacion")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly NotificacionService _notificacionService;

        public NotificacionController(NotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarCorreo([FromBody] NotificacionesClienteConfig cliente, [FromQuery] string link)
        {
            await _notificacionService.EjecutarAsync(cliente, link);
            return Ok("Correo enviado exitosamente.");
        }

        [HttpPost("aceptar")]
        public async Task<IActionResult> AceptarTerminos(string cedula)
        {
            await _notificacionService.RegistrarAceptacionAsync(cedula);
            return Ok("Aceptación registrada exitosamente.");
        }
    }
}
