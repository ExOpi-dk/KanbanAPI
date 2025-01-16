namespace Kanban.Models
{
    public class User
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
