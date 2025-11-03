using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Venta
    {
        public int Id_venta { get; set; }
        public DateTime Fecha_venta { get; set; }
        public decimal Total_venta { get; set; }
        public int Numero_factura { get; set; }
        public Caja Id_caja { get; set; }
        public Metodo_Pago Id_metodo { get; set; }
    }
}
