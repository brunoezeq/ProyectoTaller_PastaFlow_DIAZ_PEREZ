using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    // Cierre de caja:
    // - Muestra totales por método de pago y calcula efectivo total.
    // - Calcula monto esperado = monto inicial + total efectivo.
    // - Permite ingresar el monto actual contado, muestra diferencia y confirma el cierre.
    // - Persiste el cierre vía CajaDAO y restablece Session.CurrentCaja a null.
    public partial class FCerrarCaja : Form
    {
        private CajaDAO cajaDao = new CajaDAO();

        private decimal montoInicial;
        private decimal totalEfectivo;
        private decimal montoEsperado;

        public FCerrarCaja(decimal montoInicial, decimal totalVentas)
        {
            InitializeComponent();
        }

        private void FCerrarCaja_Load(object sender, EventArgs e)
        {
            var dao = new CajaDAO();
            if (Session.CurrentCaja == null)
            {
                MessageBox.Show("No hay ninguna caja abierta actualmente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            int idCaja = Session.CurrentCaja.Id_caja;

            // Totales por método de pago para informar en UI (no se usan en el cálculo de efectivo)
            var totalesPorMetodo = dao.ObtenerTotalesPorMetodo(Session.CurrentCaja.Id_caja);
            decimal totalTransferencia = totalesPorMetodo.ContainsKey("Transferencia") ? totalesPorMetodo["Transferencia"] : 0m;
            decimal totalDebito = totalesPorMetodo.ContainsKey("Débito") ? totalesPorMetodo["Débito"] : 0m;
            decimal totalCredito = totalesPorMetodo.ContainsKey("Crédito") ? totalesPorMetodo["Crédito"] : 0m;

            // Mostrar totales por método de pago
            txtTransferencia.Text = totalTransferencia.ToString("C");
            txtDebito.Text = totalDebito.ToString("C");
            txtCredito.Text = totalCredito.ToString("C");

            // Calcular montos base para el cierre
            montoInicial = Session.CurrentCaja.Monto_inicio;
            totalEfectivo = cajaDao.ObtenerTotalVentasEfectivo(idCaja);
            montoEsperado = montoInicial + totalEfectivo;

            // Mostrar montos al cierre
            txtMontoInicial.Text = montoInicial.ToString("C");
            txtTotalEfectivo.Text = totalEfectivo.ToString("C");
            txtMontoEsperado.Text = montoEsperado.ToString("C");

            txtMontoActual.Focus();   // foco para ingresar conteo real
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            // Parseo tolerante: reemplaza coma por punto y usa InvariantCulture
            if (!decimal.TryParse(txtMontoActual.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal montoActual))
            {
                MessageBox.Show("Ingrese un monto actual válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Diferencia entre lo contado y lo esperado (solo efectivo)
            decimal diferencia = montoActual - montoEsperado;
            string mensaje;
            MessageBoxIcon icono;

            if (diferencia > 0)
            {
                mensaje = $"SOBRANTE: ${diferencia:0.00}\n\n¿Desea cerrar la caja igualmente?";
                icono = MessageBoxIcon.Information;
            }
            else if (diferencia < 0)
            {
                mensaje = $"FALTANTE: ${Math.Abs(diferencia):0.00}\n\n¿Desea cerrar la caja igualmente?";
                icono = MessageBoxIcon.Warning;
            }
            else
            {
                mensaje = "Monto exacto. Caja sin diferencias.\n\n¿Desea cerrar la caja?";
                icono = MessageBoxIcon.Information;
            }

            var dr = MessageBox.Show(mensaje, "Resultado del cierre", MessageBoxButtons.YesNo, icono);
            if (dr != DialogResult.Yes) return;

            try
            {
                cajaDao.CerrarCaja(Session.CurrentCaja.Id_caja, montoActual);
                MessageBox.Show("Caja cerrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Session.CurrentCaja = null; 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar la caja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Sale 
        }
    }
}
