using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Caja
    {
        public int Id { get; set; } 
        public DateTime fecha_hora_apertura { get; set; }
        public decimal monto_inicio { get; set; }
        public DateTime fecha_hora_cierre { get; set; }
        public decimal monto_cierre { get; set; }
        public decimal monto_esperado { get; set; }
        public Usuario id_usuario { get; set; }
        public Turno id_turno { get; set; }
    }
}
