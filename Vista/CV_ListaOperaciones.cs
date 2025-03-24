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
using System.Xml.Linq;

namespace Vista
{
    public partial class CV_ListaOperaciones : Form
    {
        #region Properties

        CL_KPI KPI = new CL_KPI();
        CM_DatosOperaciones DatosKPI = new CM_DatosOperaciones();
        public delegate void OperacionSeleccionadaHandler(CM_DatosOperaciones DatosKPI);
        public event OperacionSeleccionadaHandler OperacionSeleccionada;

        #endregion
        public CV_ListaOperaciones()
        {
            InitializeComponent();
        }

        private void CV_ListaOperaciones_Load(object sender, EventArgs e)
        {
            configurarDTGV();
            cargarDatosDTGV();
        }
        private void configurarDTGV() 
        {
             DTGV_ListaOperaciones.AllowUserToResizeColumns = false;
             DTGV_ListaOperaciones.AllowUserToResizeRows = false;
             DTGV_ListaOperaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
             DTGV_ListaOperaciones.MultiSelect = false;
             DTGV_ListaOperaciones.AllowUserToAddRows = false;
             DTGV_ListaOperaciones.AllowUserToResizeColumns = false;
             DTGV_ListaOperaciones.AllowUserToResizeRows = false;
             DTGV_ListaOperaciones.ReadOnly = true;
             DTGV_ListaOperaciones.RowHeadersVisible = false;
             DTGV_ListaOperaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void cargarDatosDTGV() 
        {
            DTGV_ListaOperaciones.DataSource = KPI.BuscarInterno();
        }

        private void DTGV_ListaOperaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DTGV_ListaOperaciones_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarOperacion();
        }

        private void DTGV_ListaOperaciones_KeyDown(object sender, KeyEventArgs e)
        {

            if (DTGV_ListaOperaciones.SelectedRows.Count > 0) seleccionarOperacion();
        }

        private void seleccionarOperacion()
        {
            if (DTGV_ListaOperaciones.SelectedRows.Count > 0)
            {
                int Seleccion = DTGV_ListaOperaciones.CurrentRow.Index;

                DatosKPI.Cliente = DTGV_ListaOperaciones.Rows[Seleccion].Cells[0].Value.ToString();
                DatosKPI.Interno = DTGV_ListaOperaciones.Rows[Seleccion].Cells[1].Value.ToString();
                DatosKPI.Destinacion = DTGV_ListaOperaciones.Rows[Seleccion].Cells[2].Value.ToString();
                DatosKPI.Oficializacion = DTGV_ListaOperaciones.Rows[Seleccion].Cells[4].Value.ToString();
                DatosKPI.FechaArribo = DTGV_ListaOperaciones.Rows[Seleccion].Cells[3].Value.ToString();
                DatosKPI.Salida = DTGV_ListaOperaciones.Rows[Seleccion].Cells[5].Value.ToString();
                DatosKPI.Fondos = DTGV_ListaOperaciones.Rows[Seleccion].Cells[6].Value.ToString();
                DatosKPI.Giro = DTGV_ListaOperaciones.Rows[Seleccion].Cells[7].Value.ToString();
                DatosKPI.Canal = DTGV_ListaOperaciones.Rows[Seleccion].Cells[8].Value.ToString();
                OperacionSeleccionada(DatosKPI);

                this.Close();
            }
        }

        private void Txb_BusquedaRapida_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Txb_BusquedaRapida.Text)) DTGV_ListaOperaciones.DataSource = KPI.filtrarOperaciones(Txb_BusquedaRapida.Text);
            else { DTGV_ListaOperaciones.DataSource = KPI.BuscarInterno(); }
            
        }
    }
}
