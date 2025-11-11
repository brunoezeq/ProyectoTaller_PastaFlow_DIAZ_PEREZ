using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Forms;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PastaFlow_DIAZ_PEREZ
{
    // Pantalla de inicio de sesión:
    // - Valida DNI (solo 8 dígitos) y contraseña
    // - Permite ingresar con Enter desde cualquier control
    // - Verifica hash SHA-256 contra la base y estado del usuario
    // - Al iniciar sesión abre el menú principal y oculta el login
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();

            // Enter ejecuta el botón Ingresar
            this.AcceptButton = btnIngresar;

            // Fallback por si algún control consume Enter
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnIngresar.PerformClick();
                    e.Handled = true;
                }
            };
        }

        // Cierra la ventana de login (si no hay otras ventanas, finaliza la app)
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo números y Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }
            // Máximo 8 caracteres
            if (char.IsDigit(e.KeyChar) && txtDNI.Text.Length >= 8)
            {
                e.Handled = true;
            }
        }

        // Intenta iniciar sesión y abrir el menú
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();
            string password = txtContrasenia.Text;

            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Debe ingresar número de documento y contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Busca usuario por DNI
            var dao = new UsuarioDAO();
            var user = dao.ObtenerPorDni(dni);

            if (user == null || user.Contrasena_hash == null)
            {
                MessageBox.Show("Nro de documento o contraseña incorrectos.", "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hashea entrada y compara con lo almacenado
            byte[] hashedInput = SeguridadHelper.ComputeSha256Hash(password);
            bool iguales = hashedInput.Length == user.Contrasena_hash.Length &&
                           hashedInput.SequenceEqual(user.Contrasena_hash);

            // También requiere que el usuario esté activo
            if (!iguales || !user.Estado)
            {
                MessageBox.Show("Nro de documento o contraseña incorrectos.", "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Login OK: guarda sesión y abre menú principal
            Session.CurrentUser = user;
            FMenuCajero menu = new FMenuCajero();
            menu.Show();
            this.Hide();
            menu.FormClosing += frmClosing;
        }

        // Al cerrar el menú, limpia campos y vuelve a mostrar el login
        private void frmClosing(object sender, FormClosingEventArgs e)
        {
            txtDNI.Clear();
            txtContrasenia.Clear();
            this.Show(); 
        }
    }
}
