using System;

namespace HypermediaDriven.SocialMedia.Core
{
    public class TimeService : ITimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime Now => DateTime.Now;
    }
}
