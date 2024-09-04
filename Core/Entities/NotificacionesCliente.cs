using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class NotificacionesCliente
    {
        public int NotificacionID { get; set; }
        public string ClienteID { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string  PlantillaHTML { get; set; }
       
    }
}
