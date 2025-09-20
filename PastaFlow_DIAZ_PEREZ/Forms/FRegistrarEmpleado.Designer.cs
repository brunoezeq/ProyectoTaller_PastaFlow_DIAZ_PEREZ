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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlRegistro = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lbEmpRol = new System.Windows.Forms.Label();
            this.txtEmpRContra = new System.Windows.Forms.TextBox();
            this.lbEmpRContra = new System.Windows.Forms.Label();
            this.txtEmpContra = new System.Windows.Forms.TextBox();
            this.lbEmpContra = new System.Windows.Forms.Label();
            this.txtEmpTelefono = new System.Windows.Forms.TextBox();
            this.lbEmpTelefono = new System.Windows.Forms.Label();
            this.txtEmpCorreo = new System.Windows.Forms.TextBox();
            this.lbEmpCorreo = new System.Windows.Forms.Label();
            this.txtEmpDNI = new System.Windows.Forms.TextBox();
            this.lbEmpDNI = new System.Windows.Forms.Label();
            this.txtEmpApellido = new System.Windows.Forms.TextBox();
            this.lbEmpApellido = new System.Windows.Forms.Label();
            this.cBoxRol = new System.Windows.Forms.ComboBox();
            this.txtEmpNombre = new System.Windows.Forms.TextBox();
            this.lbEmpNombre = new System.Windows.Forms.Label();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.lbEmpTitulo = new System.Windows.Forms.Label();
            this.txtBuscarDni = new System.Windows.Forms.TextBox();
            this.cBoxBuscarRol = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.pnlRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRegistro
            // 
            this.pnlRegistro.BackColor = System.Drawing.Color.DarkRed;
            this.pnlRegistro.Controls.Add(this.btnVolver);
            this.pnlRegistro.Controls.Add(this.label1);
            this.pnlRegistro.Controls.Add(this.btnEditar);
            this.pnlRegistro.Controls.Add(this.btnRegistrar);
            this.pnlRegistro.Controls.Add(this.lbEmpRol);
            this.pnlRegistro.Controls.Add(this.txtEmpRContra);
            this.pnlRegistro.Controls.Add(this.lbEmpRContra);
            this.pnlRegistro.Controls.Add(this.txtEmpContra);
            this.pnlRegistro.Controls.Add(this.lbEmpContra);
            this.pnlRegistro.Controls.Add(this.txtEmpTelefono);
            this.pnlRegistro.Controls.Add(this.lbEmpTelefono);
            this.pnlRegistro.Controls.Add(this.txtEmpCorreo);
            this.pnlRegistro.Controls.Add(this.lbEmpCorreo);
            this.pnlRegistro.Controls.Add(this.txtEmpDNI);
            this.pnlRegistro.Controls.Add(this.lbEmpDNI);
            this.pnlRegistro.Controls.Add(this.txtEmpApellido);
            this.pnlRegistro.Controls.Add(this.lbEmpApellido);
            this.pnlRegistro.Controls.Add(this.cBoxRol);
            this.pnlRegistro.Controls.Add(this.txtEmpNombre);
            this.pnlRegistro.Controls.Add(this.lbEmpNombre);
            this.pnlRegistro.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRegistro.Location = new System.Drawing.Point(0, 0);
            this.pnlRegistro.Name = "pnlRegistro";
            this.pnlRegistro.Size = new System.Drawing.Size(218, 483);
            this.pnlRegistro.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(30, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Registrar Empleado";
            // 
            // btnEditar
            // 
            this.btnEditar.ForeColor = System.Drawing.Color.Black;
            this.btnEditar.Location = new System.Drawing.Point(105, 435);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 23);
            this.btnEditar.TabIndex = 17;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrar.Location = new System.Drawing.Point(24, 435);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(75, 23);
            this.btnRegistrar.TabIndex = 16;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // lbEmpRol
            // 
            this.lbEmpRol.AutoSize = true;
            this.lbEmpRol.ForeColor = System.Drawing.Color.White;
            this.lbEmpRol.Location = new System.Drawing.Point(11, 383);
            this.lbEmpRol.Name = "lbEmpRol";
            this.lbEmpRol.Size = new System.Drawing.Size(23, 13);
            this.lbEmpRol.TabIndex = 15;
            this.lbEmpRol.Text = "Rol";
            // 
            // txtEmpRContra
            // 
            this.txtEmpRContra.Location = new System.Drawing.Point(12, 355);
            this.txtEmpRContra.Name = "txtEmpRContra";
            this.txtEmpRContra.PasswordChar = '•';
            this.txtEmpRContra.Size = new System.Drawing.Size(186, 20);
            this.txtEmpRContra.TabIndex = 14;
            // 
            // lbEmpRContra
            // 
            this.lbEmpRContra.AutoSize = true;
            this.lbEmpRContra.ForeColor = System.Drawing.Color.White;
            this.lbEmpRContra.Location = new System.Drawing.Point(11, 339);
            this.lbEmpRContra.Name = "lbEmpRContra";
            this.lbEmpRContra.Size = new System.Drawing.Size(98, 13);
            this.lbEmpRContra.TabIndex = 13;
            this.lbEmpRContra.Text = "Repetir Contraseña";
            // 
            // txtEmpContra
            // 
            this.txtEmpContra.Location = new System.Drawing.Point(12, 310);
            this.txtEmpContra.Name = "txtEmpContra";
            this.txtEmpContra.PasswordChar = '•';
            this.txtEmpContra.Size = new System.Drawing.Size(186, 20);
            this.txtEmpContra.TabIndex = 12;
            // 
            // lbEmpContra
            // 
            this.lbEmpContra.AutoSize = true;
            this.lbEmpContra.ForeColor = System.Drawing.Color.White;
            this.lbEmpContra.Location = new System.Drawing.Point(11, 294);
            this.lbEmpContra.Name = "lbEmpContra";
            this.lbEmpContra.Size = new System.Drawing.Size(61, 13);
            this.lbEmpContra.TabIndex = 11;
            this.lbEmpContra.Text = "Contraseña";
            // 
            // txtEmpTelefono
            // 
            this.txtEmpTelefono.Location = new System.Drawing.Point(12, 263);
            this.txtEmpTelefono.Name = "txtEmpTelefono";
            this.txtEmpTelefono.Size = new System.Drawing.Size(186, 20);
            this.txtEmpTelefono.TabIndex = 10;
            // 
            // lbEmpTelefono
            // 
            this.lbEmpTelefono.AutoSize = true;
            this.lbEmpTelefono.ForeColor = System.Drawing.Color.White;
            this.lbEmpTelefono.Location = new System.Drawing.Point(11, 247);
            this.lbEmpTelefono.Name = "lbEmpTelefono";
            this.lbEmpTelefono.Size = new System.Drawing.Size(49, 13);
            this.lbEmpTelefono.TabIndex = 9;
            this.lbEmpTelefono.Text = "Teléfono";
            // 
            // txtEmpCorreo
            // 
            this.txtEmpCorreo.Location = new System.Drawing.Point(12, 220);
            this.txtEmpCorreo.Name = "txtEmpCorreo";
            this.txtEmpCorreo.Size = new System.Drawing.Size(186, 20);
            this.txtEmpCorreo.TabIndex = 8;
            // 
            // lbEmpCorreo
            // 
            this.lbEmpCorreo.AutoSize = true;
            this.lbEmpCorreo.ForeColor = System.Drawing.Color.White;
            this.lbEmpCorreo.Location = new System.Drawing.Point(11, 204);
            this.lbEmpCorreo.Name = "lbEmpCorreo";
            this.lbEmpCorreo.Size = new System.Drawing.Size(94, 13);
            this.lbEmpCorreo.TabIndex = 7;
            this.lbEmpCorreo.Text = "Correo Electrónico";
            // 
            // txtEmpDNI
            // 
            this.txtEmpDNI.Location = new System.Drawing.Point(12, 178);
            this.txtEmpDNI.Name = "txtEmpDNI";
            this.txtEmpDNI.Size = new System.Drawing.Size(186, 20);
            this.txtEmpDNI.TabIndex = 6;
            // 
            // lbEmpDNI
            // 
            this.lbEmpDNI.AutoSize = true;
            this.lbEmpDNI.ForeColor = System.Drawing.Color.White;
            this.lbEmpDNI.Location = new System.Drawing.Point(11, 162);
            this.lbEmpDNI.Name = "lbEmpDNI";
            this.lbEmpDNI.Size = new System.Drawing.Size(82, 13);
            this.lbEmpDNI.TabIndex = 5;
            this.lbEmpDNI.Text = "Nro Documento";
            // 
            // txtEmpApellido
            // 
            this.txtEmpApellido.Location = new System.Drawing.Point(12, 134);
            this.txtEmpApellido.Name = "txtEmpApellido";
            this.txtEmpApellido.Size = new System.Drawing.Size(186, 20);
            this.txtEmpApellido.TabIndex = 4;
            // 
            // lbEmpApellido
            // 
            this.lbEmpApellido.AutoSize = true;
            this.lbEmpApellido.ForeColor = System.Drawing.Color.White;
            this.lbEmpApellido.Location = new System.Drawing.Point(11, 117);
            this.lbEmpApellido.Name = "lbEmpApellido";
            this.lbEmpApellido.Size = new System.Drawing.Size(44, 13);
            this.lbEmpApellido.TabIndex = 3;
            this.lbEmpApellido.Text = "Apellido";
            // 
            // cBoxRol
            // 
            this.cBoxRol.FormattingEnabled = true;
            this.cBoxRol.Location = new System.Drawing.Point(12, 399);
            this.cBoxRol.Name = "cBoxRol";
            this.cBoxRol.Size = new System.Drawing.Size(186, 21);
            this.cBoxRol.TabIndex = 2;
            // 
            // txtEmpNombre
            // 
            this.txtEmpNombre.Location = new System.Drawing.Point(12, 93);
            this.txtEmpNombre.Name = "txtEmpNombre";
            this.txtEmpNombre.Size = new System.Drawing.Size(186, 20);
            this.txtEmpNombre.TabIndex = 1;
            // 
            // lbEmpNombre
            // 
            this.lbEmpNombre.AutoSize = true;
            this.lbEmpNombre.ForeColor = System.Drawing.Color.White;
            this.lbEmpNombre.Location = new System.Drawing.Point(11, 77);
            this.lbEmpNombre.Name = "lbEmpNombre";
            this.lbEmpNombre.Size = new System.Drawing.Size(44, 13);
            this.lbEmpNombre.TabIndex = 0;
            this.lbEmpNombre.Text = "Nombre";
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsuarios.GridColor = System.Drawing.Color.Black;
            this.dgvUsuarios.Location = new System.Drawing.Point(233, 111);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.Size = new System.Drawing.Size(555, 150);
            this.dgvUsuarios.TabIndex = 1;
            this.dgvUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);
            // 
            // lbEmpTitulo
            // 
            this.lbEmpTitulo.AutoSize = true;
            this.lbEmpTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpTitulo.ForeColor = System.Drawing.Color.Black;
            this.lbEmpTitulo.Location = new System.Drawing.Point(394, 34);
            this.lbEmpTitulo.Name = "lbEmpTitulo";
            this.lbEmpTitulo.Size = new System.Drawing.Size(229, 25);
            this.lbEmpTitulo.TabIndex = 20;
            this.lbEmpTitulo.Text = "Gestión de Empleados";
            // 
            // txtBuscarDni
            // 
            this.txtBuscarDni.Location = new System.Drawing.Point(590, 84);
            this.txtBuscarDni.Name = "txtBuscarDni";
            this.txtBuscarDni.Size = new System.Drawing.Size(121, 20);
            this.txtBuscarDni.TabIndex = 21;
            this.txtBuscarDni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarDni_KeyPress);
            // 
            // cBoxBuscarRol
            // 
            this.cBoxBuscarRol.FormattingEnabled = true;
            this.cBoxBuscarRol.Location = new System.Drawing.Point(233, 85);
            this.cBoxBuscarRol.Name = "cBoxBuscarRol";
            this.cBoxBuscarRol.Size = new System.Drawing.Size(121, 21);
            this.cBoxBuscarRol.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(465, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "Nro de documento:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoLimpiar;
            this.btnLimpiar.Location = new System.Drawing.Point(754, 82);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(34, 23);
            this.btnLimpiar.TabIndex = 23;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::PastaFlow_DIAZ_PEREZ.Properties.Resources.iconoBuscar;
            this.btnBuscar.Location = new System.Drawing.Point(717, 82);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(34, 23);
            this.btnBuscar.TabIndex = 22;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            // FRegistrarEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 483);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cBoxBuscarRol);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscarDni);
            this.Controls.Add(this.lbEmpTitulo);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.pnlRegistro);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "FRegistrarEmpleado";
            this.Text = "Registrar Empleado";
            this.Load += new System.EventHandler(this.FRegEmpleado_Load);
            this.pnlRegistro.ResumeLayout(false);
            this.pnlRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlRegistro;
        private System.Windows.Forms.ComboBox cBoxRol;
        private System.Windows.Forms.TextBox txtEmpNombre;
        private System.Windows.Forms.Label lbEmpNombre;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.TextBox txtEmpContra;
        private System.Windows.Forms.Label lbEmpContra;
        private System.Windows.Forms.TextBox txtEmpTelefono;
        private System.Windows.Forms.Label lbEmpTelefono;
        private System.Windows.Forms.TextBox txtEmpCorreo;
        private System.Windows.Forms.Label lbEmpCorreo;
        private System.Windows.Forms.TextBox txtEmpDNI;
        private System.Windows.Forms.Label lbEmpDNI;
        private System.Windows.Forms.TextBox txtEmpApellido;
        private System.Windows.Forms.Label lbEmpApellido;
        private System.Windows.Forms.Label lbEmpRol;
        private System.Windows.Forms.TextBox txtEmpRContra;
        private System.Windows.Forms.Label lbEmpRContra;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbEmpTitulo;
        private System.Windows.Forms.TextBox txtBuscarDni;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.ComboBox cBoxBuscarRol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnVolver;
    }
}