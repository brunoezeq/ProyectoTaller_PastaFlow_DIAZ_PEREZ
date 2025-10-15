using PastaFlow_DIAZ_PEREZ.DataAccess;
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
        public FVerQuejas()
        {
            InitializeComponent();
        }

        private void FVerQuejas_Load(object sender, EventArgs e)
        {
            CargarQuejas();
        }

        private void CargarQuejas(DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var dao = new QuejaDAO();
            DataTable dt = dao.BuscarQuejas(fechaInicio, fechaFin);
            dgvQuejas.DataSource = dt;

            // Ajustar apariencia
            dgvQuejas.DefaultCellStyle.ForeColor = Color.Black;
            dgvQuejas.ColumnHeadersDefaultCellStyle.Font = new Font(dgvQuejas.Font, FontStyle.Bold);
            dgvQuejas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvQuejas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Agregar botón Eliminar si no existe
            if (!dgvQuejas.Columns.Contains("Accion"))
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.Name = "Accion";
                btnEliminar.HeaderText = "Acción";
                btnEliminar.Text = "Eliminar";
                btnEliminar.UseColumnTextForButtonValue = true;
                dgvQuejas.Columns.Add(btnEliminar);
            }
        }

        private void dgvQuejas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvQuejas.Columns[e.ColumnIndex].Name == "Accion")
            {
                int idQueja = Convert.ToInt32(dgvQuejas.Rows[e.RowIndex].Cells["id_queja"].Value);

                DialogResult dr = MessageBox.Show(
                    "¿Desea eliminar esta queja?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dr == DialogResult.Yes)
                {
                    var dao = new QuejaDAO();
                    dao.EliminarQueja(idQueja);
                    MessageBox.Show("Queja eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarQuejas();
                }
            }
        }
        private void btnFiltrarQueja_Click(object sender, EventArgs e)
        {
            DateTime? desde = dtpDesde.Value.Date;
            DateTime? hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);
            CargarQuejas(desde, hasta);
        }

        private void btnLimpiarQuejas_Click(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now;
            dtpHasta.Value = DateTime.Now;
            CargarQuejas();
        }

        
    }

}

