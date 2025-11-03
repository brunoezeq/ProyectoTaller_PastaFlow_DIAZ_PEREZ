using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FVerReportes : Form
    {
        public FVerReportes()
        {
            InitializeComponent();
        }

        private VentaDao ventaDao = new VentaDao();

        private void FVerReportes_Load(object sender, EventArgs e)
        {
            CargarVentas();
        }

        private void CargarVentas()
        {
            try
            {
                DataTable dt = ventaDao.ObtenerReporteVentasConDetalles();
                dgvVentas.DataSource = dt;

                // Agregar columna de botón si no existe
                if (!dgvVentas.Columns.Contains("bGenerarPDF"))
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                    {
                        Name = "bGenerarPDF",
                        HeaderText = "Acción",
                        Text = "Generar PDF",
                        UseColumnTextForButtonValue = true,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    };
                    dgvVentas.Columns.Add(btn);
                }

                FormatearGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las ventas: " + ex.Message);
            }
        }

        private void FormatearGrilla()
        {
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.AllowUserToAddRows = false;
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvVentas.Columns[e.ColumnIndex].Name == "bGenerarPDF")
            {
                // Extraer datos de la venta seleccionada
                string numeroFactura = dgvVentas.Rows[e.RowIndex].Cells["numero_factura"].Value.ToString();
                DateTime fecha = Convert.ToDateTime(dgvVentas.Rows[e.RowIndex].Cells["fecha_venta"].Value);
                string metodo = dgvVentas.Rows[e.RowIndex].Cells["metodo_pago"].Value.ToString();
                decimal total = Convert.ToDecimal(dgvVentas.Rows[e.RowIndex].Cells["total_venta"].Value);

                // Filtrar los detalles de esa venta
                int idVenta = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["id_venta"].Value);
                DataTable dt = ((DataTable)dgvVentas.DataSource);
                var detalles = dt.AsEnumerable()
                    .Where(r => r.Field<int>("id_venta") == idVenta)
                    .CopyToDataTable();

                // Ruta y logo
                string logoPath = Path.Combine(Application.StartupPath, "Resources", "logo.png");
                string rutaSalida = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Factura_{numeroFactura}.pdf");

                try
                {
                    PdfHelper.GenerarFacturaVenta(
                        nombreLocal: "Pasta Flow",
                        logoPath: logoPath,
                        numeroFactura: numeroFactura,
                        fecha: fecha,
                        cajero: Session.CurrentUser.Nombre + " " + Session.CurrentUser.Apellido,
                        productos: detalles,
                        totalVenta: total,
                        rutaSalida: rutaSalida
                    );

                    if (File.Exists(rutaSalida))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = rutaSalida,
                            UseShellExecute = true
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar PDF: " + ex.Message);
                }
            }
        }
    }

}

