using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.Models
{
    public class Usuario
    {
        public int Id_usuario { get; set; } 
        public string Dni { get; set; }
        public string Nombre { get; set; }  
        public string Apellido { get; set; }    
        public string Correo_electronico { get; set; }
        public int Id_rol { get; set; }
        public bool Estado { get; set; }
        public byte[] Contrasena_hash { get; set; }
    }
}
