using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FVerReportes : Form
    {
        public FVerReportes()
        {
            InitializeComponent();
        }

        private VentaDao ventaDao = new VentaDao();

        // ---- Paginación (10 ventas por página) ----
        private DataTable _ventasAll;                 // todas las filas (incluye detalles)
        private List<int> _ventaIds;                  // ids de venta distintos, ordenados
        private int _pageIndex = 1;
        private const int _pageSize = 10;
        private int _totalPages = 1;

        private void FVerReportes_Load(object sender, EventArgs e)
        {
            ConfigurarEstiloVisualReportes();

            // Asegura centrado después de cada enlace de datos
            dgvVentas.DataBindingComplete -= DgvVentas_DataBindingComplete;
            dgvVentas.DataBindingComplete += DgvVentas_DataBindingComplete;

            ConectarBotonesNavegacion();
            CargarVentas();
        }

        private void DgvVentas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CentrarColumnas();
        }

        private void CargarVentas()
        {
            try
            {
                // Evita columnas duplicadas: usa solo las del diseñador
                dgvVentas.AutoGenerateColumns = false;

                // Vincular columnas del diseñador a los campos del DataTable
                TryBind("id_venta");
                TryBind("numero_factura");
                TryBind("fecha_venta");
                TryBind("metodo_pago");
                TryBind("total_venta");
                TryBind("producto");
                TryBind("cantidad");
                TryBind("precio_unitario");
                TryBind("subtotal");

                // Unificar/estilizar columna de acción
                AsegurarColumnaAccion();

                // Cargar datos completos (todas las filas/detalles)
                _ventasAll = ventaDao.ObtenerReporteVentasConDetalles() ?? new DataTable();

                // Preparar ids de venta únicos y ordenados (más recientes primero)
                _ventaIds = _ventasAll.AsEnumerable()
                    .OrderByDescending(r => r.Field<DateTime>("fecha_venta"))
                    .ThenByDescending(r => r.Field<int>("id_venta"))
                    .Select(r => r.Field<int>("id_venta"))
                    .Distinct()
                    .ToList();

                // Calcular páginas
                _totalPages = Math.Max(1, (int)Math.Ceiling(_ventaIds.Count / (double)_pageSize));
                _pageIndex = 1;

                // Mostrar página actual
                CargarPaginaActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las ventas: " + ex.Message);
            }
        }

        private void CargarPaginaActual()
        {
            dgvVentas.DataSource = ConstruirTablaPagina(_pageIndex);
            FormatearColumnas();
            ActualizarUIControlesNavegacion();
        }

        private DataTable ConstruirTablaPagina(int page)
        {
            if (_ventasAll == null || _ventasAll.Rows.Count == 0)
                return _ventasAll?.Clone() ?? new DataTable();

            // Seleccionar los 10 ids de venta de la página
            var idsPagina = _ventaIds
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .ToHashSet();

            var rows = _ventasAll.AsEnumerable()
                .Where(r => idsPagina.Contains(r.Field<int>("id_venta")));

            return rows.Any() ? rows.CopyToDataTable() : _ventasAll.Clone();
        }

        private void TryBind(string colName)
        {
            if (dgvVentas.Columns.Contains(colName))
                dgvVentas.Columns[colName].DataPropertyName = colName;
        }

        private void AsegurarColumnaAccion()
        {
            // Si existe 'accion' del Designer, renombrarla a 'bGenerarPDF'
            if (dgvVentas.Columns.Contains("accion") && !dgvVentas.Columns.Contains("bGenerarPDF"))
            {
                var btn = dgvVentas.Columns["accion"] as DataGridViewButtonColumn;
                if (btn != null)
                {
                    btn.Name = "bGenerarPDF";
                    btn.HeaderText = "ACCIÓN";
                    btn.Text = "Generar PDF";
                    btn.UseColumnTextForButtonValue = true;
                    btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            else if (!dgvVentas.Columns.Contains("bGenerarPDF"))
            {
                var btn = new DataGridViewButtonColumn
                {
                    Name = "bGenerarPDF",
                    HeaderText = "ACCIÓN",
                    Text = "Generar PDF",
                    UseColumnTextForButtonValue = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                    FlatStyle = FlatStyle.Flat
                };
                dgvVentas.Columns.Add(btn);
            }

            // Estilo del botón
            var b = dgvVentas.Columns["bGenerarPDF"] as DataGridViewButtonColumn;
            if (b != null)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.DefaultCellStyle.BackColor = Color.LemonChiffon;
                b.DefaultCellStyle.ForeColor = Color.Black;
                b.DefaultCellStyle.SelectionBackColor = Color.FromArgb(170, 40, 40);
                b.DefaultCellStyle.SelectionForeColor = Color.White;
                b.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // centrar botón
                b.MinimumWidth = 110;
                b.ReadOnly = false;
            }
        }

        private void ConfigurarEstiloVisualReportes()
        {
            var g = dgvVentas;

            g.AutoGenerateColumns = false;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // ver todo el texto
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

            // Centrado general y envoltura de texto
            g.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            g.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            g.DefaultCellStyle.BackColor = Color.White;
            g.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 204, 204);
            g.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            g.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 243, 230);

            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 0, 0);
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = g.ColumnHeadersDefaultCellStyle.BackColor;
            g.ColumnHeadersDefaultCellStyle.SelectionForeColor = g.ColumnHeadersDefaultCellStyle.ForeColor;
            g.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            g.ReadOnly = true;
            g.EditMode = DataGridViewEditMode.EditProgrammatically;
            g.ScrollBars = ScrollBars.Vertical;
        }

        private void CentrarColumnas()
        {
            // Encabezados centrados
            dgvVentas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Celdas centradas y con wrap en TODAS las columnas
            foreach (DataGridViewColumn c in dgvVentas.Columns)
            {
                c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            // Botón centrado y clicable
            if (dgvVentas.Columns.Contains("bGenerarPDF"))
            {
                dgvVentas.Columns["bGenerarPDF"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVentas.Columns["bGenerarPDF"].ReadOnly = false;
            }
        }

        private void FormatearColumnas()
        {
            if (dgvVentas.Columns.Contains("id_venta"))
                dgvVentas.Columns["id_venta"].Visible = false;

            // Centrar explícitamente cada columna de datos
            TryAlign("numero_factura", DataGridViewContentAlignment.MiddleCenter);
            TryAlign("fecha_venta", DataGridViewContentAlignment.MiddleCenter);
            TryAlign("metodo_pago", DataGridViewContentAlignment.MiddleCenter);
            TryAlign("producto", DataGridViewContentAlignment.MiddleCenter);
            TryAlign("cantidad", DataGridViewContentAlignment.MiddleCenter);
            TryAlign("precio_unitario", DataGridViewContentAlignment.MiddleCenter);
            TryAlign("subtotal", DataGridViewContentAlignment.MiddleCenter);
            TryAlign("total_venta", DataGridViewContentAlignment.MiddleCenter);

            // Formato moneda (mantiene el centrado)
            TryFormatCurrency("precio_unitario");
            TryFormatCurrency("subtotal");
            TryFormatCurrency("total_venta");

            if (dgvVentas.Columns.Contains("bGenerarPDF"))
                dgvVentas.Columns["bGenerarPDF"].ReadOnly = false;
        }

        private void TryAlign(string col, DataGridViewContentAlignment align)
        {
            if (dgvVentas.Columns.Contains(col))
            {
                dgvVentas.Columns[col].DefaultCellStyle.Alignment = align;
                if (align == DataGridViewContentAlignment.MiddleRight)
                    dgvVentas.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void TryFormatCurrency(string col)
        {
            if (dgvVentas.Columns.Contains(col))
            {
                dgvVentas.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvVentas.Columns[col].DefaultCellStyle.Format = "C2";
                dgvVentas.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvVentas.Columns[e.ColumnIndex].Name == "bGenerarPDF")
            {
                string numeroFactura = dgvVentas.Rows[e.RowIndex].Cells["numero_factura"].Value.ToString();
                DateTime fecha = Convert.ToDateTime(dgvVentas.Rows[e.RowIndex].Cells["fecha_venta"].Value);
                string metodo = dgvVentas.Rows[e.RowIndex].Cells["metodo_pago"].Value.ToString();
                decimal total = Convert.ToDecimal(dgvVentas.Rows[e.RowIndex].Cells["total_venta"].Value);

                int idVenta = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["id_venta"].Value);
                DataTable dt = ((DataTable)dgvVentas.DataSource);
                var detalles = dt.AsEnumerable()
                    .Where(r => r.Field<int>("id_venta") == idVenta)
                    .CopyToDataTable();

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

        // ---- Navegación (usa los botones existentes si sus nombres son btnAtras/btnAdelante) ----
        private void btnAtras_Click(object sender, EventArgs e)
        {
            if (_pageIndex > 1)
            {
                _pageIndex--;
                CargarPaginaActual();
            }
        }

        private void btnAdelante_Click(object sender, EventArgs e)
        {
            if (_pageIndex < _totalPages)
            {
                _pageIndex++;
                CargarPaginaActual();
            }
        }

        // Suscribe los botones si existen en el formulario (evita depender del Designer)
        private void ConectarBotonesNavegacion()
        {
            var back = FindControl<Button>("btnAtras");
            if (back != null) back.Click += btnAtras_Click;

            var next = FindControl<Button>("btnAdelante");
            if (next != null) next.Click += btnAdelante_Click;
        }

        private void ActualizarUIControlesNavegacion()
        {
            var back = FindControl<Button>("btnAtras");
            var next = FindControl<Button>("btnAdelante");
            var lbl = FindControl<Label>("lblPagina"); // opcional

            if (back != null) back.Enabled = _pageIndex > 1;
            if (next != null) next.Enabled = _pageIndex < _totalPages;
            if (lbl != null) lbl.Text = $"Página {_pageIndex} de {_totalPages}";
        }

        private T FindControl<T>(string name) where T : Control =>
            this.Controls.Find(name, true).OfType<T>().FirstOrDefault();
    }
}

