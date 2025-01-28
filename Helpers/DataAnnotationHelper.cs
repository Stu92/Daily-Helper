using System.ComponentModel.DataAnnotations;

namespace DailyHelpers
{
    /// <summary>
    /// Atributo de validación personalizado para comprobar la precisión y escala de valores decimales.
    /// </summary>
    public class DecimalPrecisionAttributeHelper : ValidationAttribute
    {
        private readonly int _precision;
        private readonly int _scale;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DecimalPrecisionAttributeHelper"/>.
        /// </summary>
        /// <param name="precision">La precisión máxima permitida (número total de dígitos).</param>
        /// <param name="scale">La escala máxima permitida (número de dígitos decimales).</param>
        public DecimalPrecisionAttributeHelper(int precision, int scale)
        {
            _precision = precision;
            _scale = scale;
        }

        /// <summary>
        /// Determina si el valor proporcionado cumple con las restricciones de precisión y escala.
        /// </summary>
        /// <param name="value">El valor a validar.</param>
        /// <param name="validationContext">El contexto de validación que contiene información adicional.</param>
        /// <returns>
        /// Un resultado de validación exitoso si el valor es válido; de lo contrario, un <see cref="ValidationResult"/> con un mensaje de error.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if (value is decimal decimalValue)
            {
                var scaleDigits = BitConverter.GetBytes(decimal.GetBits(decimalValue)[3])[2];
                var integralPart = Math.Truncate(decimalValue);
                var integralDigits = integralPart.ToString().Length;

                if (scaleDigits <= _scale && integralDigits <= (_precision - _scale))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult($"El valor debe ser un decimal con precisión {_precision} y escala {_scale}.");
        }

    }
}
