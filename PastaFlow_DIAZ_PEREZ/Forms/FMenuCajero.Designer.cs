namespace PastaFlow_DIAZ_PEREZ.Forms
{
    partial class FMenuCajero
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMenuCajero));
            this.pnlMenuLateral = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lbHora = new System.Windows.Forms.Label();
            this.lbFecha = new System.Windows.Forms.Label();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.timerHora = new System.Windows.Forms.Timer(this.components);
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnVerQuejas = new System.Windows.Forms.Button();
            this.btnRegEmpleado = new System.Windows.Forms.Button();
            this.btnVerReportes = new System.Windows.Forms.Button();
            this.btnRegQueja = new System.Windows.Forms.Button();
            this.btnInventario = new System.Windows.Forms.Button();
            this.btnRegReserva = new System.Windows.Forms.Button();
            this.btnCargarPedido = new System.Windows.Forms.Button();
            this.btnAbrirCaja = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlMenuLateral.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMenuLateral
            // 
            this.pnlMenuLateral.BackColor = System.Drawing.Color.DarkRed;
            this.pnlMenuLateral.Controls.Add(this.btnVerQuejas);
            this.pnlMenuLateral.Controls.Add(this.btnRegEmpleado);
            this.pnlMenuLateral.Controls.Add(this.btnVerReportes);
            this.pnlMenuLateral.Controls.Add(this.btnRegQueja);
            this.pnlMenuLateral.Controls.Add(this.btnInventario);
            this.pnlMenuLateral.Controls.Add(this.btnRegReserva);
            this.pnlMenuLateral.Controls.Add(this.btnCargarPedido);
            this.pnlMenuLateral.Controls.Add(this.btnAbrirCaja);
            this.pnlMenuLateral.Controls.Add(this.pnlHeader);
            this.pnlMenuLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenuLateral.Location = new System.Drawing.Point(0, 0);
            this.pnlMenuLateral.Name = "pnlMenuLateral";
            this.pnlMenuLateral.Size = new System.Drawing.Size(209, 505);
            this.pnlMenuLateral.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(209, 106);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lbHora);
            this.pnlTop.Controls.Add(this.lbFecha);
            this.pnlTop.Controls.Add(this.lbUsuario);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(209, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(853, 33);
            this.pnlTop.TabIndex = 1;
            // 
            // lbHora
            // 
            this.lbHora.AutoSize = true;
            this.lbHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHora.Location = new System.Drawing.Point(785, 9);
            this.lbHora.Name = "lbHora";
            this.lbHora.Size = new System.Drawing.Size(38, 15);
            this.lbHora.TabIndex = 2;
            this.lbHora.Text = "Hora";
            // 
            // lbFecha
            // 
            this.lbFecha.AutoSize = true;
            this.lbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFecha.Location = new System.Drawing.Point(706, 9);
            this.lbFecha.Name = "lbFecha";
            this.lbFecha.Size = new System.Drawing.Size(46, 15);
            this.lbFecha.TabIndex = 1;
            this.lbFecha.Text = "Fecha";
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsuario.Location = new System.Drawing.Point(6, 9);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(78, 15);
            this.lbUsuario.TabIndex = 0;
            this.lbUsuario.Text = "Bienvenido";
            // 
            // timerHora
            // 
            this.timerHora.Enabled = true;
            this.timerHora.Interval = 1000;
            this.timerHora.Tick += new System.EventHandler(this.timerHora_Tick);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(209, 33);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(853, 472);
            this.pnlContent.TabIndex = 2;
            // 
            // btnVerQuejas
            // 
            this.btnVerQuejas.BackColor = System.Drawing.Color.Transparent;
            this.btnVerQuejas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVerQuejas.FlatAppearance.BorderSize = 0;
            this.btnVerQuejas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerQuejas.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerQuejas.ForeColor = System.Drawing.Color.White;
            this.btnVerQuejas.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoQueja;
            this.btnVerQuejas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerQuejas.Location = new System.Drawing.Point(0, 421);
            this.btnVerQuejas.Name = "btnVerQuejas";
            this.btnVerQuejas.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnVerQuejas.Size = new System.Drawing.Size(209, 45);
            this.btnVerQuejas.TabIndex = 8;
            this.btnVerQuejas.Text = "Ver Quejas";
            this.btnVerQuejas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVerQuejas.UseVisualStyleBackColor = false;
            this.btnVerQuejas.Click += new System.EventHandler(this.btnVerQueja_Click);
            // 
            // btnRegEmpleado
            // 
            this.btnRegEmpleado.BackColor = System.Drawing.Color.Transparent;
            this.btnRegEmpleado.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegEmpleado.FlatAppearance.BorderSize = 0;
            this.btnRegEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegEmpleado.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegEmpleado.ForeColor = System.Drawing.Color.White;
            this.btnRegEmpleado.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoRegistrarUsuario;
            this.btnRegEmpleado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegEmpleado.Location = new System.Drawing.Point(0, 376);
            this.btnRegEmpleado.Name = "btnRegEmpleado";
            this.btnRegEmpleado.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnRegEmpleado.Size = new System.Drawing.Size(209, 45);
            this.btnRegEmpleado.TabIndex = 7;
            this.btnRegEmpleado.Text = "Registrar Empleado";
            this.btnRegEmpleado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegEmpleado.UseVisualStyleBackColor = false;
            this.btnRegEmpleado.Click += new System.EventHandler(this.btnRegEmpleado_Click);
            // 
            // btnVerReportes
            // 
            this.btnVerReportes.BackColor = System.Drawing.Color.Transparent;
            this.btnVerReportes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVerReportes.FlatAppearance.BorderSize = 0;
            this.btnVerReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerReportes.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerReportes.ForeColor = System.Drawing.Color.White;
            this.btnVerReportes.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoReporte;
            this.btnVerReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerReportes.Location = new System.Drawing.Point(0, 331);
            this.btnVerReportes.Name = "btnVerReportes";
            this.btnVerReportes.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnVerReportes.Size = new System.Drawing.Size(209, 45);
            this.btnVerReportes.TabIndex = 6;
            this.btnVerReportes.Text = "Ver Reportes";
            this.btnVerReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVerReportes.UseVisualStyleBackColor = false;
            this.btnVerReportes.Click += new System.EventHandler(this.btnVerReportes_Click);
            // 
            // btnRegQueja
            // 
            this.btnRegQueja.BackColor = System.Drawing.Color.Transparent;
            this.btnRegQueja.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegQueja.FlatAppearance.BorderSize = 0;
            this.btnRegQueja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegQueja.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegQueja.ForeColor = System.Drawing.Color.White;
            this.btnRegQueja.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoQueja;
            this.btnRegQueja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegQueja.Location = new System.Drawing.Point(0, 286);
            this.btnRegQueja.Name = "btnRegQueja";
            this.btnRegQueja.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnRegQueja.Size = new System.Drawing.Size(209, 45);
            this.btnRegQueja.TabIndex = 5;
            this.btnRegQueja.Text = "Registrar Queja";
            this.btnRegQueja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegQueja.UseVisualStyleBackColor = false;
            this.btnRegQueja.Click += new System.EventHandler(this.btnRegQueja_Click);
            // 
            // btnInventario
            // 
            this.btnInventario.BackColor = System.Drawing.Color.Transparent;
            this.btnInventario.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInventario.FlatAppearance.BorderSize = 0;
            this.btnInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventario.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventario.ForeColor = System.Drawing.Color.White;
            this.btnInventario.Image = ((System.Drawing.Image)(resources.GetObject("btnInventario.Image")));
            this.btnInventario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInventario.Location = new System.Drawing.Point(0, 241);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnInventario.Size = new System.Drawing.Size(209, 45);
            this.btnInventario.TabIndex = 4;
            this.btnInventario.Text = "Gestionar Inventario";
            this.btnInventario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInventario.UseVisualStyleBackColor = false;
            this.btnInventario.Click += new System.EventHandler(this.btnInventario_Click);
            // 
            // btnRegReserva
            // 
            this.btnRegReserva.BackColor = System.Drawing.Color.Transparent;
            this.btnRegReserva.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegReserva.FlatAppearance.BorderSize = 0;
            this.btnRegReserva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegReserva.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegReserva.ForeColor = System.Drawing.Color.White;
            this.btnRegReserva.Image = ((System.Drawing.Image)(resources.GetObject("btnRegReserva.Image")));
            this.btnRegReserva.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegReserva.Location = new System.Drawing.Point(0, 196);
            this.btnRegReserva.Name = "btnRegReserva";
            this.btnRegReserva.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnRegReserva.Size = new System.Drawing.Size(209, 45);
            this.btnRegReserva.TabIndex = 3;
            this.btnRegReserva.Text = "Registrar Reserva";
            this.btnRegReserva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegReserva.UseVisualStyleBackColor = false;
            this.btnRegReserva.Click += new System.EventHandler(this.btnRegReserva_Click);
            // 
            // btnCargarPedido
            // 
            this.btnCargarPedido.BackColor = System.Drawing.Color.Transparent;
            this.btnCargarPedido.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCargarPedido.FlatAppearance.BorderSize = 0;
            this.btnCargarPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarPedido.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarPedido.ForeColor = System.Drawing.Color.White;
            this.btnCargarPedido.Image = ((System.Drawing.Image)(resources.GetObject("btnCargarPedido.Image")));
            this.btnCargarPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCargarPedido.Location = new System.Drawing.Point(0, 151);
            this.btnCargarPedido.Name = "btnCargarPedido";
            this.btnCargarPedido.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCargarPedido.Size = new System.Drawing.Size(209, 45);
            this.btnCargarPedido.TabIndex = 2;
            this.btnCargarPedido.Text = "Cargar Pedido";
            this.btnCargarPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCargarPedido.UseVisualStyleBackColor = false;
            this.btnCargarPedido.Click += new System.EventHandler(this.btnPedido_Click);
            // 
            // btnAbrirCaja
            // 
            this.btnAbrirCaja.BackColor = System.Drawing.Color.Transparent;
            this.btnAbrirCaja.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAbrirCaja.FlatAppearance.BorderSize = 0;
            this.btnAbrirCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirCaja.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirCaja.ForeColor = System.Drawing.Color.White;
            this.btnAbrirCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirCaja.Image")));
            this.btnAbrirCaja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirCaja.Location = new System.Drawing.Point(0, 106);
            this.btnAbrirCaja.Name = "btnAbrirCaja";
            this.btnAbrirCaja.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnAbrirCaja.Size = new System.Drawing.Size(209, 45);
            this.btnAbrirCaja.TabIndex = 1;
            this.btnAbrirCaja.Text = "Abrir Caja";
            this.btnAbrirCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbrirCaja.UseVisualStyleBackColor = false;
            this.btnAbrirCaja.Click += new System.EventHandler(this.btnAbrirCaja_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.platopastas;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(209, 106);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FMenuCajero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(1062, 505);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlMenuLateral);
            this.Name = "FMenuCajero";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.FMenu_Load);
            this.pnlMenuLateral.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenuLateral;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnAbrirCaja;
        private System.Windows.Forms.Button btnRegReserva;
        private System.Windows.Forms.Button btnCargarPedido;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.Label lbHora;
        private System.Windows.Forms.Label lbFecha;
        private System.Windows.Forms.Timer timerHora;
        private System.Windows.Forms.Button btnRegEmpleado;
        private System.Windows.Forms.Button btnVerReportes;
        private System.Windows.Forms.Button btnRegQueja;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnVerQuejas;
        private System.Windows.Forms.Panel pnlContent;
    }
}