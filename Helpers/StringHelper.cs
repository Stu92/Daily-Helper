using System.Globalization;

namespace DailyHelpers.String
{
    /// <summary>
    /// Proporciona métodos estáticos para manipular y transformar cadenas de texto de manera eficiente.
    /// Esta clase incluye operaciones como comprobación de valores, conversión a mayúsculas, eliminación de caracteres,
    /// normalización de texto, entre otros.
    /// </summary>
    public class StringHelper
    {

        /// <summary>
        /// Comprueba si una cadena es equivalente a "si", "true", "ok" o "on" (ignorando mayúsculas y minúsculas).
        /// </summary>
        /// <param name="value">La cadena que se va a comprobar. Si se proporciona null, se devuelve false.</param>
        /// <returns>True si la cadena es equivalente a una de las opciones permitidas, False en caso contrario.</returns>
        public static bool CheckString(string value = null)
        {
            string[] allowedValues = { "si", "true", "ok", "on" };
            return allowedValues.Contains(value?.ToLower());
        }

        /// <summary>
        /// Convierte una cadena en mayúsculas y devuelve null si la cadena es nula, vacía o contiene solo espacios en blanco.
        /// </summary>
        /// <param name="value">La cadena que se va a convertir.</param>
        /// <returns>La cadena convertida a mayúsculas o null si la cadena original es nula, vacía o contiene solo espacios en blanco.</returns>
        public static string ToNullIfEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim().ToUpper();
        }

        /// <summary>
        /// Convierte una cadena en un objeto Guid opcional.
        /// </summary>
        /// <param name="idString">La cadena que se va a convertir en un objeto Guid.</param>
        /// <returns>Un objeto Guid opcional que contiene el valor convertido si la conversión es exitosa; de lo contrario, devuelve null.</returns>
        public static Guid? ConvertToGuid(string idString)
        {
            return Guid.TryParse(idString, out Guid id) ? id : (Guid?)null;
        }


        /// <summary>
        /// Reemplaza todas las ocurrencias de un carácter específico en una cadena con otro carácter.
        /// </summary>
        /// <param name="originalText">La cadena de texto original.</param>
        /// <param name="oldString">El carácter que se desea reemplazar.</param>
        /// <param name="newString">
        /// El carácter con el que se reemplazará el carácter antiguo. 
        /// Si no se proporciona, se eliminará el carácter antiguo de la cadena.
        /// </param>
        /// <returns>
        /// Una nueva cadena con el carácter especificado reemplazado por el nuevo carácter.
        /// Si la cadena original es nula o está vacía, se devolverá null.
        /// </returns>
        public static string RemoveCharacter(string originalText, string oldString, string newString = "")
        {
            return string.IsNullOrWhiteSpace(originalText) ? null : originalText.Replace(oldString, newString).Trim().ToUpper();
        }


        /// <summary>
        /// Obtiene los primeros N caracteres de una cadena de texto.
        /// </summary>
        /// <param name="text">La cadena de texto de la cual se extraerán los caracteres.</param>
        /// <param name="numberOfCharacters">El número de caracteres a extraer.</param>
        /// <returns>
        /// Una nueva cadena que contiene los primeros N caracteres de la cadena original. 
        /// Si la cadena original tiene menos de N caracteres, se devolverá la cadena completa.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Lanzada cuando el texto es nulo o vacío, o cuando el número de caracteres es menor que 0.
        /// </exception>
        public static string GetFirstNCharacters(string text, int numberOfCharacters)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("El texto no puede ser nulo o vacío. Método GetFirstNCharacters", nameof(text));
            }
            if (numberOfCharacters < 0)
            {
                throw new ArgumentException("El número de caracteres debe ser mayor o igual a 0. Método GetFirstNCharacters", nameof(numberOfCharacters));
            }
            return text.Length >= numberOfCharacters ? text.Substring(0, numberOfCharacters) : text;
        }

        /// <summary>
        /// Retorna un valor basado en una condición booleana.
        /// </summary>
        /// <typeparam name="T">El tipo de dato del valor que se retornará.</typeparam>
        /// <param name="condition">La condición booleana que determina qué valor se retorna.</param>
        /// <param name="trueValue">El valor a retornar si la condición es verdadera.</param>
        /// <param name="falseValue">El valor a retornar si la condición es falsa.</param>
        /// <returns>El valor especificado según la condición booleana.</returns>
        public static T StatusValue<T>(bool condition, T trueValue, T falseValue)
        {
            return condition ? trueValue : falseValue;
        }

        /// <summary>
        /// Normaliza un texto eliminando acentos y caracteres diacríticos, además de convertirlo a mayúsculas.
        /// </summary>
        /// <param name="value">Texto a normalizar.</param>
        /// <returns>El texto normalizado en mayúsculas o <c>null</c> si el texto es nulo o vacío.</returns>
        public static string? NormalizeValue(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null; // Manejo directo para valores nulos o solo espacios.

            // Normalización Unicode para eliminar caracteres diacríticos.
            var normalizedValue = string.Concat(
                value.Normalize(System.Text.NormalizationForm.FormD) // Forma de descomposición Unicode.
                     .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark) // Filtrar caracteres de espaciado.
            );

            // Convertir a forma compuesta y mayúsculas para estandarizar el texto.
            return normalizedValue.Normalize(System.Text.NormalizationForm.FormC).ToUpperInvariant();
        }


    }

}
