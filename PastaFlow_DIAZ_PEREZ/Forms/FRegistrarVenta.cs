using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Models;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    // Registro de ventas:
    // - Autocompletado manual con lista emergente (ListBox) filtrada por texto
    // - Agrega productos con control anti doble Enter (timestamp)
    // - Acumula cantidades si el producto ya existe en la grilla
    // - Calcula total y aplica recargo según método dePago seleccionado
    // - Genera factura PDF y la abre al confirmar
    // - Permite eliminar items desde la columna de acción
    // - Bloquea edición directa de celdas para evitar inconsistencias
    public partial class FRegistrarVenta : Form
    {
        private readonly ProductoDao _productoDao = new ProductoDao();
        private List<Producto> _productosList = new List<Producto>();
        private List<string> _nombresProductos = new List<string>();
        private ListBox _lbSuggestions;

        // Control de doble llamada (Enter repetido)
        private DateTime _lastAddTimestamp = DateTime.MinValue;
        private string _lastAddedProductoNombre = null;

        public FRegistrarVenta()
        {
            InitializeComponent();
            this.Load += FRegistrarVenta_Load;
        }

        private void FRegistrarVenta_Load(object sender, EventArgs e)
        {
            ConfigurarGrillaVisualVenta();
            FormatearGrillaVenta();

            dgvDetalleVenta.ReadOnly = true;
            dgvDetalleVenta.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvDetalleVenta.CellBeginEdit += (s, ev) => { ev.Cancel = true; };

            // Cargar productos desde BD
            try
            {
                _productosList = _productoDao.Listar(true).ToList();
                _nombresProductos = _productosList
                    .Where(p => !string.IsNullOrWhiteSpace(p.nombre))
                    .Select(p => p.nombre.Trim())
                    .Distinct(StringComparer.CurrentCultureIgnoreCase)
                    .ToList();
            }
            catch
            {
                _productosList = new List<Producto>();
                _nombresProductos = new List<string>();
            }

            // ListBox dinámico de sugerencias
            _lbSuggestions = new ListBox
            {
                Visible = false,
                Height = 120,
                Width = Math.Max(250, textBuscarProducto.Width),
                Font = textBuscarProducto.Font,
                TabStop = false
            };
            var loc = textBuscarProducto.Location;
            _lbSuggestions.Location = new Point(loc.X, loc.Y + textBuscarProducto.Height + 2);
            _lbSuggestions.MouseClick += (s, ev) => SelectSuggestionFromList();
            _lbSuggestions.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter) { SelectSuggestionFromList(); ev.Handled = true; }
                else if (ev.KeyCode == Keys.Escape) { HideSuggestions(); textBuscarProducto.Focus(); }
            };
            pnlPrincipal.Controls.Add(_lbSuggestions);
            _lbSuggestions.BringToFront();

            // Eventos de búsqueda
            textBuscarProducto.TextChanged += textBuscarProducto_TextChanged;
            textBuscarProducto.KeyDown += textBuscarProducto_KeyDown;
            textBuscarProducto.LostFocus += (s, ev) =>
            {
                Task.Delay(120).ContinueWith(_ => BeginInvoke((Action)HideSuggestions));
            };

            // Botón agregar
            btnAgregar.Click += btnAgregar_Click;
            txtCantidad.Text = "1";

            CargarMetodosPago();
            cBoxMetodoPago.SelectedIndexChanged += cBoxMetodoPago_SelectedIndexChanged;
        }

        // Filtro de sugerencias
        private void textBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            string term = textBuscarProducto.Text?.Trim() ?? "";
            if (term.Length == 0)
            {
                HideSuggestions();
                return;
            }

            var matches = _productosList
                .Where(p => p.nombre.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) >= 0)
                .OrderBy(p => p.nombre)
                .Take(50)
                .Select(p => p.nombre)
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .ToArray();

            if (matches.Length == 0)
            {
                HideSuggestions();
                return;
            }

            _lbSuggestions.BeginUpdate();
            _lbSuggestions.Items.Clear();
            _lbSuggestions.Items.AddRange(matches);
            _lbSuggestions.EndUpdate();

            _lbSuggestions.Width = Math.Max(textBuscarProducto.Width, GetListBoxPreferredWidth(_lbSuggestions, matches) + 10);
            _lbSuggestions.Location = new Point(textBuscarProducto.Location.X, textBuscarProducto.Location.Y + textBuscarProducto.Height + 2);
            _lbSuggestions.Visible = true;
        }

        private void textBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (_lbSuggestions.Visible)
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (_lbSuggestions.Items.Count > 0)
                    {
                        _lbSuggestions.Focus();
                        _lbSuggestions.SelectedIndex = 0;
                        e.Handled = true;
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (_lbSuggestions.SelectedItem != null)
                    {
                        SelectSuggestionFromList();
                        btnAgregar_Click(this, EventArgs.Empty);
                    }
                    else if (_lbSuggestions.Items.Count == 1)
                    {
                        _lbSuggestions.SelectedIndex = 0;
                        SelectSuggestionFromList();
                        btnAgregar_Click(this, EventArgs.Empty);
                    }
                    else
                    {
                        if (_lbSuggestions.Items.Count > 0)
                        {
                            _lbSuggestions.Focus();
                            _lbSuggestions.SelectedIndex = 0;
                        }
                    }
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    HideSuggestions();
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnAgregar_Click(this, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void SelectSuggestionFromList()
        {
            if (_lbSuggestions.SelectedItem != null)
            {
                textBuscarProducto.Text = _lbSuggestions.SelectedItem.ToString();
                HideSuggestions();
                textBuscarProducto.Focus();
            }
        }

        private void HideSuggestions()
        {
            if (_lbSuggestions != null && _lbSuggestions.Visible)
                _lbSuggestions.Visible = false;
        }

        // Agregar producto a la grilla
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = textBuscarProducto.Text?.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                // Evita doble enter de un agregado previo
                if ((DateTime.UtcNow - _lastAddTimestamp).TotalMilliseconds < 700)
                    return;

                MessageBox.Show("Ingrese o seleccione un producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBuscarProducto.Focus();
                return;
            }

            int cantidad = 1;
            if (!int.TryParse(txtCantidad.Text.Trim(), out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida (>0).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidad.Focus();
                return;
            }

            var producto = _productosList.FirstOrDefault(p => string.Equals(p.nombre?.Trim(), nombre, StringComparison.CurrentCultureIgnoreCase))
                           ?? _productosList.FirstOrDefault(p => p.nombre.IndexOf(nombre, StringComparison.CurrentCultureIgnoreCase) >= 0);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado. Seleccione uno de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Si ya existe, acumular cantidad
            foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
            {
                var cellVal = Convert.ToString(row.Cells["producto"].Value);
                if (string.Equals(cellVal?.Trim(), producto.nombre?.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    int existQty = 0;
                    int.TryParse(Convert.ToString(row.Cells["cantidad"].Value), out existQty);
                    int newQty = existQty + cantidad;
                    row.Cells["cantidad"].Value = newQty.ToString();
                    decimal subtotal = producto.precio * newQty;
                    row.Cells["subtotal"].Value = subtotal.ToString("C", CultureInfo.CurrentCulture);
                    UpdateTotal();
                    ClearProductEntry();
                    _lastAddedProductoNombre = producto.nombre?.Trim();
                    _lastAddTimestamp = DateTime.UtcNow;
                    return;
                }
            }

            // Nueva fila
            decimal sub = producto.precio * cantidad;
            dgvDetalleVenta.Rows.Add(producto.nombre, cantidad.ToString(), producto.precio.ToString("C", CultureInfo.CurrentCulture), sub.ToString("C", CultureInfo.CurrentCulture), "Eliminar");
            UpdateTotal();
            ClearProductEntry();
            _lastAddedProductoNombre = producto.nombre?.Trim();
            _lastAddTimestamp = DateTime.UtcNow;
        }

        private void ClearProductEntry()
        {
            textBuscarProducto.Clear();
            txtCantidad.Text = "1";
            HideSuggestions();
            textBuscarProducto.Focus();
        }

        private void UpdateTotal()
        {
            decimal total = 0m;
            foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
            {
                if (row.IsNewRow) continue;
                var s = Convert.ToString(row.Cells["subtotal"].Value);
                if (decimal.TryParse(s, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal v))
                    total += v;
            }
            txtTotal.Text = total.ToString("C", CultureInfo.CurrentCulture);
            UpdateTotalConRecargo(); // refresca si hay método seleccionado
        }

        private void dgvDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvDetalleVenta.Columns[e.ColumnIndex].Name == "bEliminar")
            {
                dgvDetalleVenta.Rows.RemoveAt(e.RowIndex);
                UpdateTotal();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleVenta.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cBoxMetodoPago.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un método de pago.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var metodo = (Metodo_Pago)cBoxMetodoPago.SelectedItem;

            decimal totalBase = 0m;
            foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
            {
                if (row.IsNewRow) continue;
                string s = Convert.ToString(row.Cells["subtotal"].Value);
                if (decimal.TryParse(s, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal v))
                    totalBase += v;
            }

            decimal totalFinal = Math.Round(totalBase + (totalBase * metodo.Recargo / 100m), 2);

            string numeroFactura = $"F-{DateTime.Now:yyyyMMddHHmmss}";
            int idCaja = Session.CurrentCaja != null ? Session.CurrentCaja.Id_caja : 1;

            var dao = new VentaDao();
            try
            {
                int idVenta = dao.RegistrarVenta(idCaja, metodo.Id_metodo, totalBase, metodo.Recargo, totalFinal, numeroFactura);

                foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
                {
                    if (row.IsNewRow) continue;
                    string nombreProd = row.Cells["producto"].Value.ToString();
                    var prod = _productosList.FirstOrDefault(p => p.nombre == nombreProd);
                    int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);
                    dao.InsertarDetalleVenta(idVenta, prod.id_producto, cantidad, prod.precio);
                }

                DataTable dtProductos = new DataTable();
                dtProductos.Columns.Add("Producto", typeof(string));
                dtProductos.Columns.Add("Cantidad", typeof(int));
                dtProductos.Columns.Add("Subtotal", typeof(decimal));

                foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
                {
                    if (row.IsNewRow) continue;
                    string prodNom = row.Cells["producto"].Value.ToString();
                    int cant = Convert.ToInt32(row.Cells["cantidad"].Value);
                    decimal subtotal = 0;
                    decimal.TryParse(row.Cells["subtotal"].Value.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out subtotal);
                    dtProductos.Rows.Add(prodNom, cant, subtotal);
                }

                string logoPath = Path.Combine(Application.StartupPath, "Resources", "logo.png");
                string rutaSalida = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Factura_{numeroFactura}.pdf");

                PdfHelper.GenerarFacturaVenta(
                    nombreLocal: "Pasta Flow",
                    logoPath: logoPath,
                    numeroFactura: numeroFactura,
                    fecha: DateTime.Now,
                    cajero: $"{Session.CurrentUser.Nombre} {Session.CurrentUser.Apellido}",
                    productos: dtProductos,
                    totalVenta: totalFinal,
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

                MessageBox.Show($"Venta registrada correctamente.\nFactura: {numeroFactura}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDetalleVenta.Rows.Clear();
                txtTotal.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar venta: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) => Close();

        // Cargar métodos de pago al combo
        private void CargarMetodosPago()
        {
            var metodos = new List<Metodo_Pago>();
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT id_metodo, nombre, recargo FROM Metodo_Pago", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        metodos.Add(new Metodo_Pago
                        {
                            Id_metodo = (int)reader["id_metodo"],
                            Nombre = reader["nombre"].ToString(),
                            Recargo = reader.GetDecimal(reader.GetOrdinal("recargo"))
                        });
                    }
                }
            }

            cBoxMetodoPago.DataSource = metodos;
            cBoxMetodoPago.DisplayMember = "Nombre";
            cBoxMetodoPago.ValueMember = "Id_metodo";
            cBoxMetodoPago.SelectedIndex = -1;
        }

        private void cBoxMetodoPago_SelectedIndexChanged(object sender, EventArgs e) => UpdateTotalConRecargo();

        private void UpdateTotalConRecargo()
        {
            if (cBoxMetodoPago.SelectedItem is Metodo_Pago metodo)
            {
                decimal totalBase = 0m;
                foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
                {
                    if (row.IsNewRow) continue;
                    var s = Convert.ToString(row.Cells["subtotal"].Value);
                    if (decimal.TryParse(s, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal v))
                        totalBase += v;
                }

                decimal recargo = metodo.Recargo / 100;
                decimal totalFinal = totalBase + (totalBase * recargo);
                txtTotal.Text = totalFinal.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        // Validación de entrada para cantidad
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void pnlPrincipal_Paint(object sender, PaintEventArgs e) { }
        private void FRegistrarVenta_Load_1(object sender, EventArgs e) { }

        // Estética de la grilla de detalle
        private void ConfigurarGrillaVisualVenta()
        {
            var g = dgvDetalleVenta;
            g.AutoGenerateColumns = true;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.RowTemplate.Height = 36;
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

            g.ReadOnly = true;
            g.EditMode = DataGridViewEditMode.EditProgrammatically;
            g.ScrollBars = ScrollBars.Vertical;
        }

        private void FormatearGrillaVenta()
        {
            var g = dgvDetalleVenta;

            if (!g.Columns.Contains("bEliminar"))
            {
                var col = new DataGridViewButtonColumn
                {
                    Name = "bEliminar",
                    HeaderText = "",
                    Text = "Eliminar",
                    UseColumnTextForButtonValue = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    FlatStyle = FlatStyle.Flat
                };
                col.DefaultCellStyle.BackColor = Color.LemonChiffon;
                col.DefaultCellStyle.ForeColor = Color.Black;
                col.ReadOnly = false;
                g.Columns.Add(col);
            }
            else
            {
                g.Columns["bEliminar"].ReadOnly = false;
            }

            TryFormatColumn(g, "producto", DataGridViewContentAlignment.MiddleLeft, DataGridViewAutoSizeColumnMode.Fill, 200);
            TryFormatColumn(g, "Descripcion", DataGridViewContentAlignment.MiddleLeft, DataGridViewAutoSizeColumnMode.Fill, 200);
            TryFormatColumn(g, "cantidad", DataGridViewContentAlignment.MiddleCenter, DataGridViewAutoSizeColumnMode.AllCells, 80);
            TryFormatColumn(g, "preciounitario", DataGridViewContentAlignment.MiddleRight, DataGridViewAutoSizeColumnMode.AllCells, 110, "C2");
            TryFormatColumn(g, "Precio", DataGridViewContentAlignment.MiddleRight, DataGridViewAutoSizeColumnMode.AllCells, 110, "C2");
            TryFormatColumn(g, "subtotal", DataGridViewContentAlignment.MiddleRight, DataGridViewAutoSizeColumnMode.AllCells, 120, "C2");
            TryFormatColumn(g, "Total", DataGridViewContentAlignment.MiddleRight, DataGridViewAutoSizeColumnMode.AllCells, 140, "C2");

            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            g.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            if (g.Columns.Contains("Descripcion"))
                g.Columns["Descripcion"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            g.ClearSelection();
        }

        private void TryFormatColumn(DataGridView g, string colName, DataGridViewContentAlignment align,
                                     DataGridViewAutoSizeColumnMode mode, int fillWeight = 100, string format = null)
        {
            DataGridViewColumn c = null;
            foreach (DataGridViewColumn col in g.Columns)
            {
                if (string.Equals(col.Name, colName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(col.HeaderText, colName, StringComparison.OrdinalIgnoreCase) ||
                    col.Name.IndexOf(colName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    c = col;
                    break;
                }
            }
            if (c == null) return;

            c.DefaultCellStyle.Alignment = align;
            c.AutoSizeMode = mode;
            if (mode == DataGridViewAutoSizeColumnMode.Fill)
                c.FillWeight = fillWeight;
            if (!string.IsNullOrEmpty(format))
                c.DefaultCellStyle.Format = format;
            c.ReadOnly = true;
        }

        private int GetListBoxPreferredWidth(ListBox listBox, string[] items)
        {
            int maxWidth = 0;
            using (Graphics g = listBox.CreateGraphics())
            {
                foreach (var item in items)
                {
                    int itemWidth = (int)g.MeasureString(item, listBox.Font).Width;
                    if (itemWidth > maxWidth)
                        maxWidth = itemWidth;
                }
            }
            return maxWidth;
        }
    }
}
