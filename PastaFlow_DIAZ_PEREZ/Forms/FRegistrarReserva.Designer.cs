namespace PastaFlow_DIAZ_PEREZ.Forms
{
    partial class FRegistrarReserva
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
            this.pnlRegistro = new System.Windows.Forms.Panel();
            this.cantPersonas = new System.Windows.Forms.NumericUpDown();
            this.cBoxEstado = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.dtpFechaHora = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lbEmpCorreo = new System.Windows.Forms.Label();
            this.lbEmpDNI = new System.Windows.Forms.Label();
            this.txtApellidoCliente = new System.Windows.Forms.TextBox();
            this.lbEmpApellido = new System.Windows.Forms.Label();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.lbEmpNombre = new System.Windows.Forms.Label();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.lbEmpTitulo = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvReservas = new System.Windows.Forms.DataGridView();
            this.pnlRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cantPersonas)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRegistro
            // 
            this.pnlRegistro.BackColor = System.Drawing.Color.DarkRed;
            this.pnlRegistro.Controls.Add(this.cantPersonas);
            this.pnlRegistro.Controls.Add(this.cBoxEstado);
            this.pnlRegistro.Controls.Add(this.label2);
            this.pnlRegistro.Controls.Add(this.btnVolver);
            this.pnlRegistro.Controls.Add(this.dtpFechaHora);
            this.pnlRegistro.Controls.Add(this.label1);
            this.pnlRegistro.Controls.Add(this.btnLimpiar);
            this.pnlRegistro.Controls.Add(this.btnRegistrar);
            this.pnlRegistro.Controls.Add(this.lbEmpCorreo);
            this.pnlRegistro.Controls.Add(this.lbEmpDNI);
            this.pnlRegistro.Controls.Add(this.txtApellidoCliente);
            this.pnlRegistro.Controls.Add(this.lbEmpApellido);
            this.pnlRegistro.Controls.Add(this.txtNombreCliente);
            this.pnlRegistro.Controls.Add(this.lbEmpNombre);
            this.pnlRegistro.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRegistro.Location = new System.Drawing.Point(0, 0);
            this.pnlRegistro.Name = "pnlRegistro";
            this.pnlRegistro.Size = new System.Drawing.Size(271, 682);
            this.pnlRegistro.TabIndex = 1;
            // 
            // cantPersonas
            // 
            this.cantPersonas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantPersonas.Location = new System.Drawing.Point(35, 376);
            this.cantPersonas.Name = "cantPersonas";
            this.cantPersonas.Size = new System.Drawing.Size(198, 26);
            this.cantPersonas.TabIndex = 32;
            // 
            // cBoxEstado
            // 
            this.cBoxEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxEstado.FormattingEnabled = true;
            this.cBoxEstado.Location = new System.Drawing.Point(33, 443);
            this.cBoxEstado.Name = "cBoxEstado";
            this.cBoxEstado.Size = new System.Drawing.Size(198, 28);
            this.cBoxEstado.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(32, 422);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 18);
            this.label2.TabIndex = 30;
            this.label2.Text = "Estado Reserva";
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.DarkRed;
            this.btnVolver.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoAtrás;
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(12, 51);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(42, 30);
            this.btnVolver.TabIndex = 29;
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // dtpFechaHora
            // 
            this.dtpFechaHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaHora.Location = new System.Drawing.Point(36, 310);
            this.dtpFechaHora.Name = "dtpFechaHora";
            this.dtpFechaHora.Size = new System.Drawing.Size(195, 24);
            this.dtpFechaHora.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(29, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 33);
            this.label1.TabIndex = 18;
            this.label1.Text = "Registrar Reserva";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.Location = new System.Drawing.Point(139, 495);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(104, 35);
            this.btnLimpiar.TabIndex = 17;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.Khaki;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoEditar;
            this.btnRegistrar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrar.Location = new System.Drawing.Point(29, 495);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(104, 35);
            this.btnRegistrar.TabIndex = 16;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrarReserva_Click);
            // 
            // lbEmpCorreo
            // 
            this.lbEmpCorreo.AutoSize = true;
            this.lbEmpCorreo.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpCorreo.ForeColor = System.Drawing.Color.White;
            this.lbEmpCorreo.Location = new System.Drawing.Point(35, 355);
            this.lbEmpCorreo.Name = "lbEmpCorreo";
            this.lbEmpCorreo.Size = new System.Drawing.Size(141, 18);
            this.lbEmpCorreo.TabIndex = 7;
            this.lbEmpCorreo.Text = "Cantidad de Personas";
            // 
            // lbEmpDNI
            // 
            this.lbEmpDNI.AutoSize = true;
            this.lbEmpDNI.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpDNI.ForeColor = System.Drawing.Color.White;
            this.lbEmpDNI.Location = new System.Drawing.Point(33, 289);
            this.lbEmpDNI.Name = "lbEmpDNI";
            this.lbEmpDNI.Size = new System.Drawing.Size(86, 18);
            this.lbEmpDNI.TabIndex = 5;
            this.lbEmpDNI.Text = "Fecha y Hora";
            // 
            // txtApellidoCliente
            // 
            this.txtApellidoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidoCliente.Location = new System.Drawing.Point(35, 243);
            this.txtApellidoCliente.Name = "txtApellidoCliente";
            this.txtApellidoCliente.Size = new System.Drawing.Size(198, 26);
            this.txtApellidoCliente.TabIndex = 4;
            this.txtApellidoCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApellidoCliente_KeyPress);
            // 
            // lbEmpApellido
            // 
            this.lbEmpApellido.AutoSize = true;
            this.lbEmpApellido.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpApellido.ForeColor = System.Drawing.Color.White;
            this.lbEmpApellido.Location = new System.Drawing.Point(34, 219);
            this.lbEmpApellido.Name = "lbEmpApellido";
            this.lbEmpApellido.Size = new System.Drawing.Size(142, 19);
            this.lbEmpApellido.TabIndex = 3;
            this.lbEmpApellido.Text = "Apellido del Cliente";
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreCliente.Location = new System.Drawing.Point(35, 172);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(198, 26);
            this.txtNombreCliente.TabIndex = 1;
            this.txtNombreCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NombreCliente_KeyPress);
            // 
            // lbEmpNombre
            // 
            this.lbEmpNombre.AutoSize = true;
            this.lbEmpNombre.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpNombre.ForeColor = System.Drawing.Color.White;
            this.lbEmpNombre.Location = new System.Drawing.Point(34, 150);
            this.lbEmpNombre.Name = "lbEmpNombre";
            this.lbEmpNombre.Size = new System.Drawing.Size(141, 19);
            this.lbEmpNombre.TabIndex = 0;
            this.lbEmpNombre.Text = "Nombre del Cliente";
            // 
            // dtpInicio
            // 
            this.dtpInicio.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Location = new System.Drawing.Point(353, 171);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(118, 20);
            this.dtpInicio.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(293, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 30;
            this.label3.Text = "Desde:";
            // 
            // dtpFin
            // 
            this.dtpFin.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(543, 171);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(118, 20);
            this.dtpFin.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(486, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 19);
            this.label4.TabIndex = 32;
            this.label4.Text = "Hasta:";
            // 
            // lbEmpTitulo
            // 
            this.lbEmpTitulo.AutoSize = true;
            this.lbEmpTitulo.BackColor = System.Drawing.Color.DarkRed;
            this.lbEmpTitulo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbEmpTitulo.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpTitulo.ForeColor = System.Drawing.Color.Snow;
            this.lbEmpTitulo.Location = new System.Drawing.Point(402, 76);
            this.lbEmpTitulo.Name = "lbEmpTitulo";
            this.lbEmpTitulo.Size = new System.Drawing.Size(288, 33);
            this.lbEmpTitulo.TabIndex = 33;
            this.lbEmpTitulo.Text = "RESERVAS REGISTRADAS";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoBuscar;
            this.btnBuscar.Location = new System.Drawing.Point(679, 166);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(49, 32);
            this.btnBuscar.TabIndex = 34;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RosyBrown;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.button1.Location = new System.Drawing.Point(734, 166);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 32);
            this.button1.TabIndex = 35;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnLimpiarReserva_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.lbEmpTitulo);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dtpFin);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dtpInicio);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(271, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(2);
            this.panel2.Size = new System.Drawing.Size(1076, 225);
            this.panel2.TabIndex = 30;
            // 
            // dgvReservas
            // 
            this.dgvReservas.AllowUserToAddRows = false;
            this.dgvReservas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReservas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvReservas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReservas.Location = new System.Drawing.Point(317, 231);
            this.dgvReservas.Name = "dgvReservas";
            this.dgvReservas.ReadOnly = true;
            this.dgvReservas.Size = new System.Drawing.Size(939, 439);
            this.dgvReservas.TabIndex = 31;
            // 
            // FRegistrarReserva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(1347, 682);
            this.Controls.Add(this.dgvReservas);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlRegistro);
            this.Name = "FRegistrarReserva";
            this.Text = "Reservas";
            this.Load += new System.EventHandler(this.FReservas_Load);
            this.pnlRegistro.ResumeLayout(false);
            this.pnlRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cantPersonas)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRegistro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lbEmpCorreo;
        private System.Windows.Forms.Label lbEmpDNI;
        private System.Windows.Forms.TextBox txtApellidoCliente;
        private System.Windows.Forms.Label lbEmpApellido;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label lbEmpNombre;
        private System.Windows.Forms.DateTimePicker dtpFechaHora;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.ComboBox cBoxEstado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown cantPersonas;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbEmpTitulo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvReservas;
    }
}