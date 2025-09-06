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
            lbFechaHora.Text = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbFechaHora_Click(object sender, EventArgs e)
        {

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbFechaHora.Text = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMontoInicial.Text, out decimal monto) && monto > 0)
            {
                MessageBox.Show($"Caja abierta con ${monto}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Cierra el formulario después de abrir la caja
            }
            else
            {
                MessageBox.Show("Ingrese un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento para el botón Atrás
        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FAbrirCaja_Load_1(object sender, EventArgs e)
        {

        }
    }
}
