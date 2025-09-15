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
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
        }

        //Cierra la aplicación
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {   //Solo permite ingresar numeros en el textbox
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }
            //Solo permite ingresar 8 caracteres en el textbox
            if (char.IsDigit(e.KeyChar) && txtDNI.Text.Length >= 8)
            {
                e.Handled = true;
            }
        }

        //Ingresa al menú
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();
            string password = txtContrasenia.Text;

            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Debe ingresar número de documento y contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dao = new UsuarioDAO();
            var user = dao.ObtenerPorDni(dni);

            if (user == null || user.Contrasena_hash == null)
            {
                MessageBox.Show("Nro de documento o contraseña incorrectos.", "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hasheamos la contraseña ingresada y comparamos
            byte[] hashedInput = SeguridadHelper.ComputeSha256Hash(password);

            bool iguales = hashedInput.Length == user.Contrasena_hash.Length &&
                           hashedInput.SequenceEqual(user.Contrasena_hash);

            if (!iguales || !user.Estado)
            {
                MessageBox.Show("Nro de documento o contraseña incorrectos.", "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Login OK: guardamos sesión y abrimos menú
            Session.CurrentUser = user;
            FMenuCajero menu = new FMenuCajero();
            menu.Show();
            this.Hide();
            menu.FormClosing += frmClosing;
        }

        //Al cerrar el menú, limpia los textbox y vuelve a mostrar el login
        private void frmClosing(object sender, FormClosingEventArgs e)
        {
            txtDNI.Clear();
            txtContrasenia.Clear();
            this.Show(); 
        }

        
    }
}
