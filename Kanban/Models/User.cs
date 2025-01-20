namespace Kanban.Models
{
    public class User
    {
        public int Id { get; }
        public string? Name { get; set; }
        public DateTime Created { get; }
        public DateTime? LastUpdated { get; }
    }
}
