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
        }

        private void FRegEmpleado_Load(object sender, EventArgs e)
        {
            // Cargar roles en el ComboBox
            List<Rol> roles = new ServiceRol().ListarRoles();
            foreach (Rol item in roles)
            {
                cBoxRol.Items.Add(new ComboOption() { Valor = item.Id_rol, Texto = item.Nombre_rol });
            }
            cBoxRol.DisplayMember = "Nombre_rol";
            cBoxRol.ValueMember = "Id_rol";
            cBoxRol.DataSource = roles;
            cBoxRol.SelectedIndex = -1;

            // Cargar usuarios en el DataGridView
            CargarUsuarios(); 
        }

        private bool ValidarCamposFormulario(out string mensajeError)
        {
            var errores = new List<string>();

            // Nombre / Apellido: solo letras y máx 25
            if (string.IsNullOrWhiteSpace(txtEmpNombre.Text))
                errores.Add("Nombre vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpNombre.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚüÜ\s]+$") || txtEmpNombre.Text.Length > 25)
                errores.Add("Nombre inválido (solo letras, max 25).");

            if (string.IsNullOrWhiteSpace(txtEmpApellido.Text))
                errores.Add("Apellido vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpApellido.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚüÜ\s]+$") || txtEmpApellido.Text.Length > 25)
                errores.Add("Apellido inválido (solo letras, max 25).");

            // DNI: 8 dígitos
            if (string.IsNullOrWhiteSpace(txtEmpDNI.Text))
                errores.Add("DNI vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpDNI.Text, @"^\d{8}$"))
                errores.Add("DNI inválido (8 dígitos).");

            // Correo
            if (string.IsNullOrWhiteSpace(txtEmpCorreo.Text))
                errores.Add("Correo vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errores.Add("Formato de correo inválido.");

            // Teléfono: 10 dígitos
            if (string.IsNullOrWhiteSpace(txtEmpTelefono.Text))
                errores.Add("Teléfono vacío.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmpTelefono.Text, @"^\d{10}$"))
                errores.Add("Teléfono inválido (10 dígitos).");

            // Contraseñas
            if (string.IsNullOrEmpty(txtEmpContra.Text))
                errores.Add("Contraseña vacía.");
            else if (txtEmpContra.Text.Length > 20)
                errores.Add("Contraseña supera 20 caracteres.");
            if (txtEmpContra.Text != txtEmpRContra.Text)
                errores.Add("Las contraseñas no coinciden.");

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
            //Si pasa la validación, agrega el empleado al DataGridView. Si no, muestra los errores.
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

        // Limpia los campos del formulario después de registrar un usuario
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

        // Carga los usuarios en el DataGridView
        private void CargarUsuarios()
        {
            using (var conn = DbConnection.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT u.nombre, 
                              u.apellido, 
                              u.dni, 
                              u.correo_electronico, 
                              u.telefono, 
                              r.nombre_rol,  
                              u.id_rol,       
                              CASE 
                                  WHEN u.estado = 1 THEN 'Activo' 
                                  ELSE 'Inactivo' 
                              END AS estado
                       FROM Usuario u
                       INNER JOIN Rol r ON u.id_rol = r.id_rol";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;

                // Ocultar la columna id_rol (solo sirve internamente)
                if (dgvUsuarios.Columns.Contains("id_rol"))
                    dgvUsuarios.Columns["id_rol"].Visible = false;

                dgvUsuarios.Columns["nombre"].HeaderText = "Nombre";
                dgvUsuarios.Columns["apellido"].HeaderText = "Apellido";
                dgvUsuarios.Columns["dni"].HeaderText = "DNI";
                dgvUsuarios.Columns["correo_electronico"].HeaderText = "Correo electrónico";
                dgvUsuarios.Columns["telefono"].HeaderText = "Teléfono";
                dgvUsuarios.Columns["nombre_rol"].HeaderText = "Rol";
                dgvUsuarios.Columns["estado"].HeaderText = "Estado";
            }
        }

        // Al hacer click en una fila del DataGridView, carga los datos en el formulario
        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];
                txtEmpNombre.Text = row.Cells["nombre"].Value.ToString();
                txtEmpApellido.Text = row.Cells["apellido"].Value.ToString();
                txtEmpDNI.Text = row.Cells["dni"].Value.ToString();
                txtEmpCorreo.Text = row.Cells["correo_electronico"].Value.ToString();
                txtEmpTelefono.Text = row.Cells["telefono"].Value.ToString();
                cBoxRol.SelectedValue = row.Cells["id_rol"].Value;
            }
        }
    }
}
