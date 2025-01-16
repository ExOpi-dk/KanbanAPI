namespace Kanban.Models
{
    public class Status
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
