using System.Text;

namespace DailyHelpers
{
    /// <summary>
    /// Clase de utilidad para realizar operaciones de codificación y decodificación en Base64.
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        /// Decodifica una cadena codificada en Base64 de vuelta a su forma original en UTF-8.
        /// </summary>
        /// <param name="base64String">La cadena codificada en Base64 que se va a decodificar.</param>
        /// <returns>La cadena decodificada en UTF-8.</returns>
        public static string DecodeBase64String(string base64String)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
        }

        /// <summary>
        /// Codifica un valor en Base64.
        /// </summary>
        /// <typeparam name="T">El tipo de valor que se va a codificar. Debe ser convertible a una cadena.</typeparam>
        /// <param name="value">El valor que se va a codificar en Base64.</param>
        /// <returns>La representación en Base64 del valor proporcionado.</returns>
        public static string EncodeToBase64<T>(T value)
        {
            string stringValue = value is string ? (string)(object)value : value.ToString();
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringValue));
        }
    }
}
