using PastaFlow_DIAZ_PEREZ.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public class CajaDAO
    {
        private readonly string _connString;

        public CajaDAO()
        {
            _connString = ConfigurationManager.ConnectionStrings["PastaFlowDB"]?.ConnectionString;
            if (string.IsNullOrEmpty(_connString))
                throw new InvalidOperationException("Cadena de conexión 'PastaFlowDB' no encontrada en app.config.");

            // Comprobar columnas que realmente usa el proyecto
            var cols = GetTableColumns("Caja");
            var required = new[] { "id_caja", "id_turno", "id_usuario", "fecha_hora_apertura", "monto_inicial", "fecha_hora_cierre" };
            var missing = required.Except(cols, StringComparer.OrdinalIgnoreCase).ToArray();
            if (missing.Length > 0)
            {
                throw new InvalidOperationException(
                    $"Columnas faltantes en la tabla 'Caja': {string.Join(", ", missing)}. Columnas existentes: {string.Join(", ", cols)}");
            }
        }

        private string[] GetTableColumns(string tableName)
        {
            var cols = new List<string>();
            const string sql = @"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table";
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@table", tableName);
                cn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        cols.Add(rdr.GetString(0));
                    }
                }
            }
            return cols.ToArray();
        }

        private int GetCurrentTurnoId(SqlConnection openConnection)
        {
            // Intentar obtener turno activo (ajuste la condición según su esquema de Turno)
            const string sqlActivo = "SELECT TOP 1 id_turno FROM Turno WHERE hora_fin IS NULL ORDER BY id_turno DESC";
            using (var cmd = new SqlCommand(sqlActivo, openConnection))
            {
                var obj = cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                    return Convert.ToInt32(obj);
            }

            // Si no hay activo, devolver el último turno existente
            const string sqlAny = "SELECT TOP 1 id_turno FROM Turno ORDER BY id_turno DESC";
            using (var cmd = new SqlCommand(sqlAny, openConnection))
            {
                var obj = cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                    return Convert.ToInt32(obj);
            }

            // No hay turnos en la tabla: informar claramente para que el usuario/admin cree uno.
            throw new InvalidOperationException("No se encontró ningún registro en la tabla 'Turno'. Cree un turno en la base de datos o modifique la lógica para proporcionar un id_turno válido.");
        }

        public Caja AbrirCaja(int usuarioId, decimal montoInicial, DateTime fechaApertura)
        {
            const string sql = @"
                INSERT INTO Caja (id_turno, id_usuario, fecha_hora_apertura, monto_inicial, monto_esperado)
                VALUES (@idTurno, @usuarioId, @fechaApertura, @montoInicial, 0);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("", cn))
            {
                cn.Open();

                // Obtener id_turno válido desde la tabla Turno
                int idTurno = GetCurrentTurnoId(cn);

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@idTurno", idTurno);
                cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@fechaApertura", fechaApertura);
                cmd.Parameters.AddWithValue("@montoInicial", montoInicial);

                var scalar = cmd.ExecuteScalar();
                if (scalar != null && scalar != DBNull.Value)
                {
                    int idCaja = Convert.ToInt32(scalar);

                    // Devolver objeto Caja con los datos insertados
                    return new Caja
                    {
                        Id_caja = idCaja,
                        Id_turno = idTurno,
                        Id_usuario = usuarioId,
                        Fecha_hora_apertura = fechaApertura,
                        Monto_inicio = montoInicial,
                        Monto_esperado = 0
                    };
                }

                return null;
            }
        }

        public bool EstaCajaAbierta(out int cajaId)
        {
            const string sql = @"SELECT TOP 1 id_caja FROM Caja WHERE fecha_hora_cierre IS NULL ORDER BY fecha_hora_apertura DESC";
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                var obj = cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                {
                    cajaId = Convert.ToInt32(obj);
                    return true;
                }
                cajaId = -1;
                return false;
            }
        }

        public decimal ObtenerTotalVentasEfectivo(int idCaja)
        {
            using (var cn = new SqlConnection(_connString))
            {
                cn.Open();
                const string sql = @"
            SELECT ISNULL(SUM(v.total_venta), 0)
            FROM Venta v
            INNER JOIN Metodo_Pago m ON v.id_metodo = m.id_metodo
            WHERE v.id_caja = @idCaja AND m.nombre = 'Efectivo';";

                using (var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idCaja", idCaja);
                    var result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        public void CerrarCaja(int idCaja, decimal montoFinal)
        {
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("sp_CerrarCaja", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_caja", idCaja);
                cmd.Parameters.AddWithValue("@monto_cierre", montoFinal);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }




    }
}