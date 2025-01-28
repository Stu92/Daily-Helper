using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DailyHelpers
{
    public static class ApiHelpers
    {

        /// <summary>
        /// Convierte un objeto <see cref="CustomResult"/> en un resultado de acción HTTP (<see cref="IActionResult"/>).
        /// </summary>
        /// <param name="customResult">El objeto <see cref="CustomResult"/> que contiene el resultado de la operación.</param>
        /// <returns>Un <see cref="IActionResult"/> que representa el resultado de la operación, con el mensaje, el contenido y el código de estado HTTP correspondientes.</returns>
        /// <remarks>
        /// Este método permite mapear de manera sencilla un <see cref="CustomResult"/> a un <see cref="ObjectResult"/> que se puede utilizar 
        /// en los controladores de una API para devolver respuestas personalizadas con un código de estado HTTP.
        /// </remarks>
        public static IActionResult ToActionResult(this CustomResult customResult)
        {
            return new ObjectResult(new
            {
                success = customResult.Succeeded,
                message = customResult.Message,
                content = customResult.Content
            })
            {
                StatusCode = (int)customResult.HttpStatusCode
            };
        }
    }

    /// <summary>
    /// Representa un resultado personalizado para operaciones, que incluye información sobre el estado, mensaje, contenido y código de estado HTTP.
    /// </summary>
    public class CustomResult
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// Mensaje asociado al resultado de la operación.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Contenido dinámico asociado al resultado de la operación.
        /// </summary>
        public dynamic Content { get; private set; }

        /// <summary>
        /// Código de estado HTTP que describe el resultado de la operación.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; private set; }

        /// <summary>
        /// Constructor privado para inicializar una instancia de <see cref="CustomResult"/> con todos los parámetros.
        /// </summary>
        /// <param name="succeeded">Indica si la operación fue exitosa.</param>
        /// <param name="message">Mensaje asociado al resultado.</param>
        /// <param name="content">Contenido dinámico asociado al resultado.</param>
        /// <param name="httpStatusCode">Código de estado HTTP.</param>
        private CustomResult(bool succeeded, string message, dynamic content, HttpStatusCode httpStatusCode)
        {
            Succeeded = succeeded;
            Message = message;
            Content = content;
            HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Constructor privado para inicializar una instancia de <see cref="CustomResult"/> sin código de estado HTTP.
        /// </summary>
        /// <param name="succeeded">Indica si la operación fue exitosa.</param>
        /// <param name="message">Mensaje asociado al resultado.</param>
        /// <param name="content">Contenido dinámico asociado al resultado.</param>
        private CustomResult(bool succeeded, string message, dynamic content)
        {
            Succeeded = succeeded;
            Message = message;
            Content = content;
        }

        /// <summary>
        /// Crea una instancia de <see cref="CustomResult"/> que indica una operación exitosa.
        /// </summary>
        /// <param name="message">Mensaje asociado al éxito de la operación.</param>
        /// <param name="content">Contenido dinámico asociado al resultado.</param>
        /// <returns>Una instancia de <see cref="CustomResult"/> representando el éxito.</returns>
        public static CustomResult Success(string message, dynamic content)
        {
            return new CustomResult(true, message, content, HttpStatusCode.OK);
        }

        /// <summary>
        /// Crea una instancia de <see cref="CustomResult"/> que indica una operación fallida con un código de estado HTTP.
        /// </summary>
        /// <param name="message">Mensaje asociado al fallo de la operación.</param>
        /// <param name="content">Contenido dinámico asociado al resultado.</param>
        /// <param name="httpStatusCode">Código de estado HTTP que describe el error.</param>
        /// <returns>Una instancia de <see cref="CustomResult"/> representando el fallo.</returns>
        public static CustomResult Failure(string message, dynamic content, HttpStatusCode httpStatusCode)
        {
            return new CustomResult(false, message.Replace("\r", "").Replace("\n", ""), content, httpStatusCode);
        }

        /// <summary>
        /// Crea una instancia de <see cref="CustomResult"/> que indica una operación fallida sin un código de estado HTTP.
        /// </summary>
        /// <param name="message">Mensaje asociado al fallo de la operación.</param>
        /// <param name="content">Contenido dinámico asociado al resultado.</param>
        /// <returns>Una instancia de <see cref="CustomResult"/> representando el fallo.</returns>
        public static CustomResult Failure(string message, dynamic content)
        {
            return new CustomResult(false, message.Replace("\r", "").Replace("\n", ""), content);
        }
    }
}
