namespace PastaFlow_DIAZ_PEREZ.Forms
{
    partial class FCerrarCaja
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
            this.pnlPrincipal = new System.Windows.Forms.Panel();
            this.lbTotalVentas = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCerrarCaja = new System.Windows.Forms.Button();
            this.txtMontoFinal = new System.Windows.Forms.TextBox();
            this.lbMontoFinal = new System.Windows.Forms.Label();
            this.lbMontoInicial = new System.Windows.Forms.Label();
            this.lbFechaHora = new System.Windows.Forms.Label();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.pnlPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.BackColor = System.Drawing.Color.Khaki;
            this.pnlPrincipal.Controls.Add(this.lbTotalVentas);
            this.pnlPrincipal.Controls.Add(this.btnCancelar);
            this.pnlPrincipal.Controls.Add(this.btnCerrarCaja);
            this.pnlPrincipal.Controls.Add(this.txtMontoFinal);
            this.pnlPrincipal.Controls.Add(this.lbMontoFinal);
            this.pnlPrincipal.Controls.Add(this.lbMontoInicial);
            this.pnlPrincipal.Controls.Add(this.lbFechaHora);
            this.pnlPrincipal.Controls.Add(this.lbTitulo);
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(800, 450);
            this.pnlPrincipal.TabIndex = 0;
            this.pnlPrincipal.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPrincipal_Paint);
            // 
            // lbTotalVentas
            // 
            this.lbTotalVentas.AutoSize = true;
            this.lbTotalVentas.Font = new System.Drawing.Font("Yu Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalVentas.Location = new System.Drawing.Point(30, 160);
            this.lbTotalVentas.Name = "lbTotalVentas";
            this.lbTotalVentas.Size = new System.Drawing.Size(143, 21);
            this.lbTotalVentas.TabIndex = 7;
            this.lbTotalVentas.Text = "Total de Ventas: $";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.LightGray;
            this.btnCancelar.Font = new System.Drawing.Font("Yu Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(300, 270);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(150, 40);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrarCaja
            // 
            this.btnCerrarCaja.BackColor = System.Drawing.Color.LightGreen;
            this.btnCerrarCaja.Font = new System.Drawing.Font("Yu Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarCaja.Location = new System.Drawing.Point(120, 270);
            this.btnCerrarCaja.Name = "btnCerrarCaja";
            this.btnCerrarCaja.Size = new System.Drawing.Size(150, 40);
            this.btnCerrarCaja.TabIndex = 5;
            this.btnCerrarCaja.Text = "Cerrar Caja";
            this.btnCerrarCaja.UseVisualStyleBackColor = false;
            this.btnCerrarCaja.Click += new System.EventHandler(this.btnCerrarCaja_Click);
            // 
            // txtMontoFinal
            // 
            this.txtMontoFinal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoFinal.Location = new System.Drawing.Point(160, 200);
            this.txtMontoFinal.Name = "txtMontoFinal";
            this.txtMontoFinal.Size = new System.Drawing.Size(150, 29);
            this.txtMontoFinal.TabIndex = 4;
            // 
            // lbMontoFinal
            // 
            this.lbMontoFinal.AutoSize = true;
            this.lbMontoFinal.Font = new System.Drawing.Font("Yu Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMontoFinal.Location = new System.Drawing.Point(30, 200);
            this.lbMontoFinal.Name = "lbMontoFinal";
            this.lbMontoFinal.Size = new System.Drawing.Size(104, 21);
            this.lbMontoFinal.TabIndex = 3;
            this.lbMontoFinal.Text = "Monto Final:";
            // 
            // lbMontoInicial
            // 
            this.lbMontoInicial.AutoSize = true;
            this.lbMontoInicial.Font = new System.Drawing.Font("Yu Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMontoInicial.Location = new System.Drawing.Point(30, 120);
            this.lbMontoInicial.Name = "lbMontoInicial";
            this.lbMontoInicial.Size = new System.Drawing.Size(125, 21);
            this.lbMontoInicial.TabIndex = 2;
            this.lbMontoInicial.Text = "Monto Inicial: $";
            // 
            // lbFechaHora
            // 
            this.lbFechaHora.AutoSize = true;
            this.lbFechaHora.Location = new System.Drawing.Point(30, 80);
            this.lbFechaHora.Name = "lbFechaHora";
            this.lbFechaHora.Size = new System.Drawing.Size(68, 13);
            this.lbFechaHora.TabIndex = 1;
            this.lbFechaHora.Text = "Fecha/Hora:";
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(180, 20);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(118, 30);
            this.lbTitulo.TabIndex = 0;
            this.lbTitulo.Text = "Cerrar Caja";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FCerrarCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlPrincipal);
            this.Name = "FCerrarCaja";
            this.Text = "Cerrar Caja";
            this.Load += new System.EventHandler(this.FCerrarCaja_Load);
            this.pnlPrincipal.ResumeLayout(false);
            this.pnlPrincipal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPrincipal;
        private System.Windows.Forms.Label lbFechaHora;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lbMontoFinal;
        private System.Windows.Forms.Label lbMontoInicial;
        private System.Windows.Forms.TextBox txtMontoFinal;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCerrarCaja;
        private System.Windows.Forms.Label lbTotalVentas;
    }
}