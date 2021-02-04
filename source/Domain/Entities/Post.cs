using Domain.Enums;
using Domain.Objects;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Post
        : AuditableEntity
    {
        public Post()
        {
            Reactions = new HashSet<Reaction>();
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public PostType Type { get; set; }
        public string EncodedTitle { get; set; }
        public string Synopsis { get; set; }

        public ICollection<Reaction> Reactions { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}