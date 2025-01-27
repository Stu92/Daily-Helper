using Microsoft.AspNetCore.Mvc;

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
}
