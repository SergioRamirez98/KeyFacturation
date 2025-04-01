using Logica;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class CV_Informes : Form
    {

        #region Properties

        CL_KPI KPI = new CL_KPI();
        CM_DatosOperacionesExcel DatosKPI = new CM_DatosOperacionesExcel();

        #endregion
        public CV_Informes()
        {
            InitializeComponent();
        }

        private void CV_Informes_Load(object sender, EventArgs e)
        {
            configurarDTGV();
            cargarDatosDTGV();
        }
        private void configurarDTGV()
        {
            
            DTGV_Informes.AllowUserToResizeColumns = false;
            DTGV_Informes.AllowUserToResizeRows = false;
            DTGV_Informes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DTGV_Informes.MultiSelect = false;
            DTGV_Informes.AllowUserToAddRows = false;
            DTGV_Informes.AllowUserToResizeColumns = false;
            DTGV_Informes.AllowUserToResizeRows = false;
            DTGV_Informes.ReadOnly = true;
            DTGV_Informes.RowHeadersVisible = false;
            DTGV_Informes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void cargarDatosDTGV()
        {
            DTGV_Informes.DataSource = KPI.ObtenerInformeKPI();
        }
    }
}
