using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Metodo_Pago
    {
        public int id_metodo { get; set; }
        public string nombre { get; set; }
        public decimal recargo { get; set; }
    }
}
