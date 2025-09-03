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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMenuCajero));
            this.pnlMenuCajero = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnRegReserva = new System.Windows.Forms.Button();
            this.btnCargarPedido = new System.Windows.Forms.Button();
            this.btnAbrirCaja = new System.Windows.Forms.Button();
            this.pnlMenuCajero.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenuCajero
            // 
            this.pnlMenuCajero.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMenuCajero.Controls.Add(this.btnRegReserva);
            this.pnlMenuCajero.Controls.Add(this.btnCargarPedido);
            this.pnlMenuCajero.Controls.Add(this.btnAbrirCaja);
            this.pnlMenuCajero.Controls.Add(this.pnlHeader);
            this.pnlMenuCajero.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenuCajero.Location = new System.Drawing.Point(0, 0);
            this.pnlMenuCajero.Name = "pnlMenuCajero";
            this.pnlMenuCajero.Size = new System.Drawing.Size(209, 505);
            this.pnlMenuCajero.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(209, 150);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnRegReserva
            // 
            this.btnRegReserva.BackColor = System.Drawing.Color.Transparent;
            this.btnRegReserva.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegReserva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegReserva.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegReserva.Image = ((System.Drawing.Image)(resources.GetObject("btnRegReserva.Image")));
            this.btnRegReserva.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegReserva.Location = new System.Drawing.Point(0, 254);
            this.btnRegReserva.Name = "btnRegReserva";
            this.btnRegReserva.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnRegReserva.Size = new System.Drawing.Size(209, 52);
            this.btnRegReserva.TabIndex = 3;
            this.btnRegReserva.Text = "Registrar Reserva";
            this.btnRegReserva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegReserva.UseVisualStyleBackColor = false;
            // 
            // btnCargarPedido
            // 
            this.btnCargarPedido.BackColor = System.Drawing.Color.Transparent;
            this.btnCargarPedido.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCargarPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarPedido.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarPedido.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoPedido;
            this.btnCargarPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCargarPedido.Location = new System.Drawing.Point(0, 202);
            this.btnCargarPedido.Name = "btnCargarPedido";
            this.btnCargarPedido.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCargarPedido.Size = new System.Drawing.Size(209, 52);
            this.btnCargarPedido.TabIndex = 2;
            this.btnCargarPedido.Text = "Cargar Pedido";
            this.btnCargarPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCargarPedido.UseVisualStyleBackColor = false;
            // 
            // btnAbrirCaja
            // 
            this.btnAbrirCaja.BackColor = System.Drawing.Color.Transparent;
            this.btnAbrirCaja.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAbrirCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirCaja.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirCaja.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoCajaReg;
            this.btnAbrirCaja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirCaja.Location = new System.Drawing.Point(0, 150);
            this.btnAbrirCaja.Name = "btnAbrirCaja";
            this.btnAbrirCaja.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnAbrirCaja.Size = new System.Drawing.Size(209, 52);
            this.btnAbrirCaja.TabIndex = 1;
            this.btnAbrirCaja.Text = "Abrir Caja";
            this.btnAbrirCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbrirCaja.UseVisualStyleBackColor = false;
            // 
            // FMenuCajero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(963, 505);
            this.Controls.Add(this.pnlMenuCajero);
            this.Name = "FMenuCajero";
            this.Text = "FMenuCajero";
            this.pnlMenuCajero.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenuCajero;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnAbrirCaja;
        private System.Windows.Forms.Button btnRegReserva;
        private System.Windows.Forms.Button btnCargarPedido;
    }
}