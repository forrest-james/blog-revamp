using Domain.Entities;
using Domain.Enums;

namespace Domain.Objects
{
    public class Reaction
    {
        public Post Parent { get; set; }
        public string IpAddress { get; set; }
        public ReactionType Type { get; set; }
    }
}