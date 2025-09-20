using PastaFlow_DIAZ_PEREZ.Utils;
using System;
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
    public partial class FAbrirCaja : Form
    {
        public FAbrirCaja()
        {
            InitializeComponent();
        }

        private void FAbrirCaja_Load(object sender, EventArgs e)
        {
            var user = Session.CurrentUser;

            if (user != null)
            {
                lbCajero.Text = $"{user.Nombre} {user.Apellido}";
            }
            lbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbHora.Text = DateTime.Now.ToString("HH:mm");
        }

        private void txtMontoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite números, punto decimal y teclas de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Solo permite un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
           
        }

        // Evento para el botón Atrás
        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
