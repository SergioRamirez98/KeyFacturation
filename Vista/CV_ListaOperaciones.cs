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
        List<CM_DatosOperacionesExcel> ListaDatos = new List<CM_DatosOperacionesExcel>();
        CM_DatosOperacionesExcel DatosKPI = new CM_DatosOperacionesExcel();
        public delegate void OperacionSeleccionadaHandler(CM_DatosOperacionesExcel DatosKPI);
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
            ListaDatos.Clear();
            ListaDatos = KPI.BuscarInterno();
            DTGV_ListaOperaciones.DataSource = ListaDatos;
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

        private void DTGV_ListaOperaciones_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    DTGV_ListaOperaciones.DataSource = KPI.OrdenarColumnas(0, ListaDatos);
                    break;
                case 1:
                    DTGV_ListaOperaciones.DataSource = KPI.OrdenarColumnas(1, ListaDatos);
                    break;
                case 2:
                    DTGV_ListaOperaciones.DataSource = KPI.OrdenarColumnas(2, ListaDatos);
                    break;
                case 3:
                    DTGV_ListaOperaciones.DataSource = KPI.OrdenarColumnas(3, ListaDatos);
                    break;
                case 4:
                    DTGV_ListaOperaciones.DataSource = KPI.OrdenarColumnas(4, ListaDatos);
                    break;
                case 5:
                    DTGV_ListaOperaciones.DataSource = KPI.OrdenarColumnas(5, ListaDatos);
                    break;
                case 6:
                    DTGV_ListaOperaciones.DataSource = KPI.OrdenarColumnas(6, ListaDatos);
                    break;
                case 7:
                    DTGV_ListaOperaciones.DataSource = KPI.OrdenarColumnas(7, ListaDatos);
                    break;
                case 8:
                    DTGV_ListaOperaciones.DataSource = KPI.OrdenarColumnas(8, ListaDatos);
                    break;
            }
        }
    }
}
