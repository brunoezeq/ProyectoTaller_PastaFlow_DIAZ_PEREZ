using System.Collections.Generic;
using System.Data.SqlClient;
using PastaFlow_DIAZ_PEREZ.Models;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    // Clase encargada de manejar las operaciones de base de datos
    // relacionadas con la tabla Categoria.
    internal class CategoriaDao
    {
        // Consulta SQL: selecciona todas las categorías activas (estado = 1)
        private const string SQL_LISTAR_ACTIVAS = @"
SELECT id_categoria, nombre_categoria, estado
FROM Categoria
WHERE estado = 1
ORDER BY nombre_categoria";

        // Método que devuelve una lista de categorías activas desde la base de datos.
        public List<Categoria> Listar()
        {
            var categorias = new List<Categoria>();

            // Abrimos conexión y ejecutamos la consulta
            using (SqlConnection conn = DbConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(SQL_LISTAR_ACTIVAS, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // Recorremos los resultados y los convertimos en objetos Categoria
                while (reader.Read())
                {
                    categorias.Add(new Categoria
                    {
                        id_categoria = (int)reader["id_categoria"],
                        nombre_categoria = reader["nombre_categoria"].ToString(),
                        estado = (bool)reader["estado"]
                    });
                }
            }

            // Retornamos la lista de categorías activas
            return categorias;
        }
    }
}
