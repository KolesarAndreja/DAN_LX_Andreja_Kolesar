using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DAN_XLII_Andreja_Kolesar.Validation
{
    class ValidJmbg : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string jmbg = value as string;
            //check length
            if (jmbg.Length != 13)
            {
                return new ValidationResult(false, "Jmbg must contain 13 digits");
            }
            else
            {
                //check digits
                for (int i = 0; i < 13; i++)
                {
                    if (!int.TryParse(jmbg[i].ToString(), out int digit))
                    {
                        return new ValidationResult(false, "Jmbg must contain 13 digits");
                    }
                }

                //check avaibility of jmbg
                if (Service.Service.IsEmployeeJmbg(jmbg))
                    return new ValidationResult(false, "This jmbg is already in the database.");


                //chack part which refers to birth date
                DateTime now = DateTime.Now;
                char[] stringCurrentYear = now.Year.ToString().Substring(1).ToCharArray();
                string day = jmbg.Substring(0, 2);
                string month = jmbg.Substring(2, 2);
                string partOfyear = jmbg.Substring(4, 3);
                if (jmbg.Substring(4, 1) == "9")
                {
                    partOfyear = "1" + partOfyear;
                }
                else
                {
                    partOfyear = "2" + partOfyear;

                }
                string date_of_birth = partOfyear + "-" + month + "-" + day;
                try
                {
                    DateTime date = DateTime.Parse(date_of_birth);
                    if (date > now)
                    {
                        return new ValidationResult(false, "Date of birth cannot be in a future.");
                    }
                    else
                    {
                        return new ValidationResult(true, null);
                    }
                }
                catch (Exception)
                {
                    return new ValidationResult(false, "Part of jmbg which refers to date of birth is invalid.");
                }
            }
        }
    }
}

