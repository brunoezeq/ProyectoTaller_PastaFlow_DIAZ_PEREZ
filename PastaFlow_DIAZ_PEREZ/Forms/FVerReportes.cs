using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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

        // ---- Paginación 9venta por página ----
        private DataTable _ventasAll;                 
        private List<int> _ventaIds;                  
        private int _pageIndex = 1;
        private const int _pageSize = 9;
        private int _totalPages = 1;

        private readonly ContextMenuStrip _headerFilterMenu = new ContextMenuStrip();
        private readonly Dictionary<string, object> _colFilters = new Dictionary<string, object>();

        private void FVerReportes_Load(object sender, EventArgs e)
        {
            ConfigurarEstiloVisualReportes();

            dgvVentas.DataBindingComplete -= DgvVentas_DataBindingComplete;
            dgvVentas.DataBindingComplete += DgvVentas_DataBindingComplete;

            // Asegura que el click del boton se procese
            dgvVentas.CellContentClick -= dgvVentas_CellContentClick;
            dgvVentas.CellContentClick += dgvVentas_CellContentClick;

            // quitar cualquier pintado custom previo
            dgvVentas.CellPainting -= dgvVentas_CellPainting;

            ConectarBotonesNavegacion();
            ConectarFiltros();
            CargarVentas();
            InicializarRangoFechas();
        }

        // -- Tooltips para ver el contenido completo cuando hay muchas líneas --
        private void DgvVentas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CentrarColumnas();

            // Asignar tooltips con el contenido completo
            foreach (DataGridViewRow row in dgvVentas.Rows)
            {
                SetTip(row, "producto");
                SetTip(row, "cantidad");
                SetTip(row, "precio_unitario");
                SetTip(row, "subtotal");
            }
        }

        private void SetTip(DataGridViewRow row, string colName)
        {
            var col = GetCol(colName);
            if (col == null) return;
            var cell = row.Cells[col.Index];
            if (cell?.Value != null) cell.ToolTipText = cell.Value.ToString();
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
                AplicarEncabezadosMultilinea();

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

        // Centraliza ir a página y garantiza EXACTAMENTE 10 ventas por página
        private void CargarPaginaActual()
        {
            // recalcula límites por si cambió el total
            _totalPages = Math.Max(1, (int)Math.Ceiling((_ventaIds?.Count ?? 0) / (double)_pageSize));
            _pageIndex = Math.Min(Math.Max(_pageIndex, 1), _totalPages);

            // limpia el grid antes de enlazar para evitar residuos
            dgvVentas.SuspendLayout();
            dgvVentas.DataSource = null;
            dgvVentas.Rows.Clear();

            var pagina = ConstruirTablaPaginaCabecera(_pageIndex); // máx. 10 filas
            dgvVentas.DataSource = pagina;

            FormatearColumnas();
            AjustarColumnasPaginaCabecera();
            ActualizarUIControlesNavegacion();
            dgvVentas.ResumeLayout();
        }

        private DataTable ConstruirTablaPaginaCabecera(int page)
        {
            var cab = CrearEsquemaVentas();
            if (_ventasAll == null || _ventasAll.Rows.Count == 0 || _ventaIds == null || _ventaIds.Count == 0)
                return cab;

            // ids únicos ya ordenados; toma exactamente _pageSize para esta página
            var start = (page - 1) * _pageSize;
            var idsPagina = _ventaIds.Skip(start).Take(_pageSize).ToList();

            var nl = Environment.NewLine;

            foreach (var id in idsPagina)
            {
                var detalles = _ventasAll.AsEnumerable()
                    .Where(x => x.Field<int>("id_venta") == id)
                    .ToList();
                if (detalles.Count == 0) continue;

                var r0 = detalles[0];
                var nr = cab.NewRow();
                nr["id_venta"]       = r0["id_venta"];
                nr["numero_factura"] = r0["numero_factura"];
                nr["fecha_venta"]    = r0["fecha_venta"];
                nr["metodo_pago"]    = r0["metodo_pago"];
                nr["total_venta"]    = r0["total_venta"];

                nr["producto"]        = string.Join(nl, detalles.Select(d => d.Field<string>("producto")));
                nr["cantidad"]        = string.Join(nl, detalles.Select(d => d.Field<int>("cantidad").ToString()));
                nr["precio_unitario"] = string.Join(nl, detalles.Select(d => d.Field<decimal>("precio_unitario").ToString("C2")));
                nr["subtotal"]        = string.Join(nl, detalles.Select(d => d.Field<decimal>("subtotal").ToString("C2")));

                cab.Rows.Add(nr);
                if (cab.Rows.Count == _pageSize) break; // seguridad extra
            }

            return cab;
        }

        // 2) Esquema explícito por si _ventasAll viene vacío o nulo
        // -- Esquema explícito: detalle como string para permitir varias líneas
        private DataTable CrearEsquemaVentas()
        {
            var t = new DataTable();
            t.Columns.Add("id_venta", typeof(int));
            t.Columns.Add("numero_factura", typeof(string));
            t.Columns.Add("fecha_venta", typeof(DateTime));
            t.Columns.Add("metodo_pago", typeof(string));
            t.Columns.Add("total_venta", typeof(decimal));
            t.Columns.Add("producto", typeof(string));        // multi-línea
            t.Columns.Add("cantidad", typeof(string));        // multi-línea
            t.Columns.Add("precio_unitario", typeof(string)); // multi-línea
            t.Columns.Add("subtotal", typeof(string));        // multi-línea
            return t;
        }

        private void TryBind(string colName)
        {
            if (dgvVentas.Columns.Contains(colName))
                dgvVentas.Columns[colName].DataPropertyName = colName;
        }

        // Columna de acción con estilo estándar (sin diseño custom)
        private void AsegurarColumnaAccion()
        {
            if (dgvVentas.Columns.Contains("accion") && !dgvVentas.Columns.Contains("bGenerarPDF"))
            {
                var btn = dgvVentas.Columns["accion"] as DataGridViewButtonColumn;
                if (btn != null)
                {
                    btn.Name = "bGenerarPDF";
                    btn.HeaderText = "ACCIÓN";
                    btn.Text = "Generar PDF";
                    btn.UseColumnTextForButtonValue = true;
                    btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    btn.Width = 120;
                    btn.MinimumWidth = 120;
                    btn.Resizable = DataGridViewTriState.False;
                    btn.FlatStyle = FlatStyle.Standard; // estilo por defecto
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
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = 120,
                    MinimumWidth = 120,
                    Resizable = DataGridViewTriState.False,
                    FlatStyle = FlatStyle.Standard // estilo por defecto
                };
                dgvVentas.Columns.Add(btn);
            }

            var b = dgvVentas.Columns["bGenerarPDF"] as DataGridViewButtonColumn;
            if (b != null)
            {
                // restaurar estilos por defecto (sin colores custom)
                b.DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                };
                b.ReadOnly = false;
                b.DisplayIndex = dgvVentas.Columns.Count - 1;
            }
        }

        // -- Encabezado y celdas más chicas + tabla más compacta --
        private void ConfigurarEstiloVisualReportes()
        {
            var g = dgvVentas;

            g.AutoGenerateColumns = false;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            g.RowTemplate.Height = 30;
            g.RowTemplate.Resizable = DataGridViewTriState.False;

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

            g.DefaultCellStyle.Font = new Font("Segoe UI", 10F); // +1 punto
            g.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            g.DefaultCellStyle.BackColor = Color.White;
            g.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 204, 204);
            g.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            g.DefaultCellStyle.Padding = new Padding(2, 3, 2, 3);

            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 243, 230);

            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            g.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 0, 0);
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Evitar que el encabezado seleccionado se pinte de celeste
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = g.ColumnHeadersDefaultCellStyle.BackColor;
            g.ColumnHeadersDefaultCellStyle.SelectionForeColor = g.ColumnHeadersDefaultCellStyle.ForeColor;

            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            g.ReadOnly = true;
            g.EditMode = DataGridViewEditMode.EditProgrammatically;
            g.ScrollBars = ScrollBars.Vertical;
        }

        // No forzamos todas las celdas al centro; solo encabezado
        private void CentrarColumnas()
        {
            dgvVentas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var b = GetCol("bGenerarPDF");
            if (b != null) b.ReadOnly = false;
        }

        // -- Alineaciones, wrap y fuentes por columna para legibilidad compacta --
        private void FormatearColumnas()
        {
            var cId = GetCol("id_venta");
            if (cId != null) cId.Visible = false;

            TryAlign("numero_factura", DataGridViewContentAlignment.MiddleCenter);
            TryAlign("fecha_venta", DataGridViewContentAlignment.MiddleCenter);
            TryAlign("metodo_pago", DataGridViewContentAlignment.MiddleCenter);

            var colProducto = GetCol("producto");
            if (colProducto != null)
            {
                colProducto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colProducto.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                colProducto.DefaultCellStyle.Font = new Font("Segoe UI", 9F); // +1
            }

            var colCantidad = GetCol("cantidad");
            if (colCantidad != null)
            {
                colCantidad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colCantidad.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                colCantidad.DefaultCellStyle.Font = new Font("Segoe UI", 9F); // +1
            }

            var colPU = GetCol("precio_unitario");
            if (colPU != null)
            {
                colPU.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colPU.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                colPU.DefaultCellStyle.Font = new Font("Segoe UI", 9F); // +1
            }

            var colSub = GetCol("subtotal");
            if (colSub != null)
            {
                colSub.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colSub.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                colSub.DefaultCellStyle.Font = new Font("Segoe UI", 9F); // +1
            }

            var colTotal = GetCol("total_venta");
            if (colTotal != null)
            {
                colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colTotal.DefaultCellStyle.Format = "C2";
                colTotal.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                colTotal.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold); // +1
            }

            var b = GetCol("bGenerarPDF");
            if (b != null) b.ReadOnly = false;
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var col = dgvVentas.Columns[e.ColumnIndex];
            if (col == null) return;

            // Acepta por nombre o por tipo columna botón
            bool esBoton = col.Name == "bGenerarPDF" || col is DataGridViewButtonColumn;
            if (!esBoton) return;

            if (dgvVentas.Rows[e.RowIndex].Cells["id_venta"]?.Value == null)
            {
                MessageBox.Show("No se pudo obtener el id de la venta.");
                return;
            }

            int idVenta = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["id_venta"].Value);
            string numeroFactura = Convert.ToString(dgvVentas.Rows[e.RowIndex].Cells["numero_factura"]?.Value);
            DateTime fecha = Convert.ToDateTime(dgvVentas.Rows[e.RowIndex].Cells["fecha_venta"]?.Value);
            string metodo = Convert.ToString(dgvVentas.Rows[e.RowIndex].Cells["metodo_pago"]?.Value);
            decimal total = Convert.ToDecimal(dgvVentas.Rows[e.RowIndex].Cells["total_venta"]?.Value);

            // Detalles seguros
            var detRows = _ventasAll.AsEnumerable()
                .Where(r => r.Field<int>("id_venta") == idVenta)
                .ToList();
            if (detRows.Count == 0)
            {
                MessageBox.Show("No se encontraron detalles para la venta seleccionada.");
                return;
            }
            var detalles = detRows.CopyToDataTable();

            // Resolver logo principal (prueba en Recursos/Resources y .png/.jpg)
            var bases = new[]
            {
                Path.Combine(Application.StartupPath, "Recursos"),
                Path.Combine(Application.StartupPath, "Resources")
            };

            // Logo en encabezado
            string logoPath = bases
                .SelectMany(b => new[] { Path.Combine(b, "platopastas.png"), Path.Combine(b, "platopastas.jpg") })
                .FirstOrDefault(File.Exists);

            // Imagen extra opcional (aparece al final del PDF)
            string imagenExtra = bases
                .SelectMany(b => new[] { Path.Combine(b, "platopastas.png"), Path.Combine(b, "platopastas.jpg") })
                .FirstOrDefault(File.Exists);

            string rutaSalida = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"Factura_{numeroFactura}.pdf");

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
                    rutaSalida: rutaSalida,
                    imagenExtraPath: imagenExtra  
                );

                if (File.Exists(rutaSalida))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = rutaSalida,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("El PDF no se generó en la ruta esperada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message);
                Debug.WriteLine("[PDF] Error: " + ex);
            }
        }

        // ---- Navegación (usa los botones existentes si sus nombres son btnAtras/btnAdelante) ----
        // Añade trazas para confirmar que los clicks llegan y que hay páginas para navegar
        private void btnAtras_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[NAV] Atrás click. pageIndex={_pageIndex}, totalPages={_totalPages}");
            if (_pageIndex <= 1) return;
            _pageIndex--;
            CargarPaginaActual();
        }

        private void btnAdelante_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[NAV] Adelante click. pageIndex={_pageIndex}, totalPages={_totalPages}");
            if (_pageIndex >= _totalPages) return;
            _pageIndex++;
            CargarPaginaActual();
        }

        // Evita doble cableado y garantiza el correcto
        private void ConectarBotonesNavegacion()
        {
            var back = FindControl<Button>("btnAtras");
            if (back != null)
            {
                back.Enabled = true;
                back.Click -= btnAtras_Click;
                back.Click += btnAtras_Click;
            }

            var next = FindControl<Button>("btnAdelante");
            if (next != null)
            {
                next.Enabled = true;
                next.Click -= btnAdelante_Click;
                next.Click += btnAdelante_Click;
            }
        }

        // Refuerza el cálculo y el estado de los botones
        private void ActualizarUIControlesNavegacion()
        {
            var back = FindControl<Button>("btnAtras");
            var next = FindControl<Button>("btnAdelante");
            var lbl = FindControl<Label>("lblPagina"); // si existe

            _totalPages = Math.Max(1, (int)Math.Ceiling((_ventaIds?.Count ?? 0) / (double)_pageSize));
            if (back != null) back.Enabled = _pageIndex > 1 && _totalPages > 1;
            if (next != null) next.Enabled = _pageIndex < _totalPages;
            if (lbl != null) lbl.Text = $"Página {_pageIndex} de {_totalPages}";

            System.Diagnostics.Debug.WriteLine($"[NAV] Estado -> pageIndex={_pageIndex}, totalPages={_totalPages}, ventasUnicas={_ventaIds?.Count ?? 0}");
        }

        private T FindControl<T>(string name) where T : Control =>
            this.Controls.Find(name, true).OfType<T>().FirstOrDefault();

        // Llama a esto después de crear/asegurar columnas
        private void AplicarEncabezadosMultilinea()
        {
            SetHeader("numero_factura", "NRO.\nFACTURA");
            SetHeader("fecha_venta", "FECHA DE\nVENTA");
            SetHeader("metodo_pago", "MÉTODO DE\nPAGO");
            SetHeader("total_venta", "TOTAL\nVENTAS");
            SetHeader("producto", "PRODUCTO");
            SetHeader("cantidad", "CANT.");
            SetHeader("precio_unitario", "PRECIO\nUNITARIO");
            SetHeader("subtotal", "SUBTOTAL");
            SetHeader("bGenerarPDF", "ACCIÓN");

            dgvVentas.AutoResizeColumnHeadersHeight();

            // Mantiene el color rojo también cuando un encabezado recibe foco/selección
            NeutralizarSeleccionEncabezados();
        }

        private void SetHeader(string key, string text)
        {
            var c = GetCol(key);
            if (c == null) return;
            c.HeaderText = text;
            c.HeaderCell.Style.WrapMode = DataGridViewTriState.True;
        }

        private DataGridViewColumn GetCol(string colName)
        {
            return dgvVentas.Columns.Contains(colName) ? dgvVentas.Columns[colName] : null;
        }

        private void AjustarColumnasPaginaCabecera()
        {
            dgvVentas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void TryAlign(string colName, DataGridViewContentAlignment align)
        {
            var col = GetCol(colName);
            if (col != null)
                col.DefaultCellStyle.Alignment = align;
        }

        // Normaliza estilos de encabezados por columna y evita resaltado por ordenación
        private void NeutralizarSeleccionEncabezados()
        {
            var hb = dgvVentas.ColumnHeadersDefaultCellStyle.BackColor;
            var hf = dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor;

            foreach (DataGridViewColumn c in dgvVentas.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable; // evita highlight al ordenar
                c.HeaderCell.Style.BackColor = hb;
                c.HeaderCell.Style.ForeColor = hf;
                c.HeaderCell.Style.SelectionBackColor = hb;
                c.HeaderCell.Style.SelectionForeColor = hf;
            }
        }

        // Pintado uniforme del botón con paleta verde
        private void dgvVentas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvVentas.Columns[e.ColumnIndex].Name != "bGenerarPDF") return;

            e.Handled = true;

            e.PaintBackground(e.CellBounds, true);

            var cell = e.CellBounds;
            int fixedButtonHeight = 28;
            int fixedButtonWidth  = 120;
            int x = cell.X + (cell.Width  - fixedButtonWidth)  / 2;
            int y = cell.Y + (cell.Height - fixedButtonHeight) / 2;
            var rectButton = new Rectangle(x, y, fixedButtonWidth, fixedButtonHeight);

            Color back = Color.FromArgb(225, 245, 234); // verdecito claro
            Color border = Color.FromArgb(56, 142, 60); // verde borde
            Color text = Color.FromArgb(27, 94, 32);    // verde texto

            using (var path = RoundedRect(rectButton, 6))
            using (var b = new SolidBrush(back))
            using (var pen = new Pen(border))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(b, path);
                e.Graphics.DrawPath(pen, path);
            }

            var txt = "Generar PDF";
            TextRenderer.DrawText(
                e.Graphics, txt, new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                rectButton, text,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
            );
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Suscribe botones y opcionalmente filtra al cambiar fechas
        private void ConectarFiltros()
        {
            var btnBuscar = FindControl<Button>("btnBuscar");
            if (btnBuscar != null)
            {
                btnBuscar.Click -= btnBuscar_Click;
                btnBuscar.Click += btnBuscar_Click;
            }

            var btnLimpiar = FindControl<Button>("btnLimpiar");
            if (btnLimpiar != null)
            {
                btnLimpiar.Click -= btnLimpiar_Click;
                btnLimpiar.Click += btnLimpiar_Click;
            }

            var dpDesde = FindControl<DateTimePicker>("dtpDesde");
            var dpHasta = FindControl<DateTimePicker>("dtpHasta");
            if (dpDesde != null) { dpDesde.ValueChanged -= RangoFechas_ValueChanged; dpDesde.ValueChanged += RangoFechas_ValueChanged; }
    if (dpHasta != null) { dpHasta.ValueChanged -= RangoFechas_ValueChanged; dpHasta.ValueChanged += RangoFechas_ValueChanged; }

    // 🔹 Nuevo: botón Excel
    var btnExcel = FindControl<Button>("btnGenerarExcel");
    if (btnExcel != null)
    {
        btnExcel.Click -= btnGenerarExcel_Click;
        btnExcel.Click += btnGenerarExcel_Click;
    }
}

private void InicializarRangoFechas()
        {
            var dpDesde = FindControl<DateTimePicker>("dtpDesde");
            var dpHasta = FindControl<DateTimePicker>("dtpHasta");
            if (_ventasAll == null || _ventasAll.Rows.Count == 0 || dpDesde == null || dpHasta == null) return;

            var fechas = _ventasAll.AsEnumerable().Select(r => r.Field<DateTime>("fecha_venta").Date);
            var min = fechas.Min();
            var max = fechas.Max();

            // Asegura rango válido
            dpDesde.MinDate = new DateTime(1900, 1, 1);
            dpHasta.MinDate = new DateTime(1900, 1, 1);
            dpDesde.MaxDate = new DateTime(2100, 12, 31);
            dpHasta.MaxDate = new DateTime(2100, 12, 31);

            dpDesde.Value = min;
            dpHasta.Value = max;
        }

        // Reutiliza SOLO el filtrado por fechas (sin encabezados)
private void btnBuscar_Click(object sender, EventArgs e)
{
    var dpDesde = FindControl<DateTimePicker>("dtpDesde");
    var dpHasta = FindControl<DateTimePicker>("dtpHasta");
    if (dpDesde == null || dpHasta == null) return;

    var desde = dpDesde.Value.Date;
    var hasta = dpHasta.Value.Date;
    if (desde > hasta) { var t = desde; desde = hasta; hasta = t; dpDesde.Value = desde; dpHasta.Value = hasta; }

    AplicarFiltroPorRango(desde, hasta);
}

private void btnLimpiar_Click(object sender, EventArgs e)
{
    // NO usar _colFilters (eliminado)
    _ventaIds = _ventasAll.AsEnumerable()
        .OrderByDescending(r => r.Field<DateTime>("fecha_venta"))
        .ThenByDescending(r => r.Field<int>("id_venta"))
        .Select(r => r.Field<int>("id_venta"))
        .Distinct()
        .ToList();

    InicializarRangoFechas();
    _pageIndex = 1;
    CargarPaginaActual();
}

private void RangoFechas_ValueChanged(object sender, EventArgs e)
{
    var dpDesde = FindControl<DateTimePicker>("dtpDesde");
    var dpHasta = FindControl<DateTimePicker>("dtpHasta");
    if (dpDesde == null || dpHasta == null) return;
    if (dpDesde.Value.Date > dpHasta.Value.Date) return;

    AplicarFiltroPorRango(dpDesde.Value.Date, dpHasta.Value.Date);
}

private void AplicarFiltroPorRango(DateTime desde, DateTime hasta)
{
    if (_ventasAll == null) return;

    var filtradas = _ventasAll.AsEnumerable()
        .Where(r =>
        {
            var f = r.Field<DateTime>("fecha_venta").Date;
            return f >= desde && f <= hasta;
        });

    _ventaIds = filtradas
        .OrderByDescending(r => r.Field<DateTime>("fecha_venta"))
        .ThenByDescending(r => r.Field<int>("id_venta"))
        .Select(r => r.Field<int>("id_venta"))
        .Distinct()
        .ToList();

    _pageIndex = 1;
    CargarPaginaActual();
}

// Unifica el filtrado por fechas + encabezados y refresca la grilla
private void AplicarFiltroCompuesto(DateTime? desde = null, DateTime? hasta = null)
{
    if (_ventasAll == null) return;

    // rango desde los DateTimePicker si no se pasa explícito
    var dpDesde = FindControl<DateTimePicker>("dtpDesde");
    var dpHasta = FindControl<DateTimePicker>("dtpHasta");
    var fDesde = (desde ?? dpDesde?.Value.Date) ?? DateTime.MinValue;
    var fHasta = (hasta ?? dpHasta?.Value.Date) ?? DateTime.MaxValue;

    var q = _ventasAll.AsEnumerable()
        .Where(r =>
        {
            var f = r.Field<DateTime>("fecha_venta").Date;
            return f >= fDesde && f <= fHasta;
        });

    foreach (var kv in _colFilters)
    {
        var col = kv.Key;
        var val = kv.Value;

        if (col == "metodo_pago")
            q = q.Where(r => r.Field<string>("metodo_pago") == (string)val);
        else if (col == "numero_factura")
            q = q.Where(r => r.Field<string>("numero_factura") == (string)val);
        else if (col == "total_venta")
            q = q.Where(r => r.Field<decimal>("total_venta") == (decimal)val);
        else if (col == "producto")
            q = q.Where(r => r.Field<string>("producto") == (string)val); // coincide si la venta tiene ese producto
    }

    _ventaIds = q
        .OrderByDescending(r => r.Field<DateTime>("fecha_venta"))
        .ThenByDescending(r => r.Field<int>("id_venta"))
        .Select(r => r.Field<int>("id_venta"))
        .Distinct()
        .ToList();

    _pageIndex = 1;
    CargarPaginaActual();
}

// Muestra menú contextual de filtro al hacer click en el encabezado
private void dgvVentas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
{
    if (e.Button != MouseButtons.Left) return;
    if (e.ColumnIndex < 0) return;

    var col = dgvVentas.Columns[e.ColumnIndex];
    var colName = col.Name;

    // columnas filtrables
    var filtrables = new[] { "numero_factura", "metodo_pago", "producto", "total_venta" };
    if (!filtrables.Contains(colName)) return;

    MostrarMenuFiltro(colName, e.ColumnIndex);
}

private void MostrarMenuFiltro(string colName, int columnIndex)
{
    _headerFilterMenu.Items.Clear();

    // 1) opción "Todos" (sin filtro en esta columna)
    var todos = new ToolStripMenuItem("Todos");
    todos.Checked = !_colFilters.ContainsKey(colName);
    todos.Click += (s, e) =>
    {
        if (_colFilters.ContainsKey(colName)) _colFilters.Remove(colName);
        AplicarFiltroCompuesto();
    };
    _headerFilterMenu.Items.Add(todos);
    _headerFilterMenu.Items.Add(new ToolStripSeparator());

    // 2) valores únicos por columna
    IEnumerable<ToolStripItem> items = Enumerable.Empty<ToolStripItem>();

    // filas de cabecera (un registro por venta)
    var cabRows = _ventasAll?.AsEnumerable()
        .GroupBy(r => r.Field<int>("id_venta"))
        .Select(g => g.First()) ?? Enumerable.Empty<DataRow>();

    if (colName == "producto")
    {
        var valores = _ventasAll?.AsEnumerable()
            .Select(r => r.Field<string>("producto"))
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Distinct()
            .OrderBy(v => v) ?? Enumerable.Empty<string>();

        items = valores.Select(v =>
        {
            var it = new ToolStripMenuItem(v) { Checked = _colFilters.TryGetValue(colName, out var x) && (string)x == v };
            it.Click += (s, e) => { _colFilters[colName] = v; AplicarFiltroCompuesto(); };
            return (ToolStripItem)it;
        });
    }
    else if (colName == "metodo_pago")
    {
        var valores = cabRows
            .Select(r => r.Field<string>("metodo_pago"))
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Distinct()
            .OrderBy(v => v);

        items = valores.Select(v =>
        {
            var it = new ToolStripMenuItem(v) { Checked = _colFilters.TryGetValue(colName, out var x) && (string)x == v };
            it.Click += (s, e) => { _colFilters[colName] = v; AplicarFiltroCompuesto(); };
            return (ToolStripItem)it;
        });
    }
    else if (colName == "numero_factura")
    {
        var valores = cabRows
            .Select(r => r.Field<string>("numero_factura"))
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Distinct()
            .OrderBy(v => v);

        items = valores.Select(v =>
        {
            var it = new ToolStripMenuItem(v) { Checked = _colFilters.TryGetValue(colName, out var x) && (string)x == v };
            it.Click += (s, e) => { _colFilters[colName] = v; AplicarFiltroCompuesto(); };
            return (ToolStripItem)it;
        });
    }
    else if (colName == "total_venta")
    {
        var valores = cabRows
            .Select(r => r.Field<decimal>("total_venta"))
            .Distinct()
            .OrderBy(v => v);

        items = valores.Select(v =>
        {
            var texto = v.ToString("C2");
            var it = new ToolStripMenuItem(texto) { Checked = _colFilters.TryGetValue(colName, out var x) && (decimal)x == v };
            it.Click += (s, e) => { _colFilters[colName] = v; AplicarFiltroCompuesto(); };
            return (ToolStripItem)it;
        });
    }

    foreach (var it in items) _headerFilterMenu.Items.Add(it);

    _headerFilterMenu.Items.Add(new ToolStripSeparator());
    var limpiarTodo = new ToolStripMenuItem("Limpiar todos los filtros");
    limpiarTodo.Click += (s, e) => { _colFilters.Clear(); AplicarFiltroCompuesto(); };
    _headerFilterMenu.Items.Add(limpiarTodo);

    var rect = dgvVentas.GetCellDisplayRectangle(columnIndex, -1, true);
    _headerFilterMenu.Show(dgvVentas, new Point(rect.Left, rect.Bottom));
}

        private void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ventasAll == null || _ventasAll.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.");
                    return;
                }

                // Aplica el mismo filtrado por fecha + filtros de encabezado
                var dpDesde = FindControl<DateTimePicker>("dtpDesde");
                var dpHasta = FindControl<DateTimePicker>("dtpHasta");
                var fDesde = (dpDesde?.Value.Date) ?? DateTime.MinValue;
                var fHasta = (dpHasta?.Value.Date) ?? DateTime.MaxValue;

                var q = _ventasAll.AsEnumerable()
                    .Where(r =>
                    {
                        var f = r.Field<DateTime>("fecha_venta").Date;
                        return f >= fDesde && f <= fHasta;
                    });

                foreach (var kv in _colFilters)
                {
                    var col = kv.Key;
                    var val = kv.Value;

                    if (col == "metodo_pago")
                        q = q.Where(r => r.Field<string>("metodo_pago") == (string)val);
                    else if (col == "numero_factura")
                        q = q.Where(r => r.Field<string>("numero_factura") == (string)val);
                    else if (col == "total_venta")
                        q = q.Where(r => r.Field<decimal>("total_venta") == (decimal)val);
                    else if (col == "producto")
                        q = q.Where(r => r.Field<string>("producto") == (string)val);
                }

                var rows = q.ToList();
                if (rows.Count == 0)
                {
                    MessageBox.Show("No hay datos filtrados para exportar.");
                    return;
                }

                // Exporta a CSV (compatible con Excel)
                var sb = new StringBuilder();

                // Encabezados
                var sep = ';'; // Excel en es-ES abre mejor con ';'
                sb.AppendLine(string.Join(sep.ToString(), new[]
                {
                    "ID_VENTA","NRO_FACTURA","FECHA_VENTA","METODO_PAGO",
                    "PRODUCTO","CANTIDAD","PRECIO_UNITARIO","SUBTOTAL","TOTAL_VENTA"
                }));

                // Filas detalle (una por producto)
                foreach (var r in rows)
                {
                    var id = r.Field<int>("id_venta");
                    var nro = r.Field<string>("numero_factura");
                    var fecha = r.Field<DateTime>("fecha_venta").ToString("dd/MM/yyyy HH:mm");
                    var metodo = r.Field<string>("metodo_pago");
                    var prod = r.Field<string>("producto");
                    var cant = r.Field<int>("cantidad");
                    var pu = r.Field<decimal>("precio_unitario").ToString("0.00", CultureInfo.InvariantCulture);
                    var sub = r.Field<decimal>("subtotal").ToString("0.00", CultureInfo.InvariantCulture);
                    var tot = r.Field<decimal>("total_venta").ToString("0.00", CultureInfo.InvariantCulture);

                    sb.AppendLine(string.Join(sep.ToString(), new []
                    {
                        Csv(id.ToString()),
                        Csv(nro),
                        Csv(fecha),
                        Csv(metodo),
                        Csv(prod),
                        Csv(cant.ToString()),
                        Csv(pu),
                        Csv(sub),
                        Csv(tot)
                    }));
                }

                // Guardar archivo
                var defaultName = $"ReporteVentas_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                using (var sfd = new SaveFileDialog
                {
                    Title = "Guardar reporte de ventas",
                    FileName = defaultName,
                    Filter = "CSV (*.csv)|*.csv",
                    OverwritePrompt = true
                })
                {
                    if (sfd.ShowDialog(this) != DialogResult.OK) return;

                    File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)); // con BOM
                    MessageBox.Show("Reporte exportado correctamente.");
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a Excel: " + ex.Message);
                Debug.WriteLine("[EXCEL] Error: " + ex);
            }
        }

        private string Csv(string value)
        {
            if (value == null) return "";
            // Escapa comillas y fuerza comillas si contiene separador o salto de línea
            var needsQuotes = value.Contains(";") || value.Contains(",") || value.Contains("\n") || value.Contains("\r") || value.Contains("\"");
            if (needsQuotes)
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }
    }
}

