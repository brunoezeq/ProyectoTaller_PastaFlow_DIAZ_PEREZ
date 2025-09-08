using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Venta
    {
        public int id_venta { get; set; }
        public DateTime fecha_venta { get; set; }
        public decimal total_venta { get; set; }
        public int numero_factura { get; set; }
        public Caja id_caja { get; set; }
        public Metodo_Pago id_metodo { get; set; }
    }
}
