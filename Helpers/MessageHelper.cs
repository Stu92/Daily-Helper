using Newtonsoft.Json;
using System.Net;

namespace DailyHelpers
{
    /// <summary>
    /// Clase auxiliar para manejar mensajes genéricos con un tipo específico, contenido y propiedades relacionadas.
    /// </summary>
    /// <typeparam name="T">El tipo de contenido asociado al mensaje.</typeparam>
    public class MessageHelper<T>
    {
        #region PROPIEDADES
        /// <summary>
        /// Tipo de mensaje (Error, Éxito, Advertencia).
        /// </summary>
        public MessageType Type { get; }

        /// <summary>
        /// Contenido genérico del mensaje.
        /// </summary>
        public T Content { get; }

        /// <summary>
        /// Marca de tiempo que indica cuándo se creó o actualizó el mensaje.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Nombre del autor del mensaje.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Lista de mensajes secundarios asociados a este mensaje.
        /// </summary>
        public List<MessageHelper<T>> Messages { get; } = new List<MessageHelper<T>>();
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Constructor de la clase MessageHelper que inicializa el tipo y el contenido del mensaje.
        /// </summary>
        /// <param name="type">El tipo de mensaje.</param>
        /// <param name="content">El contenido del mensaje.</param>
        public MessageHelper(MessageType type, T content)
        {
            Type = type;
            Content = content;
        }
        #endregion

        #region METODOS DE CONVENIENCIA
        /// <summary>
        /// Crea un mensaje de tipo Éxito con el contenido especificado.
        /// </summary>
        /// <param name="content">El contenido del mensaje de éxito.</param>
        /// <returns>Una instancia de MessageHelper de tipo Éxito.</returns>
        public static MessageHelper<T> CreateSuccess(T content)
        {
            return new MessageHelper<T>(MessageType.Success, content);
        }

        /// <summary>
        /// Crea un mensaje de tipo Error con el contenido especificado.
        /// </summary>
        /// <param name="content">El contenido del mensaje de error.</param>
        /// <returns>Una instancia de MessageHelper de tipo Error.</returns>
        public static MessageHelper<T> CreateError(T content)
        {
            return new MessageHelper<T>(MessageType.Error, content);
        }

        /// <summary>
        /// Crea un mensaje de tipo Advertencia con el contenido especificado.
        /// </summary>
        /// <param name="content">El contenido del mensaje de advertencia.</param>
        /// <returns>Una instancia de MessageHelper de tipo Advertencia.</returns>
        public static MessageHelper<T> CreateWarning(T content)
        {
            return new MessageHelper<T>(MessageType.Warning, content);
        }

        #endregion

        #region Serialización y deserialización, Formato de mensajes personalizado:
        /// <summary>
        /// Serializa la instancia actual de MessageHelper a una cadena JSON.
        /// </summary>
        /// <returns>Una representación en formato JSON de la instancia actual.</returns>
        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Deserializa una cadena JSON en una instancia de MessageHelper.
        /// </summary>
        /// <param name="json">La cadena JSON que representa un MessageHelper.</param>
        /// <returns>Una instancia de MessageHelper deserializada.</returns>
        public static MessageHelper<T> DeserializeFromJson(string json)
        {
            return JsonConvert.DeserializeObject<MessageHelper<T>>(json);
        }

        /// <summary>
        /// Aplica un formato personalizado al mensaje utilizando una plantilla.
        /// </summary>
        /// <param name="template">La plantilla que define el formato del mensaje.</param>
        /// <returns>El mensaje formateado como cadena.</returns>
        public string FormatMessage(string template)
        {
            return string.Format(template, Content);
        }

        #endregion

        #region Validación de tipo de mensaje:
        /// <summary>
        /// Determina si el mensaje es de tipo Error.
        /// </summary>
        /// <returns>True si el mensaje es de tipo Error; de lo contrario, False.</returns>
        public bool IsError()
        {
            return Type == MessageType.Error;
        }

        /// <summary>
        /// Determina si el mensaje es de tipo Éxito.
        /// </summary>
        /// <returns>True si el mensaje es de tipo Éxito; de lo contrario, False.</returns>
        public bool IsSuccess()
        {
            return Type == MessageType.Success;
        }

        /// <summary>
        /// Determina si el mensaje es de tipo Advertencia.
        /// </summary>
        /// <returns>True si el mensaje es de tipo Advertencia; de lo contrario, False.</returns>
        public bool IsWarning()
        {
            return Type == MessageType.Warning;
        }

        #endregion

        #region Gestión de múltiples mensajes:

        /// <summary>
        /// Agrega un mensaje secundario a la lista de mensajes asociados.
        /// </summary>
        /// <param name="message">El mensaje secundario a agregar.</param>
        public void Add(MessageHelper<T> message)
        {
            Messages.Add(message);
        }
        #endregion
    }

    /// <summary>
    /// Enumeración que define los tipos posibles de mensaje.
    /// </summary>
    public enum MessageType
    {
        Error,
        Success,
        Warning
    }

}
