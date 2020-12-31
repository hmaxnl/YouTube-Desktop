using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTube_Desktop.Core.Models.Media;

namespace YouTube_Desktop.Core.Models.Video
{
    public class AdaptiveFormat
    {
        public List<VideoMediaFormat> VideoMedia { get; set; }
        public List<AudioMediaFormat> AudioMedia { get; set; }
    }
}