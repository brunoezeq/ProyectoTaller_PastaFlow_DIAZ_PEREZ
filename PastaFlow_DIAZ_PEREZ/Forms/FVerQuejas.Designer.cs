namespace PastaFlow_DIAZ_PEREZ.Forms
{
    partial class FVerQuejas
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
            this.dgsQuejas = new System.Windows.Forms.DataGridView();
            this.lbQuejasTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgsQuejas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgsQuejas
            // 
            this.dgsQuejas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgsQuejas.Location = new System.Drawing.Point(41, 111);
            this.dgsQuejas.Name = "dgsQuejas";
            this.dgsQuejas.Size = new System.Drawing.Size(703, 150);
            this.dgsQuejas.TabIndex = 0;
            // 
            // lbQuejasTitulo
            // 
            this.lbQuejasTitulo.AutoSize = true;
            this.lbQuejasTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQuejasTitulo.ForeColor = System.Drawing.Color.Black;
            this.lbQuejasTitulo.Location = new System.Drawing.Point(300, 39);
            this.lbQuejasTitulo.Name = "lbQuejasTitulo";
            this.lbQuejasTitulo.Size = new System.Drawing.Size(194, 25);
            this.lbQuejasTitulo.TabIndex = 20;
            this.lbQuejasTitulo.Text = "Quejas de Clientes";
            // 
            // FVerQuejas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbQuejasTitulo);
            this.Controls.Add(this.dgsQuejas);
            this.Name = "FVerQuejas";
            this.Text = "Quejas";
            ((System.ComponentModel.ISupportInitialize)(this.dgsQuejas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgsQuejas;
        private System.Windows.Forms.Label lbQuejasTitulo;
    }
}