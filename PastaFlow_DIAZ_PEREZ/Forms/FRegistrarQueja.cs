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
        public FRegistrarQueja()
        {
            InitializeComponent();
        }

        private void btnRegistrarQueja_Click(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoCliente.Text) ||
                string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios.",
                                "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombreCliente.Text.Trim();
            string apellido = txtApellidoCliente.Text.Trim();
            string motivo = txtMotivo.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            // 🔹 Obtener el usuario logueado
            // Si ya guardás el usuario en sesión (por ejemplo en una variable global o singleton)
            int idUsuario = Session.CurrentUser.Id_usuario;
            // Si no tenés una sesión aún, podrías usar un valor de prueba (por ejemplo, 1)

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
           
            txtMotivo.Clear();
            txtDescripcion.Clear();
        }
    }
}
