namespace PastaFlow_DIAZ_PEREZ.Forms
{
    partial class FGestionarInventario
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lbProdCategoria = new System.Windows.Forms.Label();
            this.txtEmpTelefono = new System.Windows.Forms.TextBox();
            this.lbProdStock = new System.Windows.Forms.Label();
            this.txtEmpCorreo = new System.Windows.Forms.TextBox();
            this.lbProdPrecio = new System.Windows.Forms.Label();
            this.txtEmpDNI = new System.Windows.Forms.TextBox();
            this.lbEmpDNI = new System.Windows.Forms.Label();
            this.txtEmpApellido = new System.Windows.Forms.TextBox();
            this.lbProdDescripcion = new System.Windows.Forms.Label();
            this.cBoxRol = new System.Windows.Forms.ComboBox();
            this.txtProdNombre = new System.Windows.Forms.TextBox();
            this.lbProdNombre = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lbProdTitulo = new System.Windows.Forms.Label();
            this.pnlRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRegistro
            // 
            this.pnlRegistro.BackColor = System.Drawing.Color.DarkRed;
            this.pnlRegistro.Controls.Add(this.label1);
            this.pnlRegistro.Controls.Add(this.btnEditar);
            this.pnlRegistro.Controls.Add(this.btnRegistrar);
            this.pnlRegistro.Controls.Add(this.lbProdCategoria);
            this.pnlRegistro.Controls.Add(this.txtEmpTelefono);
            this.pnlRegistro.Controls.Add(this.lbProdStock);
            this.pnlRegistro.Controls.Add(this.txtEmpCorreo);
            this.pnlRegistro.Controls.Add(this.lbProdPrecio);
            this.pnlRegistro.Controls.Add(this.txtEmpDNI);
            this.pnlRegistro.Controls.Add(this.lbEmpDNI);
            this.pnlRegistro.Controls.Add(this.txtEmpApellido);
            this.pnlRegistro.Controls.Add(this.lbProdDescripcion);
            this.pnlRegistro.Controls.Add(this.cBoxRol);
            this.pnlRegistro.Controls.Add(this.txtProdNombre);
            this.pnlRegistro.Controls.Add(this.lbProdNombre);
            this.pnlRegistro.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRegistro.Location = new System.Drawing.Point(0, 0);
            this.pnlRegistro.Name = "pnlRegistro";
            this.pnlRegistro.Size = new System.Drawing.Size(218, 450);
            this.pnlRegistro.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(44, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Registrar Producto";
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(105, 407);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 23);
            this.btnEditar.TabIndex = 17;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrar.Location = new System.Drawing.Point(24, 407);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(75, 23);
            this.btnRegistrar.TabIndex = 16;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            // 
            // lbProdCategoria
            // 
            this.lbProdCategoria.AutoSize = true;
            this.lbProdCategoria.ForeColor = System.Drawing.Color.White;
            this.lbProdCategoria.Location = new System.Drawing.Point(11, 270);
            this.lbProdCategoria.Name = "lbProdCategoria";
            this.lbProdCategoria.Size = new System.Drawing.Size(54, 13);
            this.lbProdCategoria.TabIndex = 15;
            this.lbProdCategoria.Text = "Categoría";
            // 
            // txtEmpTelefono
            // 
            this.txtEmpTelefono.Location = new System.Drawing.Point(12, 243);
            this.txtEmpTelefono.Name = "txtEmpTelefono";
            this.txtEmpTelefono.Size = new System.Drawing.Size(186, 20);
            this.txtEmpTelefono.TabIndex = 10;
            // 
            // lbProdStock
            // 
            this.lbProdStock.AutoSize = true;
            this.lbProdStock.ForeColor = System.Drawing.Color.White;
            this.lbProdStock.Location = new System.Drawing.Point(11, 227);
            this.lbProdStock.Name = "lbProdStock";
            this.lbProdStock.Size = new System.Drawing.Size(35, 13);
            this.lbProdStock.TabIndex = 9;
            this.lbProdStock.Text = "Stock";
            // 
            // txtEmpCorreo
            // 
            this.txtEmpCorreo.Location = new System.Drawing.Point(12, 200);
            this.txtEmpCorreo.Name = "txtEmpCorreo";
            this.txtEmpCorreo.Size = new System.Drawing.Size(186, 20);
            this.txtEmpCorreo.TabIndex = 8;
            // 
            // lbProdPrecio
            // 
            this.lbProdPrecio.AutoSize = true;
            this.lbProdPrecio.ForeColor = System.Drawing.Color.White;
            this.lbProdPrecio.Location = new System.Drawing.Point(11, 184);
            this.lbProdPrecio.Name = "lbProdPrecio";
            this.lbProdPrecio.Size = new System.Drawing.Size(37, 13);
            this.lbProdPrecio.TabIndex = 7;
            this.lbProdPrecio.Text = "Precio";
            // 
            // txtEmpDNI
            // 
            this.txtEmpDNI.Location = new System.Drawing.Point(12, 158);
            this.txtEmpDNI.Name = "txtEmpDNI";
            this.txtEmpDNI.Size = new System.Drawing.Size(186, 20);
            this.txtEmpDNI.TabIndex = 6;
            // 
            // lbEmpDNI
            // 
            this.lbEmpDNI.AutoSize = true;
            this.lbEmpDNI.ForeColor = System.Drawing.Color.White;
            this.lbEmpDNI.Location = new System.Drawing.Point(11, 142);
            this.lbEmpDNI.Name = "lbEmpDNI";
            this.lbEmpDNI.Size = new System.Drawing.Size(82, 13);
            this.lbEmpDNI.TabIndex = 5;
            this.lbEmpDNI.Text = "Nro Documento";
            // 
            // txtEmpApellido
            // 
            this.txtEmpApellido.Location = new System.Drawing.Point(12, 114);
            this.txtEmpApellido.Name = "txtEmpApellido";
            this.txtEmpApellido.Size = new System.Drawing.Size(186, 20);
            this.txtEmpApellido.TabIndex = 4;
            // 
            // lbProdDescripcion
            // 
            this.lbProdDescripcion.AutoSize = true;
            this.lbProdDescripcion.ForeColor = System.Drawing.Color.White;
            this.lbProdDescripcion.Location = new System.Drawing.Point(11, 97);
            this.lbProdDescripcion.Name = "lbProdDescripcion";
            this.lbProdDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lbProdDescripcion.TabIndex = 3;
            this.lbProdDescripcion.Text = "Descripción";
            // 
            // cBoxRol
            // 
            this.cBoxRol.FormattingEnabled = true;
            this.cBoxRol.Location = new System.Drawing.Point(12, 286);
            this.cBoxRol.Name = "cBoxRol";
            this.cBoxRol.Size = new System.Drawing.Size(186, 21);
            this.cBoxRol.TabIndex = 2;
            // 
            // txtProdNombre
            // 
            this.txtProdNombre.Location = new System.Drawing.Point(12, 73);
            this.txtProdNombre.Name = "txtProdNombre";
            this.txtProdNombre.Size = new System.Drawing.Size(186, 20);
            this.txtProdNombre.TabIndex = 1;
            // 
            // lbProdNombre
            // 
            this.lbProdNombre.AutoSize = true;
            this.lbProdNombre.ForeColor = System.Drawing.Color.White;
            this.lbProdNombre.Location = new System.Drawing.Point(11, 57);
            this.lbProdNombre.Name = "lbProdNombre";
            this.lbProdNombre.Size = new System.Drawing.Size(44, 13);
            this.lbProdNombre.TabIndex = 0;
            this.lbProdNombre.Text = "Nombre";
            // 
            // dgvProductos
            // 
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(233, 70);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.Size = new System.Drawing.Size(555, 150);
            this.dgvProductos.TabIndex = 2;
            // 
            // lbProdTitulo
            // 
            this.lbProdTitulo.AutoSize = true;
            this.lbProdTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProdTitulo.ForeColor = System.Drawing.Color.Black;
            this.lbProdTitulo.Location = new System.Drawing.Point(395, 26);
            this.lbProdTitulo.Name = "lbProdTitulo";
            this.lbProdTitulo.Size = new System.Drawing.Size(216, 25);
            this.lbProdTitulo.TabIndex = 19;
            this.lbProdTitulo.Text = "Gestión de Inventario";
            // 
            // FGestionarInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbProdTitulo);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.pnlRegistro);
            this.Name = "FGestionarInventario";
            this.Text = "Inventario";
            this.pnlRegistro.ResumeLayout(false);
            this.pnlRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlRegistro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lbProdCategoria;
        private System.Windows.Forms.TextBox txtEmpTelefono;
        private System.Windows.Forms.Label lbProdStock;
        private System.Windows.Forms.TextBox txtEmpCorreo;
        private System.Windows.Forms.Label lbProdPrecio;
        private System.Windows.Forms.TextBox txtEmpDNI;
        private System.Windows.Forms.Label lbEmpDNI;
        private System.Windows.Forms.TextBox txtEmpApellido;
        private System.Windows.Forms.Label lbProdDescripcion;
        private System.Windows.Forms.ComboBox cBoxRol;
        private System.Windows.Forms.TextBox txtProdNombre;
        private System.Windows.Forms.Label lbProdNombre;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lbProdTitulo;
    }
}