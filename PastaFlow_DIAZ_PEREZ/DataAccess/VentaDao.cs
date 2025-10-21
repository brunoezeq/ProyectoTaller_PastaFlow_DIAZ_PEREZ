using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PastaFlow_DIAZ_PEREZ.Models;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public class VentaDao
    {
        // Mantiene compatibilidad hacia atrás
        public bool RegistrarVenta(Venta venta, List<Detalle_Venta> detalles)
            => RegistrarVenta(venta, detalles, out _);

        public bool RegistrarVenta(Venta venta, List<Detalle_Venta> detalles, out string error)
        {
            error = null;
            if (detalles == null || detalles.Count == 0)
            {
                error = "No hay detalles de venta para registrar.";
                return false;
            }

            bool exito = false;

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1️⃣ Crear la cabecera de la venta
                        using (SqlCommand cmdCab = new SqlCommand("sp_Venta_CrearCabecera", conn, trans))
                        {
                            cmdCab.CommandType = CommandType.StoredProcedure;
                            cmdCab.Parameters.AddWithValue("@numero_factura", venta.numero_factura);
                            cmdCab.Parameters.AddWithValue("@id_caja", venta.id_caja?.Id ?? 0);
                            cmdCab.Parameters.AddWithValue("@id_metodo", venta.id_metodo?.id_metodo ?? 0);

                            object result = cmdCab.ExecuteScalar();
                            if (result == null || result == DBNull.Value)
                                throw new InvalidOperationException("El procedimiento sp_Venta_CrearCabecera no devolvió el Id de venta.");

                            int idVenta = Convert.ToInt32(result);

                            // 2️⃣ Insertar cada detalle
                            foreach (var det in detalles)
                            {
                                using (SqlCommand cmdDet = new SqlCommand("sp_Venta_AgregarDetalle", conn, trans))
                                {
                                    cmdDet.CommandType = CommandType.StoredProcedure;
                                    cmdDet.Parameters.AddWithValue("@id_venta", idVenta);
                                    cmdDet.Parameters.AddWithValue("@id_producto", det.id_producto?.id_producto ?? 0);
                                    cmdDet.Parameters.AddWithValue("@cantidad", det.cantidad);
                                    cmdDet.Parameters.AddWithValue("@precio_unitario", det.precio_unitario);
                                    cmdDet.ExecuteNonQuery();
                                }
                            }

                            // 3️⃣ Recalcular el total
                            using (SqlCommand cmdTotal = new SqlCommand("sp_Venta_RecalcularTotal", conn, trans))
                            {
                                cmdTotal.CommandType = CommandType.StoredProcedure;
                                cmdTotal.Parameters.AddWithValue("@id_venta", idVenta);
                                cmdTotal.ExecuteNonQuery();
                            }
                        }

                        // 4️⃣ Confirmar la transacción
                        trans.Commit();
                        exito = true;
                    }
                    catch (Exception ex)
                    {
                        try { trans.Rollback(); } catch { /* ignora errores de rollback */ }
                        error = ex.Message;
                        exito = false;
                    }
                }
            }

            return exito;
        }
    }
}
