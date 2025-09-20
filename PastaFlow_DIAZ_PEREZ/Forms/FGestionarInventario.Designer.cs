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
            this.cBoxProdCat = new System.Windows.Forms.ComboBox();
            this.txtProdStock = new System.Windows.Forms.TextBox();
            this.txtProdPrecio = new System.Windows.Forms.TextBox();
            this.txtProdDesc = new System.Windows.Forms.TextBox();
            this.btnVolver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lbProdCategoria = new System.Windows.Forms.Label();
            this.lbProdStock = new System.Windows.Forms.Label();
            this.lbProdPrecio = new System.Windows.Forms.Label();
            this.lbProdDescripcion = new System.Windows.Forms.Label();
            this.txtProdNombre = new System.Windows.Forms.TextBox();
            this.lbProdNombre = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lbProdTitulo = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.pnlRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRegistro
            // 
            this.pnlRegistro.BackColor = System.Drawing.Color.DarkRed;
            this.pnlRegistro.Controls.Add(this.cBoxProdCat);
            this.pnlRegistro.Controls.Add(this.txtProdStock);
            this.pnlRegistro.Controls.Add(this.txtProdPrecio);
            this.pnlRegistro.Controls.Add(this.txtProdDesc);
            this.pnlRegistro.Controls.Add(this.btnVolver);
            this.pnlRegistro.Controls.Add(this.label1);
            this.pnlRegistro.Controls.Add(this.btnEditar);
            this.pnlRegistro.Controls.Add(this.btnRegistrar);
            this.pnlRegistro.Controls.Add(this.lbProdCategoria);
            this.pnlRegistro.Controls.Add(this.lbProdStock);
            this.pnlRegistro.Controls.Add(this.lbProdPrecio);
            this.pnlRegistro.Controls.Add(this.lbProdDescripcion);
            this.pnlRegistro.Controls.Add(this.txtProdNombre);
            this.pnlRegistro.Controls.Add(this.lbProdNombre);
            this.pnlRegistro.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRegistro.Location = new System.Drawing.Point(0, 0);
            this.pnlRegistro.Name = "pnlRegistro";
            this.pnlRegistro.Size = new System.Drawing.Size(218, 450);
            this.pnlRegistro.TabIndex = 1;
            // 
            // cBoxProdCat
            // 
            this.cBoxProdCat.FormattingEnabled = true;
            this.cBoxProdCat.Location = new System.Drawing.Point(16, 271);
            this.cBoxProdCat.Name = "cBoxProdCat";
            this.cBoxProdCat.Size = new System.Drawing.Size(184, 21);
            this.cBoxProdCat.TabIndex = 24;
            // 
            // txtProdStock
            // 
            this.txtProdStock.Location = new System.Drawing.Point(14, 228);
            this.txtProdStock.Name = "txtProdStock";
            this.txtProdStock.Size = new System.Drawing.Size(187, 20);
            this.txtProdStock.TabIndex = 23;
            this.txtProdStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StockProd_KeyPress);
            // 
            // txtProdPrecio
            // 
            this.txtProdPrecio.Location = new System.Drawing.Point(13, 184);
            this.txtProdPrecio.Name = "txtProdPrecio";
            this.txtProdPrecio.Size = new System.Drawing.Size(187, 20);
            this.txtProdPrecio.TabIndex = 22;
            this.txtProdPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrecioProd_KeyPress);
            // 
            // txtProdDesc
            // 
            this.txtProdDesc.Location = new System.Drawing.Point(13, 140);
            this.txtProdDesc.Name = "txtProdDesc";
            this.txtProdDesc.Size = new System.Drawing.Size(187, 20);
            this.txtProdDesc.TabIndex = 21;
            this.txtProdDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DescProd_KeyPress);
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.DarkRed;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.DarkRed;
            this.btnVolver.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoAtrás;
            this.btnVolver.Location = new System.Drawing.Point(1, 5);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(22, 23);
            this.btnVolver.TabIndex = 20;
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 20);
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
            this.lbProdCategoria.Location = new System.Drawing.Point(13, 254);
            this.lbProdCategoria.Name = "lbProdCategoria";
            this.lbProdCategoria.Size = new System.Drawing.Size(54, 13);
            this.lbProdCategoria.TabIndex = 15;
            this.lbProdCategoria.Text = "Categoría";
            // 
            // lbProdStock
            // 
            this.lbProdStock.AutoSize = true;
            this.lbProdStock.ForeColor = System.Drawing.Color.White;
            this.lbProdStock.Location = new System.Drawing.Point(13, 211);
            this.lbProdStock.Name = "lbProdStock";
            this.lbProdStock.Size = new System.Drawing.Size(35, 13);
            this.lbProdStock.TabIndex = 9;
            this.lbProdStock.Text = "Stock";
            // 
            // lbProdPrecio
            // 
            this.lbProdPrecio.AutoSize = true;
            this.lbProdPrecio.ForeColor = System.Drawing.Color.White;
            this.lbProdPrecio.Location = new System.Drawing.Point(13, 168);
            this.lbProdPrecio.Name = "lbProdPrecio";
            this.lbProdPrecio.Size = new System.Drawing.Size(37, 13);
            this.lbProdPrecio.TabIndex = 7;
            this.lbProdPrecio.Text = "Precio";
            // 
            // lbProdDescripcion
            // 
            this.lbProdDescripcion.AutoSize = true;
            this.lbProdDescripcion.ForeColor = System.Drawing.Color.White;
            this.lbProdDescripcion.Location = new System.Drawing.Point(13, 123);
            this.lbProdDescripcion.Name = "lbProdDescripcion";
            this.lbProdDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lbProdDescripcion.TabIndex = 3;
            this.lbProdDescripcion.Text = "Descripción";
            // 
            // txtProdNombre
            // 
            this.txtProdNombre.Location = new System.Drawing.Point(14, 97);
            this.txtProdNombre.Name = "txtProdNombre";
            this.txtProdNombre.Size = new System.Drawing.Size(186, 20);
            this.txtProdNombre.TabIndex = 1;
            this.txtProdNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NombreProd_KeyPress);
            // 
            // lbProdNombre
            // 
            this.lbProdNombre.AutoSize = true;
            this.lbProdNombre.ForeColor = System.Drawing.Color.White;
            this.lbProdNombre.Location = new System.Drawing.Point(13, 81);
            this.lbProdNombre.Name = "lbProdNombre";
            this.lbProdNombre.Size = new System.Drawing.Size(44, 13);
            this.lbProdNombre.TabIndex = 0;
            this.lbProdNombre.Text = "Nombre";
            // 
            // dgvProductos
            // 
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(233, 97);
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
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.btnLimpiar.Location = new System.Drawing.Point(751, 69);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(34, 23);
            this.btnLimpiar.TabIndex = 26;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoBuscar;
            this.btnBuscar.Location = new System.Drawing.Point(714, 69);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(34, 23);
            this.btnBuscar.TabIndex = 25;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.Location = new System.Drawing.Point(587, 71);
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(121, 20);
            this.txtBuscarProducto.TabIndex = 24;
            // 
            // FGestionarInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscarProducto);
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
        private System.Windows.Forms.Label lbProdStock;
        private System.Windows.Forms.Label lbProdPrecio;
        private System.Windows.Forms.Label lbProdDescripcion;
        private System.Windows.Forms.TextBox txtProdNombre;
        private System.Windows.Forms.Label lbProdNombre;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lbProdTitulo;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TextBox txtProdDesc;
        private System.Windows.Forms.ComboBox cBoxProdCat;
        private System.Windows.Forms.TextBox txtProdStock;
        private System.Windows.Forms.TextBox txtProdPrecio;
    }
}