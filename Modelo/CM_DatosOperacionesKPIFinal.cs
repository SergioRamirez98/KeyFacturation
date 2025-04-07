using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public  class CM_DatosOperacionesKPIFinal
    {
        public  int ID_KPI { get; set; }
        public  DateTime Fe_KPI { get; set; }
        public  string Cliente { get; set; }
        public  string Interno { get; set; }
        public  string Via { get; set; }
        public string Canal { get; set; }
        public  string N_Despacho { get; set; }

        public  DateTime Fe_Arribo { get; set; }
        public DateTime? Fe_CierreIngreso { get; set; }
        public DateTime? Fe_FondosAduana { get; set; }
        public  DateTime Fe_DocOriginal { get; set; }
        public  DateTime Fe_Oficializacion { get; set; }
        public  DateTime Fe_RetiroCarga { get; set; }


        public  int TotalDiasHabiles { get; set; }
        public  string Resultado { get; set; }
        public  string TipoDesvio { get; set; }
        public  string MotivoDesvio { get; set; }
        public  string DepositoGiro { get; set; }

    }
}
