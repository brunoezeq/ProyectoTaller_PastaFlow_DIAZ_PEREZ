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
            var parsed = decimal.TryParse(texto, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out monto)
                      || decimal.TryParse(texto, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out monto);

            if (!parsed)
            {
                MessageBox.Show("Monto inicial inválido. Use sólo números y un separador decimal.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoInicial.Focus();
                return;
            }

            if (monto < 0m)
            {
                MessageBox.Show("El monto inicial no puede ser negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontoInicial.Focus();
                return;
            }

            // Confirmar apertura
            var dr = MessageBox.Show($"Abrir caja con monto inicial {monto:C}?", "Confirmar apertura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            // Persistir con el DAO
            try
            {
                var dao = new CajaDAO();

                // opcional: comprobar si ya hay una caja abierta
                int abiertaId;
                if (dao.EstaCajaAbierta(out abiertaId))
                {
                    MessageBox.Show("Ya existe una caja abierta. Cierre la caja anterior antes de abrir una nueva.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = dao.AbrirCaja(Session.CurrentUser.Id_usuario, monto, DateTime.Now);
                if (id > 0)
                {
                    // Guardar valores en propiedades públicas para que el llamador los use
                    MontoApertura = monto;
                    FechaApertura = DateTime.Now;

                    // Actualizar visualmente
                    lbFecha.Text = FechaApertura.ToString("dd/MM/yyyy");
                    lbHora.Text = FechaApertura.ToString("HH:mm");

                    // Desactivar controles para evitar duplicados
                    btnAbrirCaja.Enabled = false;
                    txtMontoInicial.Enabled = false;

                    MessageBox.Show("Caja abierta correctamente.", "Apertura", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Devolver OK al llamador (por ejemplo si llamó ShowDialog)
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la apertura de caja. Revise la configuración.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la caja: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento para el botón Atrás
        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
