using System.Globalization;

namespace DailyHelpers.Date
{
    /// <summary>
    /// Clase de utilidad para realizar operaciones relacionadas con fechas.
    /// </summary>
    public class DateHelper
    {
        /// <summary>
        /// Convierte una cadena en un objeto DateTime opcional, utilizando varios formatos de fecha predefinidos.
        /// </summary>
        /// <param name="fecha">La cadena que se va a convertir en una fecha.</param>
        /// <param name="formatoAdicional">Un formato de fecha adicional opcional que se puede utilizar para la conversión. Si se proporciona, se intentará utilizar este formato en lugar de los formatos predefinidos.</param>
        /// <returns>Un objeto DateTime opcional que contiene la fecha convertida si la conversión es exitosa; de lo contrario, devuelve null.</returns>
        public static DateTime? ConvertDate(string fecha, string formatoAdicional = null)
        {
            // Comprueba si la cadena es nula o vacía
            if (string.IsNullOrEmpty(fecha))
            {
                return null; // Si la cadena es nula o vacía, devuelve null
            }

            // Array de formatos de fecha predefinidos
            string[] formatosFecha = { "dd/MM/yyyy", "yyyy-MM-dd", "MM/dd/yyyy", "yyyy/MM/dd", "dd-MM-yyyy", "yyyy.MM.dd" };

            // Si se proporciona un formato adicional, se utiliza solo ese formato
            if (!string.IsNullOrEmpty(formatoAdicional))
            {
                formatosFecha = new string[] { formatoAdicional };
            }

            // Itera sobre los formatos de fecha y trata de convertir la cadena en un objeto DateTime
            foreach (string formatoFecha in formatosFecha)
            {
                if (DateTime.TryParseExact(fecha, formatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaConvertida))
                {
                    return fechaConvertida; // Devuelve la fecha convertida si la conversión es exitosa
                }
            }

            // Devuelve null si no se puede convertir la cadena a fecha con ninguno de los formatos disponibles
            return null;
        }


        /// <summary>
        /// Combina una fecha con una hora para formar un nuevo objeto DateTime.
        /// </summary>
        /// <param name="fecha">La fecha a combinar.</param>
        /// <param name="hora">La hora a combinar.</param>
        /// <returns>Un nuevo objeto DateTime que combina la fecha y la hora.</returns>
        public static DateTime CombineDateAndTime(DateTime fecha, TimeSpan hora)
        {
            return fecha.Date.AddHours(hora.Hours).AddMinutes(hora.Minutes);
        }


        /// <summary>
        /// Analiza una cadena de fecha en un formato específico y la convierte a otro formato.
        /// </summary>
        /// <param name="dateString">La cadena de fecha a analizar.</param>
        /// <param name="inputFormat">El formato esperado de la cadena de fecha de entrada.</param>
        /// <param name="outputFormat">El formato deseado para la cadena de fecha de salida.</param>
        /// <returns>
        /// Una cadena con la fecha formateada en el formato de salida especificado.
        /// Si la cadena de fecha de entrada es nula o está vacía, devuelve un mensaje indicando esto.
        /// Si la cadena de fecha no se puede analizar, devuelve un mensaje de error indicando el formato incorrecto.
        /// </returns>
        public static string ParseAndFormatDate(string dateString, string outputFormat, string inputFormat = null)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            string[] formatosFecha = { "dd/MM/yyyy", "yyyy-MM-dd", "MM/dd/yyyy", "yyyy/MM/dd", "dd-MM-yyyy", "yyyy.MM.dd" };
            if (!string.IsNullOrEmpty(inputFormat))
            {
                formatosFecha = new string[] { inputFormat };
            }


            foreach (string formatoFecha in formatosFecha)
            {
                DateTime parsedDate;
                bool isParsed = DateTime.TryParseExact(dateString, formatoFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

                if (isParsed)
                {
                    return parsedDate.ToString(outputFormat);
                }
            }

            return null;
        }


        /// <summary>
        /// Obtiene el último día del mes especificado con la hora ajustada al final del día.
        /// </summary>
        /// <param name="mes">El mes (entre 1 y 12).</param>
        /// <param name="año">El año (mayor que 0).</param>
        /// <returns>El último día del mes especificado con la hora ajustada al final del día.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Si el mes no está entre 1 y 12 o si el año es menor que 1.</exception>
        public static DateTime ObtenerUltimoDiaDelMes(int mes, int año)
        {
            if (mes < 1 || mes > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(mes), "El mes debe estar entre 1 y 12.");
            }
            if (año < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(año), "El año debe ser mayor que 0.");
            }

            DateTime primerDiaDelMes = new DateTime(año, mes, 1, CultureInfo.InvariantCulture.Calendar);
            DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);
            ultimoDiaDelMes = ultimoDiaDelMes.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

            return ultimoDiaDelMes;
        }


    }
}
