namespace Domain.Entities
{
    public class Comment
        : AuditableEntity
    {
        public int Id { get; set; }
        public Post Parent { get; set; }
        public string Message { get; set; }
    }
}