using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastaFlow_DIAZ_PEREZ.Models;
using PastaFlow_DIAZ_PEREZ.DataAccess;

namespace PastaFlow_DIAZ_PEREZ.Services
{

    public class ServiceRol
    {
        private RolDAO obj_rol = new RolDAO();
        public List<Rol> ListarRoles()
        {
            return obj_rol.ListarRoles();
        }
    }
}