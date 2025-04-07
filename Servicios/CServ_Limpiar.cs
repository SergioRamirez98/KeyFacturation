using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios
{
    public static class CServ_Limpiar
    {
        public static void LimpiarPanelBox(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1;
                }
                else if (control is RadioButton)
                {
                    ((RadioButton)control).Checked = false;
                }
                else if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                }
                else if (control is DateTimePicker)
                {
                    ((DateTimePicker)control).Value = DateTime.Today;
                }
                if (control is MaskedTextBox)
                {
                    ((MaskedTextBox)control).Text= "  /  /   ";
                }
            }
        }
        public static void LimpiarDatosKPI() 
        {
            CM_DatosKPI_PDF.ID_KPI = 0;
            CM_DatosKPI_PDF.Fe_KPI = DateTime.Today;
            CM_DatosKPI_PDF.Cliente = null;
            CM_DatosKPI_PDF.Interno = null;
            CM_DatosKPI_PDF.Via = null;
            CM_DatosKPI_PDF.Canal = null;
            CM_DatosKPI_PDF.N_Despacho = null;

            CM_DatosKPI_PDF.Fe_Arribo = DateTime.Today;
            CM_DatosKPI_PDF.Fe_CierreIngreso = null;
            CM_DatosKPI_PDF.Fe_FondosAduana = null;
            CM_DatosKPI_PDF.Fe_DocOriginal= DateTime.Today;
            CM_DatosKPI_PDF.Fe_Oficializacion  = DateTime.Today;
            CM_DatosKPI_PDF.Fe_RetiroCarga  = DateTime.Today;

            CM_DatosKPI_PDF.TotalDiasHabiles  = 0;
            CM_DatosKPI_PDF.Resultado  = null;
            CM_DatosKPI_PDF.TipoDesvio = null;
            CM_DatosKPI_PDF.MotivoDesvio  = null;
            CM_DatosKPI_PDF.DepositoGiro  = null;
    }
    }
}
