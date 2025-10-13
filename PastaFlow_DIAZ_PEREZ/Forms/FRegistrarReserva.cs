using PastaFlow_DIAZ_PEREZ.DataAccess;
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
    public partial class FRegistrarReserva : Form
    {
        public FRegistrarReserva()
        {
            InitializeComponent();
        }

        private void FReservas_Load(object sender, EventArgs e)
        {
            dtpFechaHora.Format = DateTimePickerFormat.Custom;
            dtpFechaHora.CustomFormat = "dd/MM/yyyy HH:mm";

            cBoxEstado.Items.AddRange(new string[] { "Pendiente", "Confirmada", "Cancelada" });
            cBoxEstado.SelectedIndex = 0;

            CargarReservas();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Validaciones de entrada
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
            if (char.IsLetter(e.KeyChar) && txtApellidoCliente.Text.Length >= 100)
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
            if (char.IsDigit(e.KeyChar) && cantPersonas.Text.Length >= 15)
            {
                e.Handled = true;
            }
        }

        // Registrar reserva
        private void btnRegistrarReserva_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoCliente.Text))
            {
                MessageBox.Show("Debe ingresar el nombre y apellido del cliente.");
                return;
            }

            if (cantPersonas.Value <= 0)
            {
                MessageBox.Show("La cantidad de personas debe ser mayor que 0.");
                return;
            }

            string nombre = txtNombreCliente.Text.Trim();
            string apellido = txtApellidoCliente.Text.Trim();
            DateTime fechaHora = dtpFechaHora.Value;
            int cantidad = (int)cantPersonas.Value;
            string estado = cBoxEstado.SelectedItem.ToString();
            int idUsuario = Session.CurrentUser.Id_usuario; // usuario logueado

            try
            {
                var dao = new ReservaDAO();
                int id = dao.RegistrarReserva(nombre, apellido, fechaHora, cantidad, estado, idUsuario);

                MessageBox.Show($"Reserva registrada correctamente.");
                CargarReservas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar reserva: " + ex.Message);
            }
        }

        // Ajustes visuales del DataGridView
        private void AjustarDataGridView(DataGridView dgv)
        {
            // Ajusta columnas al contenido
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Evita las barras de desplazamiento
            dgv.ScrollBars = ScrollBars.None;

            // Permite que el texto se vea correctamente centrado
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Fuente y colores legibles
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
        }

        // Carga las reservas en el DataGridView
        private void CargarReservas()
        {
            var dao = new ReservaDAO();
            dgvReservas.DataSource = dao.ListarReservas();
            AjustarDataGridView(dgvReservas);
        }

        // Limpiar campos del formulario
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreCliente.Clear();  
            txtApellidoCliente.Clear(); 
            cantPersonas.Value = 1;
            dtpFechaHora.Value = DateTime.Now;
            cBoxEstado.SelectedIndex = 0;
        }
    }
}
