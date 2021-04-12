using System;
using System.Collections.Generic;
using System.Text;
using YouTubeScrap.Data.Interfaces;

namespace YouTubeScrap.Data.Feedback
{
    public class FeedbackAction : IClickTrackingParams
    {
        public string ClickTrackingParams { get; set; }
        public UndoFeedbackAction UndoFeedbackAction { get; set; }
    }
}