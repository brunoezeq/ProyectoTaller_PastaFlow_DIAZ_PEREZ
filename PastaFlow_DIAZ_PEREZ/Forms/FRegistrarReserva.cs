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
    // Gestión de reservas:
    // - Alta con validaciones básicas (nombre, apellido, fecha/hora, personas, estado).
    // - Listado y búsqueda por rango de fechas.
    // - Eliminación desde la grilla con confirmación.
    // - Generación de ticket PDF (no factura) con logo opcional.
    // - Estilo de grilla unificado y detección dinámica de columna ID.
    public partial class FRegistrarReserva : Form
    {
        public FRegistrarReserva()
        {
            InitializeComponent();
        }

        // Nombre de la columna ID de reserva en el resultado (se detecta tras cargar).
        private string _idReservaColumnName = "id_reserva";

        private void FReservas_Load(object sender, EventArgs e)
        {
            // Formato de fecha/hora para la reserva
            dtpFechaHora.Format = DateTimePickerFormat.Custom;
            dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";

            // Estados posibles
            cBoxEstado.Items.AddRange(new string[] { "Pendiente", "Confirmada", "Cancelada" });
            cBoxEstado.SelectedIndex = 0;

            // Estilo y handlers de la grilla
            ConfigurarGrillaVisualReservas();
            dgvReservas.CellContentClick -= dgvReservas_CellContentClick;
            dgvReservas.CellContentClick += dgvReservas_CellContentClick;

            // Carga inicial
            CargarReservas();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Validación de nombre (solo letras y espacio, máx 100)
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

        // Validación de apellido (solo letras y espacio, máx 100)
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

        // Registra la reserva y genera ticket PDF
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
            int idUsuario = Session.CurrentUser.Id_usuario; // cajero/usuario actual

            try
            {
                var dao = new ReservaDAO();
                int id = dao.RegistrarReserva(nombre, apellido, fechaHora, cantidad, estado, idUsuario);

                MessageBox.Show($"Reserva registrada correctamente.");

                // Generar ticket PDF (NO factura) en el Escritorio
                string ruta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Ticket_Reserva_{id}.pdf");

                string nombreLocal = "PastaFlow Restaurante";

                // Logo opcional (png/jpg)
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
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });

                CargarReservas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar reserva: " + ex.Message);
            }
        }

        // Carga y aplica estilo a la grilla de reservas
        private void CargarReservas()
        {
            var dao = new ReservaDAO();
            dgvReservas.DataSource = dao.ListarReservas();

            DetectarColumnaIdReserva();
            ConfigurarGrillaVisualReservas();
            FormatearGrillaReservas();
        }

        // Determina el nombre real de la columna ID en el DataGridView
        private void DetectarColumnaIdReserva()
        {
            _idReservaColumnName = null;
            foreach (DataGridViewColumn c in dgvReservas.Columns)
            {
                var nombre = c.Name;
                var header = c.HeaderText;
                var prop = c.DataPropertyName;

                bool esId =
                    Igual(nombre, "id_reserva") ||
                    Igual(prop, "id_reserva") ||
                    (Contiene(header, "id") && Contiene(header, "reserva")) ||
                    (Contiene(nombre, "id") && Contiene(nombre, "reserva")) ||
                    Igual(nombre, "IdReserva") || Igual(prop, "IdReserva") ||
                    Igual(nombre, "ID_RESERVA");

                if (esId)
                {
                    _idReservaColumnName = c.Name;
                    break;
                }
            }
        }

        private bool Igual(string a, string b) =>
            string.Equals(a?.Trim(), b?.Trim(), StringComparison.OrdinalIgnoreCase);

        private bool Contiene(string a, string b) =>
            !string.IsNullOrWhiteSpace(a) && a.IndexOf(b, StringComparison.OrdinalIgnoreCase) >= 0;

        // Limpia los campos del formulario de alta
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreCliente.Clear();
            txtApellidoCliente.Clear();
            cantPersonas.Value = 1;
            dtpFechaHora.Value = DateTime.Now;
            cBoxEstado.SelectedIndex = 0;
        }

        // Búsqueda por fecha (inicio-fin inclusivo hasta fin de día)
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = dtpInicio.Value.Date;
            DateTime fechaFin = dtpFin.Value.Date.AddDays(1).AddSeconds(-1);

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

        // Restablece filtros y recarga listado
        private void btnLimpiarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                dtpInicio.Value = DateTime.Today.AddDays(-7);
                dtpFin.Value = DateTime.Today;
                dgvReservas.DataSource = null;
                CargarReservas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar el filtro: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Estilo visual consistente con otras grillas
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

            g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            g.ScrollBars = ScrollBars.Vertical;
        }

        // Ajustes por columna y botón de acción
        private void FormatearGrillaReservas()
        {
            if (_idReservaColumnName != null && dgvReservas.Columns.Contains(_idReservaColumnName))
                dgvReservas.Columns[_idReservaColumnName].Visible = false;

            if (dgvReservas.Columns.Contains("Fecha y Hora"))
                dgvReservas.Columns["Fecha y Hora"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (dgvReservas.Columns.Contains("Cajero"))
                dgvReservas.Columns["Cajero"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvReservas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReservas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReservas.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            if (!dgvReservas.Columns.Contains("Accion"))
            {
                var col = new DataGridViewButtonColumn
                {
                    Name = "Accion",
                    HeaderText = "Acción",
                    Text = "Eliminar",
                    UseColumnTextForButtonValue = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    FlatStyle = FlatStyle.Standard
                };
                dgvReservas.Columns.Add(col);
            }
            else
            {
                var c = dgvReservas.Columns["Accion"] as DataGridViewButtonColumn;
                c.Text = "Eliminar";
                c.UseColumnTextForButtonValue = true;
                c.FlatStyle = FlatStyle.Standard;
            }
        }

        // Maneja clicks en la columna "Accion" (Eliminar)
        private void dgvReservas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var col = dgvReservas.Columns[e.ColumnIndex];
            if (col == null || col.Name != "Accion") return;

            if (string.IsNullOrEmpty(_idReservaColumnName) || !dgvReservas.Columns.Contains(_idReservaColumnName))
            {
                MessageBox.Show("No se encontró la columna de identificador de reserva en el resultado. Ajuste el SP para incluir 'id_reserva'.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var row = dgvReservas.Rows[e.RowIndex];
            var val = row.Cells[_idReservaColumnName]?.Value?.ToString();

            if (!int.TryParse(val, out int idReserva))
            {
                MessageBox.Show("No se pudo obtener el identificador de la reserva.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmar = MessageBox.Show("¿Desea eliminar la reserva seleccionada?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes) return;

            try
            {
                var dao = new ReservaDAO();
                bool ok = dao.EliminarReserva(idReserva);
                if (ok)
                {
                    CargarReservas();
                    MessageBox.Show("Reserva eliminada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la reserva.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar reserva: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
