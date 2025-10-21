using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public class CajaDAO
    {
        private readonly string _connString;

        public CajaDAO()
        {
            _connString = ConfigurationManager.ConnectionStrings["PastaFlowDB"]?.ConnectionString;
            if (string.IsNullOrEmpty(_connString))
                throw new InvalidOperationException("Cadena de conexi�n 'PastaFlowDB' no encontrada en app.config.");

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
            // Intentar obtener turno activo (ajuste la condici�n seg�n su esquema de Turno)
            const string sqlActivo = "SELECT TOP 1 id_turno FROM Turno WHERE hora_fin IS NULL ORDER BY id_turno DESC";
            using (var cmd = new SqlCommand(sqlActivo, openConnection))
            {
                var obj = cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                    return Convert.ToInt32(obj);
            }

            // Si no hay activo, devolver el �ltimo turno existente
            const string sqlAny = "SELECT TOP 1 id_turno FROM Turno ORDER BY id_turno DESC";
            using (var cmd = new SqlCommand(sqlAny, openConnection))
            {
                var obj = cmd.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                    return Convert.ToInt32(obj);
            }

            // No hay turnos en la tabla: informar claramente para que el usuario/admin cree uno.
            throw new InvalidOperationException("No se encontr� ning�n registro en la tabla 'Turno'. Cree un turno en la base de datos o modifique la l�gica para proporcionar un id_turno v�lido.");
        }

        public int AbrirCaja(int usuarioId, decimal montoInicial, DateTime fechaApertura)
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

                // Obtener id_turno v�lido desde la tabla Turno (lanzar� excepci�n si no existe ninguno)
                int idTurno = GetCurrentTurnoId(cn);

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@idTurno", idTurno);
                cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@fechaApertura", fechaApertura);
                cmd.Parameters.AddWithValue("@montoInicial", montoInicial);

                var scalar = cmd.ExecuteScalar();
                if (scalar != null && scalar != DBNull.Value)
                    return Convert.ToInt32(scalar);
                return -1;
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

        public bool CerrarCaja(int cajaId, decimal montoCierre, DateTime fechaCierre)
        {
            const string sql = @"
                UPDATE Caja
                SET monto_cierre = @montoCierre, fecha_hora_cierre = @fechaCierre
                WHERE id_caja = @cajaId
            ";
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@montoCierre", montoCierre);
                cmd.Parameters.AddWithValue("@fechaCierre", fechaCierre);
                cmd.Parameters.AddWithValue("@cajaId", cajaId);
                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}