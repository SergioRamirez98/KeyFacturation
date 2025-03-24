using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class CL_Calendario
    {
        #region Properties

        private HashSet<DateTime> feriados = new HashSet<DateTime>();
        public DateTime FeIngreso { get; set; }
        public DateTime FeSalida { get; set; }

        #endregion

        public CL_Calendario()
        {
            feriados.Add(new DateTime(2025, 1, 1)); // Año nuevo
            feriados.Add(new DateTime(2025, 3, 3)); // Carnaval
            feriados.Add(new DateTime(2025, 3, 4)); // Carnaval
            feriados.Add(new DateTime(2025, 3, 24)); // Día de la Memoria

            feriados.Add(new DateTime(2025, 4, 2)); // Malvinas
            feriados.Add(new DateTime(2025, 4, 4)); // Viernes santo


            feriados.Add(new DateTime(2025, 5, 1));  // Día del Trabajador
            feriados.Add(new DateTime(2025, 5, 2));  // Día del Trabajador x2
            feriados.Add(new DateTime(2025, 5, 25));  // Junta de gobierno

            feriados.Add(new DateTime(2025, 6, 16));  // Guemes
            feriados.Add(new DateTime(2025, 6, 20));  // Belgrano

            feriados.Add(new DateTime(2025, 7, 9));  // Día de la independencia

            feriados.Add(new DateTime(2025, 8, 15));  // José de San Martin
            feriados.Add(new DateTime(2025, 8, 17));  // José de San Martin

            feriados.Add(new DateTime(2025, 10, 12));  // Día de la diversidad cultural

            feriados.Add(new DateTime(2025, 11, 21));  // Día de la soberania

            feriados.Add(new DateTime(2025, 12, 8));  // Día de la virgen
            feriados.Add(new DateTime(2025, 12, 25));  // Navidad
        }
        public void AgregarFeriado(DateTime fecha)
        {
            feriados.Add(fecha);
        }
        public bool EsDiaHabil(DateTime fecha)
        {
            return fecha.DayOfWeek != DayOfWeek.Saturday &&
                   fecha.DayOfWeek != DayOfWeek.Sunday &&
                   !feriados.Contains(fecha);
        }

        public int CalcularDiasHabiles() 
        {
            int contador = 0;
            DateTime fechaActual = FeIngreso.AddDays(1);

            while (fechaActual <= FeSalida)
            {
                if (EsDiaHabil(fechaActual)) 
                {
                    contador++;
                }
                fechaActual = fechaActual.AddDays(1);
            }

            return contador;
        }
    }
}
