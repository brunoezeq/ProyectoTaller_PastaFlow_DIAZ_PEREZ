using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Reserva
    {
        public int id_reserva { get; set; }
        public string nomnbre_cliente { get; set; }
        public string apellido_cliente { get; set; }
        public DateTime fecha_hora_reserva { get; set; }
        public int cantidad_personas { get; set; }
        public string estado { get; set; } 
        public Usuario id_usuario { get; set; }
    }
}
