using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public abstract class CD_ConectarBDD
    {
        protected string VariableParaConectar = "data source=(local)\\SQLEXPRESS; initial catalog=Multitrans; integrated security=True;";



        public SqlConnection conexion;
        public CD_ConectarBDD()
        {
            try
            {
                conexion = new SqlConnection(VariableParaConectar);
                conexion.Open();

            }
            catch (Exception)
            {
                throw new Exception("Error al conectar con la base de datos.");
            }
        }
    }
}
