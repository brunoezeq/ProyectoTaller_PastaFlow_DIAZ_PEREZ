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
    public class BackupDAO
    {
        private readonly string _connString;

        public BackupDAO()
        {
            _connString = ConfigurationManager.ConnectionStrings["PastaFlowDB"]?.ConnectionString;
        }

        public void RealizarBackup(string rutaDestino, int idUsuario)
        {
            string nombreDB = "PastaFlowBD"; 

            string query = $@"
                BACKUP DATABASE [{nombreDB}]
                TO DISK = @ruta
                WITH INIT, NAME = 'Backup_{nombreDB}_{DateTime.Now:yyyyMMdd_HHmmss}'";

            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@ruta", rutaDestino);
                cn.Open();
                cmd.ExecuteNonQuery();
            }

            RegistrarBackup(rutaDestino, idUsuario);
        }

        private void RegistrarBackup(string ruta, int idUsuario)
        {
            const string sql = "INSERT INTO BackupHistorial (ruta, id_usuario) VALUES (@ruta, @id_usuario)";
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ruta", ruta);
                cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public (DateTime? Fecha, string Usuario, string Ruta)? ObtenerUltimoBackup()
        {
            const string sql = @"
                SELECT TOP 1 B.fecha, U.nombre + ' ' + U.apellido AS Usuario, B.ruta
                FROM BackupHistorial B
                INNER JOIN Usuario U ON B.id_usuario = U.id_usuario
                ORDER BY B.fecha DESC";

            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return (
                            dr.GetDateTime(0),
                            dr.GetString(1),
                            dr.GetString(2)
                        );
                    }
                }
            }
            return null;
        }

        public DataTable ObtenerHistorialBackups()
        {
            const string sql = @"
            SELECT 
                B.id_backup AS [ID],
                B.fecha AS [Fecha y hora],
                U.nombre + ' ' + U.apellido AS [Usuario],
                B.ruta AS [Ruta del archivo]
            FROM BackupHistorial B
            INNER JOIN Usuario U ON B.id_usuario = U.id_usuario
            ORDER BY B.fecha DESC";

            var dt = new DataTable();

            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(sql, cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        public void RestaurarBackup(string rutaBackup)
        {
            string dbName = "PastaFlowBD"; // Nombre real de tu base
            string rutaData = @"C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\";

            using (var cn = new SqlConnection(_connString))
            {
                cn.Open();

                // Desconectar usuarios activos de la BD
                string killConnections = $@"
                    ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                ";
                using (var cmd = new SqlCommand(killConnections, cn))
                {
                    cmd.ExecuteNonQuery();
                }

                // Restaurar desde el archivo seleccionado
                    string restoreSql = $@"
                RESTORE DATABASE [{dbName}]
                FROM DISK = '{rutaBackup}'
                WITH REPLACE,
                MOVE '{dbName}' TO '{rutaData}{dbName}.mdf',
                MOVE '{dbName}_log' TO '{rutaData}{dbName}_log.ldf';
                ";
                using (var cmd = new SqlCommand(restoreSql, cn))
                {
                    cmd.ExecuteNonQuery();
                }

                // Volver a modo multiusuario
                string multiUser = $@"ALTER DATABASE [{dbName}] SET MULTI_USER;";
                using (var cmd = new SqlCommand(multiUser, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
