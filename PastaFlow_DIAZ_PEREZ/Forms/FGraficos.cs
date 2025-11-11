using PastaFlow_DIAZ_PEREZ.DataAccess;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    // Panel de gráficos:
    // - Ventas por empleado (barras, TOP_N).
    // - Productos más vendidos (columnas, TOP_N).
    // - Distribución por método de pago (torta).
    // - Usa rango de fechas inclusivo (00:00 a 23:59:59) y maneja “sin datos”.
    public partial class FGraficos : Form
    {
        private const int TOP_N = 5;

        public FGraficos()
        {
            InitializeComponent();
        }

        private void FGraficos_Load(object sender, EventArgs e)
        {
            btnActualizarGraficos.Click -= BtnActualizarGraficos_Click;
            btnActualizarGraficos.Click += BtnActualizarGraficos_Click;
            PrepararCharts();
            CargarGraficos();
        }

        private void BtnActualizarGraficos_Click(object sender, EventArgs e)
        {
            CargarGraficos();
        }

        private void PrepararCharts()
        {
            foreach (var ch in new[] { chartEmpleados, chartProductos, chartMetodos })
                ch.Series.Clear();
        }

        private void CargarGraficos()
        {
            try
            {
                var dao = new ReporteDAO();

                DateTime? desde = dtpDesdeGraficos.Value.Date;
                DateTime? hasta = dtpHastaGraficos.Value.Date.AddDays(1).AddTicks(-1);

                var dtEmp = dao.VentasPorEmpleado(desde, hasta);
                dtEmp = FiltrarTop(dtEmp, "Total");
                ConfigurarYBind(chartEmpleados, dtEmp, "Empleado", "Total",
                    SeriesChartType.Bar, "Ventas por empleado");

                var dtProd = dao.TopProductos(desde, hasta);
                dtProd = FiltrarTop(dtProd, "CantidadVendida");
                ConfigurarYBind(chartProductos, dtProd, "Producto", "CantidadVendida",
                    SeriesChartType.Column, "Top productos");

                var dtMet = dao.TotalesPorMetodoPago(desde, hasta);
                chartMetodos.Series.Clear();
                var sMet = new Series("Métodos de pago")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true
                };
                foreach (DataRow r in dtMet.Rows)
                {
                    string nombre = r["MetodoPago"].ToString();
                    decimal total = Convert.ToDecimal(r["Total"]);
                    var idx = sMet.Points.AddY((double)total);
                    var p = sMet.Points[idx];
                    p.LegendText = nombre;
                    p.Label = $"{nombre}\n{total:C0}";
                }
                if (dtMet.Rows.Count == 0)
                {
                    var idx = sMet.Points.AddY(1);
                    sMet.Points[idx].LegendText = "Sin datos";
                    sMet.Points[idx].Label = "Sin datos";
                }
                chartMetodos.Series.Add(sMet);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gráficos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable FiltrarTop(DataTable dt, string colOrden)
        {
            if (dt == null || dt.Rows.Count == 0) return dt;
            var orden = dt.AsEnumerable()
                          .OrderByDescending(r => Convert.ToDecimal(r[colOrden]))
                          .Take(TOP_N);
            return orden.CopyToDataTable();
        }

        private void ConfigurarYBind(Chart chart, DataTable dt,
            string colX, string colY, SeriesChartType tipo, string nombreSerie)
        {
            chart.Series.Clear();
            var s = new Series(nombreSerie)
            {
                ChartType = tipo,
                XValueMember = colX,
                YValueMembers = colY
            };
            chart.DataSource = dt;
            chart.Series.Add(s);
            chart.DataBind();

            if (dt == null || dt.Rows.Count == 0)
            {
                chart.Series.Clear();
                var vacia = new Series("Sin datos")
                {
                    ChartType = SeriesChartType.Column
                };
                vacia.Points.AddXY("Sin datos", 0);
                chart.Series.Add(vacia);
            }

            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        }
    }
}
