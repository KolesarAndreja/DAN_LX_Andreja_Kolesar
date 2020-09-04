using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DAN_XLII_Andreja_Kolesar.Validation
{
    class ValidPhone:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string number = value as string;
            for (int i = 0; i < number.Length; i++)
            {
                if (!int.TryParse(number[i].ToString(), out int digit))
                {
                    return new ValidationResult(false, "phone number must contain digits only");
                }
            }
             return new ValidationResult(true, null);
        }
    }
}
