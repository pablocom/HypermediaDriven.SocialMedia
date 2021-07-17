using System;

namespace HypermediaDriven.SocialMedia.Core
{
    public class EventMetadata
    {
        public DateTime Timestamp { get; }
        public string CommitId { get; }

        public EventMetadata(DateTime timestamp, string commitId)
        {
            Timestamp = timestamp;
            CommitId = commitId;
        }
    }
}
