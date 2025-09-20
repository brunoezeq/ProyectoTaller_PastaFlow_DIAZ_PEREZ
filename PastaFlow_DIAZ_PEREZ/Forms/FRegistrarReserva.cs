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
    public partial class FRegistrarReserva : Form
    {
        public FRegistrarReserva()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NombreCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
            if (char.IsLetter(e.KeyChar) && txtNombreCliente.Text.Length >= 100)
            {
                e.Handled = true;
            }
        }

        private void ApellidoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
            if (char.IsLetter(e.KeyChar) && txtApCliente.Text.Length >= 100)
            {
                e.Handled = true;
            }
        }

        private void CantPersonas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (char.IsDigit(e.KeyChar) && txtCantPersonas.Text.Length >= 15)
            {
                e.Handled = true;
            }
        }
    }
}
