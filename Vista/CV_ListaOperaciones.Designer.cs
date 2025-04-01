namespace Vista
{
    partial class CV_ListaOperaciones
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
            this.DTGV_ListaOperaciones = new System.Windows.Forms.DataGridView();
            this.Txb_BusquedaRapida = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DTGV_ListaOperaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // DTGV_ListaOperaciones
            // 
            this.DTGV_ListaOperaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTGV_ListaOperaciones.Location = new System.Drawing.Point(12, 63);
            this.DTGV_ListaOperaciones.Name = "DTGV_ListaOperaciones";
            this.DTGV_ListaOperaciones.Size = new System.Drawing.Size(809, 418);
            this.DTGV_ListaOperaciones.TabIndex = 0;
            this.DTGV_ListaOperaciones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DTGV_ListaOperaciones_CellContentClick);
            this.DTGV_ListaOperaciones.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DTGV_ListaOperaciones_CellContentDoubleClick);
            this.DTGV_ListaOperaciones.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DTGV_ListaOperaciones_ColumnHeaderMouseClick);
            this.DTGV_ListaOperaciones.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTGV_ListaOperaciones_KeyDown);
            // 
            // Txb_BusquedaRapida
            // 
            this.Txb_BusquedaRapida.Location = new System.Drawing.Point(374, 27);
            this.Txb_BusquedaRapida.Name = "Txb_BusquedaRapida";
            this.Txb_BusquedaRapida.Size = new System.Drawing.Size(100, 20);
            this.Txb_BusquedaRapida.TabIndex = 1;
            this.Txb_BusquedaRapida.TextChanged += new System.EventHandler(this.Txb_BusquedaRapida_TextChanged);
            // 
            // CV_ListaOperaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 493);
            this.Controls.Add(this.Txb_BusquedaRapida);
            this.Controls.Add(this.DTGV_ListaOperaciones);
            this.Name = "CV_ListaOperaciones";
            this.Text = "Lista de operaciones";
            this.Load += new System.EventHandler(this.CV_ListaOperaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DTGV_ListaOperaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DTGV_ListaOperaciones;
        private System.Windows.Forms.TextBox Txb_BusquedaRapida;
    }
}