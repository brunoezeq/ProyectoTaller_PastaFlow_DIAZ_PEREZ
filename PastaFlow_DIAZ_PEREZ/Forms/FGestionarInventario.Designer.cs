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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FGestionarInventario));
            this.pnlRegistro = new System.Windows.Forms.Panel();
            this.btnLimpiarForm = new System.Windows.Forms.Button();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lbProdTitulo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.nombreProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlRegistro.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRegistro
            // 
            this.pnlRegistro.BackColor = System.Drawing.Color.DarkRed;
            this.pnlRegistro.Controls.Add(this.btnLimpiarForm);
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
            resources.ApplyResources(this.pnlRegistro, "pnlRegistro");
            this.pnlRegistro.Name = "pnlRegistro";
            // 
            // btnLimpiarForm
            // 
            this.btnLimpiarForm.BackColor = System.Drawing.Color.RosyBrown;
            resources.ApplyResources(this.btnLimpiarForm, "btnLimpiarForm");
            this.btnLimpiarForm.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.btnLimpiarForm.Name = "btnLimpiarForm";
            this.btnLimpiarForm.UseVisualStyleBackColor = false;
            this.btnLimpiarForm.Click += new System.EventHandler(this.btnLimpiarForm_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.LemonChiffon;
            resources.ApplyResources(this.btnVolver, "btnVolver");
            this.btnVolver.ForeColor = System.Drawing.Color.DarkRed;
            this.btnVolver.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoAtrás;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // cBoxProdCat
            // 
            this.cBoxProdCat.FormattingEnabled = true;
            resources.ApplyResources(this.cBoxProdCat, "cBoxProdCat");
            this.cBoxProdCat.Name = "cBoxProdCat";
            // 
            // txtProdStock
            // 
            resources.ApplyResources(this.txtProdStock, "txtProdStock");
            this.txtProdStock.Name = "txtProdStock";
            this.txtProdStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StockProd_KeyPress);
            // 
            // txtProdPrecio
            // 
            resources.ApplyResources(this.txtProdPrecio, "txtProdPrecio");
            this.txtProdPrecio.Name = "txtProdPrecio";
            this.txtProdPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrecioProd_KeyPress);
            // 
            // txtProdDesc
            // 
            resources.ApplyResources(this.txtProdDesc, "txtProdDesc");
            this.txtProdDesc.Name = "txtProdDesc";
            this.txtProdDesc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DescProd_KeyPress);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.RosyBrown;
            resources.ApplyResources(this.btnEditar, "btnEditar");
            this.btnEditar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoEditar;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.LemonChiffon;
            resources.ApplyResources(this.btnRegistrar, "btnRegistrar");
            this.btnRegistrar.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoRegistrar;
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // lbProdCategoria
            // 
            resources.ApplyResources(this.lbProdCategoria, "lbProdCategoria");
            this.lbProdCategoria.ForeColor = System.Drawing.Color.White;
            this.lbProdCategoria.Name = "lbProdCategoria";
            // 
            // lbProdStock
            // 
            resources.ApplyResources(this.lbProdStock, "lbProdStock");
            this.lbProdStock.ForeColor = System.Drawing.Color.White;
            this.lbProdStock.Name = "lbProdStock";
            // 
            // lbProdPrecio
            // 
            resources.ApplyResources(this.lbProdPrecio, "lbProdPrecio");
            this.lbProdPrecio.ForeColor = System.Drawing.Color.White;
            this.lbProdPrecio.Name = "lbProdPrecio";
            // 
            // lbProdDescripcion
            // 
            resources.ApplyResources(this.lbProdDescripcion, "lbProdDescripcion");
            this.lbProdDescripcion.ForeColor = System.Drawing.Color.White;
            this.lbProdDescripcion.Name = "lbProdDescripcion";
            // 
            // txtProdNombre
            // 
            resources.ApplyResources(this.txtProdNombre, "txtProdNombre");
            this.txtProdNombre.Name = "txtProdNombre";
            this.txtProdNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NombreProd_KeyPress);
            // 
            // lbProdNombre
            // 
            resources.ApplyResources(this.lbProdNombre, "lbProdNombre");
            this.lbProdNombre.ForeColor = System.Drawing.Color.White;
            this.lbProdNombre.Name = "lbProdNombre";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtBuscarProducto);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnLimpiar);
            this.panel2.Controls.Add(this.btnBuscar);
            this.panel2.Controls.Add(this.lbProdTitulo);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // txtBuscarProducto
            // 
            resources.ApplyResources(this.txtBuscarProducto, "txtBuscarProducto");
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.RosyBrown;
            resources.ApplyResources(this.btnLimpiar, "btnLimpiar");
            this.btnLimpiar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.LemonChiffon;
            resources.ApplyResources(this.btnBuscar, "btnBuscar");
            this.btnBuscar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoBuscar;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // lbProdTitulo
            // 
            resources.ApplyResources(this.lbProdTitulo, "lbProdTitulo");
            this.lbProdTitulo.ForeColor = System.Drawing.Color.Black;
            this.lbProdTitulo.Name = "lbProdTitulo";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.dgvProductos);
            this.panel1.Name = "panel1";
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
            resources.ApplyResources(this.dgvProductos, "dgvProductos");
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            // 
            // nombreProd
            // 
            resources.ApplyResources(this.nombreProd, "nombreProd");
            this.nombreProd.Name = "nombreProd";
            this.nombreProd.ReadOnly = true;
            // 
            // DescProd
            // 
            resources.ApplyResources(this.DescProd, "DescProd");
            this.DescProd.Name = "DescProd";
            this.DescProd.ReadOnly = true;
            // 
            // Precio
            // 
            resources.ApplyResources(this.Precio, "Precio");
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            // 
            // Stock
            // 
            resources.ApplyResources(this.Stock, "Stock");
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            // 
            // Categoria
            // 
            resources.ApplyResources(this.Categoria, "Categoria");
            this.Categoria.Name = "Categoria";
            this.Categoria.ReadOnly = true;
            // 
            // Estado
            // 
            resources.ApplyResources(this.Estado, "Estado");
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            // 
            // FGestionarInventario
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlRegistro);
            this.Name = "FGestionarInventario";
            this.pnlRegistro.ResumeLayout(false);
            this.pnlRegistro.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtProdDesc;
        private System.Windows.Forms.ComboBox cBoxProdCat;
        private System.Windows.Forms.TextBox txtProdStock;
        private System.Windows.Forms.TextBox txtProdPrecio;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnLimpiarForm;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lbProdTitulo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
    }
}