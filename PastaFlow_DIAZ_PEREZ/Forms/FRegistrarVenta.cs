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
            // Solo selección, no edición
            cbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            // Cargar productos en el ComboBox
            cbProducto.Items.Add("Producto A");
            cbProducto.Items.Add("Producto B");
            cbProducto.Items.Add("Producto C");

            // Ajustar ancho automático de columnas
            dgvDetalleVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Solo lectura para evitar cambios directos
            dgvDetalleVenta.ReadOnly = true;
            dgvDetalleVenta.AllowUserToAddRows = false;
        }

        // Evento para agregar producto a la grilla
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbProducto.SelectedIndex != -1 && int.TryParse(txtCantidad.Text, out int cantidad) && cantidad > 0)
            {
                string producto = cbProducto.SelectedItem.ToString();
                decimal precioUnitario = 100; // Valor fijo por ahora (después se trae de BD)
                decimal subtotal = cantidad * precioUnitario;

                // Agregar fila
                dgvDetalleVenta.Rows.Add(producto, cantidad, precioUnitario, subtotal, "Eliminar");

                // Recalcular total
                decimal total = 0;
                foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
                }
                txtTotal.Text = total.ToString("0.00");
            }
            else
            {
                MessageBox.Show("Seleccione un producto y cantidad válida.");
            }
        }


        // Evento para botón eliminar en la grilla
        private void dgvDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalleVenta.Columns[e.ColumnIndex].Name == "bEliminar" && e.RowIndex >= 0)
            {
                dgvDetalleVenta.Rows.RemoveAt(e.RowIndex);
                CalcularTotal();
            }
        }

        // 👉 Método para calcular el total
        private void CalcularTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvDetalleVenta.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
                }
            }
            txtTotal.Text = total.ToString("N2");
        }

        // Evento Confirmar
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleVenta.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Venta registrada. Total: ${txtTotal.Text}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        // Evento Cancelar
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
