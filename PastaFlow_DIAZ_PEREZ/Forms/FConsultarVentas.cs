using PastaFlow_DIAZ_PEREZ.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FConsultarVentas : Form
    {
        private DataTable ventasTable;
        public FConsultarVentas()
        {
            InitializeComponent();
        }

        private void FConsultarVentas_Load(object sender, EventArgs e)
        {
            // Crear tabla de ventas simulada
            ventasTable = new DataTable();
            ventasTable.Columns.Add("NroVenta", typeof(int));
            ventasTable.Columns.Add("Fecha", typeof(DateTime));
            ventasTable.Columns.Add("Producto", typeof(string));
            ventasTable.Columns.Add("Cantidad", typeof(int));
            ventasTable.Columns.Add("PrecioUnit", typeof(decimal));
            ventasTable.Columns.Add("Total", typeof(decimal));

            // Agregar datos de prueba
            ventasTable.Rows.Add(1, DateTime.Now.AddHours(-3), "Producto A", 2, 150.00m, 300.00m);
            ventasTable.Rows.Add(2, DateTime.Now.AddHours(-2), "Producto B", 1, 200.00m, 200.00m);
            ventasTable.Rows.Add(3, DateTime.Now.AddHours(-1), "Producto C", 3, 100.00m, 300.00m);

            // Mostrar en el DataGridView
            dgvVentas.DataSource = ventasTable;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = dtpFecha.Value.Date;

            // Filtrar por fecha
            DataView vistaFiltrada = new DataView(ventasTable);
            vistaFiltrada.RowFilter = $"CONVERT(Fecha, 'System.String') LIKE '%{fechaSeleccionada:dd/MM/yyyy}%'";

            dgvVentas.DataSource = vistaFiltrada;
        }


        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvVentas.SelectedRows.Count > 0)
            {
                string producto = dgvVentas.SelectedRows[0].Cells["Producto"].Value.ToString();
                string cantidad = dgvVentas.SelectedRows[0].Cells["Cantidad"].Value.ToString();
                string total = dgvVentas.SelectedRows[0].Cells["Total"].Value.ToString();

                MessageBox.Show($"Detalle de venta:\n\nProducto: {producto}\nCantidad: {cantidad}\nTotal: ${total}",
                                "Detalle Venta",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Seleccione una venta primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

