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
            this.label4 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dgsQuejas = new System.Windows.Forms.DataGridView();
            this.btnGenerarExcel = new System.Windows.Forms.Button();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgsQuejas)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(529, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 18);
            this.label4.TabIndex = 36;
            this.label4.Text = "Hasta :";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(577, 108);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(118, 20);
            this.dtpHasta.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(356, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 18);
            this.label3.TabIndex = 34;
            this.label3.Text = "Desde :";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(408, 108);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(118, 20);
            this.dtpDesde.TabIndex = 33;
            // 
            // dgsQuejas
            // 
            this.dgsQuejas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgsQuejas.Location = new System.Drawing.Point(53, 138);
            this.dgsQuejas.Name = "dgsQuejas";
            this.dgsQuejas.Size = new System.Drawing.Size(703, 150);
            this.dgsQuejas.TabIndex = 30;
            // 
            // btnGenerarExcel
            // 
            this.btnGenerarExcel.BackColor = System.Drawing.Color.Green;
            this.btnGenerarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarExcel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarExcel.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoExcel;
            this.btnGenerarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarExcel.Location = new System.Drawing.Point(53, 106);
            this.btnGenerarExcel.Name = "btnGenerarExcel";
            this.btnGenerarExcel.Size = new System.Drawing.Size(111, 23);
            this.btnGenerarExcel.TabIndex = 37;
            this.btnGenerarExcel.Text = "Generar excel";
            this.btnGenerarExcel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarExcel.UseVisualStyleBackColor = false;
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(314, 49);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(170, 20);
            this.lbTitulo.TabIndex = 38;
            this.lbTitulo.Text = "Reportes de Ventas";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.btnLimpiar.Location = new System.Drawing.Point(731, 106);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(25, 25);
            this.btnLimpiar.TabIndex = 32;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoBuscar;
            this.btnBuscar.Location = new System.Drawing.Point(701, 106);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(25, 25);
            this.btnBuscar.TabIndex = 31;
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // FVerReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lbTitulo);
            this.Controls.Add(this.btnGenerarExcel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgsQuejas);
            this.Name = "FVerReportes";
            this.Text = "Reportes";
            ((System.ComponentModel.ISupportInitialize)(this.dgsQuejas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgsQuejas;
        private System.Windows.Forms.Button btnGenerarExcel;
        private System.Windows.Forms.Label lbTitulo;
    }
}