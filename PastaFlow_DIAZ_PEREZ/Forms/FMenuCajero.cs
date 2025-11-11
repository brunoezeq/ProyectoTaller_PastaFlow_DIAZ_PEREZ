using PastaFlow_DIAZ_PEREZ.DataAccess;
using PastaFlow_DIAZ_PEREZ.Models;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    // Menú principal:
    // - Muestra usuario/rol y fecha/hora
    // - Habilita opciones según rol y estado de caja
    // - Carga formularios hijos en el panel central sin abrir nuevas ventanas
    // - Mantiene un estilo visual consistente (colores, fuentes, cursor, bordes)
    public partial class FMenuCajero : Form
    {
        // Botón actualmente marcado como activo (resalta selección en el menú)
        private Button _activeButton;
        // Imagen de fondo simple para el panel central cuando no hay formulario cargado
        private PictureBox pictureBox1;
        // Flag local para recordar si hay caja abierta
        private bool cajaAbierta = false;

        public FMenuCajero()
        {
            InitializeComponent();

            // Imagen base en el contenedor central
            pictureBox1 = new PictureBox();
            pnlContent.Controls.Add(pictureBox1);

            // Eventos de ciclo de vida y layout
            this.Load += FMenu_Load;
            this.pnlTop.Resize += (s, e) => AjustarHeader();

            // Garantiza que el botón se encuentre cableado una sola vez
            btnAbrirCaja.Click -= btnAbrirCaja_Click;
            btnAbrirCaja.Click += btnAbrirCaja_Click;
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
            var user = Session.CurrentUser;

            // Header: usuario y rol
            if (user != null)
            {
                lbUsuario.Text = $"Bienvenido: {user.Nombre} {user.Apellido}";
                lbRol.Text = $"Rol: {ObtenerNombreRol(user.Id_rol)}";
            }
            else
            {
                lbUsuario.Text = "Bienvenido: -";
                lbRol.Text = "Rol: -";
            }

            lbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            pnlMenuLateral.AutoScroll = true;

            // Todos visibles pero deshabilitados por defecto; luego se habilitan por rol
            foreach (var btn in GetBotonesMenu())
            {
                if (btn == null) continue;
                btn.Visible = true;
                btn.Enabled = false;
            }

            // Habilitar por rol
            if (user != null)
            {
                if (user.Id_rol == 1) // Admin
                {
                    btnVerReportes.Enabled = true;
                    btnRegEmpleado.Enabled = true;
                    btnVerQuejas.Enabled = true;
                    btnVerGraficos.Enabled = true;
                    btnBackup.Enabled = true;
                }
                else if (user.Id_rol == 2) // Gerente
                {
                    btnInventario.Enabled = true;
                    btnRegQueja.Enabled = true;
                }
                else if (user.Id_rol == 3) // Cajero
                {
                    btnAbrirCaja.Enabled = true; // El resto depende de la caja abierta
                }
            }

            // Estilo y espaciado entre botones
            AplicarEstiloMenu();
            InsertarEspaciadoresMenu(10);
            AjustarHeader();

            // Estado inicial de caja desde la sesión (habilita/inhabilita acciones de cajero)
            ActualizarUIEstadoCaja(Session.CurrentCaja != null);

            // Imagen de relleno
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BackColor = pnlContent.BackColor;
        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            // Reloj en vivo
            lbHora.Text = DateTime.Now.ToString("HH:mm:ss");
            AjustarHeader();
        }

        private void AjustarHeader()
        {
            // Reposiciona hora/fecha para que se mantengan alineadas a la derecha
            if (pnlTop == null || lbHora == null || lbFecha == null || lbUsuario == null) return;
            int paddingRight = 12;
            lbHora.Left = pnlTop.Width - lbHora.Width - paddingRight;
            lbFecha.Left = lbHora.Left - lbFecha.Width - 16;
            lbUsuario.AutoSize = false;
            int maxRight = Math.Max(0, lbFecha.Left - 16);
            lbUsuario.Width = Math.Max(120, maxRight - lbUsuario.Left);
            lbUsuario.AutoEllipsis = true;
        }

        private void AbrirFormulario(Form formHijo)
        {
            // Embebe el formulario hijo en el panel central (sin ventanas extras)
            pnlContent.Controls.Clear();
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(formHijo);
            formHijo.Show();
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            // Alterna entre abrir y cerrar caja según el estado actual
            MarcarBotonActivo(sender as Button);

            var user = Session.CurrentUser;
            if (user == null)
            {
                MessageBox.Show("No hay usuario logueado. Inicie sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool estaAbierta = Session.CurrentCaja != null;

            if (!estaAbierta)
            {
                // Abre formulario de apertura embebido
                var frm = new FAbrirCaja();
                frm.CajaAbiertaCorrectamente += (s, ev) =>
                {
                    ActualizarUIEstadoCaja(true);
                    MessageBox.Show("Caja abierta. Puede registrar ventas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                frm.FormClosed += (s, ev) => ActualizarUIEstadoCaja(Session.CurrentCaja != null);
                AbrirFormulario(frm);
            }
            else
            {
                // Abre formulario de cierre embebido
                var frm = new FCerrarCaja(0m, 0m);
                frm.FormClosed += (s, ev) =>
                {
                    ActualizarUIEstadoCaja(Session.CurrentCaja != null);
                    if (Session.CurrentCaja == null)
                        MessageBox.Show("Caja cerrada. No se permiten operaciones de venta.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                AbrirFormulario(frm);
            }
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            // Ventas
            MarcarBotonActivo(btnCargarPedido);
            AbrirFormulario(new FRegistrarVenta());
        }

        private void btnRegReserva_Click(object sender, EventArgs e)
        {
            // Reservas
            MarcarBotonActivo(sender as Button);
            pnlMenuLateral.Visible = false;
            var frm = new FRegistrarReserva();
            frm.FormClosed += (s, args) => { pnlMenuLateral.Visible = true; };
            AbrirFormulario(frm);
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            // Inventario
            MarcarBotonActivo(sender as Button);
            pnlMenuLateral.Visible = false;
            var frm = new FGestionarInventario();
            frm.FormClosed += (s, args) => { pnlMenuLateral.Visible = true; };
            AbrirFormulario(frm);
        }

        private void btnRegQueja_Click(object sender, EventArgs e)
        {
            // Registro de quejas
            MarcarBotonActivo(sender as Button);
            AbrirFormulario(new FRegistrarQueja());
        }

        private void btnVerReportes_Click(object sender, EventArgs e)
        {
            // Reportes
            MarcarBotonActivo(sender as Button);
            AbrirFormulario(new FVerReportes());
        }

        private void btnRegEmpleado_Click(object sender, EventArgs e)
        {
            // ABM de empleados
            MarcarBotonActivo(sender as Button);
            pnlMenuLateral.Visible = false;
            var frm = new FRegistrarEmpleado();
            frm.FormClosed += (s, args) => { pnlMenuLateral.Visible = true; };
            AbrirFormulario(frm);
        }

        private void btnVerQueja_Click(object sender, EventArgs e)
        {
            // Consulta de quejas
            MarcarBotonActivo(sender as Button);
            AbrirFormulario(new FVerQuejas());
        }

        private void btnVerGraficos_Click(object sender, EventArgs e)
        {
            // Panel de gráficos
            MarcarBotonActivo(sender as Button);
            AbrirFormulario(new FGraficos());
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            // Respaldo de base de datos
            MarcarBotonActivo(sender as Button);
            AbrirFormulario(new FBackup());
        }

        private IEnumerable<Button> GetBotonesMenu()
        {
            // Devuelve todos los botones del menú lateral
            return new Button[]
            {
                btnVerQuejas,
                btnRegEmpleado,
                btnVerReportes,
                btnRegQueja,
                btnInventario,
                btnRegReserva,
                btnCargarPedido,
                btnAbrirCaja,
                btnVerGraficos,
                btnBackup
            };
        }

        private void AplicarEstiloMenu()
        {
            // Estilo visual y cursor de cada botón (según Enabled)
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
                    btn.Cursor = Cursors.No; // Señal visual de opción no disponible
                }

                // Hover y redondeado
                btn.MouseEnter -= Btn_MouseEnter;
                btn.MouseEnter += Btn_MouseEnter;
                btn.MouseLeave -= Btn_MouseLeave;
                btn.MouseLeave += Btn_MouseLeave;
                btn.Resize -= (s, e) => RedondearControl(btn, 8);
                btn.Resize += (s, e) => RedondearControl(btn, 8);
                RedondearControl(btn, 8);
            }
        }

        private void InsertarEspaciadoresMenu(int alto)
        {
            // Inserta paneles separadores entre botones para mejorar legibilidad
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
            // Efecto hover si no es el botón activo
            var btn = sender as Button;
            if (btn == null || !btn.Visible || !btn.Enabled) return;
            if (btn == _activeButton) return;
            btn.BackColor = Lighten(Palette.Vino, 0.15f);
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            // Revierte hover si no es el botón activo
            var btn = sender as Button;
            if (btn == null || !btn.Visible || !btn.Enabled) return;
            if (btn == _activeButton) return;
            btn.BackColor = Palette.Vino;
        }

        private void MarcarBotonActivo(Button btn)
        {
            // Marca visualmente el botón elegido y desmarca el anterior
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
            // Bordes redondeados para botones del menú
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
            // Ajuste seguro de tipografía (evita lanzar si la fuente no existe)
            if (c == null) return;
            try { c.Font = new Font(family, size, style); } catch { }
        }

        // Paleta de colores del sistema
        private static class Palette
        {
            public static readonly Color Vino = ColorTranslator.FromHtml("#7B1E1E");
            public static readonly Color Beige = ColorTranslator.FromHtml("#F4E1A1");
            public static readonly Color Dorado = ColorTranslator.FromHtml("#D4B65F");
        }

        private string ObtenerNombreRol(int idRol)
        {
            // Traduce id de rol a nombre legible
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
            // Aclara color base (0..1)
            amount = Math.Max(0f, Math.Min(1f, amount));
            int r = color.R + (int)((255 - color.R) * amount);
            int g = color.G + (int)((255 - color.G) * amount);
            int b = color.B + (int)((255 - color.B) * amount);
            return Color.FromArgb(color.A, Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
        }

        private Color Darken(Color color, float amount)
        {
            // Oscurece color base (0..1)
            amount = Math.Max(0f, Math.Min(1f, amount));
            int r = (int)(color.R * (1f - amount));
            int g = (int)(color.G * (1f - amount));
            int b = (int)(color.B * (1f - amount));
            return Color.FromArgb(color.A, Math.Max(0, r), Math.Max(0, g), Math.Max(0, b));
        }

        private void RefrescarEstadoCaja()
        {
            // Consulta a BD si hay una caja abierta real y actualiza la UI
            try
            {
                var user = Session.CurrentUser;
                if (user == null)
                {
                    ActualizarUIEstadoCaja(false);
                    return;
                }

                var dao = new CajaDAO();
                bool cajaRealAbierta = false;
                try
                {
                    cajaRealAbierta = dao.HayCajaAbierta(user.Id_usuario);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[RefrescarEstadoCaja] Error: " + ex);
                    cajaRealAbierta = false;
                }

                ActualizarUIEstadoCaja(cajaRealAbierta);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[RefrescarEstadoCaja] Error general: " + ex);
            }
        }

        private void ActualizarUIEstadoCaja(bool abierta)
        {
            // Activa/desactiva acciones según haya caja abierta
            cajaAbierta = abierta;
            Session.CajaAbierta = abierta;
            btnAbrirCaja.Text = abierta ? "Cerrar Caja" : "Abrir Caja";

            if (Session.CurrentUser != null && Session.CurrentUser.Id_rol == 3)
            {
                btnCargarPedido.Enabled = abierta;
                btnRegReserva.Enabled = abierta;

                // Colores coherentes con estado
                btnCargarPedido.BackColor = abierta ? Palette.Vino : Darken(Palette.Vino, 0.35f);
                btnRegReserva.BackColor = abierta ? Palette.Vino : Darken(Palette.Vino, 0.35f);
            }
        }
    }
}