using Microsoft.AspNetCore.Mvc;

namespace DailyHelpers.File
{
    /// <summary>
    /// Clase de utilidad para trabajar con archivos.
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Genera un objeto FileContentResult a partir de los contenidos de un archivo y su tipo de contenido.
        /// </summary>
        /// <param name="fileContents">Los contenidos del archivo como un array de bytes.</param>
        /// <param name="contentType">El tipo de contenido del archivo.</param>
        /// <returns>Un objeto FileContentResult que representa el archivo generado.</returns>
        public static FileContentResult GenerateFile(byte[] fileContents, string contentType)
        {
            return new FileContentResult(fileContents, contentType);
        }

        /// <summary>
        /// Genera un objeto FileContentResult para descargar un archivo con los contenidos, tipo de contenido y nombre de archivo especificados.
        /// </summary>
        /// <param name="fileContents">Los contenidos del archivo como un array de bytes.</param>
        /// <param name="contentType">El tipo de contenido del archivo.</param>
        /// <param name="fileName">El nombre del archivo para la descarga.</param>
        /// <returns>Un objeto FileContentResult configurado para la descarga del archivo.</returns>
        public static FileContentResult DownloadFile(byte[] fileContents, string contentType, string fileName)
        {
            // Configurar las cabeceras para la descarga del archivo
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = false  // Indicar que el archivo debe ser tratado como una descarga, no en línea
            };

            // Crear un objeto FileContentResult con los contenidos de archivo y el tipo de contenido especificados
            var response = new FileContentResult(fileContents, contentType);

            // Establecer el nombre de descarga del archivo
            response.FileDownloadName = fileName;

            // Devolver el objeto FileContentResult configurado para la descarga del archivo
            return response;
        }

    }
}
