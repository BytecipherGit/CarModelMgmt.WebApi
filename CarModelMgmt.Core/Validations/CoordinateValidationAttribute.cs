using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Core.Validations
{
    public class CoordinateValidationAttribute: ValidationAttribute
    {
        private readonly double _minValue;
        private readonly double _maxValue;

        public CoordinateValidationAttribute(double minValue, double maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            if (value is decimal coordinate)
            {
                if (coordinate < (decimal)_minValue || coordinate > (decimal)_maxValue)
                {
                    return new ValidationResult($"The {validationContext.DisplayName} must be between {_minValue} and {_maxValue}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
