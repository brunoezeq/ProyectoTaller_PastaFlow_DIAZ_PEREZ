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
    public partial class FRegistrarVenta : Form
    {
        public FRegistrarVenta()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FRegistrarVenta_Load);
        }


        private void FRegistrarVenta_Load(object sender, EventArgs e)
        {
           
        }

        // Evento para agregar producto a la grilla
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
        }

        // Evento para botón eliminar en la grilla
        private void dgvDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalleVenta.Columns[e.ColumnIndex].Name == "bEliminar" && e.RowIndex >= 0)
            {
                dgvDetalleVenta.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Validar que txtCantidad acepte solo números
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // bloquea cualquier letra o símbolo
            }
        }


    

















        private void pnlPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FRegistrarVenta_Load_1(object sender, EventArgs e)
        {

        }
    }
}
