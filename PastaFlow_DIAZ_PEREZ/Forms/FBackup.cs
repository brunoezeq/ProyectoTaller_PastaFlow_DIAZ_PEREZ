using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FBackup : Form
    {
        private readonly BackupDAO _backupDao = new BackupDAO();

        public FBackup()
        {
            InitializeComponent();
        }

        private void FBackup_Load(object sender, EventArgs e)
        {
            ConfigurarDgvHistorial();          // <-- configurar columnas antes de enlazar
            MostrarUltimoBackup();
            CargarHistorialBackups();
        }

        private void btnSeleccionarRuta_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Backup SQL (*.bak)|*.bak";
                sfd.Title = "Seleccionar ubicación para guardar el backup";
                sfd.FileName = $"PastaFlowBD_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";

                if (sfd.ShowDialog() == DialogResult.OK)
                    txtRuta.Text = sfd.FileName;
            }
        }

        private void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRuta.Text))
                {
                    MessageBox.Show("Seleccione una ruta válida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var usuarioId = Session.CurrentUser?.Id_usuario ?? 0;
                _backupDao.RealizarBackup(txtRuta.Text, usuarioId);

                MessageBox.Show("Backup realizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MostrarUltimoBackup();
                CargarHistorialBackups();

                if (File.Exists(txtRuta.Text))
                {
                    if (MessageBox.Show("¿Desea abrir la carpeta donde se guardó el backup?", "Backup generado",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start("explorer.exe", "/select,\"" + txtRuta.Text + "\"");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar backup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarUltimoBackup()
        {
            var info = _backupDao.ObtenerUltimoBackup();
            if (info != null)
            {
                lblUltimoBackup.Text = $"Último backup: {info.Value.Fecha:dd/MM/yyyy HH:mm}\n" +
                                       $"Realizado por: {info.Value.Usuario}\n" +
                                       $"Ruta: {info.Value.Ruta}";
            }
            else
            {
                lblUltimoBackup.Text = "No hay backups registrados aún.";
            }
        }

        private void CargarHistorialBackups()
        {
            try
            {
                var dt = _backupDao.ObtenerHistorialBackups();
                dgvHistorial.DataSource = dt; // columnas ya configuradas
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial de backups: " + ex.Message);
            }
        }

        private void ConfigurarDgvHistorial()
        {
            var g = dgvHistorial;
            g.AutoGenerateColumns = false;
            g.ReadOnly = true;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.RowHeadersVisible = false;

            // Crear columnas una solo vez
            if (!g.Columns.Contains("ID"))
                g.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID", DataPropertyName = "ID", Visible = false });

            if (!g.Columns.Contains("Fecha y hora"))
                g.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha y hora", HeaderText = "Fecha y hora", DataPropertyName = "Fecha y hora", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });

            if (!g.Columns.Contains("Usuario"))
                g.Columns.Add(new DataGridViewTextBoxColumn { Name = "Usuario", HeaderText = "Usuario", DataPropertyName = "Usuario", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });

            if (!g.Columns.Contains("Ruta del archivo"))
                g.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ruta del archivo", HeaderText = "Ruta del archivo", DataPropertyName = "Ruta del archivo", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private void dgvHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar borrar columnas; opcional: abrir ubicación al hacer click en la ruta
            if (e.RowIndex < 0) return;
            if (dgvHistorial.Columns[e.ColumnIndex].Name != "Ruta del archivo") return;

            var ruta = Convert.ToString(dgvHistorial.Rows[e.RowIndex].Cells["Ruta del archivo"].Value);
            if (!string.IsNullOrWhiteSpace(ruta) && File.Exists(ruta))
                Process.Start("explorer.exe", "/select,\"" + ruta + "\"");
        }

        private void btnRestaurarBackup_Click(object sender, EventArgs e)
        {
            // Lógica para restaurar el backup aquí
            MessageBox.Show("Funcionalidad de restaurar backup aún no implementada.");
        }
    }
}
