using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    // Pantalla de gestión de inventario:
    // - Lista productos con paginación y ordenamiento en memoria
    // - Permite alta, edición, baja lógica y restauración
    // - Búsqueda rápida por nombre o categoría
    // - Estilos visuales coherentes con el resto del sistema
    public partial class FGestionarInventario : Form
    {
        // Límites de texto para validaciones simples (UI)
        private const int MAX_NOMBRE = 25;
        private const int MAX_DESCRIPCION = 100;

        // Colecciones vinculadas al DataGridView (BindingList para notificar cambios)
        private readonly BindingList<Producto> _productos = new BindingList<Producto>();
        private readonly BindingList<Categoria> _categorias = new BindingList<Categoria>();
        private readonly BindingSource _bsProductos = new BindingSource();

        // Acceso a datos
        private readonly ProductoDao _productoDao = new ProductoDao();
        private readonly CategoriaDao _categoriaDao = new CategoriaDao();

        // Flags de orden para alternar asc/desc por columna
        private bool _ordenNombreAsc = true;
        private bool _ordenDescAsc = true;
        private bool _ordenPrecioAsc = false; // Comienza mayor->menor
        private bool _ordenStockAsc = true;
        private bool _ordenCategoriaAsc = true;
        private bool _ordenEstadoAsc = true;

        // Paginación en memoria (carga completa y se segmenta)
        private int _paginaActual = 1;
        private int _totalPaginas = 1;
        private const int _itemsPorPagina = 12;
        private List<Producto> _productosTodos = new List<Producto>(); // Lista completa para paginar/filtrar

        public FGestionarInventario()
        {
            InitializeComponent();

            // Ajustes generales del formulario
            this.DoubleBuffered = true; // reduce flicker al redibujar
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // Eventos clave del ciclo de vida y del grid
            this.Load += FGestionarInventario_Load;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvProductos.CellFormatting += dgvProductos_CellFormatting;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            dgvProductos.ColumnHeaderMouseClick += dgvProductos_ColumnHeaderMouseClick; // ordenar por encabezado

            // Búsqueda incremental
            if (txtBuscarProducto != null)
                txtBuscarProducto.TextChanged += txtBuscarProducto_TextChanged;
        }

        // Carga inicial: estilos, datos y estado de controles
        private void FGestionarInventario_Load(object sender, EventArgs e)
        {
            // Se quita para evitar que la selección inicial dispare carga al form
            dgvProductos.SelectionChanged -= dgvProductos_SelectionChanged;

            ConfigurarGrillaVisual();
            ConfigurarGrillaDatos();

            _bsProductos.DataSource = _productos;
            dgvProductos.DataSource = _bsProductos;

            CargarCategorias();
            CargarProductos(true); // incluye inactivos

            LimpiarFormulario();
            dgvProductos.ClearSelection();
            dgvProductos.CurrentCell = null;

            // Foco inicial en nombre
            this.ActiveControl = txtProdNombre;

            // Se reconecta después de la preparación
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;

            // Estilo de botones de navegación de página
            EstilizarBotonNavegacion(btnAtras);
            EstilizarBotonNavegacion(btnAdelante);
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            EstilizarBotonesNavegacion();
        }

        // ========== Estilo visual del DataGridView ==========
        private void ConfigurarGrillaVisual()
        {
            // Configuración general de apariencia y comportamiento
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvProductos.RowTemplate.Height = 38;

            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeColumns = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.MultiSelect = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProductos.GridColor = Color.FromArgb(230, 200, 190);
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.Cursor = Cursors.Hand;
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Estilo de celdas
            dgvProductos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvProductos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.DefaultCellStyle.BackColor = Color.White;
            dgvProductos.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 204, 204);
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            dgvProductos.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Estilo alternado y selección
            dgvProductos.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 204, 204);
            dgvProductos.RowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 243, 230);

            // Encabezados
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProductos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 0, 0);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvProductos.ColumnHeadersHeight = 42;
            dgvProductos.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvProductos.ColumnHeadersDefaultCellStyle.BackColor;
            dgvProductos.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor;
        }

        // ========== Enlace de columnas a propiedades y estilos específicos ==========
        private void ConfigurarGrillaDatos()
        {
            // Asignación de DataPropertyName y modo de ordenamiento programático
            if (nombreProd != null)
            {
                nombreProd.DataPropertyName = "nombre";
                nombreProd.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            if (DescProd != null)
            {
                DescProd.DataPropertyName = "descripcion";
                DescProd.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            if (Precio != null)
            {
                Precio.DataPropertyName = "precio";
                Precio.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            if (Stock != null)
            {
                Stock.DataPropertyName = "stock";
                Stock.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            if (Categoria != null)
                Categoria.SortMode = DataGridViewColumnSortMode.Programmatic;
            if (Estado != null)
                Estado.SortMode = DataGridViewColumnSortMode.Programmatic;

            // Columna dinámica para acciones (baja/restaurar)
            if (dgvProductos.Columns["Accion"] == null)
            {
                var colAccion = new DataGridViewButtonColumn
                {
                    Name = "Accion",
                    HeaderText = "ACCION",
                    UseColumnTextForButtonValue = false,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dgvProductos.Columns.Add(colAccion);
            }

            var ar = CultureInfo.GetCultureInfo("es-AR");

            // Ajustes de ancho y formato por columna
            if (nombreProd != null)
            {
                nombreProd.FillWeight = 180;
                nombreProd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            if (DescProd != null)
            {
                DescProd.FillWeight = 220;
                DescProd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                DescProd.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Permite varias líneas
            }
            if (Precio != null)
            {
                Precio.FillWeight = 95;
                Precio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                Precio.DefaultCellStyle.Format = "C";
                Precio.DefaultCellStyle.FormatProvider = ar;
            }
            if (Stock != null)
            {
                Stock.FillWeight = 75;
                Stock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Stock.DefaultCellStyle.Format = "N0"; // Entero sin decimales
            }
            if (Categoria != null)
            {
                Categoria.FillWeight = 120;
                Categoria.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            if (Estado != null)
            {
                Estado.FillWeight = 90;
                Estado.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            var colAccionRef = dgvProductos.Columns["Accion"] as DataGridViewButtonColumn;
            if (colAccionRef != null)
            {
                colAccionRef.FlatStyle = FlatStyle.Popup;
                colAccionRef.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colAccionRef.Width = 90;
                colAccionRef.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colAccionRef.DefaultCellStyle.Padding = new Padding(0, 4, 0, 4);
            }
        }

        // Carga de categorías en combo (manteniendo referencia a objetos)
        private void CargarCategorias()
        {
            _categorias.Clear();
            try
            {
                List<Categoria> datos = _categoriaDao.Listar();
                foreach (var c in datos) _categorias.Add(c);
            }
            catch (Exception ex)
            {
                MostrarError("Error cargando categorías: " + ex.Message);
            }

            cBoxProdCat.DisplayMember = "nombre_categoria";
            cBoxProdCat.ValueMember = "id_categoria";
            cBoxProdCat.DataSource = _categorias;
            cBoxProdCat.SelectedIndex = _categorias.Count > 0 ? 0 : -1;
        }

        // Carga base de productos (completa) y reinicia paginación
        private void CargarProductos(bool incluirInactivos)
        {
            _productosTodos.Clear();
            try
            {
                var datos = _productoDao.Listar(incluirInactivos);
                _productosTodos = datos.ToList();
            }
            catch (Exception ex)
            {
                MostrarError("Error cargando productos: " + ex.Message);
            }
            _paginaActual = 1;
            ActualizarPaginacion();
        }

        // Calcula páginas y llena la lista visible para la página actual
        private void ActualizarPaginacion()
        {
            int totalItems = _productosTodos.Count;
            _totalPaginas = (int)Math.Ceiling(totalItems / (double)_itemsPorPagina);
            if (_paginaActual < 1) _paginaActual = 1;
            if (_paginaActual > _totalPaginas) _paginaActual = _totalPaginas;

            var productosPagina = _productosTodos
                .Skip((_paginaActual - 1) * _itemsPorPagina)
                .Take(_itemsPorPagina)
                .ToList();

            _productos.Clear();
            foreach (var p in productosPagina) _productos.Add(p);
            _bsProductos.ResetBindings(false);

            btnAtras.Enabled = _paginaActual > 1;
            btnAdelante.Enabled = _paginaActual < _totalPaginas;
        }

        // Alta de producto
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            decimal precio;
            int stock;
            if (!ValidarProducto(out precio, out stock)) return;

            if (cBoxProdCat.SelectedItem == null)
            {
                MostrarWarn("Seleccione una categoría.");
                return;
            }

            var prod = new Producto
            {
                nombre = txtProdNombre.Text.Trim(),
                descripcion = txtProdDesc.Text.Trim(),
                precio = precio,
                stock = stock,
                estado = true,
                id_categoria = (Categoria)cBoxProdCat.SelectedItem
            };

            try
            {
                prod.id_producto = _productoDao.Insertar(prod);
                _productos.Add(prod);
                _bsProductos.ResetBindings(false);
                LimpiarFormulario();
                dgvProductos.ClearSelection();
                AplicarFiltro(); // Reaplica filtro activo (si hubiera texto)
            }
            catch (Exception ex)
            {
                MostrarError("Error al insertar producto: " + ex.Message);
            }
        }

        // Edición de producto seleccionado
        private void btnEditar_Click(object sender, EventArgs e)
        {
            var prodSel = dgvProductos.CurrentRow?.DataBoundItem as Producto;
            if (prodSel == null)
            {
                MostrarInfo("Seleccione un producto de la lista.");
                return;
            }

            decimal precio;
            int stock;
            if (!ValidarProducto(out precio, out stock)) return;

            if (cBoxProdCat.SelectedItem == null)
            {
                MostrarWarn("Seleccione una categoría.");
                return;
            }

            prodSel.nombre = txtProdNombre.Text.Trim();
            prodSel.descripcion = txtProdDesc.Text.Trim();
            prodSel.precio = precio;
            prodSel.stock = stock;
            prodSel.id_categoria = (Categoria)cBoxProdCat.SelectedItem;

            try
            {
                _productoDao.Actualizar(prodSel);
                _bsProductos.ResetCurrentItem();
                LimpiarFormulario();
                dgvProductos.ClearSelection();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MostrarError("Error al actualizar producto: " + ex.Message);
            }
        }

        // Baja lógica o restauración según estado
        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProductos.Columns[e.ColumnIndex].Name != "Accion") return;

            var prod = dgvProductos.Rows[e.RowIndex].DataBoundItem as Producto;
            if (prod == null) return;

            if (prod.estado)
            {
                if (MessageBox.Show($"¿Dar de baja el producto '{prod.nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                try
                {
                    _productoDao.EliminarLogico(prod.id_producto);
                    prod.estado = false;
                    _bsProductos.ResetCurrentItem();
                }
                catch (Exception ex)
                {
                    MostrarError("Error al dar de baja: " + ex.Message);
                }
            }
            else
            {
                if (MessageBox.Show($"¿Restaurar el producto '{prod.nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                try
                {
                    _productoDao.Restaurar(prod.id_producto);
                    prod.estado = true;
                    _bsProductos.ResetCurrentItem();
                }
                catch (Exception ex)
                {
                    MostrarError("Error al restaurar: " + ex.Message);
                }
            }
        }

        // Carga datos del producto seleccionado al formulario (edición)
        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            var p = dgvProductos.CurrentRow?.DataBoundItem as Producto;
            if (p == null) return;

            txtProdNombre.Text = p.nombre;
            txtProdDesc.Text = p.descripcion;
            txtProdPrecio.Text = p.precio.ToString(CultureInfo.CurrentCulture);
            txtProdStock.Text = p.stock.ToString(CultureInfo.InvariantCulture);

            if (p.id_categoria != null)
            {
                var match = _categorias.FirstOrDefault(c => c.id_categoria == p.id_categoria.id_categoria);
                cBoxProdCat.SelectedItem = match;
            }
            else
            {
                cBoxProdCat.SelectedIndex = -1;
            }
        }

        // Formato condicional por fila/columna (estado, acción, estilos)
        private void dgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var item = dgvProductos.Rows[e.RowIndex].DataBoundItem as Producto;
            if (item == null) return;

            string colName = dgvProductos.Columns[e.ColumnIndex].Name;

            if (colName == "Categoria")
                e.Value = item.id_categoria?.nombre_categoria ?? "";
            else if (colName == "Estado")
                e.Value = item.estado ? "Activo" : "Inactivo";
            else if (colName == "Accion")
                e.Value = item.estado ? "Dar baja" : "Restaurar";

            // Badge visual para estado
            if (colName == "Estado")
            {
                if (item.estado)
                {
                    e.CellStyle.BackColor = Color.FromArgb(222, 247, 231);
                    e.CellStyle.ForeColor = Color.FromArgb(25, 135, 84);
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(240, 240, 240);
                    e.CellStyle.ForeColor = Color.DimGray;
                }
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Estilo fila si está inactivo (gris y cursiva)
            var row = dgvProductos.Rows[e.RowIndex];
            if (!item.estado)
            {
                row.DefaultCellStyle.ForeColor = Color.DimGray;
                row.DefaultCellStyle.Font = new Font(dgvProductos.Font, FontStyle.Italic);
            }
            else
            {
                row.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
                row.DefaultCellStyle.Font = new Font(dgvProductos.Font, FontStyle.Regular);
            }
        }

        // Búsqueda incremental
        private void txtBuscarProducto_TextChanged(object sender, EventArgs e) => AplicarFiltro();

        // Aplica filtro sobre la lista visible (no recarga de base)
        private void AplicarFiltro()
        {
            string termino = txtBuscarProducto?.Text?.Trim().ToLower() ?? "";
            if (termino.Length == 0)
            {
                _bsProductos.DataSource = _productos;
                _bsProductos.ResetBindings(false);
                return;
            }

            var filtrados = _productos
                .Where(p => (p.nombre ?? "").ToLower().Contains(termino) ||
                            (p.id_categoria != null && (p.id_categoria.nombre_categoria ?? "").ToLower().Contains(termino)))
                .ToList();

            _bsProductos.DataSource = new BindingList<Producto>(filtrados);
            _bsProductos.ResetBindings(false);
        }

        // Limpia campos de entrada
        private void LimpiarFormulario()
        {
            txtProdNombre.Clear();
            txtProdDesc.Clear();
            txtProdPrecio.Clear();
            txtProdStock.Clear();
            cBoxProdCat.SelectedIndex = -1;
            txtProdNombre.Focus();
        }

        private void btnLimpiarForm_Click(object sender, EventArgs e)
        {
            try
            {
                dgvProductos.SelectionChanged -= dgvProductos_SelectionChanged;
                AplicarFiltro();
                _bsProductos.Position = -1;
                dgvProductos.ClearSelection();
                dgvProductos.CurrentCell = null;
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MostrarError("Error al limpiar: " + ex.Message);
            }
            finally
            {
                dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            }
        }

        // Cerrar pantalla
        private void btnVolver_Click(object sender, EventArgs e) => this.Close();

        // Navegación paginación: atrás
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaActual > 1)
            {
                _paginaActual--;
                ActualizarPaginacion();
            }
        }

        // Navegación paginación: adelante
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (_paginaActual < _totalPaginas)
            {
                _paginaActual++;
                ActualizarPaginacion();
            }
        }

        // Validaciones básicas de campos numéricos y texto
        private bool ValidarProducto(out decimal precio, out int stock)
        {
            precio = 0m;
            stock = 0;

            string nombre = (txtProdNombre.Text ?? "").Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MostrarWarn("Debe ingresar un nombre.");
                txtProdNombre.Focus();
                return false;
            }

            string desc = (txtProdDesc.Text ?? "").Trim();
            if (string.IsNullOrEmpty(desc))
            {
                MostrarWarn("Debe ingresar una descripción.");
                txtProdDesc.Focus();
                return false;
            }

            if (!decimal.TryParse(txtProdPrecio.Text, out precio) || precio <= 0)
            {
                MostrarWarn("Ingrese un precio válido.");
                txtProdPrecio.Focus();
                return false;
            }

            if (!int.TryParse(txtProdStock.Text, out stock) || stock < 0)
            {
                MostrarWarn("Ingrese un stock válido.");
                txtProdStock.Focus();
                return false;
            }

            return true;
        }

        // Estilo botón navegación beige/vino
        private void EstilizarBotonNavegacion(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.FromArgb(128, 0, 0);
            btn.BackColor = Color.FromArgb(245, 237, 200);
            btn.ForeColor = Color.FromArgb(60, 25, 15);
            btn.Font = new Font("Segoe UI Semibold", 10F);

            btn.Region = System.Drawing.Region.FromHrgn(
                CreateRoundRectRgn(2, 2, btn.Width, btn.Height, 8, 8)
            );

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(230, 215, 170);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(245, 237, 200);
        }

        // Estilo alternativo para botones atrás / adelante (rojo oscuro)
        private void EstilizarBotonesNavegacion()
        {
            Color fondo = Color.DarkRed;
            Color texto = Color.White;
            Color borde = Color.FromArgb(100, 0, 0);

            var botones = new[] { btnAtras, btnAdelante };
            foreach (var btn in botones)
            {
                if (btn == null) continue;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 2;
                btn.FlatAppearance.BorderColor = borde;
                btn.BackColor = fondo;
                btn.ForeColor = texto;
                btn.Font = new Font("Segoe UI Semibold", 10F);

                btn.Region = System.Drawing.Region.FromHrgn(
                    CreateRoundRectRgn(2, 2, btn.Width, btn.Height, 12, 12)
                );

                btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(139, 0, 0);
                btn.MouseLeave += (s, e) => btn.BackColor = fondo;
            }
        }

        // Win32 para esquinas redondeadas
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse
        );

        // Helpers de mensajes
        private void MostrarWarn(string msg) =>
            MessageBox.Show(msg, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        private void MostrarError(string msg) =>
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        private void MostrarInfo(string msg) =>
            MessageBox.Show(msg, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Ordenamiento manual por encabezado: aplica sort en la colección visible
        private void dgvProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var visible = _bsProductos.DataSource as IEnumerable<Producto> ?? _productos;
            IEnumerable<Producto> ordenada = visible;
            SortOrder glyph;

            if (e.ColumnIndex == nombreProd.Index)
            {
                bool asc = _ordenNombreAsc;
                ordenada = asc
                    ? visible.OrderBy(p => p.nombre ?? string.Empty, StringComparer.CurrentCultureIgnoreCase)
                    : visible.OrderByDescending(p => p.nombre ?? string.Empty, StringComparer.CurrentCultureIgnoreCase);
                _ordenNombreAsc = !asc;
                glyph = asc ? SortOrder.Ascending : SortOrder.Descending;
                nombreProd.HeaderCell.SortGlyphDirection = glyph;
                DescProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                Precio.HeaderCell.SortGlyphDirection = SortOrder.None;
                Stock.HeaderCell.SortGlyphDirection = SortOrder.None;
                Categoria.HeaderCell.SortGlyphDirection = SortOrder.None;
                Estado.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            else if (e.ColumnIndex == DescProd.Index)
            {
                bool asc = _ordenDescAsc;
                ordenada = asc
                    ? visible.OrderBy(p => p.descripcion ?? string.Empty, StringComparer.CurrentCultureIgnoreCase)
                    : visible.OrderByDescending(p => p.descripcion ?? string.Empty, StringComparer.CurrentCultureIgnoreCase);
                _ordenDescAsc = !asc;
                glyph = asc ? SortOrder.Ascending : SortOrder.Descending;
                nombreProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                DescProd.HeaderCell.SortGlyphDirection = glyph;
                Precio.HeaderCell.SortGlyphDirection = SortOrder.None;
                Stock.HeaderCell.SortGlyphDirection = SortOrder.None;
                Categoria.HeaderCell.SortGlyphDirection = SortOrder.None;
                Estado.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            else if (e.ColumnIndex == Precio.Index)
            {
                bool asc = _ordenPrecioAsc;
                ordenada = asc
                    ? visible.OrderBy(p => p.precio)
                    : visible.OrderByDescending(p => p.precio);
                _ordenPrecioAsc = !asc;
                glyph = asc ? SortOrder.Ascending : SortOrder.Descending;
                nombreProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                DescProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                Precio.HeaderCell.SortGlyphDirection = glyph;
                Stock.HeaderCell.SortGlyphDirection = SortOrder.None;
                Categoria.HeaderCell.SortGlyphDirection = SortOrder.None;
                Estado.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            else if (e.ColumnIndex == Stock.Index)
            {
                bool asc = _ordenStockAsc;
                ordenada = asc
                    ? visible.OrderBy(p => p.stock)
                    : visible.OrderByDescending(p => p.stock);
                _ordenStockAsc = !asc;
                glyph = asc ? SortOrder.Ascending : SortOrder.Descending;
                nombreProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                DescProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                Precio.HeaderCell.SortGlyphDirection = SortOrder.None;
                Stock.HeaderCell.SortGlyphDirection = glyph;
                Categoria.HeaderCell.SortGlyphDirection = SortOrder.None;
                Estado.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            else if (e.ColumnIndex == Categoria.Index)
            {
                bool asc = _ordenCategoriaAsc;
                ordenada = asc
                    ? visible.OrderBy(p => p.id_categoria?.nombre_categoria ?? string.Empty, StringComparer.CurrentCultureIgnoreCase)
                    : visible.OrderByDescending(p => p.id_categoria?.nombre_categoria ?? string.Empty, StringComparer.CurrentCultureIgnoreCase);
                _ordenCategoriaAsc = !asc;
                glyph = asc ? SortOrder.Ascending : SortOrder.Descending;
                nombreProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                DescProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                Precio.HeaderCell.SortGlyphDirection = SortOrder.None;
                Stock.HeaderCell.SortGlyphDirection = SortOrder.None;
                Categoria.HeaderCell.SortGlyphDirection = glyph;
                Estado.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            else if (e.ColumnIndex == Estado.Index)
            {
                bool asc = _ordenEstadoAsc;
                ordenada = asc
                    ? visible.OrderBy(p => p.estado)               // Inactivo->Activo
                    : visible.OrderByDescending(p => p.estado);     // Activo->Inactivo
                _ordenEstadoAsc = !asc;
                glyph = asc ? SortOrder.Ascending : SortOrder.Descending;
                nombreProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                DescProd.HeaderCell.SortGlyphDirection = SortOrder.None;
                Precio.HeaderCell.SortGlyphDirection = SortOrder.None;
                Stock.HeaderCell.SortGlyphDirection = SortOrder.None;
                Categoria.HeaderCell.SortGlyphDirection = SortOrder.None;
                Estado.HeaderCell.SortGlyphDirection = glyph;
            }
            else
            {
                return;
            }

            // Suspender selección para no disparar eventos mientras se reasigna la fuente
            dgvProductos.SelectionChanged -= dgvProductos_SelectionChanged;
            try
            {
                _bsProductos.DataSource = new BindingList<Producto>(ordenada.ToList());
                _bsProductos.ResetBindings(false);
                dgvProductos.ClearSelection();
                dgvProductos.CurrentCell = null;
            }
            finally
            {
                dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            }
        }

        private void panelLateral_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
