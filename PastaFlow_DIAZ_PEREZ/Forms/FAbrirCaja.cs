using PastaFlow_DIAZ_PEREZ.Utils;
using PastaFlow_DIAZ_PEREZ.DataAccess;
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
        // Datos resultantes de la apertura
        public decimal MontoApertura { get; private set; }
        public DateTime FechaApertura { get; private set; }

        public FAbrirCaja()
        {
            InitializeComponent();
        }

        private void FAbrirCaja_Load(object sender, EventArgs e)
        {
            var user = Session.CurrentUser;
            if (user != null)
                lbCajero.Text = $"{user.Nombre} {user.Apellido}";
            lbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbHora.Text = DateTime.Now.ToString("HH:mm");
        }

        private void txtMontoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Acepta dígitos, control y un único punto
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        // Notifica a quien embebe el formulario que la caja quedó abierta
        public event EventHandler CajaAbiertaCorrectamente;

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            var texto = txtMontoInicial.Text.Trim();
            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("Ingrese el monto inicial.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoInicial.Focus();
                return;
            }

            decimal monto;
            var parsed =
                decimal.TryParse(texto, System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.CurrentCulture, out monto)
             || decimal.TryParse(texto, System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture, out monto);

            if (!parsed)
            {
                MessageBox.Show("Monto inicial inválido. Use sólo números y un separador decimal.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoInicial.Focus();
                return;
            }

            if (monto < 0m)
            {
                MessageBox.Show("El monto inicial no puede ser negativo.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoInicial.Focus();
                return;
            }

            var dr = MessageBox.Show($"Abrir caja con monto inicial {monto:C}?",
                "Confirmar apertura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            try
            {
                var dao = new CajaDAO();
                var nuevaCaja = dao.AbrirCaja(Session.CurrentUser.Id_usuario, monto, DateTime.Now);

                if (nuevaCaja != null)
                {
                    Session.CurrentCaja = nuevaCaja;
                    MontoApertura = monto;
                    FechaApertura = DateTime.Now;

                    lbFecha.Text = FechaApertura.ToString("dd/MM/yyyy");
                    lbHora.Text = FechaApertura.ToString("HH:mm");

                    btnAbrirCaja.Enabled = false;
                    txtMontoInicial.Enabled = false;

                    MessageBox.Show($"Caja abierta correctamente (ID: {nuevaCaja.Id_caja}).",
                        "Apertura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CajaAbiertaCorrectamente?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la apertura de caja.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la caja: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
