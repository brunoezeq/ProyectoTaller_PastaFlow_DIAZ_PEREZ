using PastaFlow_DIAZ_PEREZ.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FVerQuejas : Form
    {
        // Nombres detectados de columnas (rellenados al cargar)
        private string _colNombre;
        private string _colApellido;
        private string _colMotivo;
        private string _colDescripcion;
        private string _colFecha;
        private string _colId;

        public FVerQuejas()
        {
            InitializeComponent();

            // Enlazar botones del Designer a los handlers existentes
            if (btnFiltrar != null) btnFiltrar.Click += (s, e) => btnFiltrarQueja_Click(s, e);
            if (btnLimpiar != null) btnLimpiar.Click += (s, e) => btnLimpiarQuejas_Click(s, e);
            // NOTA: NO volver a suscribirse a dgvQuejas.CellContentClick aquí,
            // el Designer ya lo hace en FVerQuejas.Designer.cs

            // Aplicar estilo inicial para consistencia con Reservas/Inventario
            if (dgvQuejas != null) ConfigurarGrillaVisualReservas();
        }

        private void FVerQuejas_Load(object sender, EventArgs e)
        {
            CargarQuejas();
        }

        private void CargarQuejas(DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var dao = new QuejaDAO();
            DataTable dt = dao.BuscarQuejas(fechaInicio, fechaFin);

            if (dt == null || dt.Rows.Count == 0)
            {
                dgvQuejas.DataSource = null;
                return;
            }

            // detectar nombres de columnas en el DataTable (tolerante a distintas convenciones)
            _colId = FindColumn(dt, "id_queja", "idQueja", "id");
            _colNombre = FindColumn(dt, "nombre_cliente", "nombre", "nombre_cliente", "Nombre");
            _colApellido = FindColumn(dt, "apellido_cliente", "apellido", "Apellido");
            _colMotivo = FindColumn(dt, "motivo_queja", "motivo", "Motivo");
            _colDescripcion = FindColumn(dt, "descripcion_queja", "descripcion", "descripcion_queja", "Descripcion");
            _colFecha = FindColumn(dt, "fecha_hora_queja", "fecha", "fecha_hora", "Fecha");

            dgvQuejas.DataSource = dt;

            // Aplicar el mismo estilo y formato que usa FRegistrarReserva
            ConfigurarGrillaVisualReservas();
            FormatearGrillaReservas();

            // Renombrar encabezados legibles si existen
            TrySetHeader(_colNombre, "Nombre");
            TrySetHeader(_colApellido, "Apellido");
            TrySetHeader(_colMotivo, "Motivo");
            TrySetHeader(_colFecha, "Fecha y Hora");

            // Ocultar id interno si existe
            if (!string.IsNullOrEmpty(_colId) && dgvQuejas.Columns.Contains(_colId))
                dgvQuejas.Columns[_colId].Visible = false;

            // Añadir columna "Ver" para ver detalle (si no existe)
            if (!dgvQuejas.Columns.Contains("VerDetalle"))
            {
                var btnVer = new DataGridViewButtonColumn
                {
                    Name = "VerDetalle",
                    HeaderText = "Ver",
                    Text = "Ver",
                    UseColumnTextForButtonValue = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    FlatStyle = FlatStyle.Flat
                };
                btnVer.DefaultCellStyle.BackColor = Color.LightYellow;
                btnVer.DefaultCellStyle.ForeColor = Color.Black;
                dgvQuejas.Columns.Add(btnVer);
            }

            // Asegurar columna de acción (eliminar) exista y esté estilizada
            if (!dgvQuejas.Columns.Contains("Accion"))
            {
                var col = new DataGridViewButtonColumn
                {
                    Name = "Accion",
                    HeaderText = "Acción",
                    Text = "Eliminar",                        // mostrar texto "Eliminar"
                    UseColumnTextForButtonValue = true,       // asegurar que se use el texto
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    FlatStyle = FlatStyle.Standard
                };
                col.DefaultCellStyle.BackColor = Color.LemonChiffon;
                col.DefaultCellStyle.ForeColor = Color.Black;
                col.DefaultCellStyle.SelectionBackColor = Color.FromArgb(170, 40, 40);
                col.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvQuejas.Columns.Add(col);
            }

            dgvQuejas.ScrollBars = ScrollBars.Vertical;
            dgvQuejas.ClearSelection();
        }

        private void dgvQuejas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = dgvQuejas.Columns[e.ColumnIndex].Name;

            if (colName == "VerDetalle")
            {
                // Mostrar solo la descripción de la queja
                var row = dgvQuejas.Rows[e.RowIndex];
                string descripcion = GetCellValue(row, _colDescripcion);

                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("No hay descripción disponible para esta queja.", "Detalle de la queja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(descripcion, "Detalle de la queja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            if (colName == "Accion")
            {
                // Mantener la funcionalidad de eliminar queja si existe columna id
                int idQueja = -1;
                if (!string.IsNullOrEmpty(_colId) && dgvQuejas.Columns.Contains(_colId))
                {
                    idQueja = Convert.ToInt32(dgvQuejas.Rows[e.RowIndex].Cells[_colId].Value);
                }

                DialogResult dr = MessageBox.Show(
                    "¿Desea eliminar esta queja?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dr == DialogResult.Yes)
                {
                    if (idQueja > 0)
                    {
                        var dao = new QuejaDAO();
                        dao.EliminarQueja(idQueja);
                        MessageBox.Show("Queja eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarQuejas();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo determinar el id de la queja para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnFiltrarQueja_Click(object sender, EventArgs e)
        {
            DateTime? desde = dtpDesde.Value.Date;
            DateTime? hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);
            CargarQuejas(desde, hasta);
        }

        private void btnLimpiarQuejas_Click(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now;
            dtpHasta.Value = DateTime.Now;
            CargarQuejas();
        }

        // --- Copiado/adaptado desde FRegistrarReserva para obtener idéntico estilo ---

        private void ConfigurarGrillaVisualReservas()
        {
            var g = dgvQuejas;

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

            // Evitar que las celdas entren en modo edición con doble click o Tab
            g.ReadOnly = true;
            g.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void FormatearGrillaReservas()
        {
            // Ocultar id interno si existe
            if (!string.IsNullOrEmpty(_colId) && dgvQuejas.Columns.Contains(_colId))
                dgvQuejas.Columns[_colId].Visible = false;

            // Alinear campos específicos si existen
            if (dgvQuejas.Columns.Contains("Fecha y Hora"))
                dgvQuejas.Columns["Fecha y Hora"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (dgvQuejas.Columns.Contains("Cajero"))
                dgvQuejas.Columns["Cajero"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Aplicar formato general
            dgvQuejas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvQuejas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvQuejas.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Añadir/estilizar columna Acción si falta (se asegura en Cargar)
        }

        // --- Helpers para detección y lectura de columnas ---
        private string FindColumn(DataTable dt, params string[] candidates)
        {
            if (dt == null) return null;
            var cols = dt.Columns.Cast<DataColumn>();
            foreach (var c in candidates)
            {
                var match = cols.FirstOrDefault(x => string.Equals(x.ColumnName, c, StringComparison.OrdinalIgnoreCase));
                if (match != null) return match.ColumnName;
            }
            // buscar por subcadena
            foreach (var c in cols)
            {
                if (c.ColumnName.IndexOf("apellido", StringComparison.OrdinalIgnoreCase) >= 0) return c.ColumnName;
                if (c.ColumnName.IndexOf("nombre", StringComparison.OrdinalIgnoreCase) >= 0) return c.ColumnName;
                if (c.ColumnName.IndexOf("motivo", StringComparison.OrdinalIgnoreCase) >= 0) return c.ColumnName;
                if (c.ColumnName.IndexOf("descripcion", StringComparison.OrdinalIgnoreCase) >= 0) return c.ColumnName;
                if (c.ColumnName.IndexOf("fecha", StringComparison.OrdinalIgnoreCase) >= 0) return c.ColumnName;
                if (c.ColumnName.IndexOf("id_queja", StringComparison.OrdinalIgnoreCase) >= 0) return c.ColumnName;
            }
            return null;
        }

        private void TrySetHeader(string columnName, string headerText)
        {
            if (!string.IsNullOrEmpty(columnName) && dgvQuejas.Columns.Contains(columnName))
            {
                dgvQuejas.Columns[columnName].HeaderText = headerText;
            }
        }

        private string GetCellValue(DataGridViewRow row, string columnName)
        {
            if (string.IsNullOrEmpty(columnName) || !dgvQuejas.Columns.Contains(columnName)) return null;
            var cell = row.Cells[dgvQuejas.Columns[columnName].Index];
            return cell?.Value?.ToString();
        }
    }
}

