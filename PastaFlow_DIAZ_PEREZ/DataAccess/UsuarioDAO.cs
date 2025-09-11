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
    public class UsuarioDAO
    {
        public Usuario ObtenerPorDni(string dni)
        {
            Usuario user = null; 
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT id_usuario, dni, nombre, apellido, id_rol, estado, contrasena_hash
                               FROM Usuario
                               WHERE dni = @dni";


                using (var cmd = new SqlCommand(sql, conn))
                    {
                    cmd.Parameters.AddWithValue("@dni", dni);
                    using (var reader = cmd.ExecuteReader())    
                    {
                        if (reader.Read()){
                            user = new Usuario
                            {
                                Id_usuario = (int)reader["id_usuario"],
                                Dni = reader["dni"].ToString(),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Id_rol = (int)reader["id_rol"],
                                Estado = (bool)reader["estado"],
                                Contrasena_hash = reader["contrasena_hash"] == DBNull.Value
                                     ? null
                                     : (byte[])reader["contrasena_hash"]
                            };
                        }
                    }
                }
            }
            return user;
        }
    }
}
