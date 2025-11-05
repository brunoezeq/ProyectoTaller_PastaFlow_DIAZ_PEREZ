namespace PastaFlow_DIAZ_PEREZ.Forms
{
    partial class FVerReportes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.dgvVentas = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanelBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAtras = new System.Windows.Forms.Button();
            this.btnAdelante = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGenerarExcel = new System.Windows.Forms.Button();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.id_venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero_factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metodo_pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_unitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accion = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).BeginInit();
            this.flowLayoutPanelBotones.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbTitulo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1364, 182);
            this.panel2.TabIndex = 40;
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.BackColor = System.Drawing.Color.DarkRed;
            this.lbTitulo.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.ForeColor = System.Drawing.Color.Snow;
            this.lbTitulo.Location = new System.Drawing.Point(688, 119);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(257, 33);
            this.lbTitulo.TabIndex = 39;
            this.lbTitulo.Text = "REPORTES DE VENTAS";
            // 
            // dgvVentas
            // 
            this.dgvVentas.AllowUserToResizeRows = false;
            this.dgvVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVentas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVentas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_venta,
            this.numero_factura,
            this.fecha_venta,
            this.metodo_pago,
            this.total_venta,
            this.producto,
            this.cantidad,
            this.precio_unitario,
            this.subtotal,
            this.accion});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVentas.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvVentas.Location = new System.Drawing.Point(289, 287);
            this.dgvVentas.Name = "dgvVentas";
            this.dgvVentas.RowHeadersVisible = false;
            this.dgvVentas.Size = new System.Drawing.Size(1063, 346);
            this.dgvVentas.TabIndex = 32;
            // 
            // flowLayoutPanelBotones
            // 
            this.flowLayoutPanelBotones.Controls.Add(this.btnAtras);
            this.flowLayoutPanelBotones.Controls.Add(this.btnAdelante);
            this.flowLayoutPanelBotones.Location = new System.Drawing.Point(755, 645);
            this.flowLayoutPanelBotones.Name = "flowLayoutPanelBotones";
            this.flowLayoutPanelBotones.Size = new System.Drawing.Size(170, 44);
            this.flowLayoutPanelBotones.TabIndex = 40;
            // 
            // btnAtras
            // 
            this.btnAtras.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAtras.Location = new System.Drawing.Point(3, 3);
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Size = new System.Drawing.Size(75, 41);
            this.btnAtras.TabIndex = 0;
            this.btnAtras.Text = "Atras";
            this.btnAtras.UseVisualStyleBackColor = true;
            this.btnAtras.Click += new System.EventHandler(this.btnAtras_Click);
            // 
            // btnAdelante
            // 
            this.btnAdelante.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdelante.Location = new System.Drawing.Point(84, 3);
            this.btnAdelante.Name = "btnAdelante";
            this.btnAdelante.Size = new System.Drawing.Size(75, 41);
            this.btnAdelante.TabIndex = 1;
            this.btnAdelante.Text = "Adelante";
            this.btnAdelante.UseVisualStyleBackColor = true;
            this.btnAdelante.Click += new System.EventHandler(this.btnAdelante_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoBuscar;
            this.btnBuscar.Location = new System.Drawing.Point(1056, 217);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(52, 33);
            this.btnBuscar.TabIndex = 41;
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.btnLimpiar.Location = new System.Drawing.Point(1114, 217);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(52, 33);
            this.btnLimpiar.TabIndex = 42;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(681, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 44;
            this.label3.Text = "Desde:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(874, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 19);
            this.label4.TabIndex = 46;
            this.label4.Text = "Hasta:";
            // 
            // btnGenerarExcel
            // 
            this.btnGenerarExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnGenerarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarExcel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarExcel.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoExcel;
            this.btnGenerarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarExcel.Location = new System.Drawing.Point(548, 211);
            this.btnGenerarExcel.Name = "btnGenerarExcel";
            this.btnGenerarExcel.Size = new System.Drawing.Size(111, 39);
            this.btnGenerarExcel.TabIndex = 47;
            this.btnGenerarExcel.Text = "Generar excel";
            this.btnGenerarExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarExcel.UseVisualStyleBackColor = false;
            // 
            // dtpDesde
            // 
            this.dtpDesde.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(741, 218);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(118, 20);
            this.dtpDesde.TabIndex = 43;
            // 
            // dtpHasta
            // 
            this.dtpHasta.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(922, 218);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(118, 20);
            this.dtpHasta.TabIndex = 45;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpHasta);
            this.panel1.Controls.Add(this.dtpDesde);
            this.panel1.Controls.Add(this.btnGenerarExcel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.flowLayoutPanelBotones);
            this.panel1.Controls.Add(this.dgvVentas);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1364, 692);
            this.panel1.TabIndex = 39;
            // 
            // id_venta
            // 
            this.id_venta.HeaderText = "ID_VENTA";
            this.id_venta.Name = "id_venta";
            // 
            // numero_factura
            // 
            this.numero_factura.FillWeight = 90F;
            this.numero_factura.HeaderText = "NRO. FACTURA";
            this.numero_factura.Name = "numero_factura";
            // 
            // fecha_venta
            // 
            this.fecha_venta.FillWeight = 110F;
            this.fecha_venta.HeaderText = "FECHA DE VENTA";
            this.fecha_venta.Name = "fecha_venta";
            // 
            // metodo_pago
            // 
            this.metodo_pago.FillWeight = 120F;
            this.metodo_pago.HeaderText = "METODO DE PAGO";
            this.metodo_pago.Name = "metodo_pago";
            // 
            // total_venta
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.total_venta.DefaultCellStyle = dataGridViewCellStyle7;
            this.total_venta.FillWeight = 110F;
            this.total_venta.HeaderText = "TOTAL VENTAS";
            this.total_venta.Name = "total_venta";
            // 
            // producto
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.producto.DefaultCellStyle = dataGridViewCellStyle8;
            this.producto.FillWeight = 180F;
            this.producto.HeaderText = "PRODUCTO";
            this.producto.Name = "producto";
            // 
            // cantidad
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle9;
            this.cantidad.FillWeight = 90F;
            this.cantidad.HeaderText = "CANTIDAD";
            this.cantidad.Name = "cantidad";
            // 
            // precio_unitario
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.precio_unitario.DefaultCellStyle = dataGridViewCellStyle10;
            this.precio_unitario.FillWeight = 110F;
            this.precio_unitario.HeaderText = "PRECIO UNITARIO";
            this.precio_unitario.Name = "precio_unitario";
            // 
            // subtotal
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.subtotal.DefaultCellStyle = dataGridViewCellStyle11;
            this.subtotal.FillWeight = 110F;
            this.subtotal.HeaderText = "SUBTOTAL";
            this.subtotal.Name = "subtotal";
            // 
            // accion
            // 
            this.accion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.accion.HeaderText = "ACCION";
            this.accion.Name = "accion";
            this.accion.Width = 110;
            // 
            // FVerReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(1364, 692);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FVerReportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.FVerReportes_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).EndInit();
            this.flowLayoutPanelBotones.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.DataGridView dgvVentas;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBotones;
        private System.Windows.Forms.Button btnAtras;
        private System.Windows.Forms.Button btnAdelante;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGenerarExcel;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_venta;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero_factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_venta;
        private System.Windows.Forms.DataGridViewTextBoxColumn metodo_pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_venta;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_unitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewButtonColumn accion;
    }
}