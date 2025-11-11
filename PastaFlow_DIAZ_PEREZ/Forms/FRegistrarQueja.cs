using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    // Registro de quejas de cliente:
    // - Valida nombre/apellido (no placeholders vacíos) y motivo obligatorio.
    // - Guarda motivo y descripción opcional vinculando al usuario en sesión.
    // - Limpia el formulario tras registrar.
    public partial class FRegistrarQueja : Form
    {
        private const string PlaceholderNombre = "Nombre";
        private const string PlaceholderApellido = "Apellido";

        public FRegistrarQueja()
        {
            InitializeComponent();
        }

        // Registrar la queja (validación mínima de campos obligatorios)
        private void btnRegistrarQueja_Click(object sender, EventArgs e)
        {
            if (CamposInvalidos())
            {
                MessageBox.Show("Por favor ingrese nombre, apellido reales y el motivo.",
                                "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombreCliente.Text.Trim();
            string apellido = txtApellidoCliente.Text.Trim();
            string motivo = txtMotivo.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            int idUsuario = Session.CurrentUser.Id_usuario;

            try
            {
                var dao = new QuejaDAO();
                dao.RegistrarQueja(nombre, apellido, motivo, descripcion, idUsuario);

                MessageBox.Show("Queja registrada correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la queja: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Devuelve true si algún campo obligatorio está vacío o es placeholder
        private bool CamposInvalidos()
        {
            return string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
                   string.Equals(txtNombreCliente.Text.Trim(), PlaceholderNombre, StringComparison.OrdinalIgnoreCase) ||
                   string.IsNullOrWhiteSpace(txtApellidoCliente.Text) ||
                   string.Equals(txtApellidoCliente.Text.Trim(), PlaceholderApellido, StringComparison.OrdinalIgnoreCase) ||
                   string.IsNullOrWhiteSpace(txtMotivo.Text);
        }

        // Limpia todos los campos
        private void LimpiarFormulario()
        {
            txtNombreCliente.Clear();
            txtApellidoCliente.Clear();
            txtMotivo.Clear();
            txtDescripcion.Clear();
        }
    }
}
