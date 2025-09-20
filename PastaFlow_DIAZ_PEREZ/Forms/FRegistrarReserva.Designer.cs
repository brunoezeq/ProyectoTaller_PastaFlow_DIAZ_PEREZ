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
            this.btnVolver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.txtCantPersonas = new System.Windows.Forms.TextBox();
            this.lbEmpCorreo = new System.Windows.Forms.Label();
            this.lbEmpDNI = new System.Windows.Forms.Label();
            this.txtApCliente = new System.Windows.Forms.TextBox();
            this.lbEmpApellido = new System.Windows.Forms.Label();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.lbEmpNombre = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nombreCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellidoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidadPersonas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lbEmpTitulo = new System.Windows.Forms.Label();
            this.dtpFechaHora = new System.Windows.Forms.DateTimePicker();
            this.pnlRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRegistro
            // 
            this.pnlRegistro.BackColor = System.Drawing.Color.DarkRed;
            this.pnlRegistro.Controls.Add(this.dtpFechaHora);
            this.pnlRegistro.Controls.Add(this.btnVolver);
            this.pnlRegistro.Controls.Add(this.label1);
            this.pnlRegistro.Controls.Add(this.btnLimpiar);
            this.pnlRegistro.Controls.Add(this.btnRegistrar);
            this.pnlRegistro.Controls.Add(this.txtCantPersonas);
            this.pnlRegistro.Controls.Add(this.lbEmpCorreo);
            this.pnlRegistro.Controls.Add(this.lbEmpDNI);
            this.pnlRegistro.Controls.Add(this.txtApCliente);
            this.pnlRegistro.Controls.Add(this.lbEmpApellido);
            this.pnlRegistro.Controls.Add(this.txtNombreCliente);
            this.pnlRegistro.Controls.Add(this.lbEmpNombre);
            this.pnlRegistro.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRegistro.Location = new System.Drawing.Point(0, 0);
            this.pnlRegistro.Name = "pnlRegistro";
            this.pnlRegistro.Size = new System.Drawing.Size(224, 490);
            this.pnlRegistro.TabIndex = 1;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.DarkRed;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.DarkRed;
            this.btnVolver.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoAtrás;
            this.btnVolver.Location = new System.Drawing.Point(3, 7);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(22, 23);
            this.btnVolver.TabIndex = 19;
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(29, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Registrar Reserva";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(106, 305);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 17;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrar.Location = new System.Drawing.Point(25, 305);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(75, 23);
            this.btnRegistrar.TabIndex = 16;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            // 
            // txtCantPersonas
            // 
            this.txtCantPersonas.Location = new System.Drawing.Point(12, 266);
            this.txtCantPersonas.Name = "txtCantPersonas";
            this.txtCantPersonas.Size = new System.Drawing.Size(186, 20);
            this.txtCantPersonas.TabIndex = 8;
            this.txtCantPersonas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CantPersonas_KeyPress);
            // 
            // lbEmpCorreo
            // 
            this.lbEmpCorreo.AutoSize = true;
            this.lbEmpCorreo.ForeColor = System.Drawing.Color.White;
            this.lbEmpCorreo.Location = new System.Drawing.Point(11, 247);
            this.lbEmpCorreo.Name = "lbEmpCorreo";
            this.lbEmpCorreo.Size = new System.Drawing.Size(111, 13);
            this.lbEmpCorreo.TabIndex = 7;
            this.lbEmpCorreo.Text = "Cantidad de Personas";
            // 
            // lbEmpDNI
            // 
            this.lbEmpDNI.AutoSize = true;
            this.lbEmpDNI.ForeColor = System.Drawing.Color.White;
            this.lbEmpDNI.Location = new System.Drawing.Point(11, 198);
            this.lbEmpDNI.Name = "lbEmpDNI";
            this.lbEmpDNI.Size = new System.Drawing.Size(71, 13);
            this.lbEmpDNI.TabIndex = 5;
            this.lbEmpDNI.Text = "Fecha y Hora";
            // 
            // txtApCliente
            // 
            this.txtApCliente.Location = new System.Drawing.Point(12, 170);
            this.txtApCliente.Name = "txtApCliente";
            this.txtApCliente.Size = new System.Drawing.Size(186, 20);
            this.txtApCliente.TabIndex = 4;
            this.txtApCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApellidoCliente_KeyPress);
            // 
            // lbEmpApellido
            // 
            this.lbEmpApellido.AutoSize = true;
            this.lbEmpApellido.ForeColor = System.Drawing.Color.White;
            this.lbEmpApellido.Location = new System.Drawing.Point(11, 150);
            this.lbEmpApellido.Name = "lbEmpApellido";
            this.lbEmpApellido.Size = new System.Drawing.Size(96, 13);
            this.lbEmpApellido.TabIndex = 3;
            this.lbEmpApellido.Text = "Apellido del Cliente";
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Location = new System.Drawing.Point(12, 122);
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(186, 20);
            this.txtNombreCliente.TabIndex = 1;
            this.txtNombreCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NombreCliente_KeyPress);
            // 
            // lbEmpNombre
            // 
            this.lbEmpNombre.AutoSize = true;
            this.lbEmpNombre.ForeColor = System.Drawing.Color.White;
            this.lbEmpNombre.Location = new System.Drawing.Point(11, 102);
            this.lbEmpNombre.Name = "lbEmpNombre";
            this.lbEmpNombre.Size = new System.Drawing.Size(96, 13);
            this.lbEmpNombre.TabIndex = 0;
            this.lbEmpNombre.Text = "Nombre del Cliente";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombreCliente,
            this.apellidoCliente,
            this.fechaHora,
            this.cantidadPersonas,
            this.estado});
            this.dataGridView1.Location = new System.Drawing.Point(244, 124);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(544, 150);
            this.dataGridView1.TabIndex = 2;
            // 
            // nombreCliente
            // 
            this.nombreCliente.HeaderText = "Nombre del Cliente";
            this.nombreCliente.Name = "nombreCliente";
            this.nombreCliente.ReadOnly = true;
            // 
            // apellidoCliente
            // 
            this.apellidoCliente.HeaderText = "Apellido del Cliente";
            this.apellidoCliente.Name = "apellidoCliente";
            this.apellidoCliente.ReadOnly = true;
            // 
            // fechaHora
            // 
            this.fechaHora.HeaderText = "Fecha y Hora";
            this.fechaHora.Name = "fechaHora";
            this.fechaHora.ReadOnly = true;
            // 
            // cantidadPersonas
            // 
            this.cantidadPersonas.HeaderText = "Cantidad de Personas";
            this.cantidadPersonas.Name = "cantidadPersonas";
            this.cantidadPersonas.ReadOnly = true;
            // 
            // estado
            // 
            this.estado.HeaderText = "Estado";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(501, 91);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(118, 20);
            this.dtpDesde.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(453, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Desde :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(626, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Hasta :";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(670, 91);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(118, 20);
            this.dtpHasta.TabIndex = 24;
            // 
            // lbEmpTitulo
            // 
            this.lbEmpTitulo.AutoSize = true;
            this.lbEmpTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpTitulo.ForeColor = System.Drawing.Color.Black;
            this.lbEmpTitulo.Location = new System.Drawing.Point(406, 41);
            this.lbEmpTitulo.Name = "lbEmpTitulo";
            this.lbEmpTitulo.Size = new System.Drawing.Size(216, 25);
            this.lbEmpTitulo.TabIndex = 26;
            this.lbEmpTitulo.Text = "Reservas registradas";
            // 
            // dtpFechaHora
            // 
            this.dtpFechaHora.Location = new System.Drawing.Point(12, 217);
            this.dtpFechaHora.Name = "dtpFechaHora";
            this.dtpFechaHora.Size = new System.Drawing.Size(186, 20);
            this.dtpFechaHora.TabIndex = 20;
            // 
            // FRegistrarReserva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 490);
            this.Controls.Add(this.lbEmpTitulo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pnlRegistro);
            this.Name = "FRegistrarReserva";
            this.Text = "Reservas";
            this.pnlRegistro.ResumeLayout(false);
            this.pnlRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlRegistro;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.TextBox txtCantPersonas;
        private System.Windows.Forms.Label lbEmpCorreo;
        private System.Windows.Forms.Label lbEmpDNI;
        private System.Windows.Forms.TextBox txtApCliente;
        private System.Windows.Forms.Label lbEmpApellido;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label lbEmpNombre;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellidoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadPersonas;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lbEmpTitulo;
        private System.Windows.Forms.DateTimePicker dtpFechaHora;
    }
}