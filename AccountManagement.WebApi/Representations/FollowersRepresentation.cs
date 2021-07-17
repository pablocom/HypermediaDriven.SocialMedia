﻿using AccountManagement.WebApi.Controllers;
using AccountManagement.WebApi.Model;
using System.Collections.Generic;

namespace AccountManagement.WebApi.Representations
{
    public class FollowersRepresentation : Representation
    {
        public IEnumerable<Follower> Followers { get; }

        public FollowersRepresentation(Link self, Link[] links, IEnumerable<Follower> followers)
            : base(self, links)
        {
            Followers = followers;
        }
    }
}