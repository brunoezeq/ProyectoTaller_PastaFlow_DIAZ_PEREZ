namespace PastaFlow_DIAZ_PEREZ.Forms
{
    partial class FGraficos
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chartMetodos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartProductos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartEmpleados = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dtpHastaGraficos = new System.Windows.Forms.DateTimePicker();
            this.dtpDesdeGraficos = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnActualizarGraficos = new System.Windows.Forms.Button();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMetodos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Khaki;
            this.panel1.Controls.Add(this.lbTitulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1309, 100);
            this.panel1.TabIndex = 56;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Khaki;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.chartMetodos);
            this.panel2.Controls.Add(this.chartProductos);
            this.panel2.Controls.Add(this.chartEmpleados);
            this.panel2.Controls.Add(this.dtpHastaGraficos);
            this.panel2.Controls.Add(this.dtpDesdeGraficos);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnLimpiar);
            this.panel2.Controls.Add(this.btnActualizarGraficos);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1309, 399);
            this.panel2.TabIndex = 57;
            // 
            // chartMetodos
            // 
            chartArea4.Name = "ChartArea1";
            this.chartMetodos.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartMetodos.Legends.Add(legend4);
            this.chartMetodos.Location = new System.Drawing.Point(1007, 144);
            this.chartMetodos.Name = "chartMetodos";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartMetodos.Series.Add(series4);
            this.chartMetodos.Size = new System.Drawing.Size(304, 252);
            this.chartMetodos.TabIndex = 64;
            this.chartMetodos.Text = "chart3";
            // 
            // chartProductos
            // 
            chartArea5.Name = "ChartArea1";
            this.chartProductos.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartProductos.Legends.Add(legend5);
            this.chartProductos.Location = new System.Drawing.Point(681, 144);
            this.chartProductos.Name = "chartProductos";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chartProductos.Series.Add(series5);
            this.chartProductos.Size = new System.Drawing.Size(304, 252);
            this.chartProductos.TabIndex = 63;
            this.chartProductos.Text = "chart2";
            // 
            // chartEmpleados
            // 
            chartArea6.Name = "ChartArea1";
            this.chartEmpleados.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chartEmpleados.Legends.Add(legend6);
            this.chartEmpleados.Location = new System.Drawing.Point(348, 144);
            this.chartEmpleados.Name = "chartEmpleados";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chartEmpleados.Series.Add(series6);
            this.chartEmpleados.Size = new System.Drawing.Size(304, 252);
            this.chartEmpleados.TabIndex = 62;
            this.chartEmpleados.Text = "chart1";
            // 
            // dtpHastaGraficos
            // 
            this.dtpHastaGraficos.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHastaGraficos.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHastaGraficos.Location = new System.Drawing.Point(1091, 55);
            this.dtpHastaGraficos.Name = "dtpHastaGraficos";
            this.dtpHastaGraficos.Size = new System.Drawing.Size(118, 20);
            this.dtpHastaGraficos.TabIndex = 60;
            // 
            // dtpDesdeGraficos
            // 
            this.dtpDesdeGraficos.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesdeGraficos.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesdeGraficos.Location = new System.Drawing.Point(901, 55);
            this.dtpDesdeGraficos.Name = "dtpDesdeGraficos";
            this.dtpDesdeGraficos.Size = new System.Drawing.Size(118, 20);
            this.dtpDesdeGraficos.TabIndex = 58;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1043, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 19);
            this.label4.TabIndex = 61;
            this.label4.Text = "Hasta:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(850, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 59;
            this.label3.Text = "Desde:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.btnLimpiar.Location = new System.Drawing.Point(1263, 51);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(27, 24);
            this.btnLimpiar.TabIndex = 57;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // btnActualizarGraficos
            // 
            this.btnActualizarGraficos.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnActualizarGraficos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarGraficos.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoBuscar;
            this.btnActualizarGraficos.Location = new System.Drawing.Point(1230, 51);
            this.btnActualizarGraficos.Name = "btnActualizarGraficos";
            this.btnActualizarGraficos.Size = new System.Drawing.Size(27, 24);
            this.btnActualizarGraficos.TabIndex = 56;
            this.btnActualizarGraficos.UseVisualStyleBackColor = false;
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.BackColor = System.Drawing.Color.DarkRed;
            this.lbTitulo.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.ForeColor = System.Drawing.Color.Snow;
            this.lbTitulo.Location = new System.Drawing.Point(936, 64);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(127, 33);
            this.lbTitulo.TabIndex = 40;
            this.lbTitulo.Text = "GRÁFICOS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkRed;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Snow;
            this.label1.Location = new System.Drawing.Point(437, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 26);
            this.label1.TabIndex = 65;
            this.label1.Text = "Empleados";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkRed;
            this.label2.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Snow;
            this.label2.Location = new System.Drawing.Point(1085, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 26);
            this.label2.TabIndex = 66;
            this.label2.Text = "Métodos de Pago";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.DarkRed;
            this.label5.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Snow;
            this.label5.Location = new System.Drawing.Point(781, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 26);
            this.label5.TabIndex = 66;
            this.label5.Text = "Productos";
            // 
            // FGraficos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 499);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FGraficos";
            this.Text = "FGraficos";
            this.Load += new System.EventHandler(this.FGraficos_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMetodos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEmpleados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMetodos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProductos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEmpleados;
        private System.Windows.Forms.DateTimePicker dtpHastaGraficos;
        private System.Windows.Forms.DateTimePicker dtpDesdeGraficos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnActualizarGraficos;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
    }
}