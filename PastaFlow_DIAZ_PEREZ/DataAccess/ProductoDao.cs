using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PastaFlow_DIAZ_PEREZ.Models;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    // Clase de acceso a datos (DAO) para manejar operaciones sobre la tabla Producto.
    // Implementa inserción, actualización, eliminación lógica, restauración y listado.
    internal class ProductoDao
    {
        // CONSULTAS SQL PRINCIPALES
        private const string SQL_INSERT = @"
INSERT INTO dbo.Producto (nombre, descripcion, precio, stock, estado, id_categoria)
OUTPUT INSERTED.id_producto
VALUES (@nombre, @descripcion, @precio, @stock, 1, @id_categoria);";

        private const string SQL_UPDATE = @"
UPDATE dbo.Producto
SET nombre = @nombre,
    descripcion = @descripcion,
    precio = @precio,
    stock = @stock,
    estado = @estado,
    id_categoria = @id_categoria
WHERE id_producto = @id_producto;";

        private const string SQL_SOFT_DELETE = @"
UPDATE dbo.Producto
SET estado = 0
WHERE id_producto = @id_producto AND estado = 1;";

        private const string SQL_RESTAURAR = @"
UPDATE dbo.Producto
SET estado = 1
WHERE id_producto = @id_producto AND estado = 0;";

        private const string SQL_GET_BY_ID = @"
SELECT p.id_producto, p.nombre, p.descripcion, p.precio, p.stock, p.estado,
       c.id_categoria, c.nombre_categoria
FROM dbo.Producto p
JOIN dbo.Categoria c ON c.id_categoria = p.id_categoria
WHERE p.id_producto = @id_producto;";

        private const string SQL_LISTAR_ACTIVOS = @"
SELECT p.id_producto, p.nombre, p.descripcion, p.precio, p.stock, p.estado,
       c.id_categoria, c.nombre_categoria
FROM dbo.Producto p
JOIN dbo.Categoria c ON c.id_categoria = p.id_categoria
WHERE p.estado = 1
ORDER BY p.nombre;";

        private const string SQL_LISTAR_TODOS = @"
SELECT p.id_producto, p.nombre, p.descripcion, p.precio, p.stock, p.estado,
       c.id_categoria, c.nombre_categoria
FROM dbo.Producto p
JOIN dbo.Categoria c ON c.id_categoria = p.id_categoria
ORDER BY p.nombre;";

          // MÉTODOS CRUD PRINCIPALES
    
        // Inserta un nuevo producto y devuelve el ID generado
        public int Insertar(Producto p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            if (p.id_categoria == null) throw new ArgumentException("Se requiere categoría.", nameof(p));
            if (string.IsNullOrWhiteSpace(p.nombre)) throw new ArgumentException("Nombre obligatorio.", nameof(p));

            try
            {
                using (var cn = DbConnection.GetConnection())
                using (var cmd = new SqlCommand(SQL_INSERT, cn))
                {
                    cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 25).Value = p.nombre.Trim();
                    cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 100).Value = (object)(p.descripcion ?? string.Empty);

                    var parPrecio = cmd.Parameters.Add("@precio", SqlDbType.Decimal);
                    parPrecio.Precision = 10;
                    parPrecio.Scale = 2;
                    parPrecio.Value = p.precio;

                    cmd.Parameters.Add("@stock", SqlDbType.Int).Value = p.stock;
                    cmd.Parameters.Add("@id_categoria", SqlDbType.Int).Value = p.id_categoria.id_categoria;

                    cn.Open();
                    p.id_producto = (int)cmd.ExecuteScalar();
                    p.estado = true;
                    return p.id_producto;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al insertar producto (#{ex.Number}).", ex);
            }
        }

        // Actualiza un producto existente
        public void Actualizar(Producto p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            if (p.id_producto <= 0) throw new ArgumentException("Id inválido.", nameof(p));
            if (p.id_categoria == null) throw new ArgumentException("Categoría requerida.", nameof(p));
            if (string.IsNullOrWhiteSpace(p.nombre)) throw new ArgumentException("Nombre obligatorio.", nameof(p));

            try
            {
                using (var cn = DbConnection.GetConnection())
                using (var cmd = new SqlCommand(SQL_UPDATE, cn))
                {
                    cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 25).Value = p.nombre.Trim();
                    cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 100).Value = (object)(p.descripcion ?? string.Empty);

                    var parPrecio = cmd.Parameters.Add("@precio", SqlDbType.Decimal);
                    parPrecio.Precision = 10;
                    parPrecio.Scale = 2;
                    parPrecio.Value = p.precio;

                    cmd.Parameters.Add("@stock", SqlDbType.Int).Value = p.stock;
                    cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = p.estado;
                    cmd.Parameters.Add("@id_categoria", SqlDbType.Int).Value = p.id_categoria.id_categoria;
                    cmd.Parameters.Add("@id_producto", SqlDbType.Int).Value = p.id_producto;

                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new Exception("No se encontró el producto para actualizar.");
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al actualizar producto (#{ex.Number}).", ex);
            }
        }

        // Eliminación lógica (estado = 0)
        public void EliminarLogico(int idProducto)
        {
            if (idProducto <= 0) throw new ArgumentOutOfRangeException(nameof(idProducto));
            try
            {
                using (var cn = DbConnection.GetConnection())
                using (var cmd = new SqlCommand(SQL_SOFT_DELETE, cn))
                {
                    cmd.Parameters.Add("@id_producto", SqlDbType.Int).Value = idProducto;
                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new Exception("No se encontró el producto activo a desactivar.");
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al desactivar producto (#{ex.Number}).", ex);
            }
        }

        // Restaura un producto eliminado lógicamente (estado = 1)
        public void Restaurar(int idProducto)
        {
            if (idProducto <= 0) throw new ArgumentOutOfRangeException(nameof(idProducto));
            try
            {
                using (var cn = DbConnection.GetConnection())
                using (var cmd = new SqlCommand(SQL_RESTAURAR, cn))
                {
                    cmd.Parameters.Add("@id_producto", SqlDbType.Int).Value = idProducto;
                    cn.Open();
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new Exception("No se encontró el producto inactivo a restaurar.");
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al restaurar producto (#{ex.Number}).", ex);
            }
        }

        // Obtiene un producto por su ID
        public Producto ObtenerPorId(int idProducto)
        {
            if (idProducto <= 0) throw new ArgumentOutOfRangeException(nameof(idProducto));

            try
            {
                using (var cn = DbConnection.GetConnection())
                using (var cmd = new SqlCommand(SQL_GET_BY_ID, cn))
                {
                    cmd.Parameters.Add("@id_producto", SqlDbType.Int).Value = idProducto;
                    cn.Open();

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            var cat = new Categoria
                            {
                                id_categoria = rd.GetInt32(rd.GetOrdinal("id_categoria")),
                                nombre_categoria = rd.GetString(rd.GetOrdinal("nombre_categoria"))
                            };

                            return new Producto
                            {
                                id_producto = rd.GetInt32(rd.GetOrdinal("id_producto")),
                                nombre = rd.GetString(rd.GetOrdinal("nombre")),
                                descripcion = rd.GetString(rd.GetOrdinal("descripcion")),
                                precio = rd.GetDecimal(rd.GetOrdinal("precio")),
                                stock = rd.GetInt32(rd.GetOrdinal("stock")),
                                estado = rd.GetBoolean(rd.GetOrdinal("estado")),
                                id_categoria = cat
                            };
                        }
                    }
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al obtener producto (#{ex.Number}).", ex);
            }
        }

        // Lista todos los productos (solo activos o todos según parámetro)
        public IList<Producto> Listar(bool incluirInactivos = false)
        {
            var list = new List<Producto>();
            string sql = incluirInactivos ? SQL_LISTAR_TODOS : SQL_LISTAR_ACTIVOS;

            try
            {
                using (var cn = DbConnection.GetConnection())
                using (var cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        // Obtenemos los índices de columna para mejorar rendimiento
                        int oIdProd = rd.GetOrdinal("id_producto");
                        int oNombre = rd.GetOrdinal("nombre");
                        int oDesc = rd.GetOrdinal("descripcion");
                        int oPrecio = rd.GetOrdinal("precio");
                        int oStock = rd.GetOrdinal("stock");
                        int oEstado = rd.GetOrdinal("estado");
                        int oCatId = rd.GetOrdinal("id_categoria");
                        int oCatNom = rd.GetOrdinal("nombre_categoria");

                        while (rd.Read())
                        {
                            var cat = new Categoria
                            {
                                id_categoria = rd.GetInt32(oCatId),
                                nombre_categoria = rd.GetString(oCatNom)
                            };

                            list.Add(new Producto
                            {
                                id_producto = rd.GetInt32(oIdProd),
                                nombre = rd.GetString(oNombre),
                                descripcion = rd.GetString(oDesc),
                                precio = rd.GetDecimal(oPrecio),
                                stock = rd.GetInt32(oStock),
                                estado = rd.GetBoolean(oEstado),
                                id_categoria = cat
                            });
                        }
                    }
                }

                return list;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error SQL al listar productos (#{ex.Number}).", ex);
            }
        }
    }
}
