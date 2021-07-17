using System;

namespace HypermediaDriven.SocialMedia.Core
{
    public interface ITimeService
    {
        public DateTime UtcNow { get; }
        public DateTime Now { get; }
    }
}
