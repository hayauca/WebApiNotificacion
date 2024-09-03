using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CustomerDto
    {
        //public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }


        public string lc_emisor { get; set; }
        public string lc_solcre { get; set; }
        public string lc_tipocliente { get; set; }
        public string lc_perfilcli { get; set; }
        public string lc_score { get; set; }
        public string lc_diapago { get; set; }
    }
}
