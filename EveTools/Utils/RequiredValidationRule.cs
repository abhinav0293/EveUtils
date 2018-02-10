using System;
using System.Globalization;
using System.Windows.Controls;

namespace EveTools.Utils
{
    class RequiredValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            if (value == null || object.ReferenceEquals(value, DBNull.Value))
            {
                result = new ValidationResult(false, "Required!");
            }
            return result;
        }
    }
}