using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Models;
using PastaFlow_DIAZ_PEREZ.Services;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FRegistrarEmpleado : Form
    {
        public FRegistrarEmpleado()
        {
            InitializeComponent();
            // this.Load += FRegEmpleado_Load;  // ya suscrito en el Designer
            dgvUsuarios.CellContentClick += dgvUsuarios_CellContentClick;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
            dgvUsuarios.DataBindingComplete += dgvUsuarios_DataBindingComplete;

            AsegurarColumnaAccionComoBoton();   // garantizar botón visible y estilizado

            // Restricciones de entrada (teclado y pegado)
            txtEmpNombre.KeyPress += txtSoloLetras_KeyPress;
            txtEmpApellido.KeyPress += txtSoloLetras_KeyPress;
            txtEmpDNI.KeyPress += txtSoloNumeros_KeyPress;
            txtEmpTelefono.KeyPress += txtSoloNumeros_KeyPress;

            txtEmpNombre.TextChanged += SoloLetras_TextChanged;
            txtEmpApellido.TextChanged += SoloLetras_TextChanged;
            txtEmpDNI.TextChanged += SoloNumeros_TextChanged;
            txtEmpTelefono.TextChanged += SoloNumeros_TextChanged;

            txtEmpNombre.MaxLength = 25;
            txtEmpApellido.MaxLength = 25;
            txtEmpDNI.MaxLength = 8;
            txtEmpTelefono.MaxLength = 10;
        }

        // Cuando se abre el formulario
        private void FRegEmpleado_Load(object sender, EventArgs e)
        {
            ConfigurarGrillaVisualUsuarios();   // aplicar estilo de Inventario
            ConfigurarDgvUsuarios();

            // Cargar roles en el ComboBox principal
            List<Rol> roles = new ServiceRol().ListarRoles();
            cBoxRol.DisplayMember = "Nombre_rol";
            cBoxRol.ValueMember = "Id_rol";
            cBoxRol.DataSource = roles;
            cBoxRol.SelectedIndex = -1;

            // Cargar roles en el ComboBox de búsqueda
            List<Rol> rolesBusqueda = new ServiceRol().ListarRoles();
            cBoxBuscarRol.DisplayMember = "Nombre_rol";
            cBoxBuscarRol.ValueMember = "Id_rol";
            cBoxBuscarRol.DataSource = rolesBusqueda;
            cBoxBuscarRol.SelectedIndex = -1;

            cBoxBuscarRol.SelectedIndexChanged += cBoxBuscarRol_SelectedIndexChanged;

            // Cargar usuarios
            CargarUsuarios();
        }

        private void ConfigurarDgvUsuarios()
        {
            dgvUsuarios.AutoGenerateColumns = false;

            if (dgvUsuarios.Columns["Nombre"] != null) dgvUsuarios.Columns["Nombre"].DataPropertyName = "nombre";
            if (dgvUsuarios.Columns["Apellido"] != null) dgvUsuarios.Columns["Apellido"].DataPropertyName = "apellido";
            if (dgvUsuarios.Columns["Dni"] != null) dgvUsuarios.Columns["Dni"].DataPropertyName = "dni";
            if (dgvUsuarios.Columns["CorreoElectronico"] != null) dgvUsuarios.Columns["CorreoElectronico"].DataPropertyName = "correo_electronico";
            if (dgvUsuarios.Columns["Telefono"] != null) dgvUsuarios.Columns["Telefono"].DataPropertyName = "telefono";
            if (dgvUsuarios.Columns["Rol"] != null) dgvUsuarios.Columns["Rol"].DataPropertyName = "nombre_rol";
            if (dgvUsuarios.Columns["Estado"] != null) dgvUsuarios.Columns["Estado"].DataPropertyName = "estado";
        }

        // Estilo visual del DataGridView 

        private void ConfigurarGrillaVisualUsuarios()
        {
            var g = dgvUsuarios;

            g.AutoGenerateColumns = false;
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
        }

        private void cBoxBuscarRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? rol = cBoxBuscarRol.SelectedIndex >= 0 ? (int?)cBoxBuscarRol.SelectedValue : null;
            var dao = new UsuarioDAO();
            DataTable dt = dao.BuscarUsuarios(null, rol);
            dgvUsuarios.DataSource = dt;

            // Letras negras y encabezado en negrita
            dgvUsuarios.DefaultCellStyle.ForeColor = Color.Black;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font(dgvUsuarios.Font, FontStyle.Bold);

            // Ocultar id_rol si existe y aplicar formato de centrado/columna correo
            if (dgvUsuarios.Columns.Contains("id_rol"))
                dgvUsuarios.Columns["id_rol"].Visible = false;

            AplicarFormatoTabla();
        }

        private bool ValidarCamposFormulario(out string mensajeError, bool esEdicion = false)
        {
            var errores = new List<string>();

            // Nombre / Apellido
            if (string.IsNullOrWhiteSpace(txtEmpNombre.Text))
                errores.Add("Nombre vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpNombre.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚüÜ\s]+$") || txtEmpNombre.Text.Length > 25)
                errores.Add("Nombre inválido (solo letras, max 25).");

            if (string.IsNullOrWhiteSpace(txtEmpApellido.Text))
                errores.Add("Apellido vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpApellido.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚüÜ\s]+$") || txtEmpApellido.Text.Length > 25)
                errores.Add("Apellido inválido (solo letras, max 25).");

            // DNI
            if (string.IsNullOrWhiteSpace(txtEmpDNI.Text))
                errores.Add("DNI vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpDNI.Text, @"^\d{8}$"))
                errores.Add("DNI inválido (8 dígitos).");

            // Correo
            if (string.IsNullOrWhiteSpace(txtEmpCorreo.Text))
                errores.Add("Correo vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errores.Add("Formato de correo inválido.");

            // Teléfono
            if (string.IsNullOrWhiteSpace(txtEmpTelefono.Text))
                errores.Add("Teléfono vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpTelefono.Text, @"^\d{10}$"))
                errores.Add("Teléfono inválido (10 dígitos).");

            // Contraseña
            if (!esEdicion || !string.IsNullOrEmpty(txtEmpContra.Text))
            {
                if (string.IsNullOrEmpty(txtEmpContra.Text))
                    errores.Add("Contraseña vacía.");
                else if (txtEmpContra.Text.Length > 20)
                    errores.Add("Contraseña supera 20 caracteres.");

                if (txtEmpContra.Text != txtEmpRContra.Text)
                    errores.Add("Las contraseñas no coinciden.");
            }

            // Rol
            if (cBoxRol.SelectedItem == null)
                errores.Add("Seleccione un rol.");

            if (errores.Any())
            {
                mensajeError = string.Join(Environment.NewLine, errores);
                return false;
            }

            mensajeError = null;
            return true;
        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposFormulario(out string errores))
            {
                MessageBox.Show(errores, "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtEmpNombre.Text.Trim();
            string apellido = txtEmpApellido.Text.Trim();
            string dni = txtEmpDNI.Text.Trim();
            string correo = txtEmpCorreo.Text.Trim();
            string telefono = txtEmpTelefono.Text.Trim();
            string contrasena = txtEmpContra.Text;
            string repetir = txtEmpRContra.Text;
            int rol = Convert.ToInt32(cBoxRol.SelectedValue);
            byte[] hash = PastaFlow_DIAZ_PEREZ.Utils.SeguridadHelper.ComputeSha256Hash(contrasena);

            var dao = new UsuarioDAO();
            try
            {
                dao.RegistrarUsuario(dni, nombre, apellido, correo, telefono, rol, hash);
                MessageBox.Show("Usuario registrado con éxito.");
                LimpiarRegistro();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Limpiar campos del formulario
        private void LimpiarRegistro()
        {
            txtEmpNombre.Clear();
            txtEmpApellido.Clear();
            txtEmpDNI.Clear();
            txtEmpCorreo.Clear();
            txtEmpTelefono.Clear();
            txtEmpContra.Clear();
            txtEmpRContra.Clear();
            cBoxRol.SelectedIndex = -1;
        }

        // Cargar usuarios en el DataGridView
        private void CargarUsuarios()
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT u.nombre, u.apellido, u.dni, u.correo_electronico, u.telefono,
                              r.nombre_rol, u.id_rol,
                              CASE WHEN u.estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS estado
                       FROM Usuario u INNER JOIN Rol r ON u.id_rol = r.id_rol";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsuarios.AutoGenerateColumns = false;
                dgvUsuarios.DataSource = dt;

                dgvUsuarios.DefaultCellStyle.ForeColor = Color.Black;
                dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font(dgvUsuarios.Font, FontStyle.Bold);
                AplicarFormatoTabla();

                // Reemplaza el foreach del final de CargarUsuarios()
foreach (DataGridViewRow row in dgvUsuarios.Rows)
{
    AplicarAccionEnFila(row); // asigna botón o lo oculta si es el propio admin
}
            }
            AjustarDataGridView(dgvUsuarios);
        }

        // Ajustes columna correo)
        private void AplicarFormatoTabla()
        {
            var grid = dgvUsuarios;

            // Mantener estilo aplicado en ConfigurarGrillaVisualUsuarios()

            if (grid.Columns.Contains("CorreoElectronico"))
            {
                var colMail = grid.Columns["CorreoElectronico"];
                colMail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                colMail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colMail.FillWeight = 150;
            }
        }

        private void AjustarDataGridView(DataGridView dgv)
        {
            // Mantener configuración visual de Inventario
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // Al hacer click en una fila, cargar datos en el formulario
        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvUsuarios.Rows[e.RowIndex];
                txtEmpNombre.Text = Convert.ToString(row.Cells["Nombre"].Value);
                txtEmpApellido.Text = Convert.ToString(row.Cells["Apellido"].Value);
                txtEmpDNI.Text = Convert.ToString(row.Cells["Dni"].Value);
                txtEmpCorreo.Text = Convert.ToString(row.Cells["CorreoElectronico"].Value);
                txtEmpTelefono.Text = Convert.ToString(row.Cells["Telefono"].Value);

                // Recuperar id_rol desde el DataTable
                var drv = row.DataBoundItem as DataRowView;
                if (drv != null && drv.Row.Table.Columns.Contains("id_rol"))
                    cBoxRol.SelectedValue = drv["id_rol"];
            }
        }

        // Buscar usuarios por DNI o rol
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string dni = string.IsNullOrWhiteSpace(txtBuscarDni.Text) ? null : txtBuscarDni.Text;
            int? rol = cBoxBuscarRol.SelectedIndex >= 0 ? (int?)cBoxBuscarRol.SelectedValue : null;

            var dao = new UsuarioDAO();
            DataTable dt = dao.BuscarUsuarios(dni, rol);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron usuarios con el número de documento ingresado.",
                                "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvUsuarios.DataSource = dt;

            // Letras negras y encabezado en negrita
            dgvUsuarios.DefaultCellStyle.ForeColor = Color.Black;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font(dgvUsuarios.Font, FontStyle.Bold);

            // Ocultar id_rolsi existe y aplicar formato de centrado/columna correo
            if (dgvUsuarios.Columns.Contains("id_rol"))
                dgvUsuarios.Columns["id_rol"].Visible = false;

            AplicarFormatoTabla();
        }

        // Limpiar filtros de búsqueda
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarDni.Clear();
            cBoxBuscarRol.SelectedIndex = -1;
            CargarUsuarios();
        }

        private void txtBuscarDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (char.IsDigit(e.KeyChar) && txtBuscarDni.Text.Length >= 8)
            {
                e.Handled = true;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarForm_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
        }


        // Solo letras y espacios. 
        private void txtSoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)))
                e.Handled = true;
        }

        // Solo dígitos. 
        private void txtSoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)))
                e.Handled = true;
        }

        private void SoloLetras_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;
            int sel = tb.SelectionStart;
            string filtrado = new string(tb.Text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
            if (filtrado != tb.Text)
            {
                tb.Text = filtrado;
                tb.SelectionStart = Math.Min(sel, tb.Text.Length);
            }
        }

        //mantiene solo dígitos.
        private void SoloNumeros_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;
            int sel = tb.SelectionStart;
            string filtrado = new string(tb.Text.Where(char.IsDigit).ToArray());
            if (filtrado != tb.Text)
            {
                tb.Text = filtrado;
                tb.SelectionStart = Math.Min(sel, tb.Text.Length);
            }
        }

        // Editar usuario seleccionado
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // validación en modo edición
            if (!ValidarCamposFormulario(out string errores, esEdicion: true))
            {
                MessageBox.Show(errores, "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación
            var res = MessageBox.Show("¿Está seguro que desea guardar los cambios del usuario?",
                                      "Confirmar edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes) return;

            string nombre = txtEmpNombre.Text.Trim();
            string apellido = txtEmpApellido.Text.Trim();
            string dni = txtEmpDNI.Text.Trim();
            string correo = txtEmpCorreo.Text.Trim();
            string telefono = txtEmpTelefono.Text.Trim();
            int rol = Convert.ToInt32(cBoxRol.SelectedValue);

            string contrasena = txtEmpContra.Text.Trim();
            byte[] hash = null;
            if (!string.IsNullOrEmpty(contrasena))
                hash = SeguridadHelper.ComputeSha256Hash(contrasena);

            var dao = new UsuarioDAO();
            try
            {
                dao.ActualizarUsuario(dni, nombre, apellido, correo, telefono, rol, hash);
                MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
                LimpiarRegistro();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cambiar estado (activar/eliminar) usuario desde el DataGridView
        // Refuerza el click: solo actúa si la celda es realmente un botón visible
        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 
                && dgvUsuarios.Columns[e.ColumnIndex].Name == "Accion"
                && dgvUsuarios.Rows[e.RowIndex].Cells["Accion"] is DataGridViewButtonCell)
            {
                string dni = Convert.ToString(dgvUsuarios.Rows[e.RowIndex].Cells["Dni"].Value);
                string estado = Convert.ToString(dgvUsuarios.Rows[e.RowIndex].Cells["Estado"].Value);
                string accion = (estado == "Activo") ? "eliminar" : "activar";

                var dr = MessageBox.Show($"¿Desea {accion} este usuario?", "Confirmar acción",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes) return;

                try
                {
                    var dao = new UsuarioDAO();
                    dao.CambiarEstadoUsuario(dni);

                    if (estado == "Activo")
                    {
                        dgvUsuarios.Rows[e.RowIndex].Cells["Estado"].Value = "Inactivo";
                        dgvUsuarios.Rows[e.RowIndex].Cells["Accion"].Value = "Activar";
                    }
                    else
                    {
                        dgvUsuarios.Rows[e.RowIndex].Cells["Estado"].Value = "Activo";
                        dgvUsuarios.Rows[e.RowIndex].Cells["Accion"].Value = "Eliminar";
                    }

                    MessageBox.Show("Estado del usuario actualizado correctamente.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar estado: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Rellena el texto del botón cada vez que cambia el DataSource (búsqueda/filtro/carga)
        private void dgvUsuarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvUsuarios.Rows)
            {
                AplicarAccionEnFila(row); // asegura el estado del botón tras cualquier recarga/filtro
            }
        }

        // Convierte/crea la columna "Accion" como botón y aplica estilo
        private void AsegurarColumnaAccionComoBoton()
        {
            DataGridViewColumn col = dgvUsuarios.Columns["Accion"];
            DataGridViewButtonColumn btnCol;

            if (col is DataGridViewButtonColumn)
            {
                btnCol = (DataGridViewButtonColumn)col;
            }
            else
            {
                int index = col != null ? col.Index : dgvUsuarios.Columns.Count;
                if (col != null) dgvUsuarios.Columns.Remove(col);

                btnCol = new DataGridViewButtonColumn
                {
                    Name = "Accion",
                    HeaderText = "ACCION",
                    UseColumnTextForButtonValue = false,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dgvUsuarios.Columns.Insert(index, btnCol);
            }

            // Estilo similar a Inventario pero para botón
            btnCol.FlatStyle = FlatStyle.Flat;
            btnCol.DefaultCellStyle.BackColor = Color.LemonChiffon;
            btnCol.DefaultCellStyle.ForeColor = Color.Black;
            btnCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(170, 40, 40);
            btnCol.DefaultCellStyle.SelectionForeColor = Color.White;
            btnCol.MinimumWidth = 110;

            // Pesos de columnas para mejor distribución
            TrySetFillWeight("Nombre", 110);
            TrySetFillWeight("Apellido", 110);
            TrySetFillWeight("Dni", 90);
            TrySetFillWeight("CorreoElectronico", 180);
            TrySetFillWeight("Telefono", 120);
            TrySetFillWeight("Rol", 110);
            TrySetFillWeight("Estado", 110);
            TrySetFillWeight("Accion", 120);
        }

        private void TrySetFillWeight(string colName, float weight)
        {
            var c = dgvUsuarios.Columns[colName];
            if (c != null)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                c.FillWeight = weight;
            }
        }

        private void AplicarAccionEnFila(DataGridViewRow row)
        {
            if (row == null) return;

            // Usuario logueado
            var current = Session.CurrentUser;
            string dniRow = Convert.ToString(row.Cells["Dni"]?.Value);

            // Ocultar botón si es el propio administrador
            bool ocultar = current != null 
                           && current.Id_rol == 1 // Administrador
                           && string.Equals(current.Dni, dniRow, StringComparison.OrdinalIgnoreCase);

            if (ocultar)
            {
                // Reemplaza el botón por una celda de texto vacía (se "oculta" la acción)
                row.Cells["Accion"] = new DataGridViewTextBoxCell { Value = "" };
                return;
            }

            // Mostrar botón y asignar texto según estado
            string estadoTxt = Convert.ToString(row.Cells["Estado"]?.Value);

            if (!(row.Cells["Accion"] is DataGridViewButtonCell))
                row.Cells["Accion"] = new DataGridViewButtonCell();

            var btn = (DataGridViewButtonCell)row.Cells["Accion"];
            btn.Value = (estadoTxt == "Activo") ? "Eliminar" : "Activar";
        }
    }
}