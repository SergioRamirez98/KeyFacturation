using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CD_EjecutarSP : CD_ConectarBDD
    {
        public DataTable ejecutar(string sql, SqlParameter[] lista, bool RetDataTable = false)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conexion = new SqlConnection(VariableParaConectar))
            {
                conexion.Open();
                SqlCommand command = new SqlCommand(sql, conexion);
                command.CommandType = CommandType.StoredProcedure;
                if (lista != null)
                {
                    command.Parameters.AddRange(lista);
                }
                if (RetDataTable)
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
                else
                {
                    command.ExecuteNonQuery();
                }
            }

            return dataTable;
        }
    }
}
