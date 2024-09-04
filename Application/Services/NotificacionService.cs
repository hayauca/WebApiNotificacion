using Application.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
   public class NotificacionService
    {

        private readonly INotificacionRepository _notificacionRepository;
        private readonly IMapper _mapper;

        public NotificacionService(INotificacionRepository notificacionRepository, IMapper mapper)
        {
            _notificacionRepository = notificacionRepository;
            _mapper = mapper;
        }

        public async Task EjecutarAsync(NotificacionesClienteConfig cliente, string link)
        {
            string htmlContent = GenerarPlantillaHTML(link);
            await _notificacionRepository.EnviarEmailAsync(cliente, htmlContent);
        }

        private string GenerarPlantillaHTML(string link)
        {
            return $@"
                <html>
                <body>
                    <p>Por favor, revisa los <a href='{link}'>Términos del Contrato</a>.</p>
                    <button onclick=""window.location.href='{link}/accept'"">Aceptar</button>
                </body>
                </html>";
        }


       // public async Task RegistrarAceptacionAsync(NotificacionesClienteDto customerDto)
        public async Task RegistrarAceptacionAsync(string cedula)
        {

            try
            {
               // var customer = _mapper.Map<NotificacionesCliente>(customerDto);
                await _notificacionRepository.RegistrarAceptacionAsync(cedula);
                //customerDto.Id = customer.Id; // Update DTO with new ID
            }
            catch (Exception ex)
            {

                throw;
            }


        }



    }
}
