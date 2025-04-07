using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public static class CM_DatosKPI_PDF
    {
        public static int ID_KPI { get; set; }
        public static DateTime Fe_KPI { get; set; }
        public static string Cliente { get; set; }
        public static string Interno { get; set; }
        public static string Via { get; set; }
        public static string Canal { get; set; }
        public static string N_Despacho { get; set; }

        public static DateTime Fe_Arribo { get; set; }
        public static DateTime? Fe_CierreIngreso { get; set; }
        public static DateTime? Fe_FondosAduana { get; set; }
        public static DateTime Fe_DocOriginal { get; set; }
        public static DateTime Fe_Oficializacion { get; set; }
        public static DateTime Fe_RetiroCarga { get; set; }


        public static int TotalDiasHabiles { get; set; }
        public static string Resultado { get; set; }
        public static string TipoDesvio { get; set; }
        public static string MotivoDesvio { get; set; }
        public static string DepositoGiro { get; set; }
    }
}
