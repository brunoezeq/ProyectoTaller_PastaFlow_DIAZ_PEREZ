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
using System.Windows.Forms.DataVisualization.Charting;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FGraficos : Form
    {
        public FGraficos()
        {
            InitializeComponent();
        }

        private void FGraficos_Load(object sender, EventArgs e)
        {
            btnActualizarGraficos.Click -= BtnActualizarGraficos_Click;
            btnActualizarGraficos.Click += BtnActualizarGraficos_Click;
            CargarGraficos();

        }

        private void CargarGraficos()
        {
            var dao = new ReporteDAO();

            DateTime? desde = FindControl<DateTimePicker>("dtpDesdeGraficos")?.Value.Date;
            DateTime? hasta = FindControl<DateTimePicker>("dtpHastaGraficos")?.Value.Date;

            // 1️⃣ Gráfico de empleados
            var dtEmp = dao.VentasPorEmpleado(desde, hasta);
            chartEmpleados.Series.Clear();
            var sEmp = new Series("");
            sEmp.ChartType = SeriesChartType.Bar;
            foreach (DataRow r in dtEmp.Rows)
                sEmp.Points.AddXY(r["Empleado"], Convert.ToDecimal(r["Total"]));
            chartEmpleados.Series.Add(sEmp);
            chartEmpleados.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

            // 2️⃣ Gráfico de productos
            var dtProd = dao.TopProductos(desde, hasta);
            chartProductos.Series.Clear();
            var sProd = new Series("");
            sProd.ChartType = SeriesChartType.Column;
            foreach (DataRow r in dtProd.Rows)
                sProd.Points.AddXY(r["Producto"], Convert.ToInt32(r["CantidadVendida"]));
            chartProductos.Series.Add(sProd);

            // 3️⃣ Gráfico de métodos de pago
            var dtMet = dao.TotalesPorMetodoPago(desde, hasta);
            chartMetodos.Series.Clear();
            var sMet = new Series("");
            sMet.ChartType = SeriesChartType.Pie;
            foreach (DataRow r in dtMet.Rows)
            {
                string nombre = r["MetodoPago"].ToString();
                decimal total = Convert.ToDecimal(r["Total"]);
                var point = sMet.Points.AddY((double)total);
                sMet.Points[point].LegendText = nombre;
                sMet.Points[point].Label = $"{nombre} ({total:C0})";
            }
            chartMetodos.Series.Add(sMet);
        }

        private void BtnActualizarGraficos_Click(object sender, EventArgs e)
        {
            CargarGraficos();
        }

        private T FindControl<T>(string name) where T : Control
        {
            return this.Controls.Find(name, true)
                       .OfType<T>()
                       .FirstOrDefault();
        }

    }
}
