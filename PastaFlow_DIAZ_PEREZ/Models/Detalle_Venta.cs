using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Detalle_Venta
    {
        public int Id_detalle { get; set; }
        public Venta Id_venta { get; set; }
        public Producto Id_producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_unitario { get; set; }
        public decimal Subtotal { get; set; }

    }
}
