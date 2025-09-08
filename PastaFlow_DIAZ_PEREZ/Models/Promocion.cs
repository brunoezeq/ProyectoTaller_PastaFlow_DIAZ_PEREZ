using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Promocion
    {
        public int id_promocion { get; set; }
        public Producto id_prodcuto { get; set; }   
        public decimal precio_promocion { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }

    }
}
