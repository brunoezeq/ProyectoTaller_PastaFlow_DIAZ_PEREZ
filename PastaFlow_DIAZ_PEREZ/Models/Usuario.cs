using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; } 
        public int dni { get; set; }
        public string nombre { get; set; }  
        public string apellido { get; set; }    
        public string correo_electronico { get; set; }
        public string contraseña { get; set; }
        public Rol id_rol { get; set; }

    }
}
