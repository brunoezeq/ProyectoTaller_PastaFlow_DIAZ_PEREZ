using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Queja
    {
        public int Id { get; set; }
        public string nomnbre_cliente { get; set; }
        public string apellido_cliente { get; set; }
        public string motivo_queja { get; set; }
        public string descripcion_queja { get; set; }
        public DateTime fecha_hora_queja { get; set; }
        public Usuario id_usuario { get; set; } 
    }
}
