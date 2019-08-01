using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectLibrary.Utility
{
    public class StringHelper
    {
        public static string ConvertToUnSign(string text)
        {
            return
                new Regex(@"[^a-zA-Z_0-9 \s]").Replace(
                    new Regex(@"\s\s+").Replace(text.Normalize(NormalizationForm.FormD), " ")
                                       .Replace('\u0111', 'd')
                                       .Replace('\u0110', 'D'), "");
        }
        public static string ConvertToAlias(string text)
        {
            return ConvertToUnSign(text).Replace(" ", " ");
        }
    }
}
