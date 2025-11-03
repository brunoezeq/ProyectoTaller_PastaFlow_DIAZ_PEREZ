using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Caja
    {
        public int Id_caja { get; set; } 
        public DateTime Fecha_hora_apertura { get; set; }
        public decimal Monto_inicio { get; set; }
        public DateTime Fecha_hora_cierre { get; set; }
        public decimal Monto_cierre { get; set; }
        public decimal Monto_esperado { get; set; }
        public int Id_usuario { get; set; }
        public int Id_turno { get; set; }
    }
}
