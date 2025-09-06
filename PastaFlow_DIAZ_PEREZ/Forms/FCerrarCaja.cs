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
    public partial class FCerrarCaja : Form
    {
        private decimal montoInicial;
        private decimal totalVentas;
        public FCerrarCaja(decimal montoInicial, decimal totalVentas)
        {
            InitializeComponent();
            this.montoInicial = montoInicial;
            this.totalVentas = totalVentas;
        }

        private void FCerrarCaja_Load(object sender, EventArgs e)
        {
            // Fecha y hora actual
            lbFechaHora.Text = DateTime.Now.ToString("dd/MM/yyyy - HH:mm");

            // Mostrar datos recibidos
            lbMontoInicial.Text = $"Monto Inicial: ${montoInicial}";
            lbTotalVentas.Text = $"Total de Ventas: ${totalVentas}";
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMontoFinal.Text, out decimal montoFinal) && montoFinal >= 0)
            {
                // Muestra resumen del cierre
                MessageBox.Show($"Caja cerrada correctamente.\n\n" +
                                $"Monto Inicial: ${montoInicial}\n" +
                                $"Total de Ventas: ${totalVentas}\n" +
                                $"Monto Final: ${montoFinal}",
                                "Éxito",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                this.Close(); // Cierra el form
            }
            else
            {
                MessageBox.Show("Ingrese un monto válido.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Sale sin hacer nada
        }
        private void pnlPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
