using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DAN_XLII_Andreja_Kolesar.Validation
{
    class ValidIC : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string number = value as string;
            //check length
            if (number.Length != 9)
            {
                return new ValidationResult(false, "IC number must contain 9 digits");
            }
            else
            {
                //check digits
                for (int i = 0; i < 9; i++)
                {
                    if (!int.TryParse(number[i].ToString(), out int digit))
                    {
                        return new ValidationResult(false, "IC number contain 13 digits");
                    }
                }

                return new ValidationResult(true, null);
            }
        }
    }
}
