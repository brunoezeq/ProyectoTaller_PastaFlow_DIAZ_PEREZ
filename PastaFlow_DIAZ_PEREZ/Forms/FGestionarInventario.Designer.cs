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
            this.btnVolver = new System.Windows.Forms.Button();
            this.cBoxProdCat = new System.Windows.Forms.ComboBox();
            this.txtProdStock = new System.Windows.Forms.TextBox();
            this.txtProdPrecio = new System.Windows.Forms.TextBox();
            this.txtProdDesc = new System.Windows.Forms.TextBox();
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
            this.nombreProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbProdTitulo = new System.Windows.Forms.Label();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.pnlRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRegistro
            // 
            this.pnlRegistro.BackColor = System.Drawing.Color.DarkRed;
            this.pnlRegistro.Controls.Add(this.btnVolver);
            this.pnlRegistro.Controls.Add(this.cBoxProdCat);
            this.pnlRegistro.Controls.Add(this.txtProdStock);
            this.pnlRegistro.Controls.Add(this.txtProdPrecio);
            this.pnlRegistro.Controls.Add(this.txtProdDesc);
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
            this.pnlRegistro.Size = new System.Drawing.Size(218, 485);
            this.pnlRegistro.TabIndex = 1;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.DarkRed;
            this.btnVolver.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoAtrás;
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(8, 8);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(33, 25);
            this.btnVolver.TabIndex = 30;
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // cBoxProdCat
            // 
            this.cBoxProdCat.FormattingEnabled = true;
            this.cBoxProdCat.Location = new System.Drawing.Point(14, 303);
            this.cBoxProdCat.Name = "cBoxProdCat";
            this.cBoxProdCat.Size = new System.Drawing.Size(184, 22);
            this.cBoxProdCat.TabIndex = 24;
            // 
            // txtProdStock
            // 
            this.txtProdStock.Location = new System.Drawing.Point(12, 256);
            this.txtProdStock.Name = "txtProdStock";
            this.txtProdStock.Size = new System.Drawing.Size(187, 22);
            this.txtProdStock.TabIndex = 23;
            this.txtProdStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StockProd_KeyPress);
            // 
            // txtProdPrecio
            // 
            this.txtProdPrecio.Location = new System.Drawing.Point(11, 209);
            this.txtProdPrecio.Name = "txtProdPrecio";
            this.txtProdPrecio.Size = new System.Drawing.Size(187, 22);
            this.txtProdPrecio.TabIndex = 22;
            this.txtProdPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrecioProd_KeyPress);
            // 
            // txtProdDesc
            // 
            this.txtProdDesc.Location = new System.Drawing.Point(11, 162);
            this.txtProdDesc.Name = "txtProdDesc";
            this.txtProdDesc.Size = new System.Drawing.Size(187, 22);
            this.txtProdDesc.TabIndex = 21;
            this.txtProdDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DescProd_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(27, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 23);
            this.label1.TabIndex = 18;
            this.label1.Text = "Registrar Producto";
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.RosyBrown;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoEditar;
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditar.Location = new System.Drawing.Point(113, 347);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 25);
            this.btnEditar.TabIndex = 17;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.UseVisualStyleBackColor = false;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoRegistrar;
            this.btnRegistrar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrar.Location = new System.Drawing.Point(23, 347);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(80, 25);
            this.btnRegistrar.TabIndex = 16;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistrar.UseVisualStyleBackColor = false;
            // 
            // lbProdCategoria
            // 
            this.lbProdCategoria.AutoSize = true;
            this.lbProdCategoria.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProdCategoria.ForeColor = System.Drawing.Color.White;
            this.lbProdCategoria.Location = new System.Drawing.Point(11, 284);
            this.lbProdCategoria.Name = "lbProdCategoria";
            this.lbProdCategoria.Size = new System.Drawing.Size(59, 15);
            this.lbProdCategoria.TabIndex = 15;
            this.lbProdCategoria.Text = "Categoría";
            // 
            // lbProdStock
            // 
            this.lbProdStock.AutoSize = true;
            this.lbProdStock.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProdStock.ForeColor = System.Drawing.Color.White;
            this.lbProdStock.Location = new System.Drawing.Point(11, 238);
            this.lbProdStock.Name = "lbProdStock";
            this.lbProdStock.Size = new System.Drawing.Size(36, 15);
            this.lbProdStock.TabIndex = 9;
            this.lbProdStock.Text = "Stock";
            // 
            // lbProdPrecio
            // 
            this.lbProdPrecio.AutoSize = true;
            this.lbProdPrecio.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProdPrecio.ForeColor = System.Drawing.Color.White;
            this.lbProdPrecio.Location = new System.Drawing.Point(11, 192);
            this.lbProdPrecio.Name = "lbProdPrecio";
            this.lbProdPrecio.Size = new System.Drawing.Size(41, 15);
            this.lbProdPrecio.TabIndex = 7;
            this.lbProdPrecio.Text = "Precio";
            // 
            // lbProdDescripcion
            // 
            this.lbProdDescripcion.AutoSize = true;
            this.lbProdDescripcion.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProdDescripcion.ForeColor = System.Drawing.Color.White;
            this.lbProdDescripcion.Location = new System.Drawing.Point(11, 143);
            this.lbProdDescripcion.Name = "lbProdDescripcion";
            this.lbProdDescripcion.Size = new System.Drawing.Size(69, 15);
            this.lbProdDescripcion.TabIndex = 3;
            this.lbProdDescripcion.Text = "Descripción";
            // 
            // txtProdNombre
            // 
            this.txtProdNombre.Location = new System.Drawing.Point(12, 115);
            this.txtProdNombre.Name = "txtProdNombre";
            this.txtProdNombre.Size = new System.Drawing.Size(186, 22);
            this.txtProdNombre.TabIndex = 1;
            this.txtProdNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NombreProd_KeyPress);
            // 
            // lbProdNombre
            // 
            this.lbProdNombre.AutoSize = true;
            this.lbProdNombre.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProdNombre.ForeColor = System.Drawing.Color.White;
            this.lbProdNombre.Location = new System.Drawing.Point(11, 98);
            this.lbProdNombre.Name = "lbProdNombre";
            this.lbProdNombre.Size = new System.Drawing.Size(53, 15);
            this.lbProdNombre.TabIndex = 0;
            this.lbProdNombre.Text = "Nombre";
            // 
            // dgvProductos
            // 
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombreProd,
            this.DescProd,
            this.Precio,
            this.Stock,
            this.Categoria,
            this.Estado});
            this.dgvProductos.Location = new System.Drawing.Point(241, 128);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.Size = new System.Drawing.Size(608, 162);
            this.dgvProductos.TabIndex = 2;
            // 
            // nombreProd
            // 
            this.nombreProd.HeaderText = "Producto";
            this.nombreProd.Name = "nombreProd";
            this.nombreProd.ReadOnly = true;
            this.nombreProd.Width = 150;
            // 
            // DescProd
            // 
            this.DescProd.HeaderText = "Descripción";
            this.DescProd.Name = "DescProd";
            this.DescProd.ReadOnly = true;
            this.DescProd.Width = 150;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Width = 60;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "Stock";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 60;
            // 
            // Categoria
            // 
            this.Categoria.HeaderText = "Categoría";
            this.Categoria.Name = "Categoria";
            this.Categoria.ReadOnly = true;
            this.Categoria.Width = 80;
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Width = 60;
            // 
            // lbProdTitulo
            // 
            this.lbProdTitulo.AutoSize = true;
            this.lbProdTitulo.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProdTitulo.ForeColor = System.Drawing.Color.Black;
            this.lbProdTitulo.Location = new System.Drawing.Point(406, 37);
            this.lbProdTitulo.Name = "lbProdTitulo";
            this.lbProdTitulo.Size = new System.Drawing.Size(199, 26);
            this.lbProdTitulo.TabIndex = 19;
            this.lbProdTitulo.Text = "Gestión de Inventario";
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.Location = new System.Drawing.Point(639, 96);
            this.txtBuscarProducto.Multiline = true;
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(144, 24);
            this.txtBuscarProducto.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(504, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 18);
            this.label2.TabIndex = 27;
            this.label2.Text = "Ingrese el producto : ";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.btnLimpiar.Location = new System.Drawing.Point(820, 96);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(27, 25);
            this.btnLimpiar.TabIndex = 26;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoBuscar;
            this.btnBuscar.Location = new System.Drawing.Point(789, 96);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(25, 25);
            this.btnBuscar.TabIndex = 25;
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // FGestionarInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(861, 485);
            this.Controls.Add(this.txtBuscarProducto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lbProdTitulo);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.pnlRegistro);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
        private System.Windows.Forms.TextBox txtProdDesc;
        private System.Windows.Forms.ComboBox cBoxProdCat;
        private System.Windows.Forms.TextBox txtProdStock;
        private System.Windows.Forms.TextBox txtProdPrecio;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
    }
}