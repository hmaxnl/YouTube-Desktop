using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YouTube_Desktop.Core.Models.Video;
using YouTube_Desktop.Core.Models.Media;
using System.Dynamic;

namespace YouTube_Desktop.Core.Models
{
    /// <summary>
    /// A wrapper for all the sub properties.
    /// </summary>
    public class VideoInfoSnippet
    {
        public VideoInfoSnippet(PlayabilityStatus playabilityStat, StreamingData streamingData, VideoDetails videoDetails, Microformat microformat)
        {
            _videoStatus = playabilityStat;
            _videoStreamingData = streamingData;
            _videoDetails = videoDetails;
            _videoMicroformat = microformat;
        }
        public VideoInfoSnippet()
        {
            // Nothing
        }
        //Get only.
        public bool IsPlayable { get => (_videoStatus.Status == VideoStatus.OK) ? true : false; }
        public string Title { get => _videoDetails.VideoTitle; }
        public string Description { get { return (string.IsNullOrEmpty(_videoMicroformat.Desciption)) ? _videoMicroformat.Desciption : _videoDetails.ShortDescription; } }
        public long ViewCount { get => _videoDetails.ViewCount; }
        public bool IsLive { get => _videoDetails.IsLiveContent; }
        public string ChannelId { get => _videoDetails.ChannelId; }
        public string Author { get => _videoDetails.Author; }
        public string AuthorUrl { get => _videoMicroformat.OwnerProfileUrl; }
        // The video properties.
        private PlayabilityStatus _videoStatus { get; set; }
        private StreamingData _videoStreamingData { get; set; }
        private VideoDetails _videoDetails { get; set; }
        private Microformat _videoMicroformat { get; set; }
    }
}