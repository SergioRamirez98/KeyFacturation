using Logica;
using Modelo;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            cargarDatosDTGV(); nombrarColumnas();
        }
        private void Btn_Visualizar_Click(object sender, EventArgs e)
        {

            if (DTGV_Informes.SelectedRows.Count > 0)
            {
                int Seleccion = DTGV_Informes.CurrentRow.Index;
                KPI.ID_KPI = Convert.ToInt32(DTGV_Informes.Rows[Seleccion].Cells[0].Value);
                try
                {
                    generarPDF();
                }
                catch (Exception ex)
                {
                    CServ_MsjUsuario.MensajesDeError(ex.Message);
                }

            }
            else { CServ_MsjUsuario.MensajesDeError("No ha seleccionado ninguna operación"); }
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
            DTGV_Informes.ClearSelection();
        }
        private void generarPDF()
        {
            KPI.ObtenerDatosPDF();
            if (!CServ_CrearPDF.AbrirPDFExistente(1))
            {
                CServ_CrearPDF.ImgOapce = Properties.Resources.LogoOapce;
                CServ_CrearPDF.ImgSedex = Properties.Resources.LogoSedex;
                CServ_CrearPDF.GenerarPDF(1);
            }
        }

        private void Txb_BusqRapida_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Txb_BusqRapida.Text)) { DTGV_Informes.DataSource = KPI.FiltrarKpis(Txb_BusqRapida.Text); DTGV_Informes.ClearSelection(); nombrarColumnas(); }
            else { DTGV_Informes.DataSource = KPI.ObtenerInformeKPI(); nombrarColumnas(); }

        }


        private void nombrarColumnas()
        {
            DTGV_Informes.Columns[0].HeaderText = "ID";
            DTGV_Informes.Columns[1].HeaderText = "Fecha KPI";
            DTGV_Informes.Columns[2].HeaderText = "Cliente";
            DTGV_Informes.Columns[3].HeaderText = "Interno";
            DTGV_Informes.Columns[4].HeaderText = "Vía";
            DTGV_Informes.Columns[5].HeaderText = "Canal";
            DTGV_Informes.Columns[6].HeaderText = "Destinación";
            DTGV_Informes.Columns[7].HeaderText = "Arribo";
            DTGV_Informes.Columns[8].HeaderText = "Cierre Ingreso";

            DTGV_Informes.Columns[9].HeaderText = "Fondos";
            DTGV_Informes.Columns[10].HeaderText = "Doc. Original";
            DTGV_Informes.Columns[11].HeaderText = "Oficialización";
            DTGV_Informes.Columns[12].HeaderText = "Retiro Carga";
            DTGV_Informes.Columns[13].HeaderText = "Total días hábiles";
            DTGV_Informes.Columns[14].HeaderText = "Resultado";
            DTGV_Informes.Columns[15].HeaderText = "Tipo de desvio";
            DTGV_Informes.Columns[16].HeaderText = "Motivo";
            DTGV_Informes.Columns[17].HeaderText = "Depósito / Terminal";

        }
    }
}
