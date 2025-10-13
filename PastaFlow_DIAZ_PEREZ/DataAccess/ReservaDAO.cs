using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public class ReservaDAO
    {
        // Registrar una nueva reserva
        public int RegistrarReserva(string nombre, string apellido, DateTime fechaHora, int cantidadPersonas, string estado, int idUsuario)
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_RegistrarReserva", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_cliente", nombre);
                    cmd.Parameters.AddWithValue("@apellido_cliente", apellido);
                    cmd.Parameters.AddWithValue("@fecha_hora_reserva", fechaHora);
                    cmd.Parameters.AddWithValue("@cantidad_personas", cantidadPersonas);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                    object result = cmd.ExecuteScalar();
                    return (result != null && int.TryParse(result.ToString(), out int id)) ? id : 0;
                }
            }
        }

        // Listar todas las reservas
        public DataTable ListarReservas()
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_ListarReservas", conn))
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
