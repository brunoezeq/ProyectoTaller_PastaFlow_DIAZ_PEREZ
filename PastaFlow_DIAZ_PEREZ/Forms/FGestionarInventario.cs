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
    public partial class FGestionarInventario : Form
    {
        // -------------------------------
        // Límites de texto para los campos
        // -------------------------------
        private const int MAX_NOMBRE = 25;
        private const int MAX_DESCRIPCION = 100;

        // -------------------------------
        // Listas y conexión con la base
        // -------------------------------
        private readonly BindingList<Producto> _productos = new BindingList<Producto>();
        private readonly BindingList<Categoria> _categorias = new BindingList<Categoria>();
        private readonly BindingSource _bsProductos = new BindingSource();

        private readonly ProductoDao _productoDao = new ProductoDao();
        private readonly CategoriaDao _categoriaDao = new CategoriaDao();

        // Campo para alternar el orden
        private bool _ordenNombreAsc = true;
        private bool _ordenDescAsc = true;
        private bool _ordenPrecioAsc = false; // primero mayor->menor
        private bool _ordenStockAsc = true;
        private bool _ordenCategoriaAsc = true;
        private bool _ordenEstadoAsc = true;

        public FGestionarInventario()
        {
            InitializeComponent();

            // Configuración general del formulario
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // Eventos principales
            this.Load += FGestionarInventario_Load;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvProductos.CellFormatting += dgvProductos_CellFormatting;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            dgvProductos.ColumnHeaderMouseClick += dgvProductos_ColumnHeaderMouseClick; // ordenar al hacer click en encabezado

            // Búsqueda en tiempo real
            if (txtBuscarProducto != null)
                txtBuscarProducto.TextChanged += txtBuscarProducto_TextChanged;
        }

        // -------------------------------
        // Cuando se abre el formulario
        // -------------------------------
        private void FGestionarInventario_Load(object sender, EventArgs e)
        {
            // Desconectar el evento para evitar selección automática
            dgvProductos.SelectionChanged -= dgvProductos_SelectionChanged;

            ConfigurarGrillaVisual();
            ConfigurarGrillaDatos();

            _bsProductos.DataSource = _productos;
            dgvProductos.DataSource = _bsProductos;

            CargarCategorias();
            CargarProductos(true);

            LimpiarFormulario();
            dgvProductos.ClearSelection();
            dgvProductos.CurrentCell = null;

            // Foco en el primer campo
            this.ActiveControl = txtProdNombre;

            // Reconectar el evento después de la carga
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
        }

        // -------------------------------
        // Diseño visual del DataGridView
        // -------------------------------
        private void ConfigurarGrillaVisual()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

            // Estilo del texto
            dgvProductos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvProductos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.DefaultCellStyle.BackColor = Color.White;
            dgvProductos.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(170, 40, 40);
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 243, 230);

            // Encabezados
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProductos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 0, 0);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvProductos.ColumnHeadersHeight = 42;
        }

        // -------------------------------
        // Columnas de la tabla
        // -------------------------------
        private void ConfigurarGrillaDatos()
        {
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
            {
                Categoria.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            if (Estado != null)
            {
                Estado.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            if (dgvProductos.Columns["Accion"] == null)
            {
                var colAccion = new DataGridViewButtonColumn();
                colAccion.Name = "Accion";
                colAccion.HeaderText = "Acción";
                colAccion.UseColumnTextForButtonValue = false;
                colAccion.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvProductos.Columns.Add(colAccion);
            }
        }

        // -------------------------------
        // Cargo las categorías al ComboBox
        // -------------------------------
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

        // -------------------------------
        // Cargar productos
        // -------------------------------
        private void CargarProductos(bool incluirInactivos)
        {
            _productos.Clear();

            try
            {
                var datos = _productoDao.Listar(incluirInactivos);
                foreach (var p in datos) _productos.Add(p);
            }
            catch (Exception ex)
            {
                MostrarError("Error cargando productos: " + ex.Message);
            }

            AplicarFiltro();
        }

        // -------------------------------
        // Registrar un nuevo producto
        // -------------------------------
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
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MostrarError("Error al insertar producto: " + ex.Message);
            }
        }

        // -------------------------------
        // Editar un producto existente
        // -------------------------------
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

        // -------------------------------
        // Baja o restaurar producto
        // -------------------------------
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

        // -------------------------------
        // Muestra los datos al seleccionar
        // -------------------------------
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

        // -------------------------------
        // Formato visual del DataGridView
        // -------------------------------
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

            // Cambio visual si está inactivo
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

        // -------------------------------
        // Filtrar productos por nombre o categoría
        // -------------------------------
        private void txtBuscarProducto_TextChanged(object sender, EventArgs e) => AplicarFiltro();

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

        // -------------------------------
        // Limpia el formulario
        // -------------------------------
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

        // -------------------------------
        // Botón volver
        // -------------------------------
        private void btnVolver_Click(object sender, EventArgs e) => this.Close();

        // -------------------------------
        // Validaciones básicas
        // -------------------------------
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

        // -------------------------------
        // Mensajes
        // -------------------------------
        private void MostrarWarn(string msg) =>
            MessageBox.Show(msg, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        private void MostrarError(string msg) =>
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        private void MostrarInfo(string msg) =>
            MessageBox.Show(msg, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                bool asc = _ordenPrecioAsc; // false inicialmente => primer click Desc
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
    }
}
