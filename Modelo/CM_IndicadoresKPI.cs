using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CM_IndicadoresKPI
    {
        #region Properties
        public string Via { get; set; }
        public int NoSatisfactorio { get; set; }
        public int Requerido{ get; set; }
        public int Excelente { get; set; }
        #endregion
    }
}
