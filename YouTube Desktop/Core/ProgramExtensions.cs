using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core
{
    internal static class ProgramExtensions
    {
        public static bool IsNullOrWhite(this string strVal)
        {
            return string.IsNullOrWhiteSpace(strVal);
        }
    }
}