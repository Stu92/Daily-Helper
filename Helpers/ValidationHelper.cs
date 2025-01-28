using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DailyHelpers
{
    /// <summary>
    /// Clase de utilidad para realizar validaciones comunes.
    /// </summary>
    public class ValidationHelper
    {
        /// <summary>
        /// Comprueba si un valor está contenido en una lista de valores especificada.
        /// </summary>
        /// <typeparam name="T">El tipo de dato de los valores.</typeparam>
        /// <param name="value">El valor que se va a buscar.</param>
        /// <param name="values">Los valores en los que se va a buscar.</param>
        /// <returns>True si el valor está contenido en la lista de valores, False en caso contrario.</returns>
        public static bool ContainsValue<T>(T value, params T[] values)
        {
            HashSet<T> valueSet = new HashSet<T>(values);
            return valueSet.Contains(value);
        }




        /// <summary>
        /// Genera una representación HTML de los errores de validación contenidos en el objeto ModelStateDictionary.
        /// </summary>
        /// <param name="modelState">El objeto ModelStateDictionary que contiene los errores de validación.</param>
        /// <returns>Una cadena HTML que representa los errores de validación.</returns>
        public static string GetValidationErrorHtml(ModelStateDictionary modelState)
        {
            // Obtener los errores de validación del objeto ModelStateDictionary
            var errores = modelState
                .Where(x => x.Value.Errors.Any()) // Filtrar solo las entradas con errores
                .Select(x =>
                {
                    // Obtener el nombre del campo con error y el mensaje de error
                    var fieldName = x.Key;
                    var errorMessage = x.Value.Errors.First().ErrorMessage;

                    // Construir un elemento HTML para mostrar el nombre del campo y el mensaje de error
                    return $@"<a href='#' class='list-group-item list-group-item-action' aria-current='true' style='font-size: 13px;'>
                        <div class='d-flex w-100 justify-content-between'>
                            <h5 class='mb-1'>{fieldName}</h5>
                        </div>
                        <p class='mb-1'>{errorMessage}</p>
                        <small style='color: red;'>Error de validación de campo.</small>
                    </a>";
                });

            // Concatenar todos los elementos HTML de error en una sola cadena
            var erroresConcatenados = string.Join("\n", errores);

            // Construir un contenedor HTML para los errores de validación
            return $"<div class='list-group' style='text-align: left;'>{erroresConcatenados}</div>";
        }




        /// <summary>
        /// Genera una representación de texto plano de los errores de validación contenidos en el objeto ModelStateDictionary.
        /// </summary>
        /// <param name="modelState">El objeto ModelStateDictionary que contiene los errores de validación.</param>
        /// <returns>Una cadena de texto que representa los errores de validación.</returns>
        public static string GetValidationErrorText(ModelStateDictionary modelState)
        {
            // Obtener los errores de validación del objeto ModelStateDictionary
            var errores = modelState
                .Where(x => x.Value.Errors.Any()) // Filtrar solo las entradas con errores
                .Select(x =>
                {
                    // Obtener el nombre del campo con error y el mensaje de error
                    var fieldName = x.Key;
                    var errorMessage = x.Value.Errors.First().ErrorMessage;

                    // Construir una cadena de texto para mostrar el nombre del campo y el mensaje de error
                    return $"{fieldName}: {errorMessage}";
                });

            // Concatenar todos los errores de validación en una sola cadena, separados por saltos de línea
            return string.Join("\n", errores);
        }
    }
}
