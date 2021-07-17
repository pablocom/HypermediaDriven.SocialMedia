using System.Collections.Generic;
using AccountManagement.WebApi.Controllers;
using AccountManagement.WebApi.Model;

namespace AccountManagement.WebApi.Representations
{
    public class FollowersRepresentation : Representation
    {
        public IEnumerable<Follower> Followers { get; }
        
        public FollowersRepresentation(Link self, IEnumerable<Link> links, IEnumerable<Follower> followers) 
            : base(self, links)
        {
            Followers = followers;
        }
    }
}