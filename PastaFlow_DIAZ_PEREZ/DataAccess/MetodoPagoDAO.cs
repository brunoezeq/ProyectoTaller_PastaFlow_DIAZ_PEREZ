using PastaFlow_DIAZ_PEREZ.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaFlow_DIAZ_PEREZ.DataAccess
{

    public class MetodoPagoDao
    {
        public List<Metodo_Pago> Listar()
        {
            var lista = new List<Metodo_Pago>();

            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT id_metodo, nombre, recargo FROM Metodo_Pago", conn))
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Metodo_Pago
                            {
                                Id_metodo = dr.GetInt32(0),
                                Nombre = dr.GetString(1),
                                Recargo = dr.GetDecimal(2)
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }

}

