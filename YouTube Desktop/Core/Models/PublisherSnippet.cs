using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core.Models
{
    public class PublisherSnippet
    {
        /// <summary>
        /// Publisher (channel) id
        /// </summary>
        public string PublisherId { get; set; }
        /// <summary>
        /// Publisher (channel) url
        /// </summary>
        public string PublisherUrl { get => $"https://www.youtube.com/channel/{PublisherId}"; }
        /// <summary>
        /// Publisher (channel) name
        /// </summary>
        public string PublisherName { get; set; }
        /// <summary>
        /// Subscribers count
        /// </summary>
        public long PublisherSubscribers { get; set; }
        /// <summary>
        /// Publisher is paid
        /// </summary>
        public bool IsPaid { get; set; }
        /// <summary>
        /// Url to the publisher logo
        /// </summary>
        public string LogoUrl { get; set; }
        /// <summary>
        /// Url to the publisher banner
        /// </summary>
        public string BannerUrl { get; set; }
        /// <summary>
        /// Publisher description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The date when the publisher created the account/channel
        /// </summary>
        public DateTime PublisherCreationDate { get; set; }
    }
}