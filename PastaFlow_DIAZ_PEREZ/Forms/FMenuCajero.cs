using PastaFlow_DIAZ_PEREZ.Models;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FMenuCajero : Form
    {
        private Button _activeButton;

        public FMenuCajero()
        {
            InitializeComponent();
            this.Load += FMenu_Load;

            // Reposicionar encabezado cuando cambie el tamaño
            this.pnlTop.Resize += (s, e) => AjustarHeader();
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
            var user = Session.CurrentUser;

            if (user != null)
            {
                lbUsuario.Text = $"Bienvenido: {user.Nombre} {user.Apellido}";
                lbRol.Text = $"Rol: {ObtenerNombreRol(user.Id_rol)}";
            }
            else
            {
                lbRol.Text = "Rol: -";
            }

            lbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            // Scroll si fuese necesario
            pnlMenuLateral.AutoScroll = true;

            // Mostrar todos los botones, pero deshabilitar por rol (anulados visibles)
            foreach (var btn in GetBotonesMenu())
            {
                if (btn == null) continue;
                btn.Visible = true;
                btn.Enabled = false;
            }

            if (user != null)
            {
                if (user.Id_rol == 1) // Administrador
                {
                    btnVerReportes.Enabled = true;
                    btnRegEmpleado.Enabled = true;
                    btnVerQuejas.Enabled = true;
                }
                else if (user.Id_rol == 2) // Gerente
                {
                    btnInventario.Enabled = true;
                    btnRegQueja.Enabled = true;
                }
                else if (user.Id_rol == 3) // Cajero
                {
                    btnAbrirCaja.Enabled = true;
                    btnCargarPedido.Enabled = true;
                    btnRegReserva.Enabled = true;
                }
            }

            AplicarEstiloMenu();
            InsertarEspaciadoresMenu(10); // separa botones arriba/abajo
            AjustarHeader();              // evita superposición usuario/fecha/hora

            // Centrar y ajustar la imagen "pasaflow"
            pictureBox1.Dock = DockStyle.Fill;                 // ocupa todo el panel de contenido
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;    // mantiene proporción y centra
            pictureBox1.BackColor = pnlContent.BackColor;      // fondo consistente
        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            lbHora.Text = DateTime.Now.ToString("HH:mm:ss");
            AjustarHeader();
        }

        private void AjustarHeader()
        {
            if (pnlTop == null || lbHora == null || lbFecha == null || lbUsuario == null) return;

            int paddingRight = 12;
            // Ubicar hora y fecha a la derecha
            lbHora.Left = pnlTop.Width - lbHora.Width - paddingRight;
            lbFecha.Left = lbHora.Left - lbFecha.Width - 16;

            // Reservar espacio para el usuario hasta antes de la fecha
            lbUsuario.AutoSize = false;
            int maxRight = Math.Max(0, lbFecha.Left - 16);
            lbUsuario.Width = Math.Max(120, maxRight - lbUsuario.Left);
            lbUsuario.AutoEllipsis = true;
        }

        private void AbrirFormulario(Form formHijo)
        {
            pnlContent.Controls.Clear();
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(formHijo);
            formHijo.Show();
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            MarcarBotonActivo(sender as Button);
            AbrirFormulario(new FAbrirCaja());
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            MarcarBotonActivo(btnCargarPedido);
            AbrirFormulario(new FRegistrarVenta());
        }

        private void btnRegReserva_Click(object sender, EventArgs e)
        {
            MarcarBotonActivo(sender as Button);
            pnlMenuLateral.Visible = false;
            var frm = new FRegistrarReserva();
            frm.FormClosed += (s, args) => { pnlMenuLateral.Visible = true; };
            AbrirFormulario(frm);
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            MarcarBotonActivo(sender as Button);
            pnlMenuLateral.Visible = false;
            var frm = new FGestionarInventario();
            frm.FormClosed += (s, args) => { pnlMenuLateral.Visible = true; };
            AbrirFormulario(frm);
        }

        private void btnRegQueja_Click(object sender, EventArgs e)
        {
            MarcarBotonActivo(sender as Button);
            AbrirFormulario(new FRegistrarQueja());
        }

        private void btnVerReportes_Click(object sender, EventArgs e)
        {
            MarcarBotonActivo(sender as Button);
            AbrirFormulario(new FVerReportes());
        }

        private void btnRegEmpleado_Click(object sender, EventArgs e)
        {
            MarcarBotonActivo(sender as Button);
            pnlMenuLateral.Visible = false;
            var frm = new FRegistrarEmpleado();
            frm.FormClosed += (s, args) => { pnlMenuLateral.Visible = true; };
            AbrirFormulario(frm);
        }

        private void btnVerQueja_Click(object sender, EventArgs e)
        {
            MarcarBotonActivo(sender as Button);
            AbrirFormulario(new FVerQuejas());
        }

        private IEnumerable<Button> GetBotonesMenu()
        {
            return new Button[]
            {
                btnVerQuejas,
                btnRegEmpleado,
                btnVerReportes,
                btnRegQueja,
                btnInventario,
                btnRegReserva,
                btnCargarPedido,
                btnAbrirCaja
            };
        }

        private void AplicarEstiloMenu()
        {
            foreach (var btn in GetBotonesMenu())
            {
                if (btn == null) continue;

                btn.Height = Math.Max(btn.Height, 48);
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                TrySetFont(btn, "Segoe UI", 10.5f, FontStyle.Regular);

                btn.UseVisualStyleBackColor = false;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;

                if (btn.Enabled)
                {
                    btn.BackColor = Palette.Vino;
                    btn.ForeColor = Palette.Beige;
                    btn.Cursor = Cursors.Hand;
                }
                else
                {
                    btn.BackColor = Darken(Palette.Vino, 0.35f);
                    btn.ForeColor = Color.FromArgb(200, Palette.Beige);
                    btn.Cursor = Cursors.No;
                }

                btn.MouseEnter -= Btn_MouseEnter;
                btn.MouseEnter += Btn_MouseEnter;
                btn.MouseLeave -= Btn_MouseLeave;
                btn.MouseLeave += Btn_MouseLeave;

                btn.Resize -= (s, e) => RedondearControl(btn, 8);
                btn.Resize += (s, e) => RedondearControl(btn, 8);
                RedondearControl(btn, 8);
            }
        }

        // Inserta paneles "espaciadores" entre botones dockeados arriba
        private void InsertarEspaciadoresMenu(int alto)
        {
            if (pnlMenuLateral == null) return;

            var existentes = pnlMenuLateral.Controls.Cast<Control>().Where(c => (string)c.Tag == "spacer").ToList();
            foreach (var s in existentes) pnlMenuLateral.Controls.Remove(s);

            foreach (var btn in GetBotonesMenu().Reverse())
            {
                var spacer = new Panel
                {
                    Height = alto,
                    Dock = DockStyle.Top,
                    BackColor = Color.Transparent,
                    Tag = "spacer"
                };
                pnlMenuLateral.Controls.Add(spacer);
                spacer.BringToFront();
                btn.BringToFront();
            }
        }

        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || !btn.Visible || !btn.Enabled) return;
            if (btn == _activeButton) return;
            btn.BackColor = Lighten(Palette.Vino, 0.15f);
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null || !btn.Visible || !btn.Enabled) return;
            if (btn == _activeButton) return;
            btn.BackColor = Palette.Vino;
        }

        private void MarcarBotonActivo(Button btn)
        {
            if (btn == null || !btn.Enabled) return;

            if (_activeButton != null && _activeButton.Visible)
            {
                _activeButton.BackColor = Palette.Vino;
                _activeButton.ForeColor = Palette.Beige;
                TrySetFont(_activeButton, _activeButton.Font.FontFamily.Name, _activeButton.Font.Size, FontStyle.Regular);
            }

            _activeButton = btn;
            if (_activeButton.Visible)
            {
                _activeButton.BackColor = Palette.Dorado;
                _activeButton.ForeColor = Palette.Vino;
                TrySetFont(_activeButton, _activeButton.Font.FontFamily.Name, _activeButton.Font.Size, FontStyle.Bold);
            }
        }

        private void RedondearControl(Control c, int radius)
        {
            if (c == null || c.Width <= 0 || c.Height <= 0) return;
            using (var path = new GraphicsPath())
            {
                int d = radius * 2;
                path.StartFigure();
                path.AddArc(0, 0, d, d, 180, 90);
                path.AddArc(c.Width - d, 0, d, d, 270, 90);
                path.AddArc(c.Width - d, c.Height - d, d, d, 0, 90);
                path.AddArc(0, c.Height - d, d, d, 90, 90);
                path.CloseFigure();
                c.Region = new Region(path);
            }
        }

        private void TrySetFont(Control c, string family, float size, FontStyle style)
        {
            if (c == null) return;
            try { c.Font = new Font(family, size, style); } catch { }
        }

        private static class Palette
        {
            public static readonly Color Vino = ColorTranslator.FromHtml("#7B1E1E");
            public static readonly Color Beige = ColorTranslator.FromHtml("#F4E1A1");
            public static readonly Color Dorado = ColorTranslator.FromHtml("#D4B65F");
        }

        private string ObtenerNombreRol(int idRol)
        {
            switch (idRol)
            {
                case 1: return "Administrador";
                case 2: return "Gerente";
                case 3: return "Cajero";
                default: return "Desconocido";
            }
        }

        private Color Lighten(Color color, float amount)
        {
            amount = Math.Max(0f, Math.Min(1f, amount));
            int r = color.R + (int)((255 - color.R) * amount);
            int g = color.G + (int)((255 - color.G) * amount);
            int b = color.B + (int)((255 - color.B) * amount);
            return Color.FromArgb(color.A, Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
        }

        private Color Darken(Color color, float amount)
        {
            amount = Math.Max(0f, Math.Min(1f, amount));
            int r = (int)(color.R * (1f - amount));
            int g = (int)(color.G * (1f - amount));
            int b = (int)(color.B * (1f - amount));
            return Color.FromArgb(color.A, Math.Max(0, r), Math.Max(0, g), Math.Max(0, b));
        }
    }
}