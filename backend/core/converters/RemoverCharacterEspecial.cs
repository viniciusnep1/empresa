using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace usecases.converters
{
    public class RemoverCharacterEspecial
    {
        public static string Execute(string str)
        {
            var pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
            var replacement = "";
            var rgx = new Regex(pattern);
            var sbReturn = new StringBuilder();
            replacement = rgx.Replace(str, replacement);
            var arrayText = replacement.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                {
                    sbReturn.Append(letter);
                }
            }

            return sbReturn.ToString();

        }
    }
}
