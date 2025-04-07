using Logica;
using Modelo;
using Servicios;
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
    public partial class CV_KPI : Form
    {
        #region Properties

        CL_KPI KPI = new CL_KPI();

        public delegate void OperacionSeleccionadaHandler(CM_DatosOperacionesExcel DatosKPI);
        public event OperacionSeleccionadaHandler OperacionSeleccionada;
        int diasHabiles = 0;
        public List<CM_IndicadoresKPI> IndKPI { get; set; } = new List<CM_IndicadoresKPI>();

        #endregion
        public CV_KPI()
        {
            InitializeComponent();
            cargarControles();
        }
        private void CV_KPI_Load(object sender, EventArgs e)
        {
            configurarComboBox();
            configurarTabla();
            CServ_CrearPDF.CrearImagenIndicadores(TLP_Indicadores);
        }
        private void Btn_Generar_Click(object sender, EventArgs e)
        {
            try
            {
                
                pasarDatos(2);
                if (!KPI.BuscarKPI())
                {
                    KPI.GuardarEnBDD();
                    generarPDF();
                    CServ_MsjUsuario.Exito("KPI generado con éxito");
                    Btn_Refresh_Click(sender, e);
                }
                else { CServ_MsjUsuario.MensajesDeError("El KPI para la operación indicada ya existe."); }
            }
            catch (Exception ex)
            {
                CServ_MsjUsuario.MensajesDeError(ex.Message);                
            }
        }
        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            CServ_Limpiar.LimpiarPanelBox(Pnl_DatosKPI);
            CServ_Limpiar.LimpiarPanelBox(Pnl_ResultadoKPI);
            cargarControles();
            CV_ListaOperaciones Listado = new CV_ListaOperaciones();
            Listado.OperacionSeleccionada += seleccionOperacion;
            Listado.ShowDialog();
            calcularKPI();
        }
        private void Mtxb_RetiroCarga_TextChanged(object sender, EventArgs e)
        {
            if (Mtxb_RetiroCarga.MaskCompleted && Mtxb_Arribo.MaskCompleted)
            {
                calcularKPI();
            }
        }
        private void Txb_DiasHabiles_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Txb_DiasHabiles.Text))
            {
                try
                {
                    diasHabiles = Convert.ToInt32(Txb_DiasHabiles.Text);


                    switch (Cmb_Via.Text)
                    {
                        case "MARÍTIMO":
                            if (diasHabiles < 5) { Cmb_Resultado.SelectedIndex = 0; }
                            else if (diasHabiles == 5) { Cmb_Resultado.SelectedIndex = 1; }
                            else { Cmb_Resultado.SelectedIndex = 2; }
                            break;
                        case "TERRESTRE":
                            if (diasHabiles < 2) { Cmb_Resultado.SelectedIndex = 0; }
                            else if (diasHabiles == 2) { Cmb_Resultado.SelectedIndex = 1; }
                            else { Cmb_Resultado.SelectedIndex = 2; }
                            break;
                        case "AEREO":
                            if (diasHabiles < 3) { Cmb_Resultado.SelectedIndex = 0; }
                            else if (diasHabiles == 3) { Cmb_Resultado.SelectedIndex = 1; }
                            else { Cmb_Resultado.SelectedIndex = 2; }
                            break;
                        case "CARGA SUELTA":
                            if (diasHabiles < 10) { Cmb_Resultado.SelectedIndex = 0; }
                            else if (diasHabiles == 10) { Cmb_Resultado.SelectedIndex = 1; }
                            else { Cmb_Resultado.SelectedIndex = 2; }
                            break;
                        case "IC06":
                            if (diasHabiles < 3) { Cmb_Resultado.SelectedIndex = 0; }
                            else if (diasHabiles == 3) { Cmb_Resultado.SelectedIndex = 1; }
                            else { Cmb_Resultado.SelectedIndex = 2; }
                            break;
                    }

                }
                catch (Exception)
                {

                    throw new Exception("Por favor ingrese un valor numerico.");
                }
            }
            else
            {
                Cmb_Resultado.SelectedIndex = -1;
                Cmb_Desvio.SelectedIndex = -1;
            }
        }
        private void Cmb_Resultado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_Resultado.SelectedIndex == 0 || Cmb_Resultado.SelectedIndex == 1)
            {
                Cmb_Desvio.SelectedIndex = 0;
            }
        }
        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            CServ_Limpiar.LimpiarPanelBox(Pnl_DatosKPI);
            CServ_Limpiar.LimpiarPanelBox(Pnl_ResultadoKPI);
            CServ_Limpiar.LimpiarDatosKPI();
            cargarControles();
        }
        private void Btn_Informe_Click(object sender, EventArgs e)
        {
            CV_Informes Informes = new CV_Informes();
            Informes.Show();
        }

        private void cargarControles() 
        {
            Txb_FechaKpi.Text = DateTime.Today.ToString("dd/MM/yyyy"); 
        }
        private void seleccionOperacion(CM_DatosOperacionesExcel DatosKPI)
        {
            Mtxb_Arribo.Text = DatosKPI.FechaArribo;
            Mtxb_Fondos.Text = DatosKPI.Fondos;
            Mtxb_Oficializacion.Text = DatosKPI.Oficializacion;
            Txb_Canal.Text = DatosKPI.Canal;
            Txb_Cliente.Text = DatosKPI.Cliente;
            Txb_DepGiro.Text =DatosKPI.Giro;
            Txb_Despacho.Text = DatosKPI.Destinacion;
            Txb_Interno.Text = DatosKPI.Interno;
            if (!string.IsNullOrEmpty(DatosKPI.Giro))
            {
                switch (DatosKPI.Giro)
                {
                    case "TERMINAL 1 2 Y 3":
                    case "TERMINAL 4":
                    case "TERMINAL 5":
                    case "TERMINAL SUR":
                        Cmb_Via.SelectedIndex = 0;
                        break;
                    case "BODEGA IMPO EXPO EZE":
                        Cmb_Via.SelectedIndex = 1;
                        break;
                    default:
                        Cmb_Via.SelectedIndex = 2;
                        break;

                }
            }
            Mtxb_RetiroCarga.Text = DatosKPI.Salida;
            calcularKPI();
        }
        private void configurarComboBox()
        {
            Cmb_Desvio.Items.Add("N/A");
            Cmb_Desvio.Items.Add("INTERNO");
            Cmb_Desvio.Items.Add("EXTERNO");
            Cmb_Desvio.Items.Add("INTERNO& EXTERNO");
            Cmb_Resultado.Items.Add("EXCELENTE");
            Cmb_Resultado.Items.Add("REQUERIDO");
            Cmb_Resultado.Items.Add("NO SATISFACTORIO");
            Cmb_Via.Items.Add("MARÍTIMO");
            Cmb_Via.Items.Add("AEREO");
            Cmb_Via.Items.Add("TERRESTRE");
            Cmb_Via.Items.Add("CARGA SUELTA");
            Cmb_Via.Items.Add("IC06");
        }
        private void configurarTabla()
        {
            IndKPI.Clear();
            IndKPI=KPI.ObtenerValoresdeIndicadores();
            TLP_Indicadores.Controls.Clear();
            TLP_Indicadores.RowCount = IndKPI.Count + 1;

            TLP_Indicadores.Controls.Add(new Label()
            {
                Text = "Vía",
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 0);

            TLP_Indicadores.Controls.Add(new Label()
            {
                Text = "No satisfactorio",
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.Red,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            }, 1, 0);

            TLP_Indicadores.Controls.Add(new Label()
            {
                Text = "Requerido",
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.Green,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            }, 2, 0);

            TLP_Indicadores.Controls.Add(new Label()
            {
                Text = "Excelente",
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            }, 3, 0);

            for (int fila = 0; fila < IndKPI.Count; fila++)
            {
                var indicador = IndKPI[fila];
                /*
                TLP_Indicadores.Controls.Add(new Label() { Text = indicador.Via, AutoSize = true }, 0, i + 1);
                TLP_Indicadores.Controls.Add(new Label() { Text = indicador.NoSatisfactorio.ToString(), AutoSize = true }, 1, i + 1);
                TLP_Indicadores.Controls.Add(new Label() { Text = indicador.Requerido.ToString(), AutoSize = true }, 2, i + 1);
                TLP_Indicadores.Controls.Add(new Label() { Text = indicador.Excelente.ToString(), AutoSize = true }, 3, i + 1);*/

                TLP_Indicadores.Controls.Add(new Label() { Text = indicador.Via, AutoSize = true, TextAlign = ContentAlignment.MiddleCenter }, 0, fila + 1);
                TLP_Indicadores.Controls.Add(new Label() { Text = indicador.NoSatisfactorio.ToString(), AutoSize = true, TextAlign = ContentAlignment.BottomCenter }, 1, fila + 1);
                TLP_Indicadores.Controls.Add(new Label() { Text = indicador.Requerido.ToString(), AutoSize = true, TextAlign = ContentAlignment.MiddleCenter }, 2, fila + 1);
                TLP_Indicadores.Controls.Add(new Label() { Text = indicador.Excelente.ToString(), AutoSize = true, TextAlign = ContentAlignment.MiddleCenter }, 3, fila + 1);

            }
            TLP_Indicadores.Refresh();

        }
        private void calcularKPI() 
        {
            try
            {
                pasarDatos(1);
                diasHabiles = KPI.CalcularResultado();
                if (diasHabiles > 0) Txb_DiasHabiles.Text = Convert.ToString(diasHabiles);
                else if (diasHabiles == 0) Txb_DiasHabiles.Text = Convert.ToString(diasHabiles);
                else { Txb_DiasHabiles.Text = ""; }
            }
            catch (Exception ex)
            {
                CServ_MsjUsuario.MensajesDeError(ex.Message);
            }
        }
        private void pasarDatos(int accion) 
        {
            switch (accion)
            {
                case 1:
                    if (Mtxb_RetiroCarga.MaskCompleted)
                    {
                        KPI.FeIngreso = Convert.ToDateTime(Mtxb_Arribo.Text);
                        KPI.FeSalida = Convert.ToDateTime(Mtxb_RetiroCarga.Text);
                    }
                    break;
                case 2:
                    KPI.Fe_KPI = Txb_FechaKpi.Text;
                    KPI.Cliente= Txb_Cliente.Text;
                    KPI.Interno = Txb_Interno.Text;
                    KPI.Via = Cmb_Via.Text;
                    KPI.Canal = Txb_Canal.Text;
                    KPI.Despacho = Txb_Despacho.Text;
                    KPI.Giro = Txb_DepGiro.Text;
                    KPI.Fe_Arribo = Mtxb_Arribo.Text;
                    KPI.Fe_CID = Mtxb_CierreIngreso.Text;
                    KPI.Fe_Fondos = Mtxb_Fondos.Text;
                    KPI.Fe_Ofic = Mtxb_Oficializacion.Text;
                    KPI.Fe_DocOriginal = Mtxb_DocOriginal.Text;
                    KPI.Fe_RetiroCarga = Mtxb_RetiroCarga.Text;
                    KPI.TotDiasHabiles = Txb_DiasHabiles.Text;
                    KPI.Resultado= Cmb_Resultado.Text;
                    KPI.TipoDesvio = Cmb_Desvio.Text;
                    KPI.Motivo = Txb_Motivo.Text;
                    break;
            }
        }
        private void generarPDF() 
        {
            KPI.ObtenerDatosPDF();
            CServ_CrearPDF.ImgOapce = Properties.Resources.LogoOapce;
            CServ_CrearPDF.ImgSedex = Properties.Resources.LogoSedex;
            CServ_CrearPDF.GenerarPDF(1);
        }
       }
}
