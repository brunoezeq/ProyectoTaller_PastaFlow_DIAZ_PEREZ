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
using System.Diagnostics;
using System.IO;


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
                var dao = new BackupDAO();
                DataTable dt = dao.ObtenerHistorialBackups();

                dgvHistorial.AutoGenerateColumns = false;
                dgvHistorial.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial de backups: " + ex.Message);
            }
        }

        private void btnRestaurarBackup_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Title = "Seleccionar archivo de respaldo (.bak)";
                    ofd.Filter = "Archivos de respaldo SQL (*.bak)|*.bak";

                    if (ofd.ShowDialog() != DialogResult.OK)
                        return;

                    string rutaBackup = ofd.FileName;
                    var dao = new BackupDAO();

                    dao.RestaurarBackup(rutaBackup);

                    MessageBox.Show("Base de datos restaurada correctamente.", "Restauración completada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restaurar backup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvHistorial.Columns.Clear();
            dgvHistorial.Columns.Add("ID", "ID");
            dgvHistorial.Columns.Add("Fecha y hora", "Fecha y hora");
            dgvHistorial.Columns.Add("Usuario", "Usuario");
            dgvHistorial.Columns.Add("Ruta del archivo", "Ruta del archivo");

            dgvHistorial.Columns["Ruta del archivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
