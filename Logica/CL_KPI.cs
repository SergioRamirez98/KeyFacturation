using Datos;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logica
{
    public class CL_KPI
    {
        #region Properties


        public List<CM_DatosOperacionesExcel> DatosExcel { get; set; } = new List<CM_DatosOperacionesExcel>();  
        public List<CM_DatosOperacionesKPIFinal> DatosBDD { get; set; } = new List<CM_DatosOperacionesKPIFinal>();
        CD_KPI KPI = new CD_KPI();
        CL_Calendario Calendario = new CL_Calendario();



        public DateTime FeIngreso { get; set; }
        public DateTime FeSalida { get; set; }
        public List<CM_IndicadoresKPI> IndKPI { get; set; } = new List<CM_IndicadoresKPI>();

        public int ID_KPI { get; set; }
        public string Fe_KPI { get; set; }
        public string Cliente { get; set; }
        public string Interno { get; set; }
        public string Via { get; set; }
        public string Canal { get; set; }
        public string Despacho { get; set; }
        public string Giro { get; set; }
        public string Fe_Arribo { get; set; }
        public string Fe_CID { get; set; }
        public string Fe_Fondos { get; set; }
        public string Fe_Ofic { get; set; }
        public string Fe_DocOriginal { get; set; }
        public string Fe_RetiroCarga { get; set; }
        public string TotDiasHabiles { get; set; }
        public string Resultado { get; set; }
        public string TipoDesvio { get; set; }
        public string Motivo { get; set; }


        #endregion
        public List<CM_DatosOperacionesExcel> BuscarInterno()
        {
            DatosExcel.Clear();
            convertirDatos(KPI.CargarOperacion());
            return DatosExcel;
        }

        public List<CM_DatosOperacionesExcel> OrdenarColumnas(int valor, List<CM_DatosOperacionesExcel> ListaOperaciones)
        {
            if (ListaOperaciones != null)
            {
                IOrderedEnumerable<CM_DatosOperacionesExcel> listaOrdenada;

                switch (valor)
                {
                    case 0: 
                        listaOrdenada = ListaOperaciones.OrderBy(p => p.Cliente);
                        break;
                    case 1: 
                        listaOrdenada = ListaOperaciones.OrderBy(p => p.Interno);
                        break;
                    case 2: 
                        listaOrdenada = ListaOperaciones.OrderBy(p => p.Destinacion);
                        break;
                    case 3: 
                        listaOrdenada = ListaOperaciones.OrderBy(p => ParseFecha(p.FechaArribo));
                        break;
                    case 4: 
                        listaOrdenada = ListaOperaciones.OrderBy(p => ParseFecha(p.Oficializacion));
                        break;
                    case 5: 
                        listaOrdenada = ListaOperaciones.OrderBy(p => ParseFecha(p.Salida));
                        break;
                    case 6: 
                        listaOrdenada = ListaOperaciones.OrderBy(p => ParseFecha(p.Fondos));
                        break;
                    case 7: 
                        listaOrdenada = ListaOperaciones.OrderBy(p => p.Giro);
                        break;
                    case 8: 
                        listaOrdenada = ListaOperaciones.OrderBy(p => p.Canal);
                        break;
                    default:
                        listaOrdenada = ListaOperaciones.OrderBy(p => p.Cliente);
                        break;
                }

                return listaOrdenada.ToList();
            }
            else return ListaOperaciones;
            
        }
        private DateTime ParseFecha(string fecha)
        {
            DateTime fechaParseada;
            try
            {
                fechaParseada = Convert.ToDateTime(fecha);
                return fechaParseada;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public List<CM_IndicadoresKPI> ObtenerValoresdeIndicadores()
        {
            IndKPI.Clear();
            IndKPI = KPI.ObtenerDatos();
            return IndKPI;
        }
        public List<CM_DatosOperacionesKPIFinal> ObtenerInformeKPI() 
        {
            DatosBDD.Clear();
            DatosBDD = KPI.ObtenerInforme();
            return DatosBDD;
        }
        public int CalcularResultado()
        {
            validarFechas();
            pasarDatos(1);
            return Calendario.CalcularDiasHabiles();
        }
        public bool BuscarKPI()
        {
            bool resultado=false;
            pasarDatos(2);
            validarDatos();
            resultado = KPI.ConsultarKPI();
            return resultado;
        }
        public void GuardarEnBDD() 
        {            
            pasarDatos(2);
            validarDatos();
            ID_KPI= KPI.InsertarEnBDD();
        }
        public void ObtenerDatosPDF() 
        {
            KPI.ObtenerKPI(ID_KPI);
        }
        private void convertirDatos(List<CM_DatosOperacionesExcel> DatosOperaciones) 
        {
            obtenerFechaReciente(DatosOperaciones);
            foreach (var item in DatosOperaciones)
            {
                if (!string.IsNullOrEmpty(item.FechaArribo)|| !string.IsNullOrEmpty(item.Salida))
                {
                    DateTime fSalida = DateTime.TryParse(item.Salida, out DateTime resultSalida) ? resultSalida : DateTime.MinValue;
                    DateTime fArribo = DateTime.TryParse(item.FechaArribo, out DateTime resultArribo) ? resultArribo : DateTime.MinValue;
                }

            }
            DatosExcel=DatosOperaciones;
        }
        private void obtenerFechaReciente(List<CM_DatosOperacionesExcel> DatosOperaciones) 
        {
            foreach (var fechaDeposito in DatosOperaciones)
            {
                if (!string.IsNullOrWhiteSpace(fechaDeposito.Fondos))
                {
                    var fechaMasReciente = fechaDeposito.Fondos
                        .Split('|') // Divide las fechas separadas por '|'
                        .Select(fecha => DateTime.TryParse(fecha, out DateTime result) ? result : DateTime.MinValue) // f es el objeto, => es acción. entonces dice:
                                                                                                             // al objeto f lo vamos a convertir en datatime (f tiene que devolver como salida
                                                                                                             // un datatime) 
                                                                                                             // '? result : DateTime.MinValue' Operador ternario, es un IF corto
                                                                                                             // condición ? valor_si_verdadero : valor_si_falso;


                        .Max(); // Obtiene la fecha más reciente

                    fechaDeposito.Fondos = fechaMasReciente == DateTime.MinValue ? "" : fechaMasReciente.ToString("dd/MM/yyyy"); // Guarda la más reciente
                    //indicamos que en fechaDeposito.Deposito va a ir el resultado de lo que hicimos arriba, si el resultado de lo que hicimos arriba da un datetime valor minimo
                    //va a poner un string vacio, sino pone la fecha mas reciente
                }
            }
        }
        private void validarFechas() 
        {
            if (FeSalida<FeIngreso) 
            {
                throw new Exception("las fechas de salida no puede ser anterior a la fecha de ingreso.");
            }
        }
        private void validarDatos()
        {
            try
            {
                var validarFechas = new List<(string Nombre, DateTime? Fecha)>
                {
                ("Ingreso de mercadería", KPI.Fe_Arribo),
                ("Cierre de ingreso a Terminal", KPI.Fe_CID),
                ("Fondos Aduana", KPI.Fe_Fondos),
                ("Oficialización", KPI.Fe_Ofic),
                ("Documentación original", KPI.Fe_DocOriginal)
                };
                foreach (var fecha in validarFechas)
                {
                    if (fecha.Fecha > KPI.Fe_RetiroCarga)
                    {
                        throw new Exception($"La fecha de \"{fecha.Nombre}\" no puede ser posterior a la fecha de retiro de carga.");
                    }
                }
                if (KPI.Resultado=="NO SATISFACTORIO" && KPI.TipoDesvio=="N/A")
                {
                    throw new Exception("El resultado no fue satisfactorio, debe indicar el tipo de desvío");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void pasarDatos(int valor) 
        {
            switch (valor)
            {
                case 1:
                    Calendario.FeIngreso = FeIngreso;
                    Calendario.FeSalida = FeSalida;
                    break;
                case 2:
                    try
                    {
                        var datosRequeridos = new List<object> { Cliente, Interno, Via, Canal, Despacho, Giro, TotDiasHabiles, Resultado, TipoDesvio};

                        if (datosRequeridos.Any(dato => dato == null || string.IsNullOrWhiteSpace(dato.ToString())))
                        {
                            throw new Exception("Uno o más campos requeridos están vacíos.");
                        }
                        else if (TipoDesvio != "N/A" && String.IsNullOrEmpty(Motivo)) { throw new Exception("Por favor indique el motivo."); }
                        else
                        {

                            KPI.Fe_KPI = Convert.ToDateTime(Fe_KPI);
                            KPI.Cliente = Cliente;
                            KPI.Interno = Interno;
                            KPI.Via = Via;
                            KPI.Canal = Canal;
                            KPI.Despacho = Despacho;
                            KPI.Giro = Giro;
                            if (Fe_CID != "  /  /") 
                            { 
                                KPI.Fe_CID = Convert.ToDateTime(Fe_CID);
                            }

                            if (Fe_Fondos != "  /  /") 
                            {
                                KPI.Fe_Fondos = Convert.ToDateTime(Fe_Fondos); 
                            }


                            if (Fe_Arribo != "  /  /" && Fe_Ofic != "  /  /" && Fe_DocOriginal != "  /  /" && Fe_RetiroCarga != "  /  /")
                            {
                                KPI.Fe_Arribo = Convert.ToDateTime(Fe_Arribo);
                                KPI.Fe_Ofic = Convert.ToDateTime(Fe_Ofic);
                                KPI.Fe_RetiroCarga = Convert.ToDateTime(Fe_RetiroCarga);
                                KPI.Fe_DocOriginal = Convert.ToDateTime(Fe_DocOriginal);

                            }
                            else { throw new Exception("Las fechas de arribo, oficializacion, documentación original y retiro de carga no pueden estar vacias."); }
                            KPI.TotDiasHabiles = Convert.ToInt32(TotDiasHabiles);
                            KPI.Resultado = Resultado;
                            KPI.TipoDesvio = TipoDesvio;
                            KPI.Motivo = Motivo;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;               
            }
        }
        public List<CM_DatosOperacionesExcel> filtrarOperaciones(string Dato) 
        {
            List<CM_DatosOperacionesExcel> DatosFiltrados = new List<CM_DatosOperacionesExcel>();
            DatosFiltrados = DatosExcel;
            if (!string.IsNullOrEmpty(Dato))
            {

                        DatosFiltrados = DatosFiltrados.Where
                        (dato =>

                        dato.Cliente.IndexOf(Dato, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        dato.Interno.IndexOf(Dato, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        dato.Destinacion.IndexOf(Dato, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        dato.FechaArribo.IndexOf(Dato, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        dato.Oficializacion.IndexOf(Dato, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        dato.Salida.IndexOf(Dato, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        dato.Fondos.IndexOf(Dato, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        dato.Giro.IndexOf(Dato, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        dato.Canal.IndexOf(Dato, StringComparison.OrdinalIgnoreCase) >= 0
                        
                        ).ToList();
            }

            return DatosFiltrados;
        }

        public List<CM_DatosOperacionesKPIFinal> FiltrarKpis(string Datos) 
        {
            List<CM_DatosOperacionesKPIFinal> DatosFiltrados = new List<CM_DatosOperacionesKPIFinal>();
            DatosFiltrados = DatosBDD;
            if (!string.IsNullOrEmpty(Datos))
            {

                DatosFiltrados = DatosFiltrados.Where
                (dato =>
                dato.ID_KPI.ToString().IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Fe_KPI.ToString().IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Cliente.IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Interno.IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Via.IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Canal.IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.N_Despacho.IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||

                dato.Fe_Arribo.ToString().IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Fe_CierreIngreso.ToString().IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Fe_FondosAduana.ToString().IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Fe_DocOriginal.ToString().IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Fe_Oficializacion.ToString().IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Fe_RetiroCarga.ToString().IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.TotalDiasHabiles.ToString().IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.Resultado.IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.TipoDesvio.IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.MotivoDesvio.IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0 ||
                dato.DepositoGiro.IndexOf(Datos, StringComparison.OrdinalIgnoreCase) >= 0


                ).ToList();
            }

            return DatosFiltrados;
        }
    }
}
