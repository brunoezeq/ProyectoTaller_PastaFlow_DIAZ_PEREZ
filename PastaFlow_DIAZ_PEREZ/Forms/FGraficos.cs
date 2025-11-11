using PastaFlow_DIAZ_PEREZ.DataAccess;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    // Muestra tres gráficos:
    // 1) Barras: total vendido por empleado (TOP_N)
    // 2) Columnas: productos más vendidos (TOP_N)
    // 3) Pie: distribución por método de pago
    // Usa rango de fechas inclusivo (desde 00:00 hasta fin del día) y maneja ausencia de datos.
    public partial class FGraficos : Form
    {
        private const int TOP_N = 5; // Cantidad de elementos máximo por gráfico

        public FGraficos()
        {
            InitializeComponent();
        }

        private void FGraficos_Load(object sender, EventArgs e)
        {
            // Reasigna handler para evitar duplicados
            btnActualizarGraficos.Click -= BtnActualizarGraficos_Click;
            btnActualizarGraficos.Click += BtnActualizarGraficos_Click;

            PrepararCharts();
            CargarGraficos();
        }

        private void BtnActualizarGraficos_Click(object sender, EventArgs e) => CargarGraficos();

        // Limpia series iniciales
        private void PrepararCharts()
        {
            foreach (var ch in new[] { chartEmpleados, chartProductos, chartMetodos })
                ch.Series.Clear();
        }

        // Obtiene datos y arma cada gráfico
        private void CargarGraficos()
        {
            try
            {
                var dao = new ReporteDAO();

                // Rango inclusivo completo de días
                DateTime? desde = dtpDesdeGraficos.Value.Date;
                DateTime? hasta = dtpHastaGraficos.Value.Date.AddDays(1).AddTicks(-1);

                // Ventas por empleado
                var dtEmp = dao.VentasPorEmpleado(desde, hasta);
                dtEmp = FiltrarTop(dtEmp, "Total");
                ConfigurarYBind(chartEmpleados, dtEmp, "Empleado", "Total",
                    SeriesChartType.Bar, "Ventas por empleado");

                // Productos más vendidos
                var dtProd = dao.TopProductos(desde, hasta);
                dtProd = FiltrarTop(dtProd, "CantidadVendida");
                ConfigurarYBind(chartProductos, dtProd, "Producto", "CantidadVendida",
                    SeriesChartType.Column, "Top productos");

                // Métodos de pago (pie)
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
                    int idx = sMet.Points.AddY((double)total);
                    var p = sMet.Points[idx];
                    p.LegendText = nombre;
                    p.Label = $"{nombre}\n{total:C0}";
                }

                if (dtMet.Rows.Count == 0)
                {
                    int idx = sMet.Points.AddY(1);
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

        // Devuelve tabla con máximo TOP_N filas ordenadas desc por la columna indicada
        private DataTable FiltrarTop(DataTable dt, string colOrden)
        {
            if (dt == null || dt.Rows.Count == 0) return dt;
            var orden = dt.AsEnumerable()
                          .OrderByDescending(r => Convert.ToDecimal(r[colOrden]))
                          .Take(TOP_N);
            return orden.CopyToDataTable();
        }

        // Configura serie y hace DataBind; crea serie "Sin datos" si la tabla está vacía
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

            // Ajustes de eje X para legibilidad
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        }
    }
}
