using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models
{
    public class YoutubeLanguage
    {
        /// <summary>
        /// The language code ISO 639-1 code
        /// </summary>
        public string LangCode { get; set; }
        /// <summary>
        /// The name of that language
        /// </summary>
        public string LangName { get; set; }
    }
}