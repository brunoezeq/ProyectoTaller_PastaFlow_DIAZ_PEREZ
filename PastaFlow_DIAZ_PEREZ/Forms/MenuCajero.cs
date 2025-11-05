using System;
using System.Linq;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class MenuCajero : Form
    {
        public MenuCajero()
        {
            InitializeComponent();
        }

        // Abre la vista FGestionarInventario dentro del panel central del menú cajero
        public void ShowGestionarInventario()
        {
            // Evitar múltiples instancias
            foreach (Control c in mainPanel.Controls)
            {
                if (c is FGestionarInventario)
                {
                    c.BringToFront();
                    return;
                }
            }

            var form = new FGestionarInventario
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            mainPanel.Controls.Add(form);
            form.Show();
            form.BringToFront();
        }
    }
}