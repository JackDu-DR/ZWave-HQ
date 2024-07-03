using System.Text.RegularExpressions;

namespace ZWave.Helpers
{
    public static class InputHelper
    {
        public static bool IsValidNumericInput(string input)
        {
            return Regex.IsMatch(input, @"^[0-9\.-]*$");
        }
        public static bool IsValidNumberInput(string input)
        {
            return Regex.IsMatch(input, @"^\d+\.?\d*$");
        }
        public static bool IsValidNegativeNumberInput(string input)
        {
            return Regex.IsMatch(input, @"^-\d+\.?\d*$");
        }
    }
}
