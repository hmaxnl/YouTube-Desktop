using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models
{
    public class ThumbnailSnippet
    {
        public Thumbnail Default { get; set; }
        public Thumbnail Standard { get; set; }
        public Thumbnail Medium { get; set; }
        public Thumbnail High { get; set; }
        public Thumbnail HighRes { get; set; }
    }
}