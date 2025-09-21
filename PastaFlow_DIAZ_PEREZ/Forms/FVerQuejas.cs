using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FVerQuejas : Form
    {
        // Campo de clase: debe estar aquí
        private List<dynamic> listaQuejas;

        public FVerQuejas()
        {
            InitializeComponent();
            this.Load += FVerQuejas_Load;
            dgsQuejas.CellContentClick += dgsQuejas_CellContentClick;
        }

        private void FVerQuejas_Load(object sender, EventArgs e)
        {
            CargarQuejas();
        }

        private void CargarQuejas()
        {
            // Asigna la lista al campo de clase, NO como variable local
            listaQuejas = new List<dynamic>
            {
                new { Id = 1, Cliente = "Juan Pérez", Fecha = DateTime.Now.AddDays(-2), Descripcion = "Demora en la entrega " },
                new { Id = 2, Cliente = "Ana Díaz", Fecha = DateTime.Now.AddDays(-1), Descripcion = "Producto defectuoso" }
            };

            var tabla = new DataTable();
            tabla.Columns.Add("Id", typeof(int));
            tabla.Columns.Add("Cliente", typeof(string));
            tabla.Columns.Add("Fecha", typeof(DateTime));
            tabla.Columns.Add("Descripcion", typeof(string));

            foreach (var q in listaQuejas)
            {
                string descCorta = q.Descripcion.Length > 20 ? q.Descripcion.Substring(0, 20) + "..." : q.Descripcion;
                tabla.Rows.Add(q.Id, q.Cliente, q.Fecha, descCorta);
            }

            dgsQuejas.DataSource = tabla;

            // Si la columna de botón ya existe, elimínala para evitar duplicados
            if (dgsQuejas.Columns.Contains("Ver"))
                dgsQuejas.Columns.Remove("Ver");

            // Agrega la columna de botón "Ver"
            var btnCol = new DataGridViewButtonColumn();
            btnCol.Name = "Ver";
            btnCol.HeaderText = "Ver";
            btnCol.Text = "Ver";
            btnCol.UseColumnTextForButtonValue = true;
            dgsQuejas.Columns.Add(btnCol);

            dgsQuejas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgsQuejas.ColumnHeadersDefaultCellStyle.Font = new Font(dgsQuejas.Font, FontStyle.Bold);
            dgsQuejas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgsQuejas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            if (dgsQuejas.Columns["Descripcion"] != null)
            {
                dgsQuejas.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgsQuejas.Columns["Descripcion"].FillWeight = 200;
            }
        }

        private void dgsQuejas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgsQuejas.Columns[e.ColumnIndex].Name == "Ver" && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dgsQuejas.Rows[e.RowIndex].Cells["Id"].Value);
                // Aquí accede al campo de clase, no a una variable local
                var queja = listaQuejas.FirstOrDefault(q => q.Id == id);
                if (queja != null)
                {
                    MessageBox.Show(queja.Descripcion, "Descripción completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
