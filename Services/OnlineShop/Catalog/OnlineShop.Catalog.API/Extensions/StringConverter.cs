using System.Globalization;
using System.Text;

namespace OnlineShop.Catalog.API.Extensions
{
    public static class StringConverter
    {
        public static string RemovePolishAccents(this string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < normalized.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(normalized[i]) != UnicodeCategory.NonSpacingMark)
                {
                    if (normalized[i] != 322)
                        result.Append(normalized[i]);
                    else
                        result.Append('l');
                }
            }

            return result.ToString();
        }
    }
}