using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface INotificacionRepository
    {
        Task EnviarEmailAsync(NotificacionesClienteConfig cliente, string htmlContent);

        Task RegistrarAceptacionAsync(string cedula);
    }
}
