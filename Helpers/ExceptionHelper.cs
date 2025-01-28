using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace DailyHelpers.Exception
{
    public class ExceptionHelper
    {
        /// <summary>
        /// Formatea los mensajes de error para ser enviados como respuesta HTTP y para ser registrados en los logs de la aplicación.
        /// </summary>
        /// <param name="ex">La excepción que se desea procesar.</param>
        /// <param name="endpoint">El endpoint de la API en el que ocurrió el error.</param>
        /// <returns>
        /// Devuelve una tupla que contiene:
        /// - Un <see cref="StringBuilder"/> con el mensaje de error para mostrar al usuario.
        /// - Un <see cref="StringBuilder"/> con el mensaje detallado para los logs, que incluye la traza de la pila y la excepción base (si existe).
        /// - Un <see cref="HttpStatusCode"/> correspondiente al tipo de excepción procesada.
        /// </returns>
        /// <remarks>
        /// Este método crea un mensaje de error general para el usuario, incluyendo detalles sobre el endpoint y el tipo de excepción,
        /// y un mensaje de log detallado que incluye la traza de la pila y cualquier excepción interna o base.
        /// Dependiendo del tipo de excepción, el método asigna un código de estado HTTP apropiado.
        /// </remarks>
        public static (StringBuilder ErrorMessage, StringBuilder LogErrorMessage, HttpStatusCode StatusCode) WebApiFormatErrorMessages(Exception ex, string endpoint)
        {
            // Inicialización de los mensajes de error
            var errorMessage = new StringBuilder();
            var logErrorMessage = new StringBuilder();

            // Información básica del error
            errorMessage.Append("Ocurrió un error inesperado.");
            errorMessage.Append($" Endpoint: {endpoint}.");
            errorMessage.Append($" Tipo de excepción: {ex.GetType().Name}.");

            // Detalles adicionales para el mensaje de error
            if (!string.IsNullOrEmpty(ex.Message))
            {
                errorMessage.Append($" Detalles: {ex.Message}");
            }

            // Agregar información de la excepción interna si existe
            if (ex.InnerException != null)
            {
                errorMessage.Append($" Inner Exception: {ex.InnerException.Message}");
            }

            // Construir el mensaje de error para logs
            logErrorMessage.Append(errorMessage.ToString());
            if (!string.IsNullOrEmpty(ex.StackTrace))
            {
                logErrorMessage.Append($" Stack Trace: {ex.StackTrace}");
            }

            // Incluir la excepción base (si existe) para mayor contexto en los logs
            if (ex.GetBaseException() != ex)
            {
                logErrorMessage.Append($"Base Exception: {ex.GetBaseException().Message}");
            }

            // Determinar el código de estado HTTP
            HttpStatusCode statusCode = ex switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound,
                InvalidOperationException or ValidationException => HttpStatusCode.UnprocessableEntity,
                DbUpdateException => HttpStatusCode.Conflict,
                ArgumentNullException or FormatException => HttpStatusCode.BadRequest,
                TimeoutException or OperationCanceledException => HttpStatusCode.RequestTimeout,
                NotImplementedException => HttpStatusCode.NotImplemented,
                _ => HttpStatusCode.InternalServerError
            };

            return (errorMessage, logErrorMessage, statusCode);

        }

    }


}
