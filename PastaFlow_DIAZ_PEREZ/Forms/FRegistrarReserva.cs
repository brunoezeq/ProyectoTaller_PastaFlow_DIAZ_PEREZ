using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FRegistrarReserva : Form
    {
        public FRegistrarReserva()
        {
            InitializeComponent();
        }

        private void FReservas_Load(object sender, EventArgs e)
        {
            dtpFechaHora.Format = DateTimePickerFormat.Custom;
            dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";

            cBoxEstado.Items.AddRange(new string[] { "Pendiente", "Confirmada", "Cancelada" });
            cBoxEstado.SelectedIndex = 0;

            // Aplicar estilo visual consistente con Gestionar Inventario
            ConfigurarGrillaVisualReservas();

            CargarReservas();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Validaciones de entrada
        private void NombreCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
            if (char.IsLetter(e.KeyChar) && txtNombreCliente.Text.Length >= 100)
            {
                e.Handled = true;
            }
        }

        private void ApellidoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
            if (char.IsLetter(e.KeyChar) && txtApellidoCliente.Text.Length >= 100)
            {
                e.Handled = true;
            }
        }

        // Registrar reserva
        private void btnRegistrarReserva_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoCliente.Text))
            {
                MessageBox.Show("Debe ingresar el nombre y apellido del cliente.");
                return;
            }

            if (cantPersonas.Value <= 0)
            {
                MessageBox.Show("La cantidad de personas debe ser mayor que 0.");
                return;
            }

            string nombre = txtNombreCliente.Text.Trim();
            string apellido = txtApellidoCliente.Text.Trim();
            DateTime fechaHora = dtpFechaHora.Value;
            int cantidad = (int)cantPersonas.Value;
            string estado = cBoxEstado.SelectedItem.ToString();
            int idUsuario = Session.CurrentUser.Id_usuario; 

            try
            {
                var dao = new ReservaDAO();
                int id = dao.RegistrarReserva(nombre, apellido, fechaHora, cantidad, estado, idUsuario);

                MessageBox.Show($"Reserva registrada correctamente.");

                // Generar ticket PDF (NO factura)
                string ruta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Ticket_Reserva_{id}.pdf");

                string nombreLocal = "PastaFlow Restaurante";

                // Asegurar que el logo apunte a un archivo existente
                string logo = Path.Combine(Application.StartupPath, "Recursos", "logo.png");
                if (!File.Exists(logo))
                {
                    var logoJpg = Path.Combine(Application.StartupPath, "Recursos", "logo.jpg");
                    logo = File.Exists(logoJpg) ? logoJpg : null;
                }

                string cajero = Session.CurrentUser.Nombre + " " + Session.CurrentUser.Apellido;
                string cliente = $"{nombre} {apellido}";

                PdfHelper.GenerarTicketReserva(
                    nombreLocal,
                    logo,
                    id.ToString(),      
                    fechaHora,
                    cliente,
                    cantidad,
                    estado,
                    cajero,
                    ruta
                );

                MessageBox.Show($"Ticket generado en: {ruta}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Abre el PDF automáticamente
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });


                CargarReservas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar reserva: " + ex.Message);
            }
        }

        // Carga las reservas en el DataGridView
        private void CargarReservas()
        {
            var dao = new ReservaDAO();
            dgvReservas.DataSource = dao.ListarReservas();

            // Aplicar estilo visual completo y luego ajustes por columna
            ConfigurarGrillaVisualReservas();
            FormatearGrillaReservas();
        }

        // Limpiar campos del formulario
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreCliente.Clear();  
            txtApellidoCliente.Clear(); 
            cantPersonas.Value = 1;
            dtpFechaHora.Value = DateTime.Now;
            cBoxEstado.SelectedIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = dtpInicio.Value.Date;
            DateTime fechaFin = dtpFin.Value.Date.AddDays(1).AddSeconds(-1); // Hasta fin del día

            try
            {
                var dao = new ReservaDAO();
                DataTable dt = dao.BuscarReservasPorFechas(fechaInicio, fechaFin);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron reservas en el rango seleccionado.",
                        "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvReservas.DataSource = null;
                }
                else
                {
                    dgvReservas.DataSource = dt;
                    FormatearGrillaReservas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar reservas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpiarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                // Restablecer los DateTimePicker a sus valores por defecto
                dtpInicio.Value = DateTime.Today.AddDays(-7); // Ejemplo: últimos 7 días
                dtpFin.Value = DateTime.Today;

                // Limpiar el DataGridView
                dgvReservas.DataSource = null;

                // (Opcional) Volver a cargar todas las reservas sin filtro
                CargarReservas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar el filtro: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Configuración visual 
        private void ConfigurarGrillaVisualReservas()
        {
            var g = dgvReservas;

            g.AutoGenerateColumns = true;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.RowTemplate.Height = 38;

            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeColumns = false;
            g.AllowUserToResizeRows = false;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.RowHeadersVisible = false;

            g.BorderStyle = BorderStyle.None;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.GridColor = Color.FromArgb(230, 200, 190);
            g.BackgroundColor = Color.White;
            g.Cursor = Cursors.Hand;

            g.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            g.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            g.DefaultCellStyle.BackColor = Color.White;
            g.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(170, 40, 40);
            g.DefaultCellStyle.SelectionForeColor = Color.White;

            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 243, 230);

            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 0, 0);
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            g.ColumnHeadersHeight = 42;

            // Asegurar scroll vertical (mejor para tablas largas)
            g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            g.ScrollBars = ScrollBars.Vertical;
        }

        private void FormatearGrillaReservas()
        {
            // Ocultar columnas internas si las hay
            if (dgvReservas.Columns.Contains("id_reserva"))
                dgvReservas.Columns["id_reserva"].Visible = false;

            // Alinear campos específicos si existen
            if (dgvReservas.Columns.Contains("Fecha y Hora"))
                dgvReservas.Columns["Fecha y Hora"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (dgvReservas.Columns.Contains("Cajero"))
                dgvReservas.Columns["Cajero"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Aplicar formato general
            dgvReservas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReservas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReservas.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Si quieres un botón de acción (ej. cancelar reserva), puedes agregarlo así:
            if (!dgvReservas.Columns.Contains("Accion"))
            {
                var col = new DataGridViewButtonColumn
                {
                    Name = "Accion",
                    HeaderText = "Acción",
                    UseColumnTextForButtonValue = false,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dgvReservas.Columns.Add(col);
            }
        }
    }
}
