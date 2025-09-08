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
    public partial class FMenuCajero : Form
    {
        public FMenuCajero()
        {
            InitializeComponent();
            this.Load += FMenu_Load;
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
            lbUsuario.Text = $"Bienvenido, ";
            lbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            lbHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void AbrirFormulario(Form formHijo)
        {
            // Limpio el panel antes de cargar otro form
            pnlContent.Controls.Clear();

            // Configuro el formulario hijo
            formHijo.TopLevel = false;      // No es ventana principal
            formHijo.FormBorderStyle = FormBorderStyle.None; // Sin bordes
            formHijo.Dock = DockStyle.Fill; // Que ocupe todo el panel

            // Agrego al panel
            pnlContent.Controls.Add(formHijo);
            formHijo.Show(); 
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FAbrirCaja());
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FRegistrarVenta());
        }

        private void btnRegReserva_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FRegistrarReserva());
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FGestionarInventario());
        }

        private void btnRegQueja_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FRegistrarQueja());
        }

        private void btnVerReportes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FVerReportes());
        }

        private void btnRegEmpleado_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FRegistrarEmpleado());  
        }

        private void btnVerQueja_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FVerQuejas());  
        }
    }
}
