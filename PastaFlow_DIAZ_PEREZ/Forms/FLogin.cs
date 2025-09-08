using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PastaFlow_DIAZ_PEREZ.Forms;

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
