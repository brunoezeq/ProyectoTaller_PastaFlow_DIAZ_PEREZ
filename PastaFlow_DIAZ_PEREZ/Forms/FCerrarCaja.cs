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
    // Formulario para cerrar la caja: muestra totales por método de pago, calcula efectivo esperado
    // y permite registrar el cierre comparando contra el monto contado.
    public partial class FCerrarCaja : Form
    {
        // DAO para operaciones de caja (totales y cierre)
        private CajaDAO cajaDao = new CajaDAO();

        // Montos calculados para el cierre
        private decimal montoInicial;      // Monto con que se abrió la caja
        private decimal totalEfectivo;     // Ventas cobradas en efectivo
        private decimal montoEsperado;     // Suma de monto inicial + efectivo

        // Recibe montos iniciales/ventas, pero actualmente se recalculan en Load desde la sesión.
        // Podría usarse si se quisiera pasar datos precargados (se dejó por compatibilidad).
        public FCerrarCaja(decimal montoInicial, decimal totalVentas)
        {
            InitializeComponent();
        }

        private void FCerrarCaja_Load(object sender, EventArgs e)
        {
            var dao = new CajaDAO();

            // Validar que exista una caja abierta en la sesión
            if (Session.CurrentCaja == null)
            {
                MessageBox.Show("No hay ninguna caja abierta actualmente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            int idCaja = Session.CurrentCaja.Id_caja;

            // Obtener totales por método de pago (Transferencia, Débito, Crédito)
            var totalesPorMetodo = dao.ObtenerTotalesPorMetodo(Session.CurrentCaja.Id_caja);
            decimal totalTransferencia = totalesPorMetodo.ContainsKey("Transferencia") ? totalesPorMetodo["Transferencia"] : 0m;
            decimal totalDebito = totalesPorMetodo.ContainsKey("Débito") ? totalesPorMetodo["Débito"] : 0m;
            decimal totalCredito = totalesPorMetodo.ContainsKey("Crédito") ? totalesPorMetodo["Crédito"] : 0m;

            // Mostrar totales por método
            txtTransferencia.Text = totalTransferencia.ToString("C");
            txtDebito.Text = totalDebito.ToString("C");
            txtCredito.Text = totalCredito.ToString("C");

            // Montos base para cierre
            montoInicial = Session.CurrentCaja.Monto_inicio;
            totalEfectivo = cajaDao.ObtenerTotalVentasEfectivo(idCaja);
            montoEsperado = montoInicial + totalEfectivo;

            // Mostrar en interfaz
            txtMontoInicial.Text = montoInicial.ToString("C");
            txtTotalEfectivo.Text = totalEfectivo.ToString("C");
            txtMontoEsperado.Text = montoEsperado.ToString("C");

            // Foco para ingresar lo contado
            txtMontoActual.Focus();
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            // Parsear monto actual contado; admite punto como separador
            if (!decimal.TryParse(txtMontoActual.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal montoActual))
            {
                MessageBox.Show("Ingrese un monto actual válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calcular diferencia entre contado y esperado
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

            // Confirmar cierre con la diferencia mostrada
            var dr = MessageBox.Show(mensaje, "Resultado del cierre", MessageBoxButtons.YesNo, icono);
            if (dr != DialogResult.Yes) return;

            try
            {
                // Registrar cierre y limpiar sesión
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
            // Salir sin cerrar caja
            this.Close();
        }
    }
}
