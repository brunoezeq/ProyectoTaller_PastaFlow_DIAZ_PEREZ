using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FGestionarInventario : Form
    {
        public FGestionarInventario()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NombreProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
            if (char.IsLetter(e.KeyChar) && txtProdNombre.Text.Length >= 25)
            {
                e.Handled = true;
            }
        }

        private void DescProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
            if (char.IsLetter(e.KeyChar) && txtProdDesc.Text.Length >= 100)
            {
                e.Handled = true;
            }
        }

        private void PrecioProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Evitar que se ingresen dos puntos
            if (e.KeyChar == '.' && txtProdPrecio.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void StockProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (char.IsDigit(e.KeyChar) && txtProdStock.Text.Length >= 10)
            {
                e.Handled = true;
            }
        }
    }
}
