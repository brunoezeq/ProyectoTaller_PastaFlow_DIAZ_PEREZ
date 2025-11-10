using PastaFlow_DIAZ_PEREZ.DataAccess;
using System;
using PastaFlow_DIAZ_PEREZ.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FRegistrarQueja : Form
    {
        private const string PlaceholderNombre = "Nombre";
        private const string PlaceholderApellido = "Apellido";

        public FRegistrarQueja()
        {
            InitializeComponent();
        }

        private void btnRegistrarQueja_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios evitando placeholders (comparación robusta)
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
                string.Equals(txtNombreCliente.Text.Trim(), PlaceholderNombre, StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(txtApellidoCliente.Text) ||
                string.Equals(txtApellidoCliente.Text.Trim(), PlaceholderApellido, StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Por favor ingrese nombre y apellido reales y el motivo.",
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

        private void LimpiarFormulario()
        {
            txtNombreCliente.Clear();
            txtApellidoCliente.Clear(); 
            txtMotivo.Clear();
            txtDescripcion.Clear();
        }
    }
}
