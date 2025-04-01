using System;
using System.Collections.Generic;
using Modelo;

using System.IO;
using ExcelDataReader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text.RegularExpressions;

namespace Datos
{
    public class CD_KPI: CD_EjecutarSP
    {
        #region Properties
        public List<CM_DatosOperacionesExcel> DatosExcel { get; set; } = new List<CM_DatosOperacionesExcel>();
        public List<CM_IndicadoresKPI> IndKPI { get; set; } = new List<CM_IndicadoresKPI>();
        public List<CM_DatosOperacionesKPIFinal> DatosBDD { get; set; } = new List<CM_DatosOperacionesKPIFinal>();


        SqlParameter[] lista = null;

        public DateTime Fe_KPI { get; set; }
        public string Cliente { get; set; }
        public string Interno { get; set; }
        public string Via { get; set; }
        public string Canal { get; set; }
        public string Despacho { get; set; }
        public string Giro { get; set; }
        public DateTime Fe_Arribo { get; set; }
        public DateTime? Fe_CID { get; set; } //El signo de interrogación indica que se puede insertar un valor NULO
        public DateTime? Fe_Fondos { get; set; }//El signo de interrogación indica que se puede insertar un valor NULO
        public DateTime Fe_Ofic { get; set; }
        public DateTime Fe_DocOriginal { get; set; }
        public DateTime Fe_RetiroCarga { get; set; }
        public int TotDiasHabiles { get; set; }
        public string Resultado { get; set; }
        public string TipoDesvio { get; set; }
        public string Motivo { get; set; }


        #endregion

        public List<CM_IndicadoresKPI> ObtenerDatos() 
        {
            DataTable dt = new DataTable();
            try
            {
                string sSql = "SP_ObtenerIndKPI";
                List<SqlParameter> listaparametros = new List<SqlParameter>();
                SqlParameter[] parametros = listaparametros.ToArray();

                dt = ejecutar(sSql, parametros, true);

            }
            catch (Exception)
            {
                throw new Exception("No se ha podido realizar la operación. Error CD_KPI||ObtenerDatos");
            }
            if (dt.Rows.Count > 0)
            {
                cargarIndKPI(dt);
            }
            return IndKPI;
        }
        public List<CM_DatosOperacionesExcel> CargarOperacion()
        {
            DatosExcel.Clear();

            string DireccionDelExcel = @"C:\Users\ramir\OneDrive\Documentos\kpi.xlsx"; //La dirección del archivo

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            /*ExcelDataReader usa codificaciones especiales para leer archivos de Excel en formato .xls y .xlsx.  Algunos sistemas pueden no tener habilitada esta codificación, 
             * por lo que necesitamos registrarla manualmente.
             * Si omites esta línea, es posible que obtengas un error de codificación al leer el archivo.*/

            try
            {

                using (var stream = File.Open(DireccionDelExcel, FileMode.Open, FileAccess.Read)) //Abre el archivo 
                using (var reader = ExcelReaderFactory.CreateReader(stream)) //lo lee
                {

                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()  // utilizo un data set porque convierte todas las hojas del excel,
                                                                                   // en cambio, el dt.Load toma un formato de tipo SQL
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // Lo hago de esta manera para hacer que la primera fila sea el nombre de las columnas.
                        }
                    });

                    DataTable dt = result.Tables[0]; // Primera hoja del Excel

                    foreach (DataRow dr in dt.Rows)
                    {

                        CM_DatosOperacionesExcel DatosSeleccionados = new CM_DatosOperacionesExcel
                        {
                            Interno = Convert.ToString(dr["INTERNO"]),
                            FechaArribo = Convert.ToString(dr["Fecha de arribo"]),
                            Destinacion = Convert.ToString(dr["Destinación"]),
                            Salida = Convert.ToString(dr["Fecha de Carga-Salida"]),
                            Fondos = Convert.ToString(dr["Fec. Depósito"]),
                            Cliente = Convert.ToString(dr["R. Social Import/Export"]),
                            Giro = Convert.ToString(dr["Depósito/Lugar de giro"]),
                            Oficializacion= Convert.ToString(dr["Fecha Oficialización"]),
                            Canal = Convert.ToString(dr["Canal"]),
                        };

                        DatosExcel.Add(DatosSeleccionados);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception (ex.Message);
            }
           
            return DatosExcel;

        }

        public List<CM_DatosOperacionesKPIFinal> ObtenerInforme() 
        {
            DatosBDD.Clear();
            DataTable dt = new DataTable();
            try
            {
                string sSql = "SP_Obtener_KPIs";
                List<SqlParameter> listaparametros = new List<SqlParameter>();
                SqlParameter[] parametros = listaparametros.ToArray();

                dt= ejecutar(sSql, parametros, true);

            }
            catch (Exception)
            {
                throw new Exception("No se ha podido realizar la operación. Error CD_KPI||ObtenerInforme");
            }
            if (dt.Rows.Count>0)
            {
                cargarCM_DatosBDD(dt);
            }
            return DatosBDD;
        }

        private void cargarCM_DatosBDD(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CM_DatosOperacionesKPIFinal Datos = new CM_DatosOperacionesKPIFinal
                    {
                        ID_KPI = Convert.ToInt32(dr["ID_KPI"]),

                        Fe_KPI = Convert.ToDateTime(dr["Fe_KPI"]),
                        Cliente = dr["Cliente"].ToString(),
                        Interno = dr["Interno"].ToString(),
                        Via = dr["Via"].ToString(),
                        Canal = dr["Canal"].ToString(),
                        N_Despacho = dr["N_Despacho"].ToString(),

                        Fe_Arribo = Convert.ToDateTime(dr["Fe_Arribo"]),
                        Fe_CierreIngreso = Convert.ToDateTime(dr["Fe_CierreIngreso"]),
                        Fe_FondosAduana = Convert.ToDateTime(dr["Fe_FondosAduana"]),
                        Fe_DocOriginal = Convert.ToDateTime(dr["Fe_DocOriginal"]),
                        Fe_Oficializacion = Convert.ToDateTime(dr["Fe_Oficializacion"]),
                        Fe_RetiroCarga = Convert.ToDateTime(dr["Fe_RetiroCarga"]),

                        TotalDiasHabiles = Convert.ToInt32(dr["TotalDiasHabiles"]),
                        Resultado = dr["Resultado"].ToString(),
                        TipoDesvio = dr["TipoDesvio"].ToString(),
                        MotivoDesvio = dr["MotivoDesvio"].ToString(),
                        DepositoGiro = dr["Deposito"].ToString(),
                    };

                    DatosBDD.Add(Datos);
                }

            }
        }
        private void cargarIndKPI(DataTable dt) 
        {
            foreach (DataRow dr in dt.Rows) 
            {
                CM_IndicadoresKPI kpi = new CM_IndicadoresKPI {
                    Via = dr["Via"].ToString(),
                    NoSatisfactorio = Convert.ToInt32(dr["NoSatisfactorio"]),
                    Requerido = Convert.ToInt32(dr["Requerido"]),
                    Excelente = Convert.ToInt32(dr["Excelente"]),
                };
                IndKPI.Add(kpi);
            }            
        }
        public void InsertarEnBDD() 
        {
            try
            {
                string sSql = "SP_InsertarKPI";
                SqlParameter param_Fe_KPI = new SqlParameter("@Fe_KPI", SqlDbType.DateTime);
                param_Fe_KPI.Value = Fe_KPI;
                SqlParameter param_Cliente = new SqlParameter("@Cliente", SqlDbType.NVarChar, 200);
                param_Cliente.Value = Cliente;
                SqlParameter param_Interno = new SqlParameter("@Interno", SqlDbType.NVarChar, 200);
                param_Interno.Value = Interno;
                SqlParameter param_Via = new SqlParameter("@Via", SqlDbType.NVarChar, 200);
                param_Via.Value = Via;
                SqlParameter param_Canal = new SqlParameter("@Canal", SqlDbType.NVarChar, 200);
                param_Canal.Value = Canal;
                SqlParameter param_Despacho = new SqlParameter("@N_Despacho", SqlDbType.NVarChar, 200);
                param_Despacho.Value = Despacho;
                SqlParameter param_Giro = new SqlParameter("@Deposito", SqlDbType.NVarChar, 200);
                param_Giro.Value = Giro;
                SqlParameter param_Fe_Arribo = new SqlParameter("@Fe_Arribo", SqlDbType.DateTime);
                param_Fe_Arribo.Value = Fe_Arribo;
                SqlParameter param_Fe_CID = new SqlParameter("@Fe_CierreIngreso", SqlDbType.DateTime);
                param_Fe_CID.Value = Fe_CID;
                SqlParameter param_Fe_Fondos = new SqlParameter("@Fe_FondosAduana", SqlDbType.DateTime);
                param_Fe_Fondos.Value = Fe_Fondos;
                SqlParameter param_Fe_Ofic = new SqlParameter("@Fe_Oficializacion", SqlDbType.DateTime);
                param_Fe_Ofic.Value = Fe_Ofic;
                SqlParameter param_Fe_DocOriginal = new SqlParameter("@Fe_DocOriginal", SqlDbType.DateTime);
                param_Fe_DocOriginal.Value = Fe_DocOriginal;
                SqlParameter param_Fe_RetiroCarga = new SqlParameter("@Fe_RetiroCarga", SqlDbType.DateTime);
                param_Fe_RetiroCarga.Value = Fe_RetiroCarga;
                SqlParameter param_TotDiasHabiles = new SqlParameter("@TotalDiasHabiles", SqlDbType.Int);
                param_TotDiasHabiles.Value = TotDiasHabiles;
                SqlParameter param_Resultado = new SqlParameter("@Resultado", SqlDbType.NVarChar, 200);
                param_Resultado.Value = Resultado;
                SqlParameter param_TipoDesvio = new SqlParameter("@TipoDesvio", SqlDbType.NVarChar, 200);
                param_TipoDesvio.Value = TipoDesvio;
                SqlParameter param_Motivo = new SqlParameter("@MotivoDesvio", SqlDbType.NVarChar, 200);
                param_Motivo.Value = Motivo;
                

                List<SqlParameter> listaParametros = new List<SqlParameter>();
                listaParametros.Add(param_Fe_KPI);
                listaParametros.Add(param_Cliente);
                listaParametros.Add(param_Interno);
                listaParametros.Add(param_Via);
                listaParametros.Add(param_Canal);
                listaParametros.Add(param_Despacho);
                listaParametros.Add(param_Giro);
                listaParametros.Add(param_Fe_Arribo); 
                listaParametros.Add(param_Fe_CID);
                listaParametros.Add(param_Fe_Fondos);
                listaParametros.Add(param_Fe_Ofic);
                listaParametros.Add(param_Fe_DocOriginal);
                listaParametros.Add(param_Fe_RetiroCarga);
                listaParametros.Add(param_TotDiasHabiles);
                listaParametros.Add(param_Resultado);
                listaParametros.Add(param_TipoDesvio);
                listaParametros.Add(param_Motivo);
                lista = listaParametros.ToArray();

                ejecutar(sSql, lista, false);
           
            }
            catch (Exception)
            {

                throw new Exception("No se ha podido realizar la operación. Error CD_KPI||InsertarEnBDD.");
            }
        }

    }
}
