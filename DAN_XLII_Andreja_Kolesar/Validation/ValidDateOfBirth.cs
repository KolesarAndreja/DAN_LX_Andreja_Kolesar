using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DAN_XLII_Andreja_Kolesar.Validation 
{
    class ValidDateOfBirth:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string dateOfBirth = value as string;
            if (!DateTime.TryParse(dateOfBirth, out DateTime result))
            {
                return new ValidationResult(false, "Invalid format");
            }
            else
            {
                if (result > DateTime.Now)
                {
                    return new ValidationResult(false, "Date of birth cannot be in a future");
                }
                return new ValidationResult(true, null);
            }
        }
    }
}
