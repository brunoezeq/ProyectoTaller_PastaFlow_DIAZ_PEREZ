using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PastaFlow_DIAZ_PEREZ.Models;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public class RolDAO
    {
        public List<Rol> ListarRoles()
        {
            List<Rol> roles = new List<Rol>();
            using (var conn = DbConnection.GetConnection())
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT Id_rol, Nombre_rol FROM Rol");
                    SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            roles.Add(new Rol()
                            {
                                Id_rol = Convert.ToInt32(dr["Id_rol"]),
                                Nombre_rol = dr["Nombre_rol"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar roles: " + ex.Message);
                }
            }
            return roles;
        }
    }
}