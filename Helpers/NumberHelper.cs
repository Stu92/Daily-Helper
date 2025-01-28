using System.Globalization;

namespace DailyHelpers
{
    /// <summary>
    /// Clase de utilidad para realizar operaciones relacionadas con números.
    /// </summary>
    public class NumberHelper
    {
        /// <summary>
        /// Formatea un número con separación de miles utilizando comas y separación de decimales utilizando un punto.
        /// </summary>
        /// <param name="number">El número a formatear. Si es nulo, se asume un valor de 0.</param>
        /// <param name="decimals">El número de decimales a mostrar.</param>
        /// <returns>El número formateado como una cadena.</returns>
        public static string FormatNumber(decimal? number, int decimals = 2)
        {
            // Si el número es nulo, asignar un valor de 0
            decimal valueToFormat = number ?? 0;

            // Define una cultura invariante con el separador de miles como coma y el separador decimal como punto.
            var cultureInfo = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            cultureInfo.NumberFormat.NumberGroupSeparator = ",";
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            // Formatea el número con el número especificado de decimales.
            string formatString = "N" + decimals;
            return valueToFormat.ToString(formatString, cultureInfo);
        }

        /// <summary>
        /// Redondea un número de tipo double a su entero más cercano.
        /// </summary>
        /// <param name="value">El número double a redondear.</param>
        /// <returns>El valor redondeado al entero más cercano.</returns>
        public static int RoundToNearestInteger(double? number)
        {
            double valueToFormat = number ?? 0;
            return (int)Math.Round(valueToFormat, MidpointRounding.AwayFromZero);
        }
    }
}
