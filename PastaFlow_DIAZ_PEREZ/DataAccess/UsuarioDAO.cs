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
                        if (reader.Read())
                        {
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

        //Verificación de existencia de DNI
        public bool ExisteDni(string dni)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Usuario WHERE dni = @dni";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        //Registro de nuevo usuario con verificación de DNI único
        public void RegistrarUsuario(string dni, string nombre, string apellido, string correo, string telefono, int idRol, byte[] contrasena)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();

                //Verificar si el DNI ya existe
                if (ExisteDni(dni))
                {
                    throw new Exception($"El DNI {dni} ya está registrado en el sistema.");
                }

                using (var cmd = new SqlCommand("sp_RegistrarUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dni", dni);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@id_rol", idRol);

                    var p = cmd.Parameters.Add("@contrasena", SqlDbType.VarBinary, 64);
                    p.Value = contrasena ?? (object)DBNull.Value;

                    cmd.Parameters.AddWithValue("@estado", 1);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Búsqueda de usuarios con filtros opcionales
        public DataTable BuscarUsuarios(string dni = null, int? idRol = null)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_BuscarUsuarios", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dni", (object)dni ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_rol", (object)idRol ?? DBNull.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // Edición de usuario existente
        public void ActualizarUsuario(string dni, string nombre, string apellido, string correo, string telefono, int idRol, byte[] contrasena)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_ActualizarUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dni", dni);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@id_rol", idRol);

                    var p = cmd.Parameters.Add("@contrasena", SqlDbType.VarBinary, 64);
                    p.Value = (object)contrasena ?? DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        // Cambio de estado (activo/inactivo) de usuario
        public void CambiarEstadoUsuario(string dni)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_CambiarEstadoUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dni", dni);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Error al cambiar estado del usuario: {ex.Message}");
                    }
                }
            }
        }
    }
}
