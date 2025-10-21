using PastaFlow_DIAZ_PEREZ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{
    public class TurnoDAO
    {
        public List<Turno> ObtenerTurnos()
        {
            var turnos = new List<Turno>();
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Id, nombre_turno, hora_inicio, hora_fin FROM Turno ORDER BY hora_inicio", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            turnos.Add(new Turno
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                nombre_turno = reader.GetString(reader.GetOrdinal("nombre_turno")),
                                hora_inicio = reader.GetTimeSpan(reader.GetOrdinal("hora_inicio")),
                                hora_fin = reader.GetTimeSpan(reader.GetOrdinal("hora_fin"))
                            });
                        }
                    }
                }
            }
            return turnos;
        }
    }
}