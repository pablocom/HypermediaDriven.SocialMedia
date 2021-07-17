using System.Collections.Generic;

namespace AccountManagement.WebApi.Controllers
{
    public class Representation
    {
        public Link Self { get; init; }
        public IEnumerable<Link> Links { get; init; }

        public Representation(Link self, IEnumerable<Link> links)
        {
            Self = self;
            Links = links;
        }
    }
}