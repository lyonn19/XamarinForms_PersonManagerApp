using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PersonManager.Behaviors
{
    /// <summary>
    /// Clase helper para validaciones se encuentran las expresiones regulares
    /// </summary>
    public static class ValidatorsFactory
    {
        private const string EmailRegex =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";


        private const string AlphaRegex = @"^[A-Za-z]+$";
        private const string AlphaNumericRegex = @"^\w*$";
        private const string AlphaNamesRegex = @"^[A-Za-zñÑáéíóúÁÉÍÓÚ\s]+$";

        //Ecuador cell phone regex
        const string TelephoneRegex = @"^[0]{1}[8-9]{1}[0-9]{8}$";//@" ^[9|8][0-9]{7}$"  //@"^[0]{1}[9]{1}[0-9]{8}$"

        //Ecuador convencional phone regex
        const string PhoneRegex = @"^[0]{1}[2-9]{1}[0-9]{7,8}$";

        const string AddressRegex = @"^[A-Za-zñÑáéíóúÁÉÍÓÚ0-9#-.%\s]+$";

        const string SlashNameRegex = @"^[A-Za-zñÑ/\s]+$";

        const string DurationRegex = @"^[A-Za-zñÑáéíóúÁÉÍÓÚ0-9\s]+$";

        public static bool IsValidEmail(string input)
        {
            return (Regex.IsMatch(input, EmailRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
        public static bool IsValidAlpha(string input)
        {
            return (Regex.IsMatch(input, AlphaRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
        public static bool IsValidAlphaNumeric(string input)
        {
            return (Regex.IsMatch(input, AlphaNumericRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
        public static bool IsValidAlphaName(string input)
        {
            if (input == null) return true;
            return (Regex.IsMatch(input, AlphaNamesRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
        public static bool IsValidAlphaDuration(string input)
        {
            return (Regex.IsMatch(input, DurationRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
        public static bool IsValidEmpty(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
        public static bool IsValidMaxLength(string input, int maxlength)
        {
            return input.Length <= maxlength;
        }
        public static bool IsValidMinLength(string input, int minlength)
        {
            return input.Length >= minlength;
        }
        public static bool IsValidNumber(string input)
        {
            decimal num;
            return decimal.TryParse(input, out num);
        }
        public static bool IsValidTelephone(string input)
        {
            return (Regex.IsMatch(input, TelephoneRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
        public static bool IsValidTelephoneCon(string input)
        {
            return (Regex.IsMatch(input, PhoneRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
        public static bool IsValidMaxValue(string input, decimal value)
        {
            decimal number;
            if (decimal.TryParse(input, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out number))
            {
                return number <= value;
            }
            return false;
        }
        public static bool IsValidMinValue(string input, decimal value)
        {
            decimal number;
            if (decimal.TryParse(input, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out number))
            {
                return number >= value;
            }
            return false;
        }
        public static bool IsValidAddress(string input)
        {
            return (Regex.IsMatch(input, AddressRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
        
        public static bool IsValidSlashName(string input)
        {
            return (Regex.IsMatch(input, SlashNameRegex,
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }

        public static bool IsPasswordMatch(string input, string value)
        {
            return value != null && value.Equals(input);
        }

        public static bool IsValidRange(string value)
        {
            int.TryParse(value, out int newintvalue);
            return newintvalue > 0 && newintvalue < 100;
        }

    }
}
