using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PastaFlow_DIAZ_PEREZ.Models;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public class VentaDao
    {
        public int RegistrarVenta(int idCaja, int idMetodo, decimal totalBase, decimal recargoPorcentaje, decimal totalFinal, string numeroFactura)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_RegistrarVenta", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_caja", idCaja);
                    cmd.Parameters.AddWithValue("@id_metodo", idMetodo);
                    cmd.Parameters.AddWithValue("@total_base", totalBase);
                    cmd.Parameters.AddWithValue("@recargo_porcentaje", recargoPorcentaje);
                    cmd.Parameters.AddWithValue("@total_final", totalFinal);
                    cmd.Parameters.AddWithValue("@numero_factura", numeroFactura);

                    int idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                    return idVenta;
                }
            }
        }

        public void InsertarDetalleVenta(int idVenta, int idProducto, int cantidad, decimal precioUnitario)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_InsertarDetalleVenta", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@precio_unitario", precioUnitario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerReporteVentasConDetalles()
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_ReporteVentasConDetalles", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }



    }
}
