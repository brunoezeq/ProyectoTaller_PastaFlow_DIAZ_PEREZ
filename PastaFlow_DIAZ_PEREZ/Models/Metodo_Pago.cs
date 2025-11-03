using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Metodo_Pago
    {
        public int Id_metodo { get; set; }
        public string Nombre { get; set; }
        public decimal Recargo { get; set; }
    }
}
