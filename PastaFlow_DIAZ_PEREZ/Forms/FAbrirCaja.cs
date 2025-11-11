using PastaFlow_DIAZ_PEREZ.Utils;
using PastaFlow_DIAZ_PEREZ.DataAccess;
using System;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    // Apertura de caja:
    // - Muestra cajero y fecha/hora actuales.
    // - Valida el monto inicial (numérico, un separador decimal, no negativo).
    // - Abre la caja vía CajaDAO, guarda en Session.CurrentCaja y dispara evento de éxito.
    // - Maneja culturas al parsear el monto (admite coma o punto).
    public partial class FAbrirCaja : Form
    {
        // Propiedades públicas para que quien abra el formulario recupere la apertura
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
            {
                lbCajero.Text = $"{user.Nombre} {user.Apellido}";
            }
            lbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbHora.Text = DateTime.Now.ToString("HH:mm");
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

        // Evento para avisar que se abrió correctamente a quien embeba este form
        public event EventHandler CajaAbiertaCorrectamente;

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            // Validar entrada
            var texto = txtMontoInicial.Text.Trim();
            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("Ingrese el monto inicial.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoInicial.Focus();
                return;
            }

            // Intentar parsear con cultura actual e invariante (acepta ',' o '.')
            decimal monto;
            var parsed = decimal.TryParse(texto, System.Globalization.NumberStyles.Number,
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

            // Confirmar apertura
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

                    // Notificar a quien embebe este formulario
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

        // Botón Atrás: cierra el formulario
        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
