using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public class ReporteDAO
    {
        private readonly string _connStr =
            System.Configuration.ConfigurationManager.ConnectionStrings["PastaFlowDB"].ConnectionString;

        public DataTable VentasPorEmpleado(DateTime? desde, DateTime? hasta)
        {
            using (var cn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("sp_VentasPorEmpleado", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaDesde", (object)desde ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaHasta", (object)hasta ?? DBNull.Value);

                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable TopProductos(DateTime? desde, DateTime? hasta)
        {
            using (var cn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("sp_TopProductos", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaDesde", (object)desde ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaHasta", (object)hasta ?? DBNull.Value);

                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable TotalesPorMetodoPago(DateTime? desde, DateTime? hasta)
        {
            using (var cn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("sp_TotalesPorMetodoPago", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaDesde", (object)desde ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaHasta", (object)hasta ?? DBNull.Value);

                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
