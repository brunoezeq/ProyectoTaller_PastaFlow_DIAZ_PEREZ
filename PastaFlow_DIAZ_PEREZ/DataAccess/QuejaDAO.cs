using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public class QuejaDAO
    {
        public void RegistrarQueja(string nombre, string apellido, string motivo, string descripcion, int idUsuario)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_RegistrarQueja", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_cliente", nombre);
                    cmd.Parameters.AddWithValue("@apellido_cliente", apellido);
                    cmd.Parameters.AddWithValue("@motivo_queja", motivo);
                    cmd.Parameters.AddWithValue("@descripcion_queja", descripcion);
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable BuscarQuejas(DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_BuscarQuejas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fechaInicio", (object)fechaInicio ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaFin", (object)fechaFin ?? DBNull.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public void EliminarQueja(int idQueja)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_EliminarQueja", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_queja", idQueja);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
