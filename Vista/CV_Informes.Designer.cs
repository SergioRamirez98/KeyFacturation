namespace Vista
{
    partial class CV_Informes
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
            this.DTGV_Informes = new System.Windows.Forms.DataGridView();
            this.Btn_Visualizar = new System.Windows.Forms.Button();
            this.Txb_BusqRapida = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DTGV_Informes)).BeginInit();
            this.SuspendLayout();
            // 
            // DTGV_Informes
            // 
            this.DTGV_Informes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTGV_Informes.Location = new System.Drawing.Point(12, 58);
            this.DTGV_Informes.Name = "DTGV_Informes";
            this.DTGV_Informes.Size = new System.Drawing.Size(776, 383);
            this.DTGV_Informes.TabIndex = 0;
            // 
            // Btn_Visualizar
            // 
            this.Btn_Visualizar.Location = new System.Drawing.Point(356, 459);
            this.Btn_Visualizar.Name = "Btn_Visualizar";
            this.Btn_Visualizar.Size = new System.Drawing.Size(75, 23);
            this.Btn_Visualizar.TabIndex = 1;
            this.Btn_Visualizar.Text = "Visualizar";
            this.Btn_Visualizar.UseVisualStyleBackColor = true;
            this.Btn_Visualizar.Click += new System.EventHandler(this.Btn_Visualizar_Click);
            // 
            // Txb_BusqRapida
            // 
            this.Txb_BusqRapida.Location = new System.Drawing.Point(344, 22);
            this.Txb_BusqRapida.Name = "Txb_BusqRapida";
            this.Txb_BusqRapida.Size = new System.Drawing.Size(100, 20);
            this.Txb_BusqRapida.TabIndex = 2;
            this.Txb_BusqRapida.TextChanged += new System.EventHandler(this.Txb_BusqRapida_TextChanged);
            // 
            // CV_Informes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 502);
            this.Controls.Add(this.Txb_BusqRapida);
            this.Controls.Add(this.Btn_Visualizar);
            this.Controls.Add(this.DTGV_Informes);
            this.Name = "CV_Informes";
            this.Text = "Informes";
            this.Load += new System.EventHandler(this.CV_Informes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DTGV_Informes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DTGV_Informes;
        private System.Windows.Forms.Button Btn_Visualizar;
        private System.Windows.Forms.TextBox Txb_BusqRapida;
    }
}