using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectricalEngineerTools.Framework.PL.Services
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool canConvert = double.TryParse(value as string, out _);
            return new ValidationResult(canConvert, "Not a valid double");
        }
    }
}
