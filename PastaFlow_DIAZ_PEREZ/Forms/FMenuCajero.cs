using PastaFlow_DIAZ_PEREZ.Models;
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
    public partial class FMenuCajero : Form
    {
        public FMenuCajero()
        {
            InitializeComponent();
            this.Load += FMenu_Load;
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
            var user = Session.CurrentUser;
            
            if (user != null)
            {
                lbUsuario.Text = $"Bienvenido, {user.Nombre} {user.Apellido}";
            }
            lbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            btnVerReportes.Visible = false;
            btnRegEmpleado.Visible = false;
            btnVerQuejas.Visible = false;
            btnInventario.Visible = false;
            btnRegQueja.Visible = false;
            btnAbrirCaja.Visible = false;
            btnCargarPedido.Visible = false;
            btnRegReserva.Visible = false;

            if (user.Id_rol == 1) // Administrador
            {
                btnVerReportes.Visible = true;
                btnRegEmpleado.Visible = true;
                btnVerQuejas.Visible = true;
            }
            else if (user.Id_rol == 2) // Gerente
            {
                btnInventario.Visible = true;
                btnRegQueja.Visible = true;
            }
            else if (user.Id_rol == 3) // Cajero
            {
                btnAbrirCaja.Visible = true;
                btnCargarPedido.Visible = true;
                btnRegReserva.Visible = true;
            }
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
            pnlMenuLateral.Visible = false;         // Oculto el menú lateral

            var frm = new FRegistrarReserva();   // Creo el formulario hijo

            frm.FormClosed += (s, args) =>          // Cuando el hijo se cierra, muestro de nuevo el menú
            {
                pnlMenuLateral.Visible = true;
            };

            AbrirFormulario(frm);   // Lo abro dentro del panel de contenido
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            pnlMenuLateral.Visible = false;         // Oculto el menú lateral

            var frm = new FGestionarInventario();   // Creo el formulario hijo

            frm.FormClosed += (s, args) =>          // Cuando el hijo se cierra, muestro de nuevo el menú
            {
                pnlMenuLateral.Visible = true;
            };

            AbrirFormulario(frm);   
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
            pnlMenuLateral.Visible = false;         // Oculto el menú lateral
            
            var frm = new FRegistrarEmpleado();     // Creo el formulario hijo

            
            frm.FormClosed += (s, args) =>          // Cuando el hijo se cierra, muestro de nuevo el menú
            {
                pnlMenuLateral.Visible = true;
            };

            AbrirFormulario(frm);
        }

        private void btnVerQueja_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FVerQuejas());  
        }
    }
}
