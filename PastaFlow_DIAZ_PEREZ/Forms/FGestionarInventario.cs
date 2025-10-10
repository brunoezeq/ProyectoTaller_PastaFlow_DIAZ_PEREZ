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
        // ----------------------------------------------------------
        // Reglas de negocio
        // ----------------------------------------------------------
        private const int MAX_NOMBRE = 25;
        private const int MAX_DESCRIPCION = 100;

        // ----------------------------------------------------------
        // Datos y DAOs 
        // ----------------------------------------------------------
        private readonly BindingList<Producto> _productos = new BindingList<Producto>();
        private readonly BindingList<Categoria> _categorias = new BindingList<Categoria>();
        private readonly BindingSource _bsProductos = new BindingSource();

        private readonly ProductoDao _productoDao = new ProductoDao();
        private readonly CategoriaDao _categoriaDao = new CategoriaDao();

        public FGestionarInventario()
        {
            InitializeComponent();

            // Estilo general del formulario
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.BackColor = Color.FromArgb(255, 250, 230);

            // Eventos de ciclo de vida y grilla
            this.Load += FGestionarInventario_Load;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            dgvProductos.CellFormatting += dgvProductos_CellFormatting;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;

            // Búsqueda en tiempo real (opcional solicitado)
            if (txtBuscarProducto != null)
                txtBuscarProducto.TextChanged += txtBuscarProducto_TextChanged;


        }

        // ----------------------------------------------------------
        // Load: configura vista, enlaza datos y carga listas
        // ----------------------------------------------------------
        private void FGestionarInventario_Load(object sender, EventArgs e)
        {
            ConfigurarGrillaVisual();
            ConfigurarGrillaDatos();

            // Enlazar BindingSource a la grilla
            _bsProductos.DataSource = _productos;
            dgvProductos.DataSource = _bsProductos;

            CargarCategorias();
            CargarProductos(true);

            // Estado inicial limpio
            LimpiarFormulario();
            dgvProductos.ClearSelection();
        }

        // ----------------------------------------------------------
        // DataGridView: estilo visual cálido y legible
        // ----------------------------------------------------------
        private void ConfigurarGrillaVisual()
        {
            dgvProductos.Dock = DockStyle.Fill;
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

            dgvProductos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvProductos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.DefaultCellStyle.BackColor = Color.White;
            dgvProductos.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(170, 40, 40);
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 243, 230);

            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProductos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 0, 0);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvProductos.ColumnHeadersHeight = 42;
        }

        // ----------------------------------------------------------
        // DataGridView: mapea propiedades y agrega columna de acción
        // ----------------------------------------------------------
        private void ConfigurarGrillaDatos()
        {
            if (nombreProd != null) nombreProd.DataPropertyName = "nombre";
            if (DescProd != null) DescProd.DataPropertyName = "descripcion";
            if (Precio != null) Precio.DataPropertyName = "precio";
            if (Stock != null) Stock.DataPropertyName = "stock";
            // Categoria y Estado se resuelven por CellFormatting

            // Agregar columna botón "Acción" si no existe
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

        // ----------------------------------------------------------
        // Categorías: carga y enlaza al ComboBox
        // ----------------------------------------------------------
        private void CargarCategorias()
        {
            _categorias.Clear();

            try
            {
                List<Categoria> datos = _categoriaDao.Listar();
                for (int i = 0; i < datos.Count; i++)
                    _categorias.Add(datos[i]);
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

        // ----------------------------------------------------------
        // Productos: carga desde la base y refresca la vista
        // ----------------------------------------------------------
        private void CargarProductos(bool incluirInactivos)
        {
            _productos.Clear();

            try
            {
                IList<Producto> datos = _productoDao.Listar(incluirInactivos);
                for (int i = 0; i < datos.Count; i++)
                    _productos.Add(datos[i]);
            }
            catch (Exception ex)
            {
                MostrarError("Error cargando productos: " + ex.Message);
            }

            AplicarFiltro(); // respeta el texto de búsqueda actual
        }

        // ----------------------------------------------------------
        // Registrar: inserta nuevo producto y actualiza la lista
        // ----------------------------------------------------------
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

        // ----------------------------------------------------------
        // Editar: actualiza el producto seleccionado
        // ----------------------------------------------------------
        private void btnEditar_Click(object sender, EventArgs e)
        {
            var prodSel = dgvProductos.CurrentRow != null
                ? dgvProductos.CurrentRow.DataBoundItem as Producto
                : null;

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

        // ----------------------------------------------------------
        // Botón en grilla: dar de baja/restaurar
        // ----------------------------------------------------------
        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvProductos.Columns[e.ColumnIndex].Name != "Accion") return;

            var prod = dgvProductos.Rows[e.RowIndex].DataBoundItem as Producto;
            if (prod == null) return;

            if (prod.estado)
            {
                if (MessageBox.Show("¿Dar de baja el producto '" + prod.nombre + "'?",
                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
                if (MessageBox.Show("¿Restaurar el producto '" + prod.nombre + "'?",
                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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

        // ----------------------------------------------------------
        // Selección en grilla: muestra datos del producto
        // ----------------------------------------------------------
        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            var p = dgvProductos.CurrentRow != null
                ? dgvProductos.CurrentRow.DataBoundItem as Producto
                : null;
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

        // ----------------------------------------------------------
        // Formateo en grilla: columnas de categoría, estado y acción
        // ----------------------------------------------------------
        private void dgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var item = dgvProductos.Rows[e.RowIndex].DataBoundItem as Producto;
            if (item == null) return;

            string colName = dgvProductos.Columns[e.ColumnIndex].Name;

            if (colName == "Categoria")
            {
                e.Value = item.id_categoria != null ? item.id_categoria.nombre_categoria : "";
                e.FormattingApplied = true;
            }
            else if (colName == "Estado")
            {
                e.Value = item.estado ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
            else if (colName == "Accion")
            {
                e.Value = item.estado ? "Dar baja" : "Restaurar";
                e.FormattingApplied = true;
            }

            // Estilo visual para filas inactivas
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

        // ----------------------------------------------------------
        // Búsqueda en tiempo real: filtra por nombre o categoría
        // ----------------------------------------------------------
        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string termino = txtBuscarProducto != null ? (txtBuscarProducto.Text ?? "").Trim().ToLower() : "";
            if (termino.Length == 0)
            {
                _bsProductos.DataSource = _productos;
                _bsProductos.ResetBindings(false);
                return;
            }

            var filtrados = _productos
                .Where(p =>
                    (p.nombre ?? "").ToLower().Contains(termino) ||
                    (p.id_categoria != null && (p.id_categoria.nombre_categoria ?? "").ToLower().Contains(termino)))
                .ToList();

            // Mantener BindingList + BindingSource como se solicitó
            _bsProductos.DataSource = new BindingList<Producto>(filtrados);
            _bsProductos.ResetBindings(false);
        }

        // ----------------------------------------------------------
        // Limpiar: deja formulario en estado neutro sin validaciones
        // ----------------------------------------------------------
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
                // Evitar que la selección de la grilla vuelva a llenar los campos
                dgvProductos.SelectionChanged -= dgvProductos_SelectionChanged;

                // Si hay filtro, aplicarlo primero (esto puede cambiar la selección)
                AplicarFiltro();

                // Quitar selección/celda actual y resetear posición del BindingSource
                _bsProductos.Position = -1;
                dgvProductos.ClearSelection();
                dgvProductos.CurrentCell = null;

                // Limpiar controles del formulario
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MostrarError("Error al limpiar el formulario: " + ex.Message);
            }
            finally
            {
                // Restaurar el evento de selección
                dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            }
        }

        // ----------------------------------------------------------
        // Volver
        // ----------------------------------------------------------
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ----------------------------------------------------------
        // Validación: reglas básicas de entrada y consistencia
        // ----------------------------------------------------------
        private bool ValidarProducto(out decimal precio, out int stock)
        {
            precio = 0m;
            stock = 0;

            string nombre = (txtProdNombre.Text ?? "").Trim();
            if (nombre.Length == 0)
            {
                MostrarWarn("Debe ingresar un nombre de producto.");
                txtProdNombre.Focus();
                return false;
            }
            if (nombre.Length > MAX_NOMBRE)
            {
                MostrarWarn("El nombre no puede superar los " + MAX_NOMBRE + " caracteres.");
                txtProdNombre.Focus();
                return false;
            }

            string descripcion = (txtProdDesc.Text ?? "").Trim();
            if (descripcion.Length == 0)
            {
                MostrarWarn("Debe ingresar una descripción.");
                txtProdDesc.Focus();
                return false;
            }
            if (descripcion.Length > MAX_DESCRIPCION)
            {
                MostrarWarn("La descripción no puede superar los " + MAX_DESCRIPCION + " caracteres.");
                txtProdDesc.Focus();
                return false;
            }

            string sep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string normalized = (txtProdPrecio.Text ?? "").Replace(",", sep).Replace(".", sep);
            decimal parsedPrecio;
            if (!decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.CurrentCulture, out parsedPrecio) || parsedPrecio <= 0)
            {
                MostrarWarn("Debe ingresar un precio válido.");
                txtProdPrecio.Focus();
                return false;
            }

            int parsedStock;
            if (!int.TryParse(txtProdStock.Text, out parsedStock) || parsedStock < 0)
            {
                MostrarWarn("Debe ingresar un stock válido.");
                txtProdStock.Focus();
                return false;
            }

            precio = parsedPrecio;
            stock = parsedStock;
            return true;
        }

        // ----------------------------------------------------------
        // Validaciones de teclado: letras, decimales y enteros
        // ----------------------------------------------------------
        private void NombreProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
                e.Handled = true;

            if (!e.Handled && txtProdNombre.Text.Length >= MAX_NOMBRE && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void DescProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
                e.Handled = true;

            if (!e.Handled && txtProdDesc.Text.Length >= MAX_DESCRIPCION && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void PrecioProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                (txtProdPrecio.Text.Contains(".") || txtProdPrecio.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void StockProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;

            if (!e.Handled && char.IsDigit(e.KeyChar) && txtProdStock.Text.Length >= 10)
                e.Handled = true;
        }

        // ----------------------------------------------------------
        // Helpers de mensajes
        // ----------------------------------------------------------
        private void MostrarWarn(string msg) =>
            MessageBox.Show(msg, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void MostrarError(string msg) =>
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void MostrarInfo(string msg) =>
            MessageBox.Show(msg, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
