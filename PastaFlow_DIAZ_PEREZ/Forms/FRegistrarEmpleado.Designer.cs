namespace PastaFlow_DIAZ_PEREZ.Forms
{
    partial class FRegistrarEmpleado
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbEmpNombre = new System.Windows.Forms.Label();
            this.pnlRegistro = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lbEmpApellido = new System.Windows.Forms.Label();
            this.lbEmpDNI = new System.Windows.Forms.Label();
            this.lbEmpCorreo = new System.Windows.Forms.Label();
            this.lbEmpContraseña = new System.Windows.Forms.Label();
            this.lbEmpTelefono = new System.Windows.Forms.Label();
            this.lbEmpRContraseña = new System.Windows.Forms.Label();
            this.txtEmpNombre = new System.Windows.Forms.TextBox();
            this.txtEmpApellido = new System.Windows.Forms.TextBox();
            this.txtEmpDNI = new System.Windows.Forms.TextBox();
            this.txtEmpCorreo = new System.Windows.Forms.TextBox();
            this.txtEmpTelefono = new System.Windows.Forms.TextBox();
            this.txtEmpContrasena = new System.Windows.Forms.TextBox();
            this.txtEmpRContrasena = new System.Windows.Forms.TextBox();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.cBoxRol = new System.Windows.Forms.ComboBox();
            this.lbEmpRol = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Correo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lbEmpNombre
            // 
            this.lbEmpNombre.AutoSize = true;
            this.lbEmpNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpNombre.ForeColor = System.Drawing.Color.White;
            this.lbEmpNombre.Location = new System.Drawing.Point(18, 65);
            this.lbEmpNombre.Name = "lbEmpNombre";
            this.lbEmpNombre.Size = new System.Drawing.Size(44, 13);
            this.lbEmpNombre.TabIndex = 0;
            this.lbEmpNombre.Text = "Nombre";
            // 
            // pnlRegistro
            // 
            this.pnlRegistro.BackColor = System.Drawing.Color.DarkRed;
            this.pnlRegistro.Controls.Add(this.lbEmpRol);
            this.pnlRegistro.Controls.Add(this.cBoxRol);
            this.pnlRegistro.Controls.Add(this.lbTitulo);
            this.pnlRegistro.Controls.Add(this.btnEditar);
            this.pnlRegistro.Controls.Add(this.btnRegistrar);
            this.pnlRegistro.Controls.Add(this.txtEmpRContrasena);
            this.pnlRegistro.Controls.Add(this.txtEmpContrasena);
            this.pnlRegistro.Controls.Add(this.txtEmpTelefono);
            this.pnlRegistro.Controls.Add(this.txtEmpCorreo);
            this.pnlRegistro.Controls.Add(this.txtEmpDNI);
            this.pnlRegistro.Controls.Add(this.txtEmpApellido);
            this.pnlRegistro.Controls.Add(this.txtEmpNombre);
            this.pnlRegistro.Controls.Add(this.lbEmpRContraseña);
            this.pnlRegistro.Controls.Add(this.lbEmpTelefono);
            this.pnlRegistro.Controls.Add(this.lbEmpContraseña);
            this.pnlRegistro.Controls.Add(this.lbEmpCorreo);
            this.pnlRegistro.Controls.Add(this.lbEmpDNI);
            this.pnlRegistro.Controls.Add(this.lbEmpApellido);
            this.pnlRegistro.Controls.Add(this.lbEmpNombre);
            this.pnlRegistro.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRegistro.Location = new System.Drawing.Point(0, 0);
            this.pnlRegistro.Name = "pnlRegistro";
            this.pnlRegistro.Size = new System.Drawing.Size(218, 467);
            this.pnlRegistro.TabIndex = 1;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Apellido,
            this.Documento,
            this.Correo,
            this.Telefono,
            this.Rol,
            this.Estado,
            this.Eliminar});
            this.dataGridView.Location = new System.Drawing.Point(229, 92);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(767, 159);
            this.dataGridView.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(515, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Lista Empleados";
            // 
            // lbEmpApellido
            // 
            this.lbEmpApellido.AutoSize = true;
            this.lbEmpApellido.ForeColor = System.Drawing.Color.White;
            this.lbEmpApellido.Location = new System.Drawing.Point(18, 110);
            this.lbEmpApellido.Name = "lbEmpApellido";
            this.lbEmpApellido.Size = new System.Drawing.Size(44, 13);
            this.lbEmpApellido.TabIndex = 0;
            this.lbEmpApellido.Text = "Apellido";
            // 
            // lbEmpDNI
            // 
            this.lbEmpDNI.AutoSize = true;
            this.lbEmpDNI.ForeColor = System.Drawing.Color.White;
            this.lbEmpDNI.Location = new System.Drawing.Point(20, 154);
            this.lbEmpDNI.Name = "lbEmpDNI";
            this.lbEmpDNI.Size = new System.Drawing.Size(82, 13);
            this.lbEmpDNI.TabIndex = 1;
            this.lbEmpDNI.Text = "Nro Documento";
            // 
            // lbEmpCorreo
            // 
            this.lbEmpCorreo.AutoSize = true;
            this.lbEmpCorreo.ForeColor = System.Drawing.Color.White;
            this.lbEmpCorreo.Location = new System.Drawing.Point(19, 195);
            this.lbEmpCorreo.Name = "lbEmpCorreo";
            this.lbEmpCorreo.Size = new System.Drawing.Size(94, 13);
            this.lbEmpCorreo.TabIndex = 2;
            this.lbEmpCorreo.Text = "Correo Electrónico";
            // 
            // lbEmpContraseña
            // 
            this.lbEmpContraseña.AutoSize = true;
            this.lbEmpContraseña.ForeColor = System.Drawing.Color.White;
            this.lbEmpContraseña.Location = new System.Drawing.Point(18, 281);
            this.lbEmpContraseña.Name = "lbEmpContraseña";
            this.lbEmpContraseña.Size = new System.Drawing.Size(61, 13);
            this.lbEmpContraseña.TabIndex = 3;
            this.lbEmpContraseña.Text = "Contraseña";
            // 
            // lbEmpTelefono
            // 
            this.lbEmpTelefono.AutoSize = true;
            this.lbEmpTelefono.ForeColor = System.Drawing.Color.White;
            this.lbEmpTelefono.Location = new System.Drawing.Point(19, 238);
            this.lbEmpTelefono.Name = "lbEmpTelefono";
            this.lbEmpTelefono.Size = new System.Drawing.Size(49, 13);
            this.lbEmpTelefono.TabIndex = 4;
            this.lbEmpTelefono.Text = "Teléfono";
            // 
            // lbEmpRContraseña
            // 
            this.lbEmpRContraseña.AutoSize = true;
            this.lbEmpRContraseña.ForeColor = System.Drawing.Color.White;
            this.lbEmpRContraseña.Location = new System.Drawing.Point(18, 324);
            this.lbEmpRContraseña.Name = "lbEmpRContraseña";
            this.lbEmpRContraseña.Size = new System.Drawing.Size(98, 13);
            this.lbEmpRContraseña.TabIndex = 5;
            this.lbEmpRContraseña.Text = "Repetir Contraseña";
            // 
            // txtEmpNombre
            // 
            this.txtEmpNombre.Location = new System.Drawing.Point(21, 82);
            this.txtEmpNombre.Name = "txtEmpNombre";
            this.txtEmpNombre.Size = new System.Drawing.Size(175, 20);
            this.txtEmpNombre.TabIndex = 6;
            this.txtEmpNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpNombre_KeyPress);
            // 
            // txtEmpApellido
            // 
            this.txtEmpApellido.Location = new System.Drawing.Point(21, 128);
            this.txtEmpApellido.Name = "txtEmpApellido";
            this.txtEmpApellido.Size = new System.Drawing.Size(175, 20);
            this.txtEmpApellido.TabIndex = 7;
            this.txtEmpApellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpApellido_KeyPress);
            // 
            // txtEmpDNI
            // 
            this.txtEmpDNI.Location = new System.Drawing.Point(21, 171);
            this.txtEmpDNI.Name = "txtEmpDNI";
            this.txtEmpDNI.Size = new System.Drawing.Size(175, 20);
            this.txtEmpDNI.TabIndex = 8;
            this.txtEmpDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpDNI_KeyPress);
            // 
            // txtEmpCorreo
            // 
            this.txtEmpCorreo.Location = new System.Drawing.Point(21, 213);
            this.txtEmpCorreo.Name = "txtEmpCorreo";
            this.txtEmpCorreo.Size = new System.Drawing.Size(175, 20);
            this.txtEmpCorreo.TabIndex = 9;
            // 
            // txtEmpTelefono
            // 
            this.txtEmpTelefono.Location = new System.Drawing.Point(21, 254);
            this.txtEmpTelefono.Name = "txtEmpTelefono";
            this.txtEmpTelefono.Size = new System.Drawing.Size(175, 20);
            this.txtEmpTelefono.TabIndex = 10;
            this.txtEmpTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpTelefono_KeyPress);
            // 
            // txtEmpContrasena
            // 
            this.txtEmpContrasena.Location = new System.Drawing.Point(21, 297);
            this.txtEmpContrasena.Name = "txtEmpContrasena";
            this.txtEmpContrasena.PasswordChar = '•';
            this.txtEmpContrasena.Size = new System.Drawing.Size(175, 20);
            this.txtEmpContrasena.TabIndex = 11;
            this.txtEmpContrasena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpContra_KeyPress);
            // 
            // txtEmpRContrasena
            // 
            this.txtEmpRContrasena.Location = new System.Drawing.Point(21, 342);
            this.txtEmpRContrasena.Name = "txtEmpRContrasena";
            this.txtEmpRContrasena.PasswordChar = '•';
            this.txtEmpRContrasena.Size = new System.Drawing.Size(175, 20);
            this.txtEmpRContrasena.TabIndex = 12;
            this.txtEmpRContrasena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpRContra_KeyPress);
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.ForeColor = System.Drawing.Color.White;
            this.lbTitulo.Location = new System.Drawing.Point(37, 25);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(150, 20);
            this.lbTitulo.TabIndex = 15;
            this.lbTitulo.Text = "Registrar Empleado";
            // 
            // cBoxRol
            // 
            this.cBoxRol.FormattingEnabled = true;
            this.cBoxRol.Location = new System.Drawing.Point(21, 383);
            this.cBoxRol.Name = "cBoxRol";
            this.cBoxRol.Size = new System.Drawing.Size(175, 21);
            this.cBoxRol.TabIndex = 16;
            // 
            // lbEmpRol
            // 
            this.lbEmpRol.AutoSize = true;
            this.lbEmpRol.ForeColor = System.Drawing.Color.White;
            this.lbEmpRol.Location = new System.Drawing.Point(20, 367);
            this.lbEmpRol.Name = "lbEmpRol";
            this.lbEmpRol.Size = new System.Drawing.Size(23, 13);
            this.lbEmpRol.TabIndex = 17;
            this.lbEmpRol.Text = "Rol";
            // 
            // btnEditar
            // 
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoEditar;
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(111, 419);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(79, 23);
            this.btnEditar.TabIndex = 14;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoGuardar;
            this.btnRegistrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistrar.Location = new System.Drawing.Point(26, 419);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(79, 23);
            this.btnRegistrar.TabIndex = 13;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            // 
            // Apellido
            // 
            this.Apellido.HeaderText = "Apellido";
            this.Apellido.Name = "Apellido";
            // 
            // Documento
            // 
            this.Documento.HeaderText = "Nro Documento";
            this.Documento.Name = "Documento";
            this.Documento.Width = 120;
            // 
            // Correo
            // 
            this.Correo.HeaderText = "Correo Electrónico";
            this.Correo.Name = "Correo";
            this.Correo.Width = 150;
            // 
            // Telefono
            // 
            this.Telefono.HeaderText = "Teléfono";
            this.Telefono.Name = "Telefono";
            // 
            // Rol
            // 
            this.Rol.HeaderText = "Rol";
            this.Rol.Name = "Rol";
            this.Rol.Width = 75;
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.Width = 50;
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            // 
            // FRegistrarEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 467);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.pnlRegistro);
            this.Name = "FRegistrarEmpleado";
            this.Text = "Registrar Empleado";
            this.Load += new System.EventHandler(this.FRegEmpleado_Load);
            this.pnlRegistro.ResumeLayout(false);
            this.pnlRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbEmpNombre;
        private System.Windows.Forms.Panel pnlRegistro;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbEmpApellido;
        private System.Windows.Forms.Label lbEmpRContraseña;
        private System.Windows.Forms.Label lbEmpTelefono;
        private System.Windows.Forms.Label lbEmpContraseña;
        private System.Windows.Forms.Label lbEmpCorreo;
        private System.Windows.Forms.Label lbEmpDNI;
        private System.Windows.Forms.TextBox txtEmpCorreo;
        private System.Windows.Forms.TextBox txtEmpDNI;
        private System.Windows.Forms.TextBox txtEmpApellido;
        private System.Windows.Forms.TextBox txtEmpNombre;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.TextBox txtEmpRContrasena;
        private System.Windows.Forms.TextBox txtEmpContrasena;
        private System.Windows.Forms.TextBox txtEmpTelefono;
        private System.Windows.Forms.Label lbEmpRol;
        private System.Windows.Forms.ComboBox cBoxRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Correo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewButtonColumn Eliminar;
    }
}