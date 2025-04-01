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
            ((System.ComponentModel.ISupportInitialize)(this.DTGV_Informes)).BeginInit();
            this.SuspendLayout();
            // 
            // DTGV_Informes
            // 
            this.DTGV_Informes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTGV_Informes.Location = new System.Drawing.Point(12, 12);
            this.DTGV_Informes.Name = "DTGV_Informes";
            this.DTGV_Informes.Size = new System.Drawing.Size(776, 383);
            this.DTGV_Informes.TabIndex = 0;
            // 
            // CV_Informes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DTGV_Informes);
            this.Name = "CV_Informes";
            this.Text = "Informes";
            this.Load += new System.EventHandler(this.CV_Informes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DTGV_Informes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DTGV_Informes;
    }
}