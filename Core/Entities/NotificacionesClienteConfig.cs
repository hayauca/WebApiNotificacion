using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class NotificacionesClienteConfig
    {
        public int ConfiguracionID { get; set; }
        public string ClienteID { get; set; }
        public string Nombres { get; set; }
        public string TipoNotificacion { get; set; }
        public int Habilitado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Email { get; set; }

        public string PlantillaHTML { get; set; }

        
    }
}
